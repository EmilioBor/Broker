using Microsoft.AspNetCore.Mvc;

using Service.Interface;
using Data.Models;
using AutoMapper.Configuration.Conventions;

namespace Broker.Controllers
{
    [ApiController]
    [Route("WebApi/[controller]")]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;
        public CuentaController(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        [HttpGet("listar")] // Listar cuentas
        public async Task<IEnumerable<Cuenta>> listarCuentas()
        {
            var resultados = await _cuentaService.listarCuentas();

            return resultados;
        }
        [HttpGet("{numero}")]
        public async Task<ActionResult<Cuenta>> GetNumero(int numero)
        {
            var cuenta = await _cuentaService.buscarCuenta(numero);
            if (cuenta == null)
            {
                return NotFound();
            }
            return Ok(cuenta);

        }
    }
}
