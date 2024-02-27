

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Broker.Dtos;
using Data.Models;
using Service.Interface;
using Data;
using Dtos.Response;

namespace Service.Metodos
{
    public class BancoService : IBancoService
    {
        private readonly BrokerDBContext _context;
       

        public BancoService(IConfiguration configuration, BrokerDBContext context)
        {
            _context = context;

           

        }

        public async Task<IEnumerable<BancoDtoOut>> listarBancos()
        {
            // Realiza una consulta a la base de datos para devolver todos los Bancos
            var bancos = await _context.Banco
                .Select(c => new BancoDtoOut{
                    Id = c.Id,
                    RazonSocial = c.RazonSocial,
                    NombreEstadoBanco = c.IdEstadoBancoNavigation.Descripcion,
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

        public async Task<BancoDtoOut?> GetDtoById(int id)
        {
            return await _context.Banco
                .Where(n => n.Id == id)
                .Select(n => new BancoDtoOut
                {
                    Id = n.Id,
                    RazonSocial = n.RazonSocial,
                    NombreEstadoBanco = n.IdEstadoBancoNavigation.Nombre,
                    Numero = n.Numero

                }).SingleOrDefaultAsync();

        }





        public async Task<Banco> agregarBanco(BancoDtoIn bancodto)
        {
            //try
            //{
                //if (bancodto == null)
                //{
                //    return false;
                //}

                var banco = new Banco();
                
                banco.Id = bancodto.Id;
                banco.RazonSocial = bancodto.RazonSocial;
                banco.IdEstadoBanco = bancodto.IdEstadoBanco = 1; // seteo el estado ( 1 para inactivo, 2 para activo)
                banco.Numero = bancodto.Numero;

                // Agregar el Banco al contexto de la base de datos
                _context.Banco.Add(banco);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
            return banco;
            //    return true;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}


        }
    }
}
