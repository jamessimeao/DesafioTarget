using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioTargetTestes
{
    public static class PostTests
    {
        public static async Task Test(
            uint numeroDoDesafio,
            HttpClient httpClient,
            uint port,
            string action,
            string caminhoDoJsonLocal,
            string mensagemAntesDaResposta)
        {
            Console.WriteLine($"\n ------- Teste para o desafio {numeroDoDesafio} -------");
            try
            {
                // Lê o arquivo vendas.json
                string jsonSerializado = ReadLocalJson(caminhoDoJsonLocal);
                Console.WriteLine("json a ser enviado:");
                Console.WriteLine(jsonSerializado);

                // Faz um POST request para o endpoint de solução do desafio 1, enviando o json de vendas
                string? responseContent = await Post(httpClient, port, action, jsonSerializado, numeroDoDesafio);
                if (responseContent == null)
                {
                    Console.WriteLine("Erro: responseContent = null");
                    return;
                }
                Console.WriteLine(mensagemAntesDaResposta);
                Console.WriteLine(responseContent);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return;
            }
        }

        private static string ReadLocalJson(string relativePath)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string jsonFilePath = Path.Combine(baseDir, relativePath);
            using StreamReader reader = new StreamReader(jsonFilePath);
            return reader.ReadToEnd();
        }

        private static async Task<string?> Post(
            HttpClient httpClient,
            uint port,
            string action,
            string jsonSerializado,
            uint numeroDoDesafio)
        {
            HttpContent content = new StringContent(jsonSerializado, Encoding.UTF8, "application/json");
            Console.WriteLine("Fazendo um POST request...");
            string url = $"http://localhost:{port}/desafio{numeroDoDesafio}/Solucao/{action}";
            HttpResponseMessage response = await httpClient.PostAsync(url, content);

            // Processamento da resposta
            Console.WriteLine($"Status da resposta: {response.StatusCode}");
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            else
            {
                return null;
            }
        }
    }
}
