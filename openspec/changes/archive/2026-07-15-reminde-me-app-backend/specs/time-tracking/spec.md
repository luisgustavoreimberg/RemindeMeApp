## ADDED Requirements

### Requirement: Free Time Tracking
O sistema DEVE permitir inicializar um timer livre associado a uma Task ou Subtask, computando o tempo e adicionando-o ao TempoTotalGastoSegundos quando for parado ou pausado.

#### Scenario: Stopping a free timer
- **WHEN** o usuário para um timer livre de uma Task
- **THEN** a diferença entre o start time e o momento da parada em segundos é somada ao TempoTotalGastoSegundos da respectiva Task.

### Requirement: Pomodoro Time Tracking
O sistema DEVE permitir iniciar um timer Pomodoro (Foco/Descanso) que contabiliza um tempo predefinido (DuracaoEsperadaSegundos) e, ao fim do ciclo de foco, adiciona o tempo computado à Task/Subtask atrelada.

#### Scenario: Completing a Pomodoro cycle
- **WHEN** o tempo de Foco de um Pomodoro chega ao fim
- **THEN** a duração decorrida é somada à Task/Subtask e um novo status/notificação de Descanso é acionado (no backend via lógica ou evento).

### Requirement: Manual Time Insertion
O sistema DEVE fazer o parse de inputs de horas/minutos (HH:MM) para inserção manual e adicionar ao TempoTotalGastoSegundos da Task/Subtask.

#### Scenario: Inserting time manually
- **WHEN** um serviço recebe um input manual de "01:30"
- **THEN** ele converte para 5400 segundos (90 minutos) e soma ao TempoTotalGastoSegundos da tarefa selecionada.
