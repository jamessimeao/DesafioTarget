using DesafioTarget2.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioTarget2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<EstoqueProduto> Estoque { get; set; }
        public DbSet<MovimentacaoDeEstoque> MovimentacoesDeEstoque { get; set; }
    }
}
