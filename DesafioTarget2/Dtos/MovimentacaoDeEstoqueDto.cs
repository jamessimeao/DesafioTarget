using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioTarget2.Dtos
{
    public class MovimentacaoDeEstoqueDto
    {
        public required string Descricao { get; set; }
        public required int CodigoProduto { get; set; }
        public required int Quantidade { get; set; }
    }
}
