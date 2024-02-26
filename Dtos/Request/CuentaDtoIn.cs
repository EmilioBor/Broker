using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Broker.Dtos
{
    public class CuentaDtoIn
    {
        public int id { get; set; }
        public long numero { get; set; }
        public string? cbu { get; set; }
        public string? Banco { get; set; }
    }
}
