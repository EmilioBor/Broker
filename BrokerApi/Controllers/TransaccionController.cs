using Microsoft.AspNetCore.Mvc;
using Broker.Dtos;
using Data.Models;
using Service.Interface;

namespace Broker.Controllers
{
    [ApiController]
    [Route("WebApi/[controller]")]
    public class TransaccionController : ControllerBase
    {
        private readonly ITransaccionService _transaccionService;
        public TransaccionController(ITransaccionService transaccionService)
        {
            _transaccionService = transaccionService;
        }

        [HttpGet("listar")] // Listar transacciones
        public async Task<IEnumerable<Transaccion>> listarTransacciones()
        {
            var transacciones = await _transaccionService.listarTransacciones();

            return transacciones;
        }
        [HttpGet("listarTransaccionesPorBancoYFecha")] // Listar transacciones banco y fecha
        public async Task<IEnumerable<Transaccion>> listarTransaccionesPorBancoYFecha(int numero, DateTime fecha)
        {
            var transacciones = await _transaccionService.listarTransaccionesPorBancoYFecha(numero,fecha);

            return transacciones;
        }

        [HttpGet("listarPorFecha")] // Listar transacciones por fecha
        public async Task<IEnumerable<Transaccion>> listarTransaccionesPorFecha()
        {
            var transacciones = await _transaccionService.listarTransaccionesPorFecha();

            return transacciones;
        }

        [HttpPost] // agrega transaccion
        public async Task<IActionResult> agregarTransaccion([FromBody] TransaccionDtoAgregarIn transaccion)
        {
            if (transaccion == null)
            {
                return BadRequest("Los datos de la transaccion no son válidos.");
            }
            string numero = await _transaccionService.agregarTransaccion(transaccion);

            // Devuelvo una respuesta de éxito con el código de estado 201 (Created)
            return StatusCode(201, transaccion.Numero);
           
        }
    }
}

