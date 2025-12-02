namespace DesafioTarget2.Dtos
{
    public class EstoqueInicialDto
    {
        // Chamo de Estoque para a desserizalização do Json ocorrer corretamente
        public required IEnumerable<EstoqueProdutoDto> Estoque { get; set;}
    }
}
