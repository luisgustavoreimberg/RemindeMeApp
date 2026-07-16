# Especificação Técnica: RemindeMeApp

## 1. Visão Geral do Projeto
O **RemindeMeApp** é um aplicativo desktop local para Windows voltado para a criação, gestão e notificação de lembretes e tarefas. O foco principal é facilitar a organização diária do usuário e gerenciar o tempo de foco por meio de timers livres e técnica Pomodoro.

**Restrições Principais:**
- Aplicação 100% local (offline).
- Sem autenticação, sem telas de login e sem gestão de múltiplos usuários.
- Sem integrações com APIs externas ou bancos de dados em nuvem.

## 2. Stack Tecnológica
- **Linguagem:** C# (.NET LTS)
- **Host Desktop:** Photino.NET (encapsulamento de interface web nativa)
- **Frontend Web:** ASP.NET Core Razor Pages
- **Banco de Dados:** SQLite (armazenamento local)
- **ORM:** Entity Framework Core (Abordagem Code-First com Migrations)
- **UI/UX:** HTML/CSS/JS (Framework CSS à escolha do frontend, estritamente em Dark Mode)

## 3. Arquitetura e Padrões de Projeto
O código gerado deve adotar as seguintes premissas arquiteturais para garantir manutenibilidade e escalabilidade:
- **Princípios SOLID:** Aplicação rigorosa em todas as camadas.
- **Arquitetura em Camadas:** 
  - `Models/Entities`: Classes anêmicas ou com regras de negócio intrínsecas da entidade.
  - `Services/Use Cases`: Contém toda a lógica de negócio e regras de persistência. **Nenhuma regra de negócio deve residir nos `PageModels`**.
  - `UI`: Razor Pages e PageModels atuando apenas como controladores de apresentação.
- **Injeção de Dependência (DI):** Uso nativo do contêiner do .NET para injetar repositórios e serviços.
- **Assincronismo:** Uso de `async/await` para todo o acesso ao banco de dados (EF Core) e I/O, evitando o bloqueio da thread da UI.
- **Segurança:** Validação rigorosa de inputs para prevenir injeção de comandos.

## 4. Integração com o Sistema Operacional
- **Photino.NET Wrapper:** A aplicação roda Kestrel em background, mas exibe a interface via WebView2 providenciado pelo Photino.
- **System Tray (Bandeja do Sistema):** Ao clicar em fechar (X) ou minimizar a janela principal, o processo do aplicativo **não deve ser encerrado**. A aplicação deve ser ocultada para a bandeja do sistema. O encerramento definitivo só ocorre via interação com o ícone no System Tray (Botão Direito > Sair > Confirmação em popup).
- **Notificações Nativas:** Utilização de Toast Notifications nativas do Windows para:
  - Horário exato de lembretes agendados.
  - Conclusão de ciclo de Foco (Pomodoro).
  - Conclusão de ciclo de Descanso.
- **Modo "Não Perturbe" (Do Not Disturb):** Toggle global na UI. Se ativo, as notificações nativas são suprimidas silenciosamente no backend.

## 5. Modelagem de Dados
Tabelas necessárias no SQLite via EF Core:

- **`Tag`**
  - `Id` (PK)
  - `Nome` (string)
  - `CorHexadecimal` (string)
  - *Nota:* Assumir o estado "Sem Tag" logicamente caso uma tarefa não possua vínculo.
- **`Task`** (Tarefa Pai / Lembrete)
  - `Id` (PK)
  - `Titulo` (string, obrigatório)
  - `Descricao` (string, opcional)
  - `DataHoraLembrete` (DateTime?, opcional)
  - `IsAtivo` (bool, default true)
  - `TagId` (FK?, opcional)
  - `TempoTotalGastoSegundos` (int, default 0)
- **`Subtask`**
  - `Id` (PK)
  - `ParentTaskId` (FK, obrigatória)
  - `Titulo` (string, obrigatório)
  - `IsAtivo` (bool, default true)
  - `TempoTotalGastoSegundos` (int, default 0)
  - *Nota:* Apenas 1 nível de profundidade. Subtarefas não possuem subtarefas.
