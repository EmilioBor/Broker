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

        public async Task<bool> validarTransaccion(Transaccion transaccion, int cuitOrigen, int cuitDestino, string cbuOrigen, string cbuDestino)
        {                                       // transaccion va por referencia para poder cambiar su estado(Los metodos asincronicos no pueden tener parametros ref , in ni out)
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
                    //transaccion.IdValidacionEstado = 7; // poner el numero de estado correspondiente y en la descripcion aclarar que el banco origen no pasó la validación
                    

                    var registroEstado = new Registroestado(); // creo registro y le seteo la info de transaccion
                    registroEstado.FechaHora = DateTime.Now;  // !!! Al modelo registro Estado hay que cambiarle el tipo a DateTime
                    registroEstado.IdTransaccion = transaccion.Id;
                    registroEstado.IdValidadoEstado = transaccion.IdValidacionEstado = 7;
                    registroEstado.IdAceptadoEstado = transaccion.IdAceptadoEstado;
                     
                    // Agrego el registro  al  contexto de la base de datos
                    _context.Registroestado.Add(registroEstado);

                    // Guardo los cambios en la base de datos
                    await _context.SaveChangesAsync();

                    // Para que no quede tanto chorizo de codigo al registrar los estados, hay que crear un RegistroEstado Service
                    // y crear ahi dentro una funcion que reciba la transaccion como parametro, y que asigne los valores de la transaccion al registro ( o sea hacer un cut paste del seteo que va de la linea 87 a la 91)
                    // y que la funcion devuelva el registroEstado ya seteado, para desde acá solamente guardarlo a la bd
                    // asi nos ahorramos dejamos el codigo transaccionService más limpio cada vez que se cambien los estados
                    // Una vez hecha la función falta invocarla cada vez que se cambie un estado y guardarla.

                    //var validacion = await _registroEstadoService.FuncionDeTransaccion(transaccion);

                    //----------------------------------------------------------------------------------------------------------------
                    return false;
                }
                if ( bancoDestino == null)
                {
                    transaccion.IdValidacionEstado = 8; // poner el numero de estado correspondiente y en la descripcion aclarar que el banco destino no pasó la validación
                    return false;
                }

                // consulto al endpoint del Renaper la validez de los cuit (falta insertar endpoint)
                bool esValidocuitOrigen = true;// await _httpClient.GetAsync(apiUrl/);
                bool esValidocuitDestino = true;//await _httpClient.GetAsync(apiUrl/);

                if (esValidocuitOrigen == false )
                {
                    transaccion.IdValidacionEstado = 6; // poner el numero de estado correspondiente y en la descripcion aclarar que el cuit origen no pasó la validación
                    return false;
                }
                if(esValidocuitDestino == false)
                {
                    transaccion.IdValidacionEstado = 5; // poner el numero de estado correspondiente y en la descripcion aclarar que el cuit destino no pasó la validación
                    return false;
                }

                return true;  // retorno true si las validaciones son exitosas

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> agregarTransaccion(TransaccionDtoAgregar transaccionDto, int cuitOrigen, int cuitDestino, string cbuOrigen, string cbuDestino)
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

                // validacionEstados: (1: validando bancos, 2: validando cuit, 3: validacion exitosa, 4: validacion rechazada)
                transaccion.IdValidacionEstado = 1; // seteo estado validando bancos

                bool validacion = await validarTransaccion(transaccion, cuitOrigen, cuitDestino, cbuOrigen, cbuDestino);
                                                        // transaccion va por referencia para poder cambiar su estado
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

