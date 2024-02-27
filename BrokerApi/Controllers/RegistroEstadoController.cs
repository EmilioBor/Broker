using Microsoft.AspNetCore.Mvc;


using System.Diagnostics;
using Broker.Dtos;
using Service.Interface;
using Data.Models;
using Dtos.Response;

namespace BrokerApi.Controllers
{
    [ApiController]
    [Route("WebApi/[controller]")]
    public class RegistroEstadoController : ControllerBase
    {
        private readonly IRegistroEstadoService _registroEstadoService;
        public RegistroEstadoController(IRegistroEstadoService registroEstadoService)
        {
            _registroEstadoService = registroEstadoService;
        }

        [HttpGet("listarRegistrosPorTransaccion/{numero}")] // Listar Registros por transaccion
        public async Task<IEnumerable<RegistroEstadoDtoOut?>> listarRegistrosTransaccion(string numero)
        {
            var resultados = await _registroEstadoService.listarRegistrosTransaccion(numero);

            return resultados;
        }
    }
}
