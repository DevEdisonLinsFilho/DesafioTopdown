# Desafio Técnico - Catálogo e Pedidos (.NET 8 + Angular 17)

Este repositório contém a solução para o desafio técnico Full-Stack, que consiste em uma aplicação web completa de "Catálogo e Pedidos".

## Objetivo do Projeto

O objetivo foi construir um sistema web completo que permite a gestão de produtos e clientes (CRUD) e a criação de pedidos com validação de estoque e idempotência, utilizando .NET 8 para o backend e Angular 17 para o frontend[cite: 1].

## 🚀 Tech Stack

A solução foi construída utilizando as seguintes tecnologias:

- **Backend**: .NET 8, ASP.NET Core Web API [cite: 58]
- **Frontend**: Angular 17+, TypeScript [cite: 59]
- **Banco de Dados**: PostgreSQL [cite: 60]
- **ORM**: Abordagem híbrida com Entity Framework Core (para CRUD) e Dapper (para leituras otimizadas) [cite: 58]
- **Arquitetura**: Clean Architecture, CQRS com MediatR, Padrão de Repositório e Unit of Work.
- **Testes**: xUnit e Moq.
- **Containerização**: Docker e Docker Compose[cite: 62].

## 📂 Estrutura do Projeto

O repositório está organizado em duas pastas principais: `backend` e `frontend`.

- **`backend`**: Contém a solução .NET, estruturada em projetos seguindo os princípios da Clean Architecture[cite: 23]:
    - **Domain**: Entidades e regras de negócio centrais.
    - **Application**: Casos de uso (Commands/Queries), DTOs e interfaces.
    - **Infrastructure**: Implementações de acesso a dados (EF Core, Dapper), serviços externos, etc.
    - **WebApi**: A camada de API (Minimal APIs), configuração e injeção de dependência.
- **`frontend`**: Contém a aplicação Angular, com uma estrutura modular por funcionalidade (`features`).

## ⚙️ Como Executar o Projeto

A aplicação é totalmente containerizada e pode ser executada com um único comando.

### Pré-requisitos

- Docker e Docker Compose instalados e em execução.

### Passo a Passo

1.  **Clone o Repositório**
    ```bash
    git clone <URL_DO_SEU_REPOSITORIO>
    cd <PASTA_DO_PROJETO>
    ```

2.  **Configure as Variáveis de Ambiente**
    Na raiz do projeto, renomeie o arquivo `.env.example` para `.env` e defina uma senha para o banco de dados.
    ```env
    DB_NAME=topdown_db
    DB_USER=postgres
    DB_PASSWORD={SuaSenhaAqui}
    ```

3.  **Inicie os Containers**
    Execute o seguinte comando na raiz do projeto:
    ```bash
    docker-compose up --build
    ```
    Este comando irá construir as imagens do frontend e do backend e iniciar os três containers (frontend, backend, db).

4.  **Acesse a Aplicação**
    - **Frontend**: [http://localhost:4200](http://localhost:4200)
    - **Backend (Swagger UI)**: [https://localhost:7128/swagger](https://localhost:7128/swagger)

## 🧪 Como Rodar os Testes

Os testes unitários do backend podem ser executados com o seguinte comando, a partir da pasta raiz do projeto (`backend/ProjetoTopdown`):

```bash
dotnet test
```

## ✨ Uso de IA

**Enable IA no Angular CLI**
    - Ao criar o projeto do Angular, usando o CLI, ele me deu opção de fazer enable de varias IA's durante a criação, o que gerou as pastas .gemini e .github
Por mais que isso tenha sido feito, não tenho certeza do que isso realmente habilita, os usos de IA que eu fiz na verdade foi exclusivamente no Gemini Pro 2.5 com a opção "Parceiro de Programação"

**IA nos Testes Unitários**
    - Fiz uma cobertura pequena de testes devido o meu tempo para execução do desafio, mas para definir os cenários e correção de alguns erros nos testes que fui escrevendo, utilizei o Gemini como mencionado.

**Correções em erros durante o desenvolvimento**
    - Ao desenvolver o desafio, em alguns momentos tive alguns erros principalmente em configurações do projeto do frontend, onde estou menos habituado que no backend. Ao definir interceptors do 0 e o proxy, tive alguns erros por estar usando seções erradas na injeção, ou até mesmo erros na construção do interceptor, o Gemini me ajudou aqui também.

**Conteinerização**    
    -Na criação do DockerFile tive um problema com o local do DockerFile em relação as outras referencias necessarias, o mesmo no docker-compose.yml, o Gemini me ajudou a arrumar a localização e também me recomendou algumas sugestões que ficaram nas versões finais.

**README** 
    Este README foi gerado por IA, na verdade, quase todo ele, expliquei a minha estrutura completa para ele e utilizando a mesma thread que me ajudou com os containers, consegui gerar este readme, até a secção desta explicação, essa eu fiz manualmente tentando seguir o resto da formatação que ele gerou.