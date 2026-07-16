## ADDED Requirements

### Requirement: Task and Subtask CRUD
O sistema DEVE permitir criar, ler, atualizar e deletar Tarefas (Tasks) e Subtarefas (Subtasks). Ao criar uma Tarefa, ela deve possuir título obrigatório. Ao concluir ou deletar uma Task, todas as suas Subtasks DEVEM ser marcadas como inativas (cascade).

#### Scenario: Cascading subtask completion
- **WHEN** uma Task principal é marcada como inativa (concluída)
- **THEN** todas as suas Subtasks atreladas recebem o status IsAtivo = false.

### Requirement: Inline Tag Creation
O sistema DEVE permitir a criação de Tags durante a criação/edição de uma Task. Se o usuário fornecer um nome de Tag inexistente, a Tag DEVE ser criada no mesmo escopo transacional e associada à Task.

#### Scenario: Creating a task with a new tag
- **WHEN** o usuário cria uma Task informando o nome de uma Tag que não existe na base
- **THEN** o sistema salva a nova Tag com uma cor gerada e associa essa TagId à Task criada.

### Requirement: Tag deletion effects
O sistema DEVE desassociar (remover vínculo) Tags de Tasks quando uma Tag for excluída, ou oferecer cascata de exclusão. Por padrão, a exclusão de uma Tag deve setar o TagId como nulo para todas as Tasks associadas.

#### Scenario: Tag deleted updates Tasks
- **WHEN** uma Tag é removida do sistema
- **THEN** todas as Tasks que usavam esta Tag são atualizadas para TagId = null.
