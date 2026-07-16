## 1. Configuração do Projeto de Testes

- [x] 1.1 Criar o projeto de testes `RemindeMeApp.Tests` do tipo xUnit na solution.
- [x] 1.2 Adicionar a referência do projeto `RemindeMeApp.Backend` para o projeto de testes.
- [x] 1.3 Adicionar os pacotes NuGet de dependência: `Moq`, `FluentAssertions`, `Microsoft.EntityFrameworkCore.Sqlite`.

## 2. Configuração de Base (Infraestrutura)

- [x] 2.1 Criar utilitário/factory para inicializar o DbContext com SQLite in-memory (`Data Source=:memory:`) assegurando que a conexão permaneça aberta durante a execução de cada teste.
- [x] 2.2 Configurar o setup inicial para mocks (ex: `TimeProvider` ou serviço de data/hora equivalente) para permitir manipulação determinística do tempo nos testes de resiliência.

## 3. Implementação dos Testes (Fase Vermelha - TDD)

- [x] 3.1 Criar classe de testes `TimeParsingTests` para validação de parse de tempo (HH:MM para segundos e validação de inputs incorretos).
- [x] 3.2 Criar classe de testes `ValidationTests` para validação de campos obrigatórios na criação de Tasks e Subtasks.
- [x] 3.3 Criar classe de testes `TagCreationTests` para verificar a regra de criação inline de Tags na associação com nova Task.
- [x] 3.4 Criar classe de testes `CascadeOperationsTests` para verificar se a conclusão de Task propaga para as Subtasks ativas ignorando as já concluídas.
- [x] 3.5 Criar classe de testes `TimerResilienceTests` cobrindo o cenário de boot com Tracking Livre transcorrido.
- [x] 3.6 Criar classe de testes `TimerResilienceTests` cobrindo o cenário de boot com Pomodoro expirado.
- [x] 3.7 Criar classe de testes `TimerResilienceTests` cobrindo o cenário de boot com Pomodoro retomado.

## 4. Finalização

- [x] 4.1 Compilar o projeto e executar todos os testes, validando que falham conforme esperado (TDD - Fase Vermelha).
