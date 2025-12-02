using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioTarget2.Models
{
    public class MovimentacaoDeEstoque
    {
        [Key]
        public int Id { get; set; }
        public required string Descricao { get; set; }

        [ForeignKey("Produto")]
        public required int CodigoProduto { get; set; }
        public required int Quantidade { get; set; }
    }
}
