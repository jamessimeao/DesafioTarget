# DesafioTarget

Minha solução para o desafio é estruturada da seguinte forma. A solução para cada um dos 3 problemas é implementada num projeto diferente. A solução para o problema N está no projeto DesafioTargetN. Para os dois primeiros problemas fiz uma API REST, em que a solução está em uma das ações do Controller. Para o problema 3, fiz um console app. Escrevi também alguns testes para as soluções. Há testes para os problemas 1 e 2 no projeto DesafioTargetPostTests. Há testes unitários para o problema 3 no projeto DesafioTarget3Tests.

# Problema 1

O problema pede para implementar um programa que calcule as comissões dos vendedores a partir de um arquivo json. Para isto fiz o controller SolucaoController, cuja ação Solucao processa tal arquivo json e retorna um outro json com os nomes dos vendedores e respectivas comissões. Essa ação é executada ao fazer uma requisão POST para a API, enviando o json no corpo da requisição. Para o json fornecido no enunciado do problema, foi o obtido o seguinte resultado:

{
  "João Silva":495.6770,
  "Maria Souza":465.9495,
  "Carlos Oliveira":379.3715,
  "Ana Lima":404.9805
}

Os métodos que calculam essas comissões estão na classe estática CalculaComissao.

# Problema 2

O problema 2 pede para implementar um programa que permite realizar movimentações de produtos de um estoque. Os produtos do estoque foram fornecidos num json no enunciado do problema. Tal programa precisa guardar o estoque em memória. Para isto, usei um banco de dados para guardar o estoque. Para simplificar testar o projeto em outros computadores, utilizei o EntityFrameworkCore com um banco de dados InMemory. Assim, não é necessário configurar uma connection string, nem instalar um banco de dados, para executar o projeto. O EntityFrameworkCore também permite trocar o banco de dados utilizado sem muita dificuldade. Apesar disso, tendo a utilizar o Dapper por dar mais controle sobre a criação das tabelas no banco de dados, e por permitir escrever as queries SQL diretamente, mas não fiz isso neste projeto pelos motivos anteriores. O banco de dados possui 3 tabelas, correspondentes aos 3 modelos definidos na pasta Models. Os modelos são:

<ul>
  <li> Produto, </li>
  <li> EstoqueProduto, </li>
  <li> MovimentacaoDeEstoque </li>
</ul>

A classe Produto contém o código de um produto e sua descrição. Como espero que o código de um produto seja único, utilizei-o como primary key da tabela correspondente, mas também poderia ter criado uma primary key Id se necessário. A classe EstoqueProduto possui uma primary key Id, guarda o código de um produto (foreign key) e sua quantidade em estoque. Ela corresponde a uma tabela no banco de dados com as informações do estoque de produtos. A classe MovimentacaoDeEstoque guarda as informações sobre uma movimentação de estoque realizada. Ela tem uma primary key Id, que é o **identificador único** pedido no enunciado. Ela possui também uma propriedade Descricao, que é **a descrição para identificar o tipo da movimentação realizada**, conforme pedido no enunciado. O Id é gerado automaticamente pelo banco de dados, mas a Descricao deve ser fornecida. Essa classe tem também o código de um produto e quantidade, necessários para movimentar o estoque.

Os endpoints da API estão definidos no controle SolucaoController. Para inicializar o banco de dados com o estoque fornecido no json do enunciado do problema, fiz uma ação RegistraEstoque no controle SolucaoController. Essa ação processa o json de estoque e inicializa o banco de dados com essa informação. Fiz dessa forma por simplicidade, mas poderia também criar uma ação que registra produtos no banco de dados, inicializando seu estoque com quantidade 0, e depois fazer movimentações de estoque colocando os valores fornecidos no json do enunciado.

Há também uma ação MovimentaEstoque no controle SolucaoController. Ela é a ação principal da API, sendo responsável por fazer as movimentações de estoque. Ela é acessada através de um POST request que envia um json com as informações da movimentação. Esse json é convertido num dto (data transfer object) MovimentacaoDeEstoqueDto, que contém as mesma propriedades de MovimentacaoDeEstoque, exceto o Id, pois este é gerado pelo banco de dados automaticamente. As informações da movimentação realizada é guardada na tabela correspondente à classe MovimentacaoDeEstoque, e a tabela de estoque é atualizada com a nova quantidade de estoque do produto. Essa quantidade é retornada na resposta da requisição POST.

# Problema 3

A solução deste problema é implementada num projeto do tipo console app. Foi criada uma classe estática Solucao, cujo método Juros calcula o juros a ser pego devido. O juros foi calculado da seguinte forma. Se não passou do vencimento, não há juros, logo ele é 0. Agora digamos que passaram d dias após o vencimento, sendo d um inteiro com d > 0. Dado um valor inicial v, a cada dia deve ser aplicada uma multa sobre o valor do dia anterior. Um dia após o vencimento, a multa é

multa = taxaDeJuros * v.

Vamos utilizar taxaDeJuros = 0,025, correspondente a uma taxa de juros de 2,5%. O valor total a ser pago 1 dia após o vencimento é então

montanteAposUmDia = v + multa = (1+taxaDeJuros) * v.

Assim, o valor total a ser pago d dias após o vencimento é dado pelo valor do dia anterior multiplicado por 1+taxaDeJuros. Portanto, o valor total a ser pago ao passarem d dias após o vencimento é

montante = Pow(1+taxaDeJuros,d) * v,

em que Pow(x,d) é x elevado à potência d. Por fim, o juros a ser pago é o valor que foi acrescentado a v, ou seja,

juros = montante - v
      = Pow(1+taxaDeJuros,d) * v - v
      = (Pow(1+taxaDeJuros,d) -1)*v.

Essa é a expressão utilizada pelo método Juros.

# Testes

Foram feitos dois projetos para testar as soluções. Um é o DesafioTargetPostTests, que é um projeto do tipo console app. Ele faz requisições POST para as APIs dos projetos DesafioTarget1 e DesafioTarget2. Devido a isto, é necessário primeiro executar esses dois projetos antes de executar o projeto DesafioTargetPostTests. Esse projeto realiza um POST para o DesafioTarget1 enviando o json com as vendas. Essa requisição é processada, sendo calculadas as comissões dos vendedores e estas são retornadas na resposta da requisição como um json. A resposta é lida pelo console app e impressa no terminal. Também são feitos POSTs para a API do projeto DesafioTarget2. Primeiro é feito um POST que envia o json de estoque fornecido no enunciado do problema 2. Esse json é utilizado para inicializar o banco de dados. Após isso, outro POST é feito em que é enviado um json para fazer uma movimentação de estoque. As informações da movimentação são registradas numa tabela de movimentações no banco de dados. É gerado um identificador único para a movimentação de forma automática pelo banco de dados. O codigo do produto e quantidade informadas no json de movimentação são utilizados para atualizar a quantidade do produto no estoque. Por fim, a API retorna na resposta da requisição a quantidade final do produto no estoque. Essa quantidade é lida pelo console app e impressa no terminal.

O outro projeto de testes é do tipo xUnit Test Project. Ele utiliza o xUnit para fazer testes unitários, sem ter um Program.cs. Devido a isto, os testes são executadas pelo comando dotnet test ou pelo Test Explorer do Visual Studio. Esse projeto faz testes unitários para o método Juros da Classe Solucao do projeto DesafioTarget3. São feitos testes para antes e após a data de vencimento. Antes da data de vencimento não há juros, ou seja, o juros é 0.
