using Data.Models;

namespace Service.Interface
{
    public interface IRegistroEstadoService
    {
        Task<bool> AgregarRegistroEstado(Transaccion transaccion);
    }
}