//using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Broker.Dtos;

using Service.Interface;
using Data.Models;


namespace Service.Metodos
{
    public class TransaccionService : ITransaccionService
    {
        private readonly BrokerDBContext _context;
        //private readonly IMapper _mapper;
        private readonly ICuentaService _cuentaService;
        private readonly IBancoService _bancoService;



        public TransaccionService(IConfiguration configuration, BrokerDBContext context, ICuentaService cuentaService, IBancoService bancoService)
        {
            _context = context;

            

            _cuentaService = cuentaService;

            _bancoService = bancoService;

        }
        public async Task<int> RetornarIdTipo(string tipo)
        {
            var numero = await _context.Tipo
                .Where(t => t.Descripcion == tipo)
                .FirstOrDefaultAsync();

            return numero.Id;



        }

        public async Task<IEnumerable<Transaccion>> listarTransacciones()
        {
            // Realiza una consulta a la base de datos para devolver todas las transacciones
            var transacciones = await _context.Transaccion.ToListAsync();

            // Devuelve la lista de transacciones
            return transacciones;
        }

        public async Task<IEnumerable<Transaccion>> listarTransaccionesPorFecha()
        {
            // Realiza una consulta a la base de datos para devolver todas las transacciones
            var transacciones = await _context.Transaccion.ToListAsync();

            // ordena las transacciones por fecha
            var transaccionesOrdenadas = transacciones.OrderBy(t => t.FechaHora);

            // Devuelve la lista de transacciones por fecha
            return transaccionesOrdenadas;
        }

        public async Task<bool> validarTransaccion(TransaccionDtoAgregar transaccionDto, int dniOrigen, int dniDestino, string cbuOrigen, string cbuDestino)
        {
            //estrategia: 
            // -recibir cbu Origen y destino, Obtener bancos de los cbu y verificar existencia en nuestra bd
            // -recibir dni origen y destino, enviarselos al renaper para verificar.
            // -retorno true si cumple ambas verificaciones, de lo contrario retorno false
            try
            {
                string numeroBancoOrigen = cbuOrigen.Substring(0, 9);
                string numeroBancoDestino = cbuOrigen.Substring(0, 9);

                // busco los bancos en la bd
                var bancoOrigen = await _bancoService.buscarBanco(int.Parse(numeroBancoOrigen));
                var bancoDestino = await _bancoService.buscarBanco(int.Parse(numeroBancoDestino));

                if (bancoOrigen == null || bancoDestino == null) // si no existen en mi bd rechazo la transaccion
                {
                    return false;
                }

                // consulto al endpoint del Renaper la validez de los Dni (falta insertar endpoint)
                bool esValidoDniOrigen = true;// await _httpClient.GetAsync(apiUrl/);
                bool esValidoDniDestino = true;//await _httpClient.GetAsync(apiUrl/);

                if (esValidoDniOrigen == false || esValidoDniDestino == false)
                {
                    return false;
                }

                return true;  // retorno true si las validaciones son exitosas

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> agregarTransaccion(TransaccionDtoAgregar transaccionDto, int dniOrigen, int dniDestino, string cbuOrigen, string cbuDestino)
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
                    return false;
                }

                var transaccion = new Transaccion();// creo Transaccion

                // validacionEstados: (1: validando bancos, 2: validando DNI, 3: validacion exitosa, 4: validacion rechazada)
                transaccion.IdValidacionEstado = 1; // seteo estado validando bancos

                // problema: tenemos que cambiar el estado de la transacción dentro de la funcion validarTransaccion y no dentro de esta funcion
                // hay que pasar de alguna manera el objeto transacción a la funcion validar para poder cambiarle el estado a la transaccion y se pueda ver el proceso de que 
                // cosas se estan validando

                bool validacion = await validarTransaccion(transaccionDto, dniOrigen, dniDestino, cbuOrigen, cbuDestino);

                if (validacion == true)
                {
                    transaccion.IdValidacionEstado = 3; // seteo transaccion como aceptada

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

                    transaccion.Monto = transaccionDto.Monto;
                    transaccion.FechaHora = transaccionDto.FechaHora;
                    var tipo = transaccionDto.Tipo;
                    var id = await RetornarIdTipo(tipo);
                    transaccion.IdTipo = id;
                    // falta agregar el tipo buscando el id  del tipo en la bd con el nombre del tipo que nos pasan en el dto

                    // Agrego la transaccion al  contexto de la base de datos
                    _context.Transaccion.Add(transaccion);

                    // Guardo los cambios en la base de datos
                    await _context.SaveChangesAsync();

                    // envio numero de transacción a banco (lo debo retornar en la funcion o lo envio directamente a un endpoint del banco ?)

                    return true;



                }
                else
                {
                    // si no cumple la validación seteo estado rechazada 
                    transaccion.IdValidacionEstado = 3;

                    // Falta guardar la transaccion rechazada, acá solo le cambie el estado, pero hay que guardarle la info y agregarla a la bd
                    transaccion.Monto = transaccionDto.Monto;
                    transaccion.FechaHora = transaccionDto.FechaHora;

                    //------------------------------------------------------------------------------------------------------------------------------
                    // Realiza una consulta a la base de datos para buscar Tipo por nombre
                    var tipo = transaccionDto.Tipo;
                    var id = await RetornarIdTipo(tipo);
                    //
                    //
                    transaccion.IdTipo = id;
                    // Agrego la transaccion al  contexto de la base de datos
                    _context.Transaccion.Add(transaccion);

                    // Guardo los cambios en la base de datos
                    await _context.SaveChangesAsync();
                    return false;
                    //------------------------------------------------------------------------------------------------------------------------------
                }

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

