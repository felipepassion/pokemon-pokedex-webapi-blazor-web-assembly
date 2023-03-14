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
