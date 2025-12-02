namespace DesafioTarget3
{
    internal class Program
    {
        public static void Main()
        {
            Decimal valor = 100;
            DateOnly vencimento = new DateOnly(2025, 11, 1);
            DateOnly hoje = new DateOnly(2025, 11, 2);
            Decimal juros = Solucao.Juros(hoje, vencimento, valor);
            Console.WriteLine($"valor = {valor:C}");
            Console.WriteLine($"vencimento = {vencimento}");
            Console.WriteLine($"hoje = {hoje}");
            Console.WriteLine($"juros = {juros:C}");
        }
    }
}