## 1. Configuração Inicial do Repositório

- [x] 1.1 Criar arquivo `.gitignore` para .NET na raiz do repositório (incluindo `bin/`, `obj/`, `*.db`, `.vs/`, `*.user`)
- [x] 1.2 Criar arquivo `global.json` na raiz do repositório especificando a versão do .NET SDK LTS com `rollForward: latestPatch`

## 2. Criação da Solução e Projetos

- [x] 2.1 Criar a solução `RemindeMeApp.sln` na raiz do repositório
- [x] 2.2 Criar o projeto `src/RemindeMeApp.Shared/RemindeMeApp.Shared.csproj` como Class Library (.NET LTS)
- [x] 2.3 Criar o projeto `src/RemindeMeApp.Backend/RemindeMeApp.Backend.csproj` como console app (.NET LTS)
- [x] 2.4 Criar o projeto `src/RemindeMeApp.Frontend/RemindeMeApp.Frontend.csproj` como Razor Class Library (SDK `Microsoft.NET.Sdk.Razor`)
- [x] 2.5 Adicionar os três projetos à solução
- [x] 2.6 Adicionar ProjectReference do Backend para o Shared
- [x] 2.7 Adicionar ProjectReference do Backend para o Frontend
- [x] 2.8 Adicionar ProjectReference do Frontend para o Shared

## 3. Pacotes NuGet

- [x] 3.1 Adicionar `Photino.NET` ao projeto Backend
- [x] 3.2 Adicionar `Microsoft.EntityFrameworkCore.Sqlite` ao projeto Backend
- [x] 3.3 Adicionar `Microsoft.EntityFrameworkCore.Tools` ao projeto Backend
- [x] 3.4 Verificar que `dotnet restore` executa com sucesso

## 4. Entidades do Domínio (no projeto Shared)

- [x] 4.1 Criar o enum `TimerType` com valores `FreeTimer` e `Pomodoro` e XML doc comments em `src/RemindeMeApp.Shared/Models/TimerType.cs`
- [x] 4.2 Criar a entidade `Tag` com propriedades `Id`, `Nome`, `CorHexadecimal` e coleção de navegação para `TaskItem`, com XML doc comments, em `src/RemindeMeApp.Shared/Models/Tag.cs`
- [x] 4.3 Criar a entidade `TaskItem` com todas as propriedades (`Id`, `Titulo`, `Descricao`, `DataHoraLembrete`, `IsAtivo`, `TagId`, `TempoTotalGastoSegundos`) e navegações, com XML doc comments, em `src/RemindeMeApp.Shared/Models/TaskItem.cs`
- [x] 4.4 Criar a entidade `Subtask` com propriedades `Id`, `ParentTaskId`, `Titulo`, `IsAtivo`, `TempoTotalGastoSegundos` e navegação para `TaskItem`, com XML doc comments, em `src/RemindeMeApp.Shared/Models/Subtask.cs`
- [x] 4.5 Criar a entidade `TimerSession` com propriedades `Id`, `ReferenceId`, `IsSubtask`, `StartTime`, `Tipo`, `DuracaoEsperadaSegundos`, com XML doc comments, em `src/RemindeMeApp.Shared/Models/TimerSession.cs`

## 5. Configuração do DbContext (no projeto Backend)

- [x] 5.1 Criar a classe `AppDbContext` herdando de `DbContext` em `src/RemindeMeApp.Backend/Data/AppDbContext.cs`
- [x] 5.2 Definir `DbSet<TaskItem>`, `DbSet<Subtask>`, `DbSet<Tag>` e `DbSet<TimerSession>` no AppDbContext
- [x] 5.3 Implementar `OnModelCreating` com Fluent API: configurar `TaskItem` (required `Titulo`, defaults para `IsAtivo` e `TempoTotalGastoSegundos`, FK opcional para `Tag`)
- [x] 5.4 Implementar Fluent API para `Subtask` (required `Titulo`, defaults, FK obrigatória para `TaskItem` com cascade delete)
- [x] 5.5 Implementar Fluent API para `Tag` (required `Nome` e `CorHexadecimal`)
- [x] 5.6 Implementar Fluent API para `TimerSession` (conversão `TimerType` para string)

## 6. Interfaces de Serviço (no projeto Shared)

- [x] 6.1 Criar interface `ITaskService` com métodos assíncronos tipados (`GetAllAsync`, `GetByIdAsync`, `CreateAsync`, `UpdateAsync`, `DeleteAsync`, `ToggleActiveAsync`) e XML doc comments em `src/RemindeMeApp.Shared/Services/ITaskService.cs`
- [x] 6.2 Criar interface `ISubtaskService` com métodos assíncronos tipados (`GetByParentTaskIdAsync`, `GetByIdAsync`, `CreateAsync`, `UpdateAsync`, `DeleteAsync`, `ToggleActiveAsync`) e XML doc comments em `src/RemindeMeApp.Shared/Services/ISubtaskService.cs`
- [x] 6.3 Criar interface `ITagService` com métodos assíncronos tipados (`GetAllAsync`, `GetByIdAsync`, `CreateAsync`, `UpdateAsync`, `DeleteAsync`, `GetOrCreateByNameAsync`) e XML doc comments em `src/RemindeMeApp.Shared/Services/ITagService.cs`
- [x] 6.4 Criar interface `ITimeTrackerService` com métodos assíncronos tipados (`StartTrackingAsync`, `StopTrackingAsync`, `GetActiveSessionAsync`, `AddManualTimeAsync`) e XML doc comments em `src/RemindeMeApp.Shared/Services/ITimeTrackerService.cs`
- [x] 6.5 Criar interface `IPomodoroService` com métodos assíncronos tipados (`StartFocusAsync`, `StartBreakAsync`, `StopAsync`, `GetActiveSessionAsync`) e XML doc comments em `src/RemindeMeApp.Shared/Services/IPomodoroService.cs`

## 7. Configuração do Program.cs, DI e Boilerplate

- [x] 7.1 Criar `appsettings.json` no Backend com connection string do SQLite configurável
- [x] 7.2 Configurar `Program.cs` no Backend: criar WebApplication builder, registrar DbContext (`AddDbContext<AppDbContext>` com connection string do `appsettings.json`), registrar Razor Pages (`AddRazorPages`)
- [x] 7.3 Adicionar comentários preparatórios no `Program.cs` para registro futuro de cada interface de serviço via `AddScoped<>`
- [x] 7.4 Configurar bootstrapping Photino + Kestrel: iniciar Kestrel com `app.StartAsync()`, criar `PhotinoWindow` na thread principal apontando para URL local, `WaitForClose()`, e `app.StopAsync()` no shutdown
- [x] 7.5 Criar `Pages/_ViewImports.cshtml` no Frontend com `@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers`
- [x] 7.6 Criar `Pages/_ViewStart.cshtml` no Frontend

## 8. Verificação

- [x] 8.1 Executar `dotnet restore` e confirmar que todos os pacotes são resolvidos
- [x] 8.2 Executar `dotnet build` e confirmar compilação sem erros em toda a solução
- [x] 8.3 Verificar que o grafo de dependências é unidirecional (sem referências circulares)
