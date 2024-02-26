using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Broker.Dtos
{
    public class TransaccionDtoOut
    {
        public DateOnly FechaHora { get; set; }

        public string? cbu_origen { get; set; }
        public int cuil_origen { get; set; }

        public string? cbu_destino { get; set; }
        public int cuil_destino { get; set; }

        public float Monto { get; set; }
        public string? Tipo { get; set; }
        public int Numero { get; set; }

        public string? ValidacionEstado { get; set; }
        public string? AceptadoEstado { get; set; }

        public int CuentaOrigen { get; set; }
        public int CuentaDestino { get; set; }

    }
}
