using Broker.Dtos;
using Data.Models;

namespace Service.Interface
{
    public interface ITransaccionService
    {
        Task<int> RetornarIdTipo(string tipo);
        Task<IEnumerable<Transaccion>> listarTransacciones();
        Task<IEnumerable<Transaccion>> listarTransaccionesPorBancoYFecha(int numeroBanco, DateTime fecha);
        Task<IEnumerable<Transaccion>> listarTransaccionesPorFecha();
        Task<bool> validarTransaccion(Transaccion transaccion, int cuitOrigen, int cuitDestino, string cbuOrigen, string cbuDestino);
        Task<string> agregarTransaccion(TransaccionDtoAgregarIn transaccionDto);
    }
}