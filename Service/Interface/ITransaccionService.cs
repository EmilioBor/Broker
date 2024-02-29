using Broker.Dtos;
using Data.Models;
using Dtos.Response;

namespace Service.Interface
{
    public interface ITransaccionService
    {
        Task<int> RetornarIdTipo(string tipo);
        Task<IEnumerable<TransaccionDtoOut>> listarTransacciones();
        Task<TransaccionDtoOut?> GetDtoById(string numero);
        Task<IEnumerable<Transaccion>> listarTransaccionesPorBancoYFecha(int numeroBanco, DateTime fecha);
        Task<IEnumerable<TransaccionDtoOut>> listarTransaccionesPorFecha();
        Task<bool> validarTransaccion(Transaccion transaccion, int cuitOrigen, int cuitDestino, string cbuOrigen, string cbuDestino);
        Task<ConfirmacionEstadoDtoOut> agregarTransaccion(TransaccionDtoAgregarIn transaccionDto);
        Task confirmacion(ConfirmacionEstadoDtoOut confirmacionEstado);
    }
}