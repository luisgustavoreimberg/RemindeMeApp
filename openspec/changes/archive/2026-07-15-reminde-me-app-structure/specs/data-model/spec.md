## ADDED Requirements

### Requirement: Entidade TaskItem
O sistema SHALL definir uma classe `TaskItem` no namespace `RemindeMeApp.Shared.Models` com as propriedades: `Id` (int, PK), `Titulo` (string, obrigatório), `Descricao` (string, opcional/nullable), `DataHoraLembrete` (DateTime?, opcional), `IsAtivo` (bool, default true), `TagId` (int?, FK opcional), `TempoTotalGastoSegundos` (int, default 0). A entidade SHALL possuir propriedades de navegação para `Tag` e coleção de `Subtask`. A classe SHALL conter XML doc comments (`///`) descrevendo a entidade e cada propriedade.

#### Scenario: TaskItem possui todas as propriedades da especificação
- **WHEN** o desenvolvedor inspeciona a classe `TaskItem`
- **THEN** SHALL existir todas as propriedades definidas com os tipos corretos conforme a especificação

#### Scenario: TaskItem possui navegação para Tag
- **WHEN** a entidade `TaskItem` é carregada pelo EF Core
- **THEN** SHALL ser possível navegar para a entidade `Tag` associada (nullable) via propriedade de navegação

#### Scenario: TaskItem possui coleção de Subtasks
- **WHEN** a entidade `TaskItem` é carregada pelo EF Core
- **THEN** SHALL ser possível acessar a coleção de `Subtask` filhas via propriedade de navegação

#### Scenario: TaskItem possui XML doc comments
- **WHEN** o desenvolvedor inspeciona o arquivo da classe `TaskItem`
- **THEN** SHALL existir `///` summary na classe e em cada propriedade pública

### Requirement: Entidade Subtask
O sistema SHALL definir uma classe `Subtask` no namespace `RemindeMeApp.Shared.Models` com as propriedades: `Id` (int, PK), `ParentTaskId` (int, FK obrigatória), `Titulo` (string, obrigatório), `IsAtivo` (bool, default true), `TempoTotalGastoSegundos` (int, default 0). Subtasks SHALL ter apenas 1 nível de profundidade — sem subtarefas de subtarefas. A classe SHALL conter XML doc comments.

#### Scenario: Subtask possui todas as propriedades da especificação
- **WHEN** o desenvolvedor inspeciona a classe `Subtask`
- **THEN** SHALL existir todas as propriedades com os tipos corretos

#### Scenario: Subtask possui navegação para TaskItem pai
- **WHEN** a entidade `Subtask` é carregada pelo EF Core
- **THEN** SHALL ser possível navegar para a `TaskItem` pai via propriedade de navegação

### Requirement: Entidade Tag
O sistema SHALL definir uma classe `Tag` no namespace `RemindeMeApp.Shared.Models` com as propriedades: `Id` (int, PK), `Nome` (string, obrigatório), `CorHexadecimal` (string, obrigatório). A entidade SHALL possuir coleção de navegação para `TaskItem`. A classe SHALL conter XML doc comments.

#### Scenario: Tag possui todas as propriedades da especificação
- **WHEN** o desenvolvedor inspeciona a classe `Tag`
- **THEN** SHALL existir as propriedades `Id`, `Nome` e `CorHexadecimal` com os tipos corretos

#### Scenario: Tag possui coleção de TaskItems
- **WHEN** a entidade `Tag` é carregada pelo EF Core
- **THEN** SHALL ser possível acessar a coleção de `TaskItem` associadas

### Requirement: Entidade TimerSession
O sistema SHALL definir uma classe `TimerSession` no namespace `RemindeMeApp.Shared.Models` com as propriedades: `Id` (int, PK), `ReferenceId` (int), `IsSubtask` (bool), `StartTime` (DateTime), `Tipo` (TimerType enum), `DuracaoEsperadaSegundos` (int?, apenas para Pomodoro). A classe SHALL conter XML doc comments.

#### Scenario: TimerSession possui todas as propriedades da especificação
- **WHEN** o desenvolvedor inspeciona a classe `TimerSession`
- **THEN** SHALL existir todas as propriedades com os tipos corretos

### Requirement: Enum TimerType
O sistema SHALL definir um enum `TimerType` no namespace `RemindeMeApp.Shared.Models` com os valores `FreeTimer` e `Pomodoro`. O enum SHALL conter XML doc comments.

#### Scenario: TimerType possui os valores definidos
- **WHEN** o desenvolvedor inspeciona o enum `TimerType`
- **THEN** SHALL existir exatamente os valores `FreeTimer` e `Pomodoro`
