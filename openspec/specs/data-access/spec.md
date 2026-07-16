## ADDED Requirements

### Requirement: AppDbContext configurado com EF Core e SQLite
O sistema SHALL definir uma classe `AppDbContext` herdando de `DbContext` no projeto Backend (`RemindeMeApp.Backend.Data`) com `DbSet<>` para todas as entidades: `TaskItems`, `Subtasks`, `Tags`, `TimerSessions`.

#### Scenario: DbContext expõe DbSets para todas as entidades
- **WHEN** o desenvolvedor inspeciona a classe `AppDbContext`
- **THEN** SHALL existir propriedades `DbSet<TaskItem>`, `DbSet<Subtask>`, `DbSet<Tag>` e `DbSet<TimerSession>`

### Requirement: Fluent API para mapeamentos de entidades
O `AppDbContext` SHALL configurar todos os mapeamentos de entidades via Fluent API no método `OnModelCreating`, incluindo: chaves primárias, campos obrigatórios, valores default, relacionamentos (FK) e conversão de enum `TimerType` para string.

#### Scenario: TaskItem configurado via Fluent API
- **WHEN** o EF Core inicializa o modelo
- **THEN** SHALL estar configurado: `Titulo` como required, `IsAtivo` com default `true`, `TempoTotalGastoSegundos` com default `0`, FK opcional para `Tag`

#### Scenario: Subtask configurado via Fluent API
- **WHEN** o EF Core inicializa o modelo
- **THEN** SHALL estar configurado: `Titulo` como required, `IsAtivo` com default `true`, `TempoTotalGastoSegundos` com default `0`, FK obrigatória para `TaskItem` com delete cascade

#### Scenario: Tag configurado via Fluent API
- **WHEN** o EF Core inicializa o modelo
- **THEN** SHALL estar configurado: `Nome` como required, `CorHexadecimal` como required

#### Scenario: TimerSession configurado via Fluent API
- **WHEN** o EF Core inicializa o modelo
- **THEN** SHALL estar configurado: `Tipo` com conversão para string via `HasConversion<string>()`

### Requirement: Connection string externalizada via appsettings.json
A connection string do SQLite SHALL ser definida no arquivo `appsettings.json` do projeto Backend, não hardcoded no código. O path do banco de dados SHALL ser configurável.

#### Scenario: Connection string em appsettings.json
- **WHEN** o desenvolvedor inspeciona `appsettings.json` no Backend
- **THEN** SHALL existir uma seção `ConnectionStrings` com uma entrada para o SQLite

#### Scenario: AppDbContext usa connection string do configuration
- **WHEN** o AppDbContext é registrado via DI
- **THEN** SHALL utilizar a connection string lida do `IConfiguration`, não uma string hardcoded

### Requirement: DbContext registrado via Dependency Injection
O `AppDbContext` SHALL ser registrado no contêiner de DI nativo do .NET no `Program.cs` usando `AddDbContext<AppDbContext>` com a connection string lida do `appsettings.json`.

#### Scenario: DbContext disponível via DI
- **WHEN** um serviço solicita `AppDbContext` via construtor
- **THEN** SHALL receber uma instância configurada corretamente com SQLite
