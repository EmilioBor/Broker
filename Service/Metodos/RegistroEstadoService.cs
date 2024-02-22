using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Service.Interface;

namespace Service.Metodos
{
    public class RegistroEstadoService : IRegistroEstadoService
    {
        private readonly BrokerDBContext _context;
        public RegistroEstadoService(BrokerDBContext context)
        {
            _context = context;
        }
        public async Task FuncionDeTransaccion(Transaccion transaccion)
        {
            if (transaccion == null)
            {
                return;
            }
        }
    }
}
