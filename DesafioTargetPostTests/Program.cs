using System.Globalization;
using System.Net.Http;
using System.Text;

// Primeiro execute DesafioTarget1 e DesafioTarget2, depois execute este projeto.
// Este projeto faz POST requests para as APIs dos 2 primeiros desafios.

namespace DesafioTargetTestes
{
    internal class Program
    {
        public static async Task Main()
        {
            HttpClient httpClient = new HttpClient();

            // Testa o desafio 1
            // Calcula a comissão total de cada vendedor, usando o arquivo vendas.json
            await PostTests.Test(1, httpClient, 5096, "", @"Data\vendas.json", "Comissão total por vendedor:");

            // Testa o desafio 2
            // Primeiro envia o json com o estoque, para inicializar o banco de dados
            // Este POST só deve ser executado uma vez.
            await PostTests.Test(2, httpClient, 5278, "RegistraEstoque", @"Data\estoque.json", "Banco de dados inicializado.");
            // Em seguida, faz uma movimentação de estoque, usando o arquivo movimentacaoEstoque.json.
            // A movimentação tem uma descrição, o código do produto a ser movimentado e a quantidade a ser movimentada.
            // Ela recebe um identificador único ao ser registrada no banco de dados, sendo esse identificador uma 
            // primary key.
            await PostTests.Test(2, httpClient, 5278, "MovimentaEstoque", @"Data\movimentacaoEstoque.json", "Quantidade em estoque:");
        }
    }
}