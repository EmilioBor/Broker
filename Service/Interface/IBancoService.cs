using Broker.Dtos;
using Data.Models;
using Dtos.Response;

namespace Service.Interface
{
    public interface IBancoService
    {
        Task<bool> agregarBanco(BancoDtoAgregarIn bancodto);
        Task<int> getIdBanco(int numero);
        Task<Banco?> buscarBanco(int numero);
        Task<IEnumerable<BancoDtoOut>> listarBancos();
    }
}