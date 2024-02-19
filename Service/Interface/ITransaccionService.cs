using Broker.Dtos;
using Data.Models;

namespace Service.Interface
{
    public interface ITransaccionService
    {
        Task<int> RetornarIdTipo(string tipo);
        Task<IEnumerable<Transaccion>> listarTransacciones();
        Task<IEnumerable<Transaccion>> listarTransaccionesPorFecha();
        Task<bool> validarTransaccion(TransaccionDtoAgregar transaccionDto, int dniOrigen, int dniDestino, string cbuOrigen, string cbuDestino);
        Task<bool> agregarTransaccion(TransaccionDtoAgregar transaccionDto, int dniOrigen, int dniDestino, string cbuOrigen, string cbuDestino);
    }
}