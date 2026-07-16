## Context

O repositório RemindeMeApp existe apenas com documentação (especificação técnica em `docs/application-spec.md`) e configuração do OpenSpec. Não há código-fonte, solução .NET, projetos, nem qualquer infraestrutura de build. A stack definida na especificação é: C# (.NET LTS), Photino.NET como host desktop, ASP.NET Core Razor Pages para o frontend web, SQLite via EF Core (Code-First), e interface em Dark Mode com HTML/CSS/JS.

Esta change cria a fundação completa para que todas as funcionalidades futuras (CRUD, timers, Pomodoro, notificações) possam ser implementadas de forma incremental.

## Goals / Non-Goals

**Goals:**
- Criar a solução `RemindeMeApp.sln` com estrutura de diretórios organizada em `src/`, incluindo `.gitignore` e `global.json`
- Configurar o projeto compartilhado (`RemindeMeApp.Shared`) como Class Library contendo entidades, enums e interfaces de serviço
- Configurar o projeto backend (`RemindeMeApp.Backend`) com Photino.NET como host, servindo Razor Pages via Kestrel embarcado em background thread
- Configurar o projeto frontend (`RemindeMeApp.Frontend`) como Razor Class Library com boilerplate mínimo (`_ViewImports.cshtml`, `_ViewStart.cshtml`)
- Definir todas as entidades do domínio conforme a especificação: `TaskItem`, `Subtask`, `Tag`, `TimerSession`, e enum `TimerType`
- Configurar o `AppDbContext` com EF Core + SQLite, incluindo Fluent API para mapeamentos, connection string via `appsettings.json`
- Criar interfaces de serviço (contratos) para todas as camadas de negócio, com assinaturas e tipos de parâmetro definidos
- Registrar serviços e DbContext via DI nativa do .NET no `Program.cs`
- Instalar todos os pacotes NuGet necessários
- Adicionar XML doc comments (`///`) em todas as entidades e interfaces

**Non-Goals:**
- Implementação de regras de negócio (CRUD, validações, lógica de timer)
- Criação de interface de usuário (páginas Razor, layouts visuais, CSS)
- Geração ou execução de migrations do EF Core
- Integração com System Tray ou notificações do Windows
- Testes unitários ou de integração

## Decisions

### 1. Estrutura da Solução: Três Projetos em `src/`

**Decisão**: Criar três projetos — `RemindeMeApp.Shared` (Class Library), `RemindeMeApp.Backend` (Console App / Host) e `RemindeMeApp.Frontend` (Razor Class Library).

**Alternativas consideradas**:
- *Dois projetos (Backend/Frontend)*: Cria dependência circular — o Backend referencia o Frontend (para servir Razor Pages), mas o Frontend precisa das interfaces e entidades que estariam no Backend para seus PageModels.
- *Projeto único*: Mais simples, mas acopla host desktop com lógica de apresentação e domínio, dificultando testes e manutenção.

**Rationale**: Três projetos resolvem a dependência circular. O `Shared` contém entidades e interfaces que são consumidas por ambos os outros projetos. O grafo de dependências fica unidirecional: Backend → Shared, Backend → Frontend, Frontend → Shared. Nenhum projeto referencia o Backend, eliminando ciclos.

```
RemindeMeApp.Shared (Class Library)
  ├── Models/ (TaskItem, Subtask, Tag, TimerSession, TimerType)
  └── Services/ (ITaskService, ISubtaskService, ITagService, ...)

RemindeMeApp.Backend (Console App)
  ├── Data/ (AppDbContext)
  ├── Services/ (implementações futuras)
  └── Program.cs (host Photino + Kestrel)
  → refs: Shared, Frontend

RemindeMeApp.Frontend (Razor Class Library)
  ├── Pages/ (Razor Pages futuras)
  └── wwwroot/ (assets estáticos futuros)
  → refs: Shared
```

### 2. Nomeação da Entidade: `TaskItem` (não `Task`)

**Decisão**: Nomear a entidade como `TaskItem` para evitar conflito com `System.Threading.Tasks.Task`.

**Rationale**: Usar `Task` como nome de classe em C# causa ambiguidades constantes em imports e IntelliSense. `TaskItem` é claro, explícito, e não conflita com nenhum tipo do framework.

### 3. Configuração do DbContext: Fluent API sobre Data Annotations

**Decisão**: Usar Fluent API no `OnModelCreating` para todos os mapeamentos.

**Alternativas consideradas**:
- *Data Annotations*: Mais simples para casos triviais, mas espalha configuração nas entidades, violando separação de responsabilidades.

