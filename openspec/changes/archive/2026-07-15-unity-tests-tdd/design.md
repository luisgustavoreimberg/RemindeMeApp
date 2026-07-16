## Context

O RemindeMeApp possui lógicas de negócio complexas, como parse de tempo (HH:MM para segundos), gestão do ciclo de vida de um timer (Pomodoro e Timer Livre), controle de resiliência e fallback quando a aplicação é interrompida, além de regras de hierarquia e dependência de estados entre tarefas e subtarefas.
Para assegurar a qualidade e previsibilidade, e adotar as práticas de Test-Driven Development (TDD), precisamos criar uma base sólida de testes unitários que defina os comportamentos esperados do sistema. Estes testes falharão no primeiro momento, servindo como a especificação executável (Fase Vermelha) para guiar o desenvolvimento do backend.

## Goals / Non-Goals

**Goals:**
- Configurar o framework de testes unitários (`xUnit`) para a solução.
- Estabelecer a base para mocks de dependências (`Moq`) e asserções legíveis (`FluentAssertions`).
- Configurar banco de dados em memória utilizando SQLite InMemory (`Data Source=:memory:`) para testes isolados sem persistência em disco.
- Desenvolver os cenários de testes que documentem todas as regras de negócio chave estabelecidas na especificação.

**Non-Goals:**
- Implementação ou correção das lógicas de negócio e funcionalidades em si no backend.
- Criação de testes de integração ou End-to-End (E2E) com a interface do usuário.
- Alterações no modelo de dados ou Entity Framework Migrations fora do escopo de testes.

## Decisions

- **Framework de Testes**: `xUnit`.
  - *Rationale*: xUnit é um framework moderno, alinhado com as práticas atuais do ecossistema .NET, com paralelismo nativo de testes.
- **Banco de Dados em Memória**: SQLite em modo InMemory.
  - *Rationale*: A especificação pede "SQLite in-memory". Diferente do provedor `EF InMemory` da Microsoft (que não suporta restrições relacionais reais), o SQLite InMemory atua de forma similar ao banco de produção, validando as constraints do banco e garantindo testes mais realistas sem o overhead de I/O de disco.
- **Asserções e Mocks**: `FluentAssertions` para asserções declarativas e fáceis de ler, e `Moq` para mockar dependências de serviços, repositórios e provedores de tempo (como relógio do sistema para testes de timers).

## Risks / Trade-offs

- **[Risco] Testes utilizando SQLite in-memory podem ser frágeis se conexões forem fechadas prematuramente.** → **[Mitigação]** Garantir que a conexão do SQLite no setup dos testes permaneça aberta (utilizando um `SqliteConnection` estático ou por escopo) enquanto o contexto for necessário, descartando-a no `Dispose` do teste.
- **[Risco] O provedor de tempo do .NET (DateTime.Now) torna os testes de timers não determinísticos.** → **[Mitigação]** Utilizar injeção de dependência via a abstração `TimeProvider` introduzida no .NET 8 (ou uma abstração customizada) e mockar o retorno nos testes de resiliência (ex: simular passagem de tempo entre boot e shutdown).
