# Estágio 1: Build da Aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia o arquivo de solução
COPY *.sln .

# Copia explicitamente cada arquivo de projeto que existe
COPY src/Application/*.csproj ./src/Application/
COPY src/Domain/*.csproj ./src/Domain/
COPY src/Infrastructure/*.csproj ./src/Infrastructure/
COPY src/WebApi/*.csproj ./src/WebApi/
# A LINHA PARA O PROJETO 'Migrations' FOI REMOVIDA

# Copia os projetos de teste
COPY tests/Application.UnitTests/*.csproj ./tests/Application.UnitTests/
COPY tests/Domain.UnitTests/*.csproj ./tests/Domain.UnitTests/
COPY tests/Infrastructure.UnitTests/*.csproj ./tests/Infrastructure.UnitTests/
COPY tests/WebApi.AcceptanceTests/*.csproj ./tests/WebApi.AcceptanceTests/
COPY tests/WebApi.SmokeTests/*.csproj ./tests/WebApi.SmokeTests/

# Restaura as dependências NuGet
RUN dotnet restore

# Copia todo o resto do código fonte
COPY . .

RUN dotnet publish src/WebApi/WebApi.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "WebApi.dll"]