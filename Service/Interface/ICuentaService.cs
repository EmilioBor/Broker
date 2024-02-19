using Broker.Dtos;
using Data.Models;

namespace Service.Interface
{
    public interface ICuentaService
    {
        Task<Cuenta?> buscarCuenta(long numero);
        Task<int> agregarCuenta(long numero, string cbu);
        Task<IEnumerable<Cuenta>> listarCuentas();
    }
}