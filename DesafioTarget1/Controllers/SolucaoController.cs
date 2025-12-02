using DesafioTarget1.Dtos;
using DesafioTarget1.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTarget1.Controllers
{
    [ApiController]
    [Route("desafio1/[controller]")]

    public class SolucaoController : ControllerBase
    {
        // Endpoint de solução do desafio 1
        [HttpPost]
        public ActionResult Solucao(JsonDto json)
        {
            IEnumerable<Venda> vendas = json.Vendas;
            Dictionary<string,Decimal> commissaoTotalPorVendedor = CalculaComissao.TotalPorVendedor(vendas);
            return Ok(commissaoTotalPorVendedor);
        }
    }
}
