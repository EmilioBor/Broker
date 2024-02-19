using Broker.Dtos;
using Data.Models;

namespace Service.Interface
{
    public interface IBancoService
    {
        Task<bool> agregarBanco(BancoDtoAgregar bancodto);
        Task<int> getIdBanco(int numero);
        Task<Banco?> buscarBanco(int numero);
        Task<IEnumerable<Banco>> listarBancos();
    }
}