using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.Response
{
    public class BancoDtoOut
    {
        public int Id { get; set; }
        public string? RazonSocial { get; set; }

        public string? NombreEstadoBanco { get; set; }

        public int Numero { get; set; }
    }
}
