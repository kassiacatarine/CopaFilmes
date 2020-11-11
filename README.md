# Copa de Filmes

Projeto para realizar a Copa do mundo de filmes, a partir de 8 filmes o campeonato é iniciado. Após a criação do campeonato só resta aguardar o resultado de classificação dos escolhidos.

## Projeto Frontend

- Tecnologia: Angular 10
- Link Deploy: [Site](https://copafilmes.netlify.app/)
- Rotas de acesso:
  - Criar campeonato: [Link](https://copafilmes.netlify.app/)
  - Visualizar classificação: [Link](https://copafilmes.netlify.app/standings/5fab2bf12d29f29bc1b72951)
- Comando de execução:

      cd championship-app && ng s -o

- Comando de build:

      cd championship-app && ng build --prod

## Projeto Backend

- Tecnologia: Dotnet Core 3.1
- Banco de dados: MongoDb
- Design de arquitetura: DDD e outros
- Framework de teste: XUnit
- Link Deploy: [Site](https://copafilmes.herokuapp.com/)
- Rotas de acesso:
  - Get Movies (Filmes): GET [Link](https://copafilmes.herokuapp.com/api/v1/movies)
  - Create Tournament (Campeonato): POST [Link](https://copafilmes.herokuapp.com/api/v1/tournaments)
  - Get Standing (Classificação): GET [Link](https://copafilmes.herokuapp.com/api/v1/standings/5fab2bf12d29f29bc1b72951)
