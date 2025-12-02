using DesafioTarget1.Models;

namespace DesafioTarget1.Dtos
{
    public class JsonDto
    {
        public required IEnumerable<Venda> Vendas { get; set; }
    }
}
