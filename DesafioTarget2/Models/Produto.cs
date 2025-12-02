using System.ComponentModel.DataAnnotations;

namespace DesafioTarget2.Models
{
    public class Produto
    {
        [Key]
        public required int CodigoProduto { get; set; }
        public required string DescricaoProduto { get; set; }

    }
}
