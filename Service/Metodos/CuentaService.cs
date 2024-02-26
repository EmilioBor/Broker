

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Broker.Dtos;
using Data.Models;
using Service.Interface;
using Data;

namespace Service.Metodos
{
    public class CuentaService : ICuentaService
    {
        private readonly ApiDb _context;

        private readonly IBancoService _bancoService;


        public CuentaService(ApiDb context, IBancoService bancoService)
        {
            _context = context;
            _bancoService = bancoService; //?? throw new ArgumentNullException(nameof(bancoService)); 
        }

        public async Task<IEnumerable<Cuenta>> listarCuentas()
        {
            // Realiza una consulta a la base de datos para devolver todos las cuentas
            //var cuentas = await _context.Cuenta.ToListAsync();
            return await _context.Cuenta.Select(c => new Cuenta
            {
                Id = c.Id,
                Cbu = c.Cbu,
                Numero = c.Numero,
                IdBanco = c.IdBanco,
            }).ToListAsync();
            // Devuelve la lista de cuentas

        }

        public async Task<int> agregarCuenta(long numero, string cbu)
        {
            var bancoNumero = int.Parse(cbu.Substring(0, 9));  // extraigo del cbu el numero identificador del banco
            var idBanco = await _bancoService.getIdBanco(bancoNumero);  // busco el banco en mi bd y guardo el id

            var cuenta = new Cuenta();
            cuenta.Numero = numero;
            cuenta.Cbu = cbu;
            cuenta.IdBanco = idBanco;  // engancho la FK con el id del banco que consegui

            _context.Cuenta.Add(cuenta);

            await _context.SaveChangesAsync();

            return cuenta.Id;
        }

        public async Task<Cuenta?> buscarCuenta(long numero)
        {
            // Realiza la búsqueda de cuenta por numero
            var cuenta = await _context.Cuenta
                .Where(c => c.Numero == numero)
                .SingleOrDefaultAsync();

            //var cuentaDto = _mapper.Map<CuentaDto>(cuenta);

            return cuenta;
        }
    }
}

