using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.Response
{
    public class RegistroEstadoDtoOut
    {
        public int Id { get; set; }

        public DateTime FechaHora { get; set; }
        public string? NombreTransaccion { get; set; }
        public string? NombreValidadoEstado { get; set; }
        public string? NombreAceptadoEstado { get; set; }
    }
}
