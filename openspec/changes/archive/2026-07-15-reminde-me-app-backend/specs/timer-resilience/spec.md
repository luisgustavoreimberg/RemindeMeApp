## ADDED Requirements

### Requirement: Save Timer Context
Sempre que um Timer (Livre ou Pomodoro) for iniciado, o sistema DEVE gravar um registro em TimeTrackerContext no banco de dados com a referência à tarefa e a data de início (StartTime).

#### Scenario: Starting a new timer saves context
- **WHEN** um usuário inicia um novo timer
- **THEN** o TimeTrackerContext grava um novo estado ativo no banco contendo StartTime e ReferenceId.

### Requirement: Recover Timer Context on Boot
Ao ser inicializado, o backend DEVE consultar a base para TimeTrackerContext ativo e, caso encontre um, recuperar o status considerando o delta de tempo (DateTime.Now - StartTime). 

#### Scenario: Pomodoro overflow during shutdown
- **WHEN** o sistema encontra um TimeTrackerContext de Pomodoro e percebe que o delta ultrapassou a DuracaoEsperadaSegundos
- **THEN** o tempo é finalizado, consolidado na Task/Subtask, a notificação de término é preparada e o contexto é limpo.

#### Scenario: Free timer continues
- **WHEN** o sistema encontra um TimeTrackerContext de timer livre
- **THEN** ele retorna os segundos já decorridos para que a interface continue a contagem sem interrupções e não descarta o contexto.
