using DesafioTarget1.Models;

namespace DesafioTarget1
{
    internal static class CalculaComissao
    {
        public static Decimal PorVenda(Venda venda)
        {
            Decimal valor = venda.Valor;
            if (valor >= 500)
            {
                return valor / 20;
            }
            else if (valor >= 100)
            {
                return valor / 100;
            }
            else
            {
                return 0;
            }
        }

        public static Dictionary<string, Decimal> TotalPorVendedor(IEnumerable<Venda> vendas)
        {
            // Cria um dicionário para guardar o nome do vendedor e sua commissão total
            Dictionary<string,Decimal> comissaoTotalPorVendedor = new Dictionary<string,Decimal>();
            foreach(Venda venda in vendas)
            {
                string vendedor = venda.Vendedor;
                if (comissaoTotalPorVendedor.ContainsKey(vendedor) == false)
                {
                    comissaoTotalPorVendedor[vendedor] = 0;
                }
                Decimal commisaoParcial = PorVenda(venda);
                comissaoTotalPorVendedor[vendedor] += commisaoParcial;
            }
            return comissaoTotalPorVendedor;
        }
    }
}
