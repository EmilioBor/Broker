using Data.Models;

namespace Service.Interface
{
    public interface IRegistroEstadoService
    {
        Task FuncionDeTransaccion(Transaccion transaccion);
    }
}