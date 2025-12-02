using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioTarget1.Models
{
    public class Venda
    {
        public required string Vendedor { get; set; }
        public required Decimal Valor { get; set; }
    }
}
