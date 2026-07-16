## Context

A aplicação RemindeMeApp já possui as interfaces e testes desenvolvidos (`RemindeMeApp.Tests` e `RemindeMeApp.Backend` parcialmente estruturado). O próximo passo é implementar as classes concretas para serviços e acesso a dados e fazer a aplicação passar na suite de testes, conectando o Entity Framework Core com um banco local SQLite.

## Goals / Non-Goals

**Goals:**
- Configurar e instanciar `TimeTrackerContext`, `Task`, `Subtask` e `Tag` no Entity Framework Core.
- Implementar `TaskService` e `TimeTrackingService` aplicando TDD, passando em todos os testes previamente escritos.
- Configurar a injeção de dependência na inicialização do aplicativo (`Program.cs` ou classe similar).

**Non-Goals:**
- Qualquer alteração em `Razor Pages` ou interface do usuário (UI).
- Autenticação ou Cloud Sync (requisito explicitly offline).
- Alteração da lógica dos testes já implementados (devemos adaptar o código da aplicação, não os testes, exceto por mocks se estritamente necessário para DB).

## Decisions

- **ORM**: Entity Framework Core com SQLite. Motivo: É o padrão .NET, leve, suporta Code-First e atende à necessidade de armazenamento local offline do app.
- **Injeção de Dependências (DI)**: Injeção do `DbContext` configurado como Scoped, injetado diretamente nos Services. Motivo: Mantém o controle de transações (ex: SaveChangesAsync) nos Services. Restringe o acesso aos dados para as classes designadas.
- **TDD (Test-Driven Development)**: Utilizar a base de testes existente em `RemindeMeApp.Tests`. O design e a implementação das classes `TimeTrackingService` e `TaskService` serão guiados pelas asserções e mocks (se houver) da suite de testes.
- **Resiliência de Timer**: O backend consultará a tabela `TimeTrackerContext` no boot para recuperar estado de timers, utilizando o `StartTime` para calcular o delta de tempo perdido em caso de fechamento do app.

## Risks / Trade-offs

- **Risk**: SQLite lock devido a acessos concorrentes.
  - **Mitigation**: O uso será estritamente local (single-user), e as operações de DB serão assíncronas (`async/await`) usando o contexto Scoped do EF Core.

- **Risk**: O acesso indevido ao DbContext pode espalhar regras de negócio em componentes não autorizados.
  - **Mitigation**: A interface dos Services fará a abstração. O DbContext não será acessado diretamente na camada de UI, sendo manipulado unicamente dentro dos repositórios ou dos próprios services.
