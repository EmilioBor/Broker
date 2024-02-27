using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.Request
{
    public class RegistroEstadoDtoOut
    {
        public int Id { get; set; }

        public DateTime FechaHora { get; set; }

        public int IdValidadoEstado { get; set; }

        public int IdAceptadoEstado { get; set; }

        public int IdTransaccion { get; set; }
    }
}
