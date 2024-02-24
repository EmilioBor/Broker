

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Broker.Dtos;
using Data.Models;
using Service.Interface;

namespace Service.Metodos
{
    public class BancoService : IBancoService
    {
        private readonly ApiDb _context;
       

        public BancoService(IConfiguration configuration, ApiDb context)
        {
            _context = context;

           

        }

        public async Task<IEnumerable<Banco>> listarBancos()
        {
            // Realiza una consulta a la base de datos para devolver todos los Bancos
            var bancos = await _context.Banco
                .Select(c => new Banco{
                    Id = c.Id,
                    RazonSocial = c.RazonSocial,
                    IdEstadoBanco = c.IdEstadoBanco,
                    Numero = c.Numero
            }).ToListAsync();
            
            // Devuelve la lista de Bancos
            return bancos;
        }
        public async Task<Banco?> buscarBanco(int numero)
        {
            // Realiza una consulta a la base de datos para buscar Banco por numero
            var banco = await _context.Banco
            .Where(b => b.Numero == numero)
            .Include(b => b.IdEstadoBanco)
            .FirstOrDefaultAsync();

            

            return banco; // Devuelvo el banco
        }
        public async Task<int> getIdBanco(int numero)
        {
            // Realiza una consulta a la base de datos para buscar Banco por numero
            var banco = await _context.Banco
            .Where(b => b.Numero == numero)
            .FirstOrDefaultAsync();

            return banco.Id; // Devuelvo el banco

        }
        public async Task<bool> agregarBanco(BancoDtoAgregar bancodto)
        {
            try
            {
                if (bancodto == null)
                {
                    return false;
                }

                var banco = new Banco();
                banco.RazonSocial = bancodto.razonSocial;
                banco.IdEstadoBanco = 1; // seteo el estado ( 1 para inactivo, 2 para activo)
                banco.Numero = bancodto.numero;

                // Agregar el Banco al contexto de la base de datos
                _context.Banco.Add(banco);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
    }
}