**Rationale**: Fluent API centraliza toda a configuração de banco no DbContext, mantém as entidades POCO limpas, e suporta cenários mais complexos (índices compostos, value conversions para enums).

### 4. Enum `TimerType` como Value Conversion para string no SQLite

**Decisão**: Armazenar o enum `TimerType` como string no SQLite usando `HasConversion<string>()`.

**Rationale**: SQLite não tem suporte nativo a enums. Armazenar como string torna os dados legíveis no banco e facilita debugging, com custo mínimo de performance para uma aplicação local single-user.

### 5. Frontend como Razor Class Library (RCL)

**Decisão**: O projeto frontend será uma Razor Class Library, não um Web App standalone.

**Rationale**: Uma RCL permite que o backend (Photino host) referencie e sirva as páginas Razor diretamente via Kestrel embarcado, sem necessidade de executar dois processos. Isso se alinha com a arquitetura Photino, onde o backend é o processo principal e o WebView renderiza o conteúdo servido localmente.

### 6. Renomeação: `TimeTrackerContext` → `TimerSession`

**Decisão**: Renomear a entidade `TimeTrackerContext` (da especificação original) para `TimerSession`.

**Alternativas consideradas**:
- *Manter `TimeTrackerContext`*: Causa confusão semântica com `DbContext` do EF Core. O DbSet se chamaria `TimeTrackerContexts`, que soa como "múltiplos DbContexts".
- *`TrackingRecord`*: Válido, mas `TimerSession` comunica melhor que é uma sessão ativa de tracking com início e fim.

**Rationale**: `TimerSession` é claro, não conflita com conceitos do EF Core, e o DbSet `TimerSessions` é legível e intuitivo.

### 7. Bootstrapping Photino.NET + Kestrel: Thread Model

**Decisão**: Kestrel será iniciado em uma background thread via `Task.Run`, e a inicialização do Photino.NET ocorrerá na thread principal (STA).

**Alternativas consideradas**:
- *Kestrel na thread principal com `app.RunAsync()`*: Funcional, mas requer cuidado com o lifecycle — `RunAsync` retorna um `Task` que deve ser awaited para shutdown graceful.
- *Photino em background thread*: Não funciona — Photino requer thread STA (Single Thread Apartment) no Windows para interagir com WebView2.

**Rationale**: O padrão recomendado pelo Photino.NET é:
1. Configurar e iniciar Kestrel com `app.RunAsync()` ou `app.StartAsync()` (não-bloqueante)
2. Criar e abrir a `PhotinoWindow` na thread principal apontando para `http://localhost:{porta}`
3. Chamar `window.WaitForClose()` (bloqueante na main thread)
4. Ao fechar a janela, chamar `app.StopAsync()` para shutdown do Kestrel

Isso evita deadlocks e respeita o requisito de STA do WebView2.

### 8. Connection String Externalizada

**Decisão**: A connection string do SQLite será definida em `appsettings.json`, não hardcoded no código.

**Rationale**: Permite configuração diferente por ambiente (desenvolvimento vs. produção) e segue as boas práticas do ASP.NET Core. O path do banco pode referenciar `Environment.GetFolderPath(SpecialFolder.LocalApplicationData)` para evitar criar o `.db` no working directory (que pode variar).

## Risks / Trade-offs

- **[Três projetos para app single-user]** → Pode parecer over-engineering, mas é o mínimo necessário para evitar dependência circular entre Backend e Frontend. O `Shared` é leve (apenas POCOs e interfaces). **Mitigação**: O custo de manutenção é baixo — são apenas 2 referências extras.

- **[SQLite sem concorrência]** → SQLite tem limitações com escritas concorrentes. **Mitigação**: A aplicação é single-user e single-process, eliminando cenários de concorrência.

- **[Photino.NET maturity]** → Photino.NET é menos maduro que Electron ou MAUI. **Mitigação**: Ele é leve, funcional para o caso de uso, e a camada de UI (Razor Pages) é desacoplada o suficiente para migrar de host se necessário. A versão do pacote NuGet deve ser explicitamente especificada no `.csproj`.

- **[Sem migrations nesta change]** → O DbContext será configurado, mas o banco não será criado até que migrations sejam executadas em uma change futura. **Mitigação**: Isso é intencional — esta change foca exclusivamente em estrutura.

- **[Assinaturas de interfaces podem mudar]** → As interfaces definidas nesta change incluem parâmetros provisórios (entidades como entrada). Quando DTOs forem introduzidos em changes futuras, as assinaturas provavelmente serão ajustadas. **Mitigação**: Aceitável para scaffold; o objetivo é estabelecer os contratos conceituais, não as assinaturas finais.
