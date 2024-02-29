using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Broker.Dtos
{
    public class TransaccionDtoAgregarIn
    {

       // public DateOnly FechaHora { get; set; }

        public string? origin_cbu { get; set; }
        public float amount { get; set; }
        public string? destination_cbu { get; set; }
        public string? motive { get; set; }
        public int origin_cuil { get; set; }
        public int destination_cuil { get; set; }
        //public int Numero { get; set; }

       // public int IdValidacionEstado { get; set; }

       // public int IdAceptadoEstado { get; set; }

       // public int IdCuentaOrigen { get; set; }

       // public int IdCuentaDestino { get; set; }
    }
}
