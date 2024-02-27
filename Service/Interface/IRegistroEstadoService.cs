using Data.Models;
using Dtos.Response;

namespace Service.Interface
{
    public interface IRegistroEstadoService
    {
        Task<bool> AgregarRegistroEstado(Transaccion transaccion);
        Task<IEnumerable<RegistroEstadoDtoOut>> listarRegistrosTransaccion(int numero);
    }
}