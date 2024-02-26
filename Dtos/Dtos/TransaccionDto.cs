using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Broker.Dtos
{
    public class TransaccionDto
    {
        //public DateTime fechaHora { get; set; }
        //public long numero { get; set; }
        //public string? tipo { get; set; }
        //public string? validacionEstado { get; set; }
        //public string? aceptadoEstado { get; set; }
        //public float monto { get; set; }
        //public string? cbuOrigen { get; set; }
        //public string? cbuDestino { get; set; }

        ////
        ///
        

        public float Monto { get; set; }

        public DateOnly FechaHora { get; set; }

        public int IdTipo { get; set; }

        public string? numero { get; set; }
        public int validacionEstado { get; set; }

        public int aceptadoEstado { get; set; }

        public int cbuOrigen { get; set; }

        public int cbuDestino { get; set; }

    }
}
