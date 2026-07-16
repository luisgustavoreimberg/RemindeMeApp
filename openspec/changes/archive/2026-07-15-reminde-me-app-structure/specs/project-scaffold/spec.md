## ADDED Requirements

### Requirement: Solução .NET na raiz do repositório
O sistema SHALL possuir um arquivo `RemindeMeApp.sln` na raiz do repositório contendo referências a todos os projetos da aplicação.

#### Scenario: Solução criada com projetos referenciados
- **WHEN** o desenvolvedor abre `RemindeMeApp.sln` no Visual Studio ou executa `dotnet build` na raiz
- **THEN** a solução SHALL compilar com sucesso e conter referências aos projetos `RemindeMeApp.Shared`, `RemindeMeApp.Backend` e `RemindeMeApp.Frontend`

### Requirement: Arquivo .gitignore para .NET
O repositório SHALL possuir um arquivo `.gitignore` na raiz configurado para projetos .NET, ignorando diretórios `bin/`, `obj/`, arquivos de usuário do Visual Studio, e o arquivo de banco SQLite.

#### Scenario: .gitignore exclui artefatos de build
- **WHEN** o desenvolvedor executa `dotnet build` e verifica `git status`
- **THEN** os diretórios `bin/` e `obj/` SHALL não aparecer como untracked files

### Requirement: Arquivo global.json para pinnar SDK
O repositório SHALL possuir um arquivo `global.json` na raiz especificando a versão do .NET SDK LTS a ser utilizada, com `rollForward` configurado para permitir patch updates.

#### Scenario: SDK version pinna
- **WHEN** o desenvolvedor executa `dotnet --version` no diretório do repositório
- **THEN** SHALL utilizar a versão do SDK especificada no `global.json` (ou roll-forward compatível)

### Requirement: Projeto Shared como Class Library
O projeto `src/RemindeMeApp.Shared/RemindeMeApp.Shared.csproj` SHALL ser uma Class Library (.NET LTS) contendo entidades do domínio, enums e interfaces de serviço compartilhadas entre Backend e Frontend.

#### Scenario: Projeto Shared compila como Class Library
- **WHEN** o desenvolvedor executa `dotnet build` no projeto Shared
- **THEN** o projeto SHALL compilar com sucesso sem dependências de ASP.NET Core ou Photino

### Requirement: Projeto Backend como host Photino.NET
O projeto `src/RemindeMeApp.Backend/RemindeMeApp.Backend.csproj` SHALL ser uma aplicação console (.NET LTS) configurada com Photino.NET como host desktop e Kestrel embarcado para servir o frontend.

#### Scenario: Projeto backend compila com dependências Photino
- **WHEN** o desenvolvedor executa `dotnet build` no projeto Backend
- **THEN** o projeto SHALL compilar com sucesso e conter referência ao pacote `Photino.NET`

#### Scenario: Backend referencia Shared e Frontend
- **WHEN** o projeto Backend é compilado
- **THEN** ele SHALL conter ProjectReferences para `RemindeMeApp.Shared` e `RemindeMeApp.Frontend`

### Requirement: Projeto Frontend como Razor Class Library
O projeto `src/RemindeMeApp.Frontend/RemindeMeApp.Frontend.csproj` SHALL ser uma Razor Class Library com suporte a Razor Pages e assets estáticos, incluindo arquivos boilerplate mínimos.

#### Scenario: Projeto frontend compila como RCL
- **WHEN** o desenvolvedor executa `dotnet build` no projeto Frontend
- **THEN** o projeto SHALL compilar com sucesso com o SDK `Microsoft.NET.Sdk.Razor`

#### Scenario: Frontend referencia apenas Shared
- **WHEN** o desenvolvedor inspeciona o `.csproj` do Frontend
- **THEN** SHALL existir apenas ProjectReference para `RemindeMeApp.Shared`, sem referência ao Backend

### Requirement: Boilerplate Razor Pages no Frontend
O projeto Frontend SHALL conter os arquivos boilerplate mínimos para que Razor Pages funcionem: `_ViewImports.cshtml` (com imports de Tag Helpers) e `_ViewStart.cshtml`.

#### Scenario: ViewImports existe com Tag Helpers
- **WHEN** o desenvolvedor inspeciona `Pages/_ViewImports.cshtml` no Frontend
- **THEN** SHALL existir `@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers`

#### Scenario: ViewStart existe
- **WHEN** o desenvolvedor inspeciona `Pages/_ViewStart.cshtml` no Frontend
- **THEN** SHALL existir o arquivo com referência ao layout padrão

### Requirement: Grafo de dependências unidirecional
O grafo de dependências entre projetos SHALL ser unidirecional, sem dependências circulares: Backend → Shared, Backend → Frontend, Frontend → Shared.

#### Scenario: Sem dependência circular
- **WHEN** o desenvolvedor executa `dotnet build` na solução
- **THEN** SHALL compilar com sucesso sem erros de dependência circular

### Requirement: Estrutura de diretórios do projeto
O repositório SHALL seguir a estrutura de diretórios `src/` para organizar os projetos de código-fonte.

#### Scenario: Diretórios de projeto existem
- **WHEN** o desenvolvedor navega no repositório
- **THEN** SHALL existir os diretórios `src/RemindeMeApp.Shared/`, `src/RemindeMeApp.Backend/` e `src/RemindeMeApp.Frontend/`

### Requirement: Pacotes NuGet instalados
Os projetos SHALL conter todos os pacotes NuGet necessários para compilação e funcionamento.

#### Scenario: Backend contém pacotes necessários
- **WHEN** o desenvolvedor inspeciona o `.csproj` do Backend
- **THEN** SHALL existir referências a `Photino.NET`, `Microsoft.EntityFrameworkCore.Sqlite` e `Microsoft.EntityFrameworkCore.Tools`

#### Scenario: Shared não possui pacotes de infraestrutura
- **WHEN** o desenvolvedor inspeciona o `.csproj` do Shared
- **THEN** SHALL não existir referências a Photino, EF Core ou ASP.NET Core (apenas .NET standard library)
