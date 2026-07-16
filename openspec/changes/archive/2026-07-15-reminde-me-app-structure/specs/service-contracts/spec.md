## ADDED Requirements

### Requirement: Interface ITaskService
O sistema SHALL definir uma interface `ITaskService` no namespace `RemindeMeApp.Shared.Services` contendo assinaturas de métodos assíncronos para operações de CRUD e gestão de tarefas: `GetAllAsync`, `GetByIdAsync`, `CreateAsync`, `UpdateAsync`, `DeleteAsync`, `ToggleActiveAsync`. Todos os métodos SHALL ter tipos de parâmetro e retorno explícitos usando as entidades do domínio. A interface SHALL conter XML doc comments (`///`) descrevendo cada método.

#### Scenario: ITaskService possui assinaturas de CRUD com tipos definidos
- **WHEN** o desenvolvedor inspeciona a interface `ITaskService`
- **THEN** SHALL existir métodos assíncronos com parâmetros e retornos tipados para: listar todas as tarefas (`Task<IEnumerable<TaskItem>>`), buscar por ID (`Task<TaskItem?>`), criar (`Task<TaskItem>`), atualizar (`Task<TaskItem>`), deletar (`Task`), alternar status ativo (`Task`)

### Requirement: Interface ISubtaskService
O sistema SHALL definir uma interface `ISubtaskService` no namespace `RemindeMeApp.Shared.Services` contendo assinaturas de métodos assíncronos para operações de CRUD de subtarefas. A interface SHALL conter XML doc comments.

#### Scenario: ISubtaskService possui assinaturas de CRUD com tipos definidos
- **WHEN** o desenvolvedor inspeciona a interface `ISubtaskService`
- **THEN** SHALL existir métodos assíncronos com parâmetros e retornos tipados para: listar por tarefa pai, buscar por ID, criar, atualizar, deletar e alternar status ativo

### Requirement: Interface ITagService
O sistema SHALL definir uma interface `ITagService` no namespace `RemindeMeApp.Shared.Services` contendo assinaturas de métodos assíncronos para operações de CRUD de tags, incluindo criação inline. A interface SHALL conter XML doc comments.

#### Scenario: ITagService possui assinaturas de CRUD e criação inline
- **WHEN** o desenvolvedor inspeciona a interface `ITagService`
- **THEN** SHALL existir métodos assíncronos com parâmetros e retornos tipados para: listar todas, buscar por ID, criar, atualizar, deletar e obter-ou-criar por nome (criação inline)

### Requirement: Interface ITimeTrackerService
O sistema SHALL definir uma interface `ITimeTrackerService` no namespace `RemindeMeApp.Shared.Services` contendo assinaturas de métodos assíncronos para controle de tracking de tempo. A interface SHALL conter XML doc comments.

#### Scenario: ITimeTrackerService possui assinaturas de tracking
- **WHEN** o desenvolvedor inspeciona a interface `ITimeTrackerService`
- **THEN** SHALL existir métodos assíncronos com parâmetros e retornos tipados para: iniciar tracking, parar tracking, obter sessão ativa e adicionar tempo manual

### Requirement: Interface IPomodoroService
O sistema SHALL definir uma interface `IPomodoroService` no namespace `RemindeMeApp.Shared.Services` contendo assinaturas de métodos assíncronos para controle do modo Pomodoro. A interface SHALL conter XML doc comments.

#### Scenario: IPomodoroService possui assinaturas de Pomodoro
- **WHEN** o desenvolvedor inspeciona a interface `IPomodoroService`
- **THEN** SHALL existir métodos assíncronos com parâmetros e retornos tipados para: iniciar foco, iniciar descanso, parar sessão e obter sessão ativa

### Requirement: Interfaces registradas no contêiner de DI
Todas as interfaces de serviço SHALL ser preparadas para registro no contêiner de DI no `Program.cs` do Backend como `AddScoped<>`, com comentários indicando onde registrar implementações futuras.

#### Scenario: Interfaces preparadas para DI
- **WHEN** o desenvolvedor inspeciona o `Program.cs`
- **THEN** SHALL existir comentários preparatórios indicando onde registrar cada interface de serviço com sua implementação futura via `AddScoped<>`

### Requirement: Todos os métodos são assíncronos
Todas as assinaturas de métodos nas interfaces de serviço SHALL retornar `Task<T>` ou `Task`, seguindo o padrão de assincronismo da aplicação.

#### Scenario: Métodos retornam Task
- **WHEN** o desenvolvedor inspeciona qualquer interface de serviço
- **THEN** todos os métodos SHALL ter retorno `Task<T>` ou `Task` e nomes terminados em `Async`
