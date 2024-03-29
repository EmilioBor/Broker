﻿//using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Broker.Dtos;
using System;
using Service.Interface;
using Data.Models;
using Data;
using Dtos.Response;
using System.Net.Http;


namespace Service.Metodos
{
    public class TransaccionService : ITransaccionService
    {
        private readonly BrokerDBContext _context;
        //private readonly IMapper _mapper;
        private readonly ICuentaService _cuentaService;
        private readonly IBancoService _bancoService;
        private readonly IRegistroEstadoService _registroEstadoService;


        public TransaccionService(BrokerDBContext context, ICuentaService cuentaService, IBancoService bancoService, IRegistroEstadoService registroEstadoService)
        {
            _context = context;
            _cuentaService = cuentaService;
            _bancoService = bancoService;
            _registroEstadoService = registroEstadoService;
        }
        public async Task<int> RetornarIdTipo(string tipo)
        {
            var numero = await _context.Tipo
                .Where(t => t.Descripcion == tipo)
                .FirstOrDefaultAsync();
            return numero.Id;
        }

        public async Task<IEnumerable<TransaccionDtoOut>> listarTransacciones()
        {
            // Realiza una consulta a la base de datos para devolver todas las transacciones
            var transacciones = await _context.Transaccion.Select(b => new TransaccionDtoOut
            {
                Id = b.Id,
                Monto = b.Monto,
                FechaHora = b.FechaHora,
                NombreTipo = b.IdTipoNavigation.Descripcion,
                NombreValidacionEstado = b.IdValidacionEstadoNavigation.Estado,
                NombreAceptadoEstado = b.IdAceptadoEstadoNavigation.Estado,
                NombreCuentaOrigen = b.IdCuentaOrigenNavigation.Cbu,
                NombreCuentaDestino = b.IdCuentaDestinoNavigation.Cbu,
                Numero = b.Numero
            }).ToListAsync();

            // Devuelve la lista de transacciones
            return transacciones;
        }

        public async Task<TransaccionDtoOut?> GetDtoById(string numero)
        {
            // Realiza una consulta a la base de datos para devolver todas las transacciones
            var transaccion = await _context.Transaccion
                .Where(b => b.Numero == numero)
                .Select(b => new TransaccionDtoOut
            {
                Id = b.Id,
                Monto = b.Monto,
                FechaHora = b.FechaHora,
                NombreTipo = b.IdTipoNavigation.Descripcion,
                NombreValidacionEstado = b.IdValidacionEstadoNavigation.Estado,
                NombreAceptadoEstado = b.IdAceptadoEstadoNavigation.Estado,
                NombreCuentaOrigen = b.IdCuentaOrigenNavigation.Cbu,
                NombreCuentaDestino = b.IdCuentaDestinoNavigation.Cbu,
                Numero = b.Numero
            }).SingleOrDefaultAsync();

            // Devuelve la lista de transacciones
            return transaccion;
        }

        public async Task<IEnumerable<Transaccion>> listarTransaccionesPorBancoYFecha(int numeroBanco, DateTime fecha)
        {   // busco el id del banco a listar las transacciones     
            int idBanco = await _bancoService.getIdBanco(numeroBanco);

            var transacciones = await _context.Transaccion

            // chequeo que el banco asociado a la cuenta destino u origen hayan participado de la transaccion para listarla
            // chequeo que coincida la fecha para listarla
            .Where(t => (t.IdCuentaOrigenNavigation.IdBanco == idBanco || t.IdCuentaDestinoNavigation.IdBanco == idBanco) && t.FechaHora.Date == fecha.Date)
            .OrderBy(t => t.FechaHora)
            .ToListAsync();

            // Devuelve la lista de transacciones filtradas
            return transacciones;
        }


        public async Task<IEnumerable<TransaccionDtoOut>> listarTransaccionesPorFecha()
        {
            // Realiza una consulta a la base de datos para devolver todas las transacciones
            var transacciones = await _context.Transaccion
                .Select(b => new TransaccionDtoOut
                {
                    Id = b.Id,
                    Monto = b.Monto,
                    FechaHora = b.FechaHora,
                    NombreTipo = b.IdTipoNavigation.Descripcion,
                    NombreValidacionEstado = b.IdValidacionEstadoNavigation.Estado,
                    NombreAceptadoEstado = b.IdAceptadoEstadoNavigation.Estado,
                    NombreCuentaOrigen = b.IdCuentaOrigenNavigation.Cbu,
                    NombreCuentaDestino = b.IdCuentaDestinoNavigation.Cbu,
                    Numero = b.Numero
            }).ToListAsync();

            // ordena las transacciones por fecha
            var transaccionesOrdenadas = transacciones.OrderBy(t => t.FechaHora);
            
            // Devuelve la lista de transacciones por fecha
            return transaccionesOrdenadas;
        }

