namespace DesafioTarget3
{
    public static class Solucao
    {
        private const Decimal taxaDeJuros = 0.025m; // 2,5%
        private const Decimal fator = 1 + taxaDeJuros;

        public static Decimal Juros(DateOnly hoje, DateOnly vencimento, Decimal valor)
        {
            int diasAposVencimento = hoje.DayNumber-vencimento.DayNumber;
            if(diasAposVencimento <= 0)
            {
                // Até o vencimento não há juros
                return 0;
            }
            else
            {
                return (Pow(fator, (uint)diasAposVencimento) - 1) * valor;
            }
        }

        private static Decimal Pow(Decimal x, uint n)
        {
            Decimal result = 1;
            for(uint i=0; i<n; i++)
            {
                result *= x;
            }
            return result;
        }
    }
}
