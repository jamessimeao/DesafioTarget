namespace DesafioTarget2.Dtos
{
    public class EstoqueProdutoDto
    {
        public required int CodigoProduto { get; set; }
        public required string DescricaoProduto { get; set; }
        public required int Estoque { get; set; } 
    }
}