- **`TimeTrackerContext`** (Controle de Resiliência)
  - `Id` (PK)
  - `ReferenceId` (int, aponta para Task ou Subtask)
  - `IsSubtask` (bool)
  - `StartTime` (DateTime)
  - `Tipo` (Enum: `FreeTimer` ou `Pomodoro`)
  - `DuracaoEsperadaSegundos` (int?, apenas para Pomodoro)

## 6. Regras de Negócio e Funcionalidades

### 6.1. Gestão de Tarefas e Tags
- CRUD completo.
- Se uma `Task` for marcada como inativa/concluída, as `Subtasks` filhas também devem ser concluídas.
- **Criação Inline de Tags:** No formulário de criação da tarefa, se o usuário digitar o nome de uma Tag que não existe, o backend deve criar a Tag e associá-la à nova Tarefa na mesma transação com uma cor genérica/aleatória.
- **Exclusão de Tags:** Se uma Tag for excluída, deve aparecer uma opção para marcar as `Tasks` com essa tag como "Sem Tag" ou excluir as tarefas que possuem essa tag.

### 6.2. Tracking Livre
- Inicialização de timer referenciado a uma Task ou Subtask.
- Ações de Play/Pause ou Stop convertem o tempo decorrido em segundos e somam ao `TempoTotalGastoSegundos`.
- **Inserção Manual:** O usuário pode imputar tempo manualmente. O front-end envia no formato `HH:MM`. O backend deve fazer o parse, converter em segundos e adicionar à entidade.

### 6.3. Modo Pomodoro
- Tempos configuráveis para Foco e Descanso.
- Pode rodar solto ou vinculado a uma entidade (Task/Subtask).
- Se vinculado, ao final do Foco, o tempo é somado ao `TempoTotalGastoSegundos`.
- Transição automática (disparando notificação) de Foco para Descanso.

### 6.4. Resiliência e Fallback de Timers
- Ao iniciar qualquer timer, o `TimeTrackerContext` é salvo no banco com o `StartTime`.
- **Comportamento no Boot:** Ao iniciar a aplicação, o sistema consulta a tabela `TimeTrackerContext`. Se houver registro ativo:
  - Calcula a diferença entre `DateTime.Now` e `StartTime`.
  - Se for Pomodoro e o tempo estourou enquanto o app estava fechado: O backend soma o tempo à tarefa, dispara notificação atrasada e limpa o contexto.
  - Se o tempo ainda estiver correndo (ou for tracking livre): A UI deve retomar a contagem visual a partir do delta calculado.

## 7. Interface e Frontend (Referência)
- O projeto possuirá um Menu em Topbar para navegação entre "Dashboard", "Foco", "Tags" e "Configurações".
- Design restrito ao **Dark Mode**, com cores de destaque focadas em produtividade.
- O código gerado pelo agente frontend (HTML/CSS/JS) deve ser adaptado pelo agente fullstack para a sintaxe Razor (`.cshtml`), implementando Tag Helpers para formulários e roteamento (`asp-page`, `asp-route`).
- Atualizações de timer e status na tela principal devem ocorrer de forma assíncrona usando `fetch` para os *Page Handlers* do Razor (ex: `?handler=StartTimer`), sem causar refresh completo da página.
- Considerar como base de exemplo e inspiração do design os arquivos presentes em /docs/stitch_remindemeapp_modern_desktop_interface(estrutura gerada no stitch/gemini com a estrutura de páginas do app, mas sem o código backend em si)

# 8. Documentação
- Todas as ações devem ser documentadas em 2 níveis:
    - Markdown no próprio repo
      - documento de contexto/memória para agentes de IA sobre a solução(incluindo visão geral, decisão de arquitetura, padrões adotados, etc)
      - README da solução com: descrição do projeto, stack utilizada, estrutura/arquitetura, como rodar em desenvolvimento, como fazer o build, como executar os testes, deploy/geração de pacote, padrão de controle de versão
      - README de cada projeto (backend, frontend) com descrição, informações, dependências e estruturas específicas de cada camada
      - README de integração entre as camadas(como frontend e backend se comunicam, como usar os endpoints da API, etc)
    - Docblocks em C#
      - documentação específica de métodos, classes, serviços, repositórios