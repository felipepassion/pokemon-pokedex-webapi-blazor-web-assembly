# Pokemon API

Este projeto é uma API desenvolvida em .NET Framework que consome a API PokeAPI para fornecer informações sobre Pokémons.

## Funcionalidades

- Obter informações sobre 10 Pokémons aleatórios
- Obter informações sobre 1 Pokémon específico
- Cadastrar mestre Pokémon com nome, idade e CPF em um banco de dados SQLite
- Informar que um Pokémon foi capturado
- Listar todos os Pokémons capturados

## Tecnologias Utilizadas

O projeto foi desenvolvido utilizando as seguintes tecnologias:

- [Microsoft.NET](https://dotnet.microsoft.com/) - plataforma de desenvolvimento utilizada
- [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet) - framework utilizado para desenvolvimento da API
- [SQLite](https://www.sqlite.org/index.html) - sistema de gerenciamento de banco de dados utilizado para persistência dos dados da aplicação
- [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) - biblioteca utilizada para geração de documentação da API
- [Tynamix.ObjectFiller](https://github.com/Tynamix/ObjectFiller.NET) - biblioteca utilizada para geração de dados aleatórios
- [FluentAssertions](https://fluentassertions.com/)
- [Tynamix.ObjectFiller](https://github.com/Tynamix/ObjectFiller.NET)
- [xUnit](https://xunit.net/)
- [Microsoft.NET.Test.Sdk](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test)

## Como Usar

1. Clone este repositório em sua máquina
2. Abra o projeto em seu IDE preferido
3. Instale as dependências do projeto
4. Compile e execute a aplicação
5. Acesse as rotas da API conforme descrito abaixo

## Pré-requisitos para executar o projeto

- Baixar o .NET 7 SDK e Runtime: O .NET 7 SDK é um conjunto de ferramentas essenciais para a criação e execução de aplicativos .NET. Certifique-se de baixar e instalar o .NET 7 SDK no seu computador antes de tentar executar o projeto.
- [Download .NET 7]([https://github.com/Tynamix/ObjectFiller.NET](https://dotnet.microsoft.com/en-us/download/dotnet/7.0))

- Instalar o Visual Studio 2022: O Visual Studio 2022 é um ambiente de desenvolvimento integrado que fornece uma interface amigável para criar, depurar e gerenciar projetos .NET. Certifique-se de instalar o Visual Studio 2022 no seu computador antes de tentar executar o projeto.
- [Download Visual Studio 2022]([[https://github.com/Tynamix/ObjectFiller.NET](https://visualstudio.microsoft.com/pt-br/vs/)](https://dotnet.microsoft.com/en-us/download/dotnet/7.0))
- 
- Verificar as especificações do sistema: Certifique-se de que o seu computador atenda às especificações necessárias para executar o .NET 7 SDK e o Visual Studio 2022. Verifique as informações de requisitos mínimos de sistema para cada um deles antes de tentar instalar.

-

## Rotas

# API de Pokémons

## Roteamento da API do PokemonController
Este arquivo descreve as rotas da API do PokemonController.

# Rotas da API do PokemonController

A seguir estão as rotas da API do PokemonController com suas descrições, métodos HTTP e parâmetros.

## 1 - Obter todos os Pokémons
- Método: GetAllPokemonsAsync
- Rota: GET /api/pokemon
- Descrição: Obtém uma lista de todos os Pokémons disponíveis.
- Parâmetros de consulta:
  - limit (opcional): O número máximo de Pokémons a serem retornados.
- Retorno: Uma lista de Pokémons.

## 2 - Pesquisar um Pokémon
- Método: SearchPokemonAsync
- Rota: GET /api/pokemon/{pokemonName}
- Descrição: Busca um Pokémon específico pelo seu nome ou ID na API.
- Parâmetros de consulta:
  - pokemonName: O nome do Pokémon a ser procurado.
- Retorno: Um objeto GetResponseDTO contendo um PokemonDTO se o Pokémon for encontrado, caso contrário retorna NotFound.

## 3 - Obter informações sobre as evoluções de um Pokémon
- Método: GetPokemonEvolutionsAsync
- Rota: GET /api/pokemon/{pokemonName}/evolutions
- Descrição: Obtém informações sobre as evoluções de um Pokémon especificado.
- Parâmetros de consulta:
  - pokemonName: O nome do Pokémon a ser pesquisado.
- Retorno: Uma lista de informações sobre as evoluções do Pokémon.

## 4 - Capturar um Pokémon para um mestre Pokémon especificado
- Método: CapturePokemon
- Rota: POST /api/pokemon/{pokemonName}/{masterId}/capture
- Descrição: Captura um Pokémon para um mestre Pokémon especificado.
- Parâmetros de corpo:
  - masterId: O ID do mestre Pokémon.
  - pokemonName: O nome do Pokémon a ser capturado.
  - forceCapture (opcional): Define se a captura deve ser forçada, mesmo que o Pokémon já tenha sido capturado anteriormente pelo mestre.
- Retorno: Um objeto GetResponseDTO contendo o número de Pokémons capturados pelo mestre até o momento.

## 5 - Obter todos os Pokémons capturados filtrando por um mestre ou não
- Método: GetAllCapturedPokemons
- Rotas:
  - GET /api/pokemon/captured/{masterId}
  - GET /api/pokemon/captured
- Descrição: Obtém todos os Pokémons capturados filtrando por um mestre ou não.
- Parâmetros de consulta:
  - masterId (opcional): O ID do mestre Pokémon.
- Retorno: Um objeto GetResponseDTO contendo uma lista de Pokémons capturados pelo mestre especificado ou por todos os mestres.

# Testes de Integração via `Postman` e `XUnit`

![image](https://user-images.githubusercontent.com/29386600/225140301-613ab251-433d-4629-9a5c-a8f5bb77144e.png)

## Todos os testes funcionando 
![image](https://user-images.githubusercontent.com/29386600/225140439-9708ba9e-2026-4aa9-b476-3b4db14ce44c.png)
![image](https://user-images.githubusercontent.com/29386600/225140513-0dcd3acc-eae8-437f-ac1e-c00fb104d7c6.png)

## Metodologias utilizadas: 

- DDD
- TDD
- CLEAN ARCH. 
- CLEAN CODE
- SOLID

A metodologia DDD ajuda a focar no negócio e nos requisitos do sistema, permitindo que criar uma estrutura de código que reflita o domínio da aplicação. Isso significa que a arquitetura da aplicação é baseada em um modelo de domínio rico, que descreve as entidades, objetos de valor, agregados e serviços que compõem o sistema. Essa abordagem ajuda a criar uma separação clara entre as camadas de negócio e de infraestrutura, garantindo que as funcionalidades de negócio possam ser implementadas sem depender dos detalhes da infraestrutura.

A metodologia TDD, por sua vez, ajuda a garantir a qualidade do código e a evitar problemas de regressão. Com ela, temos testes automatizados para cada funcionalidade do sistema antes mesmo de começar a implementá-las. Isso ajuda a definir claramente os requisitos de cada funcionalidade e a evitar que o código seja implementado de forma incorreta ou incompleta. Além disso, a prática de escrever testes antes de implementar o código ajuda a identificar e corrigir problemas de design e arquitetura precocemente.

Com base em Clean Architecture e Clean Code, seu projeto tem uma estrutura de código bem organizada, clara e legível, com foco na separação de responsabilidades e na manutenção da coesão e baixo acoplamento entre as diferentes camadas da aplicação. Isso ajuda a garantir a escalabilidade do projeto, permitindo que ele cresça e evolua ao longo do tempo sem se tornar difícil de manter.

Em resumo, a combinação de DDD, TDD, Clean Architecture e Clean Code traz diversos benefícios para escalabilidade do projeto, permitindo que ele seja implementado de forma eficiente, com qualidade e robustez, e que possa ser facilmente mantido e evoluído ao longo do tempo.

 Além das metodologias DDD e TDD, a API também conta com uma tela inicial básica ao executar o projeto BLAZOR em WebAssembly. Essa tela contém três botões para acessar a documentação, o repositório GitHub e os testes da API via Swagger.

A tela inicial é uma ótima forma de fornecer aos usuários um acesso rápido e fácil às informações importantes sobre a API. Ao clicar no botão "Documentação", o usuário é direcionado para uma página que contém a documentação completa da API, incluindo informações sobre os endpoints, os parâmetros de entrada, os formatos de retorno e muito mais.
![image](https://user-images.githubusercontent.com/29386600/225142422-4db44899-4700-47b8-bca9-212bb7e46fab.png)