        public async Task<bool> validarTransaccion(Transaccion transaccion, int cuitOrigen, int cuitDestino, string cbuOrigen, string cbuDestino)
        {                                     
            //estrategia: 
            // -recibir cbu Origen y Destino, Obtener bancos de los cbu y verificar existencia en nuestra bd
            // -recibir cuit Origen y Destino, enviarselos al renaper para verificar.
            // -retorno TRUE si cumple AMBAS verificaciones, de lo CONTRARIO retorno FALSE
            try
            {
                string numeroBancoOrigen = cbuOrigen.Substring(0, 9);
                string numeroBancoDestino = cbuOrigen.Substring(0, 9);

                // busco los bancos en la bd
                var bancoOrigen = await _bancoService.buscarBanco(int.Parse(numeroBancoOrigen));
                var bancoDestino = await _bancoService.buscarBanco(int.Parse(numeroBancoDestino));

                // si algun banco no existe, rechazo la transaccion y seteo el estado correspondiente explicando en la descripcion que banco fue rechazado
                if (bancoOrigen == null )
                {
                    transaccion.IdValidacionEstado = 7; // poner el numero de estado correspondiente y en la descripcion aclarar que el banco origen no pasó la validación
                    
                    await _registroEstadoService.AgregarRegistroEstado(transaccion);
                   
                    //----------------------------------------------------------------------------------------------------------------
                    return false;
                }
                if ( bancoDestino == null)
                {
                    transaccion.IdValidacionEstado = 8; // poner el numero de estado correspondiente y en la descripcion aclarar que el banco destino no pasó la validación
                    await _registroEstadoService.AgregarRegistroEstado(transaccion);

                    return false;
                }

                using HttpClient client = new HttpClient();
                HttpResponseMessage responseOrigen = await client.GetAsync("https://colosal.duckdns.org:15001/Renaper/api/Persona/getParaBroker/{transaccionDto.cuil_origen}");
                HttpResponseMessage responseDestino = await client.GetAsync("https://colosal.duckdns.org:15001/Renaper/api/Persona/getParaBroker/{transaccionDto.cuil_destino}");

                string content = await responseOrigen.Content.ReadAsStringAsync();
                bool esValidocuitOrigen;
                bool.TryParse(content, out esValidocuitOrigen);

                string contente = await responseDestino.Content.ReadAsStringAsync();
                bool esValidocuitDestino;
                bool.TryParse(contente, out esValidocuitDestino);

                // consulto al endpoint del Renaper la validez de los cuit (falta insertar endpoint)

                if (esValidocuitOrigen == false )
                {
                    transaccion.IdValidacionEstado = 6; // poner el numero de estado correspondiente y en la descripcion aclarar que el cuit origen no pasó la validación
                    await _registroEstadoService.AgregarRegistroEstado(transaccion);

                    return false;
                }
                if(esValidocuitDestino == false)
                {
                    transaccion.IdValidacionEstado = 5; // poner el numero de estado correspondiente y en la descripcion aclarar que el cuit destino no pasó la validación
                    await _registroEstadoService.AgregarRegistroEstado(transaccion);

                    return false;
                }

                return true;  // retorno true si las validaciones son exitosas

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ConfirmacionEstadoDtoOut> agregarTransaccion(TransaccionDtoAgregarIn transaccionDto)
        {
            // Estrategia:
            // valido la información de la transacción
            // Si recibo true persisto las cuentas, acepto transaccion, y envio numero de transaccion al banco.
            // En caso contrario no persisto las cuentas y rechazo transferencia.
            // si estan validadas me fijo si ya las tengo creadas las cuentas, y si no las creo
            try
            {
                if (transaccionDto == null)
                {
                    return null;
                }

                int cuitOrigen= transaccionDto.origin_cuil;
                int cuitDestino=transaccionDto.destination_cuil;
                string? cbuOrigen = transaccionDto.origin_cbu;
                string? cbuDestino=transaccionDto.destination_cbu;

                var transaccion = new Transaccion();// creo Transaccion
                transaccion.FechaHora = DateTime.Now; // le asigno la fecha en la que ingreso a nuestro sistema
                transaccion.Numero = Guid.NewGuid().ToString(); // le creo un numero unico
                // validacionEstados: (1: validando bancos, 2: validando cuit, 3: validacion exitosa, 4: validacion rechazada)
                transaccion.IdValidacionEstado = 1; // seteo estado validando bancos
                await _registroEstadoService.AgregarRegistroEstado(transaccion);

                bool validacion = await validarTransaccion(transaccion, cuitOrigen, cuitDestino, cbuOrigen, cbuDestino);
                                                       
                if (validacion == true)
                {
                    transaccion.IdValidacionEstado = 3; // seteo transaccion como aceptada
                    await _registroEstadoService.AgregarRegistroEstado(transaccion);

                    var cuentaOrigen = cbuOrigen.Substring(10, 21);
                    var cuentaDestino = cbuDestino.Substring(10, 21);

                    // busco las cuentas en la BD
                    //45465465464654654654654654654654654646
                    var cuentaOrigenBd = await _cuentaService.buscarCuenta(int.Parse(cuentaOrigen));
                    var cuentaDestinoBd = await _cuentaService.buscarCuenta(int.Parse(cuentaDestino));

                    if (cuentaOrigen == null) // si no las tengo registradas las registro
                    {
                        await _cuentaService.agregarCuenta(int.Parse(cuentaOrigen), cbuOrigen);
                        // guardo id de la cuenta origen, en la transaccion
                        transaccion.IdCuentaOrigen = cuentaOrigenBd.Id;
                    }
                    if (cuentaDestino == null)
                    {
                        await _cuentaService.agregarCuenta(int.Parse(cuentaDestino), cbuDestino);
                        // guardo id de la cuenta destino, en la transaccion
                        transaccion.IdCuentaDestino = cuentaDestinoBd.Id;
                    }

                    transaccion.Monto = transaccionDto.amount;
                    var tipo = transaccionDto.motive;
                    var id = await RetornarIdTipo(tipo);
                    transaccion.IdTipo = id;

                    transaccion.IdValidacionEstado = 14;
                    await _registroEstadoService.AgregarRegistroEstado(transaccion);


                    // Agrego la transaccion al  contexto de la base de datos
                    _context.Transaccion.Add(transaccion);

                    // Guardo los cambios en la base de datos
                    await _context.SaveChangesAsync();

                    // envio numero de transacción a banco (lo debo retornar en la funcion o lo envio directamente a un endpoint del banco ?)
                    var confirmacion = new ConfirmacionEstadoDtoOut();

                    confirmacion.estado = transaccion.IdValidacionEstadoNavigation.Estado;
                    confirmacion.numero = transaccion.Numero;

                    return confirmacion;



                }
                else
                {
                    // si no cumple la validación guardo sus datos y la seteo estado rechazada 
                    transaccion.Monto = transaccionDto.amount;

                    // Realiza una consulta a la base de datos para buscar Tipo por nombre
                    var tipo = transaccionDto.motive;
                    var id = await RetornarIdTipo(tipo);
                    transaccion.IdTipo = id;

                    transaccion.IdValidacionEstado = 13;
                    // Agrego la transaccion al  contexto de la base de datos
                    _context.Transaccion.Add(transaccion);
                    
                    //Guardamos en registro
                    await _registroEstadoService.AgregarRegistroEstado(transaccion);

                    //Seteamos la confirmacion y devolvemos al banco
                    var confirmacion = new ConfirmacionEstadoDtoOut();
                    confirmacion.estado = transaccion.IdValidacionEstadoNavigation.Estado;
                    confirmacion.numero = transaccion.Numero;


                    // Guardo los cambios en la base de datos
                    await _context.SaveChangesAsync();
                    return confirmacion;
                    //------------------------------------------------------------------------------------------------------------------------------
                }
                

            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task confirmacion(ConfirmacionEstadoDtoOut confirmacionEstado)
        {


            var transaccion = new TransaccionDtoOut(); 
                
            var buscar = GetDtoById(confirmacionEstado.numero);

            if(buscar is not null)
            {
                transaccion.NombreAceptadoEstado = confirmacionEstado.estado;


                await _context.SaveChangesAsync();
                
            }

            

        }
    }
}

