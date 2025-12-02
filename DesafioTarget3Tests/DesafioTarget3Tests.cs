using DesafioTarget3;

namespace DesafioTargetTestes
{
    public class DesafioTarget3Tests
    {
        public static TheoryData<int,Decimal,Decimal> Data()
        {
            return new TheoryData<int, Decimal, Decimal>()
            {
                // diasAposVencimento, valor, jurosCorreto
                {-2, 1, 0 },
                {-1, 10, 0 },
                { 0, 100, 0 },
                { 1, 100, 2.5m },
                { 2, 100, 5.0625m}
            };
        }

        // Usa MemberData no lugar de InlineData para poder usar Decimal
        [MemberData(nameof(Data))]
        [Theory]
        public void Solucao_Juros_ShouldReturnDecimal(int diasAposVencimento, Decimal valor, Decimal jurosCorreto)
        {
            const int diaVencimento = 15;
            DateOnly vencimento = new DateOnly(2025, 11, diaVencimento);
            int diaHoje = diaVencimento + diasAposVencimento;
            DateOnly hoje = new DateOnly(2025, 11, diaHoje);
            Decimal juros = Solucao.Juros(hoje, vencimento, valor);

            Assert.Equal(juros, jurosCorreto);
        }
    }
}
