using Broker.Dtos;
using Data.Models;
using Dtos.Response;

namespace Service.Interface
{
    public interface IBancoService
    {
        Task<BancoDtoOut?> GetDtoById(int id);
        Task<Banco> agregarBanco(BancoDtoIn bancodto);
        //Task<bool> agregarBanco(BancoDtoIn bancodto);
        Task<int> getIdBanco(int numero);
        Task<Banco?> buscarBanco(int numero);
        Task<IEnumerable<BancoDtoOut>> listarBancos();
    }
}