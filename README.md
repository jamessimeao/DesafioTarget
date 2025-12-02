# DesafioTarget

Minha solução para o desafio é estruturada da seguinte forma. A solução para cada um dos 3 problemas é implementada num projeto diferente. A solução para o problema N está no projeto DesafioTargetN. Para os dois primeiros problemas fiz uma API REST, em que a solução está em um dos endpoints do Controller. Para o problema 3, fiz um Console app. Escrevi também alguns testes para as soluções. Há testes para os problemas 1 e 2 no projeto DesafioTargetPostTests. Há testes unitários para o problema 3 no projeto DesafioTarget3Tests.

#Problema 1

O problema pede para implementar um programa que calculasse as comissões dos vendedores a partir de um arquivo json. Para isto fiz o controller SolucaoController, cuja ação Solucao processa tal arquivo json e retorna um outro json com os nomes dos vendedores e respectivas comissões. Essa ação é executada ao fazer uma requisão POST para a API, enviando o json no corpo da requisição. Para o json fornecido no enunciado do problema, foi o obtido o seguinte resultado:

{
  "João Silva":495.6770,
  "Maria Souza":465.9495,
  "Carlos Oliveira":379.3715,
  "Ana Lima":404.9805
}

Os métodos que calculam essas comissões estão na classe estática CalculaComissao.

#Problema 2

O problema 2 pede para implementar um programa que permite realizar movimentações de produtos de um estoque. Os produtos do estoque foram fornecidos num json no enunciado do problema. Para isto é necessário guardar o estoque em memória. Para isto, usei um banco de dados para guardar o estoque. Para simplificar testar o projeto em outros computadores, utilizei o EntityFrameworkCore com um banco de dados InMemory. Assim, não é necessário configurar uma connection string, nem instalar um banco de dados, para executar o projeto. O EntityFrameworkCore também permite trocar o banco de dados utilizado sem muita dificuldade. Apesar disso, tendo a utilizar o Dapper por dar mais controle sobre a criação das tabelas no banco de dados, e por permitir escrever as queries SQL diretamente, mas não fiz isso neste projeto pelos motivos anteriores.
