using Microsoft.AspNetCore.Mvc;


using System.Diagnostics;
using Broker.Dtos;
using Service.Interface;
using Data.Models;
using Dtos.Response;

namespace Broker.Controllers
{
    [ApiController]
    [Route("WebApi/[controller]")]
    public class BancoController : ControllerBase
    {
        private readonly IBancoService _bancoService;
        public BancoController(IBancoService bancoService)
        {
            _bancoService = bancoService;
        }

        [HttpGet("listar")] // Listar Bancos
        public async Task<IEnumerable<BancoDtoOut>>listarBancos()
        {
            var resultados = await _bancoService.listarBancos();

            return resultados;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BancoDtoOut>> GetById(int id)
        {
            var banco = await _bancoService.GetDtoById(id);

            if (banco is null)
                return BadRequest();

            return banco;
        }

        //AGREGAR
        [HttpPost]
        public async Task<IActionResult> Create(BancoDtoIn banco)
        {
            var newBanco = await _bancoService.agregarBanco(banco);


            return CreatedAtAction(nameof(GetById), new { id = newBanco.Id }, newBanco);
        }

        //[HttpPost] // agrega banco
        //public async Task<IActionResult>agregarBanco([FromBody] BancoDtoIn banco)
        //{
        //    if (banco == null)
        //    {
        //        return BadRequest("Los datos del banco no son válidos.");
        //    }

        //    if (await _bancoService.agregarBanco(banco) is not null)
        //    {

        //        // Devuelvo una respuesta de éxito con el código de estado 201 (Created)
        //        return CreatedAtAction("listarBancos", banco);
        //    }
        //    else
        //    {
        //        // Retorno respuesta de fallo del servidor con el codigo 500
        //        return StatusCode(500, "Banco no creado, error interno del servidor.");
        //    }
        //    //puede que ande
        //}

    }
}
