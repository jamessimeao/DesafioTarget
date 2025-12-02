using DesafioTarget2.Data;
using DesafioTarget2.Dtos;
using DesafioTarget2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioTarget2.Controllers
{
    [ApiController]
    [Route("desafio2/[controller]/[action]")]
    public class SolucaoController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public SolucaoController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Para inicializar o banco de dados com o json de estoque fornecido no desafio 2.
        // Este endpoint poderia ser substituído por um endpoint de registrar produto,
        // e então inicializar o banco de dados primeiro registrando os produtos e depois fazendo
        // movimentações de estoque. Por simplicidade, vou fazer toda inicialização com só este endpoint.
        [HttpPost]
        public async Task<ActionResult> RegistraEstoque(EstoqueInicialDto jsonDto)
        {
            IEnumerable<EstoqueProdutoDto> estoque = jsonDto.Estoque;
            try
            {
                foreach (EstoqueProdutoDto estoqueProdutoDto in estoque)
                {
                    Produto produto = new Produto()
                    {
                        CodigoProduto = estoqueProdutoDto.CodigoProduto,
                        DescricaoProduto = estoqueProdutoDto.DescricaoProduto,
                    };
                    _dbContext.Produtos.Add(produto);

                    EstoqueProduto estoqueProduto = new EstoqueProduto()
                    {
                        CodigoProduto = estoqueProdutoDto.CodigoProduto,
                        Quantidade = estoqueProdutoDto.Estoque,
                    };
                    _dbContext.Estoque.Add(estoqueProduto);
                }

                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return BadRequest();
            }
        }

        // Endpoint principal do desafio 2. Ele é responsável por fazer movimentações de estoque.
        [HttpPost]
        public async Task<ActionResult<int>> MovimentaEstoque(MovimentacaoDeEstoque movimentacaoDeEstoque)
        {
            // Encontra no banco de dados o estoque do produto com o código especificado em movimentacaoDeEstoque
            EstoqueProduto? estoqueProduto = await _dbContext.Estoque
                .FirstOrDefaultAsync(
                    estoqueProduto => estoqueProduto.CodigoProduto == movimentacaoDeEstoque.CodigoProduto
                    );
            if(estoqueProduto == null)
            {
                Console.WriteLine("Não achou produto no estoque com o código especificado.");
                return BadRequest();
            }

            // Atualiza o estoque, somando à quantidade de estoque a quantidade especificada em movimentacaoDeEstoque
            estoqueProduto.Quantidade += movimentacaoDeEstoque.Quantidade;
            _dbContext.Estoque.Update(estoqueProduto);

            // Registra a movimentação
            _dbContext.MovimentacoesDeEstoque.Add(movimentacaoDeEstoque);

            // Executa as atualizações do banco de dados anteriores
            await _dbContext.SaveChangesAsync();

            // Obtém a quantidade de estoque registrada no banco de dados
            EstoqueProduto? estoqueProdutoAtualizado = await _dbContext.Estoque
                .FirstOrDefaultAsync(
                    estoqueProduto => estoqueProduto.CodigoProduto == movimentacaoDeEstoque.CodigoProduto
                    );
            if (estoqueProdutoAtualizado == null)
            {
                Console.WriteLine("Não achou produto no estoque com o código especificado.");
                return BadRequest();
            }
            
            // Retorna essa quantidade
            return Ok(estoqueProdutoAtualizado.Quantidade);
        }
    }
}
