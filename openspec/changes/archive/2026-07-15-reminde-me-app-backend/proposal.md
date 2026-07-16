## Why

O RemindeMeApp precisa de um backend funcional desenvolvido sob uma abordagem TDD, aplicando os princípios SOLID, Clean Architecture (Models, Services, Repositories, etc) e garantindo uma injeção de dependência adequada do EF Core. Já possuímos testes e interfaces, então este passo concluirá a lógica central do aplicativo, permitindo persistência e funcionamento de timers, gestão de tarefas e recuperação de contexto.

## What Changes

- Instalação de pacotes faltantes como Entity Framework Core (SQLite e Design).
- Desenvolvimento das classes concretas das entidades e implementação de lógicas internas se necessário.
- Desenvolvimento dos Services (Use Cases) a partir de contratos e injeção do `DbContext`.
- Implementação de lógica de timers (Tracking Livre e Pomodoro), CRUD de tarefas, e sub-tarefas e tags (inline creation).
- Implementação de resiliência de timers (continuidade no restart do app).
- Acesso restrito ao banco de dados: apenas pelas classes designadas.
- Passar na suíte de testes existente.

## Capabilities

### New Capabilities
- `task-management`: CRUD completo de Task, Subtask e Tag, incluindo regras como criação e exclusão inline e exclusão/conclusão em cascata.
- `time-tracking`: Tracking livre e Pomodoro, além de inserção de tempo manual.
- `timer-resilience`: Salva e recupera o contexto de timers ativos após interrupções de execução da aplicação.

### Modified Capabilities

## Impact

- Criação de banco local SQLite com EF Core Migrations.
- Adição dos serviços de banco e repositórios no injetor de dependências na base do aplicativo (`Program.cs` ou classe equivalente).
- Resolução de testes pendentes na suíte `RemindeMeApp.Tests`.
