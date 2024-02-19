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

        [HttpGet("listarPorFecha")] // Listar transacciones por fecha
        public async Task<IEnumerable<Transaccion>> listarTransaccionesPorFecha()
        {
            var transacciones = await _transaccionService.listarTransaccionesPorFecha();

            return transacciones;
        }

        [HttpPost] // agrega transaccion
        public async Task<IActionResult> agregarTransaccion([FromBody] TransaccionDtoAgregar transaccion, int dniOrigen, int dniDestino, string cbuOrigen, string cbuDestino)
        {
            if (transaccion == null)
            {
                return BadRequest("Los datos de la transaccion no son válidos.");
            }

            if (await _transaccionService.agregarTransaccion(transaccion,dniOrigen,dniDestino, cbuOrigen, cbuDestino))
            {

                // Devuelvo una respuesta de éxito con el código de estado 201 (Created)
                return CreatedAtAction("listarTransacciones", transaccion);
            }
            else
            {
                // Retorno respuesta de fallo del servidor con el codigo 500
                return StatusCode(500, "Transaccion no creada, error interno del servidor.");
            }
        }
    }
}

