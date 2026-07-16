# Integração Backend e Frontend

Este documento descreve como a camada de visualização (Frontend) interage com a camada de dados e lógica (Backend) no ecossistema do **RemindeMeApp**.

## Modelo de Comunicação In-Process

Diferente de aplicações web tradicionais baseadas numa arquitetura Client-Server distribuída sobre a rede (com APIs REST/GraphQL consumidas por um framework SPA como React ou Angular), o RemindeMeApp é uma **aplicação desktop híbrida**. 

Isso significa que **Frontend e Backend rodam no mesmo processo do Sistema Operacional**, gerenciado pela Photino. O servidor ASP.NET Core roda internamente na porta local (`localhost:porta-random`) e a WebView nativa aponta para ele.

### Benefícios:
- Latência de rede nula (Zero-latency)
- Segurança aprimorada (serviços não expostos em portas públicas por padrão)
- Simplificação arquitetural (sem necessidade de DTOs HTTP e serialização pesada para chamadas internas, já que compartilhamos instâncias via DI).

## Como os Dados Trafegam

1. **Injeção de Dependência (DI):**
   Os *PageModels* no projeto `RemindeMeApp.Frontend` (ex: `IndexModel`) declaram as interfaces no seu construtor, como `ITaskService`. O contêiner IoC gerado em `RemindeMeApp.Backend/Program.cs` injeta as instâncias concretas ao instanciar as páginas.

2. **Renderização Inicial (Server-Side):**
   Durante o método `OnGetAsync()` da Razor Page, o Frontend chama diretamente os métodos do serviço C#, por exemplo, `await _taskService.GetAllAsync()`. Os dados retornam no formato da entidade (do projeto `Shared`) e a página HTML é renderizada no servidor e enviada para a WebView.

3. **Ações Assíncronas (Handlers):**
   Para atualizar status sem recarregar a tela (como completar uma tarefa ou iniciar um timer), o Frontend utiliza *Page Handlers*.
   - **No CSHTML.cs:** Definimos métodos como `OnPostStartTimerAsync()`.
   - **No JS (`timer.js`):** Disparamos uma requisição HTTP via `fetch` para `?handler=StartTimer`, assegurando o envio do `RequestVerificationToken` gerado pelo anti-forgery do ASP.NET.
   O servidor embutido processa o request localmente, atualiza o banco (via Entity Framework) e retorna o JSON.

## Guia para Criação de Novas Features

Para adicionar um novo fluxo (exemplo: Criar Tag):
1. **Shared:** Defina `Task<Tag> CreateTagAsync(string name)` em `ITagService`.
2. **Backend:** Implemente o método em `TagService.cs` persistindo através do DbContext.
3. **Frontend (View):** Adicione um form ou botão no `.cshtml` que dispare um método de formulário ou uma chamada via `fetch`.
4. **Frontend (Model):** No `Index.cshtml.cs`, injete `ITagService` e implemente `OnPostCreateTagAsync()`, consumindo o serviço localmente.
