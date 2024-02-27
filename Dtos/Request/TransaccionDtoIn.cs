using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Broker.Dtos
{
    public class TransaccionDtoIn
    {
        //public DateTime fechaHora { get; set; }
        //public string numero { get; set; }
        //public string? tipo { get; set; }
        //public string? validacionEstado { get; set; }
        //public string? aceptadoEstado { get; set; }
        //public float monto { get; set; }
        //public string? cbuOrigen { get; set; }
        //public string? cbuDestino { get; set; }

        ////
        public int Id { get; set; }

        public float Monto { get; set; }

        public DateTime FechaHora { get; set; }

        public int IdTipo { get; set; }

        public int IdValidacionEstado { get; set; }

        public int IdAceptadoEstado { get; set; }

        public int IdCuentaOrigen { get; set; }

        public int IdCuentaDestino { get; set; }

        public string? Numero { get; set; }

        ///

        public int cbuOrigen { get; set; }

        public int cbuDestino { get; set; }



    }
}
