using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Broker.Dtos
{
    public class TransaccionDtoAgregar
    {
        

        public float Monto { get; set; }

        public DateOnly FechaHora { get; set; }

        public string? Tipo { get; set; }

        public int IdValidacionEstado { get; set; }

        public int IdAceptadoEstado { get; set; }

        public int IdCuentaOrigen { get; set; }

        public int IdCuentaDestino { get; set; }
    }
}
