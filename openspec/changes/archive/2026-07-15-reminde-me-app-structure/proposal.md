## Why

O RemindeMeApp precisa de uma estrutura base sólida antes que qualquer funcionalidade possa ser implementada. Atualmente o repositório contém apenas a especificação técnica — não há solução .sln, projetos, entidades, nem configuração de banco de dados. Esta change estabelece o "esqueleto" da aplicação: a solução .NET com três projetos (Shared, Backend e Frontend), entidades do domínio, configuração do SQLite via EF Core, e as interfaces (contratos) da camada de serviços. Isso permite que changes futuras foquem exclusivamente em regras de negócio e UI, sem se preocupar com bootstrapping.

## What Changes

- Criação da solução `RemindeMeApp.sln` na raiz do repositório com `.gitignore` e `global.json`
- Criação do projeto compartilhado `RemindeMeApp.Shared` (Class Library) com entidades, enums e interfaces de serviço
- Criação do projeto backend `RemindeMeApp.Backend` (.NET LTS, console/web host com Photino.NET)
- Criação do projeto frontend `RemindeMeApp.Frontend` (Razor Class Library com boilerplate mínimo)
- Definição das entidades do domínio: `TaskItem`, `Subtask`, `Tag`, `TimerSession` e enum `TimerType`
- Configuração do `AppDbContext` (EF Core + SQLite) com mapeamentos Fluent API e connection string via `appsettings.json`
- Registro dos serviços e DbContext via Dependency Injection no `Program.cs`
- Criação das interfaces de serviço: `ITaskService`, `ISubtaskService`, `ITagService`, `ITimeTrackerService`, `IPomodoroService`
- Instalação dos pacotes NuGet necessários (Photino.NET, EF Core SQLite, EF Core Tools)

## Capabilities

### New Capabilities
- `project-scaffold`: Estrutura da solução .NET com três projetos (Shared, Backend, Frontend), configuração de build, `.gitignore` e `global.json`
- `data-model`: Entidades do domínio (TaskItem, Subtask, Tag, TimerSession) e enum TimerType
- `data-access`: Configuração do AppDbContext com EF Core + SQLite, Fluent API, connection string externalizada e DI
- `service-contracts`: Interfaces da camada de serviços (ITaskService, ISubtaskService, ITagService, ITimeTrackerService, IPomodoroService)

### Modified Capabilities
_Nenhuma — este é o primeiro change do projeto._

## Impact

- **Estrutura do repositório**: Novos diretórios `src/RemindeMeApp.Shared/`, `src/RemindeMeApp.Backend/` e `src/RemindeMeApp.Frontend/`
- **Dependências**: Pacotes NuGet — `Photino.NET`, `Microsoft.EntityFrameworkCore.Sqlite`, `Microsoft.EntityFrameworkCore.Tools`
- **Banco de dados**: Arquivo SQLite local será criado em runtime (após migration futura); nesta change apenas o DbContext é configurado
- **Nenhuma funcionalidade executável**: Esta change entrega apenas a estrutura — sem regras de negócio, sem UI, sem endpoints funcionais
