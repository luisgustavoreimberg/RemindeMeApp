## Why

Para garantir o correto funcionamento das regras de negócio e seguir as práticas de TDD (Test-Driven Development), precisamos criar os testes unitários da aplicação antes da implementação real das regras de negócio. Os testes falharão inicialmente (Fase Vermelha) e servirão como um guia contínuo durante a futura implementação das funcionalidades, garantindo que as regras descritas nos requisitos sejam atendidas.

## What Changes

- Configuração de um banco de dados em memória (SQLite in-memory) para uso exclusivo e isolado na suíte de testes.
- Configuração de dependências adicionais voltadas para testes unitários (ex: frameworks de Mock, FluentAssertions, etc.).
- Criação de testes unitários abrangendo as seguintes lógicas de negócios e validações do backend:
  - Testes de parse de tempo (ex: conversão de formato HH:MM para segundos).
  - Testes matemáticos e de resiliência (ex: simulação de timer expirado/correndo quando o app é fechado e reaberto).
  - Testes de persistência (ex: garantir que a conclusão de uma task reflete na conclusão de suas subtasks, etc).
  - Testes de validações e demais regras de negócio (ex: preenchimento de campos obrigatórios, bloqueio de re-conclusão de subtasks, etc).

## Capabilities

### New Capabilities
- `business-logic-tests`: Define a infraestrutura de testes unitários (SQLite in-memory, mocks) e o conjunto de cenários de teste TDD cobrindo as regras de negócio da aplicação (parsing, resiliência, hierarquia de tarefas).

### Modified Capabilities

## Impact

- **Testes:** Adição de uma suite completa de testes unitários na solução, inicialmente configurada para falhar.
- **Dependências:** Adição de pacotes NuGet para testes (como `xUnit`, `Moq`, `Microsoft.EntityFrameworkCore.InMemory`, etc.) apenas no escopo de testes.
- As implementações das regras de negócio em si, interfaces do usuário, ou alterações em entidades ou banco de dados SQLite real estão fora do escopo desta mudança.
