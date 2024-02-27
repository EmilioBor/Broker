using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Broker.Dtos
{
    public class TransaccionDtoOut
    {
        //public DateOnly FechaHora { get; set; }

        //public string? cbu_origen { get; set; }
        //public int cuil_origen { get; set; }

        //public string? cbu_destino { get; set; }
        //public int cuil_destino { get; set; }

        //public float Monto { get; set; }
        //public string? Tipo { get; set; }
        //public int Numero { get; set; }

        //public string? NombreValidacionEstado { get; set; }
        //public string? NombreAceptadoEstado { get; set; }

        //public int NombreCuentaOrigen { get; set; }
        //public int NombreCuentaDestino { get; set; }
        public int Id { get; set; }

        public float Monto { get; set; }

        public DateTime FechaHora { get; set; }

        public string? NombreTipo { get; set; }

        public string? NombreValidacionEstado { get; set; }

        public string? NombreAceptadoEstado { get; set; }

        public string? NombreCuentaOrigen { get; set; }

        public string? NombreCuentaDestino { get; set; }

        public string? Numero { get; set; }
    }
}
