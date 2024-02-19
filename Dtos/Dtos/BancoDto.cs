using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Broker.Dtos
{
    public class BancoDto
    {
        public string? razonSocial { get; set; }
        public string? estado { get; set; }
        public long cuenta { get; set; }
        public int numero { get; set; }

    }
}
