using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Broker.Dtos
{
    public class BancoDtoIn
    {
        public int Id { get; set; }

        public string? RazonSocial { get; set; }

        public int IdEstadoBanco { get; set; }

        public int Numero { get; set; }

    }
}
