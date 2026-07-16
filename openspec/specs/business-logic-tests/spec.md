## ADDED Requirements

### Requirement: Time Parsing Validations
O sistema MUST realizar o parse correto do formato de tempo `HH:MM` informado manualmente para o equivalente em segundos para a persistência.

#### Scenario: Valid HH:MM string to seconds
- **WHEN** a string no formato "01:30" for informada
- **THEN** a conversão deverá retornar 5400 segundos (1h * 3600 + 30m * 60)

#### Scenario: Invalid input for time parsing
- **WHEN** uma string inválida ou fora do formato (ex: "XX:YY" ou "25:80") for informada
- **THEN** o sistema deve lançar um erro de validação (ou retornar estado de erro) e rejeitar o input

### Requirement: Hierarchical Cascade Operations
O sistema MUST propagar alterações de estado (conclusão/inativação) de uma Tarefa pai para as suas respectivas Subtarefas em uma mesma transação.

#### Scenario: Inactivating parent task inactivates subtasks
- **WHEN** uma Tarefa que possui subtarefas ativas for marcada como inativa/concluída
- **THEN** todas as subtarefas associadas devem ser automaticamente marcadas como inativas/concluídas

#### Scenario: Avoid redundant subtask completion
- **WHEN** uma Tarefa for concluída, mas possuir uma ou mais subtarefas que já estavam concluídas previamente
- **THEN** as subtarefas já concluídas não devem sofrer alteração redundante de estado (não reconcluir)

### Requirement: Mandatory Fields Validation
O sistema MUST impedir a criação e persistência de entidades essenciais (Tasks, Subtasks) caso os campos obrigatórios não sejam preenchidos ou contenham informações inválidas.

#### Scenario: Prevent Task without title
- **WHEN** tentar criar uma Tarefa (Task) com o campo Título em branco ou nulo
- **THEN** uma falha de validação deve ocorrer e a persistência na base deve ser bloqueada

#### Scenario: Prevent Subtask without parent ID
- **WHEN** tentar criar uma Subtarefa sem especificar o ParentTaskId
- **THEN** uma falha de validação deve ocorrer e a criação ser bloqueada

### Requirement: Timer Resilience on System Boot
O sistema MUST recuperar e calcular corretamente o estado de `TimeTrackerContext` que ficaram ativos entre a inicialização e desligamento abrupto do aplicativo.

#### Scenario: Free Tracking across app restarts
- **WHEN** o aplicativo inicializar e encontrar um contexto ativo do tipo `FreeTimer`
- **THEN** o tempo transcorrido a partir do `StartTime` salvo no banco de dados até o `DateTime` atual deve ser calculado e o timer deve ser sinalizado como continuado

#### Scenario: Pomodoro expired while app closed
- **WHEN** o aplicativo inicializar e encontrar um contexto ativo do tipo `Pomodoro` em que o tempo passado seja maior que a `DuracaoEsperadaSegundos`
- **THEN** a duração esperada do Pomodoro deve ser somada ao `TempoTotalGastoSegundos` da Tarefa associada
- **AND** o contexto de tracking deve ser limpo da base
- **AND** um evento de notificação de Pomodoro atrasado deve ser disparado

#### Scenario: Pomodoro running after app restart
- **WHEN** o aplicativo inicializar e encontrar um contexto ativo do tipo `Pomodoro` e o tempo percorrido for menor que a `DuracaoEsperadaSegundos`
- **THEN** o timer do Pomodoro deve ser retomado calculando o delta com precisão

### Requirement: Inline Tag Creation Workflow
O sistema MUST suportar a criação transparente de Tags não existentes durante a criação de uma Tarefa.

#### Scenario: Automatic Tag creation
- **WHEN** o usuário criar uma Tarefa informando um nome de Tag que ainda não existe no banco de dados
- **THEN** o backend deve criar uma nova entidade Tag
- **AND** deve vinculá-la à nova Tarefa na mesma transação persistida
