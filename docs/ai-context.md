# AI Context / Memory

Este documento serve como memória técnica e de contexto para agentes de IA que venham a trabalhar no projeto **RemindeMeApp**.

## Visão Geral
O **RemindeMeApp** é um aplicativo de produtividade desktop moderno, construído para Windows, que combina rastreamento de tempo (Time Tracker / Pomodoro) com gerenciamento de tarefas. O foco principal é manter uma interface minimalista e um modo "Não Perturbe" (Do Not Disturb) para evitar distrações.

## Arquitetura e Decisões Técnicas
- **Plataforma Desktop:** Utilizamos [Photino.NET](https://github.com/tryphotino/photino.NET) como host. Ele cria uma janela nativa leve e renderiza o conteúdo web utilizando o controle de navegador nativo do sistema operacional (WebView2 no Windows).
- **Backend:** ASP.NET Core MVC/API integrado no mesmo processo do host Photino. Responsável por expor serviços e conectar-se ao banco de dados.
- **Frontend:** Desenvolvido com **ASP.NET Core Razor Pages** (`Microsoft.NET.Sdk.Razor`). Renderizado diretamente pelo servidor embutido.
- **Estilização e UI:** O frontend utiliza TailwindCSS, com um design system rígido mantido através do arquivo `site.css` (Dark Mode puro). Há forte aderência ao protótipo visual fornecido (`docs/stitch_remindemeapp_modern_desktop_interface/`).
- **Banco de Dados:** SQLite com Entity Framework Core (Code-First). O banco de dados é um arquivo local embutido, ideal para uma aplicação desktop single-user.
- **Comunicação:** In-Process. O Frontend consome os serviços da camada Shared (`ITaskService`, `ITagService`, etc.) diretamente via Injeção de Dependência, sem a necessidade de requisições HTTP RESTful completas, usando Page Handlers para interações assíncronas (`fetch`).

## Padrões Adotados
- **Clean Architecture Simplificada:**
  - `RemindeMeApp.Backend`: Host do Photino, bootstrap do ASP.NET Core e integrações de SO (bandeja, notificações).
  - `RemindeMeApp.Frontend`: Views em Razor Pages (`.cshtml`), UI, Handlers.
  - `RemindeMeApp.Shared`: Modelos de Domínio, Interfaces de Serviço, e lógicas de negócios reaproveitáveis.
  - `RemindeMeApp.Tests`: Testes unitários utilizando xUnit, Moq e in-memory SQLite.
- **Controle de Versão:** Conventional Commits + GitHub Flow.
- **Test-Driven Development (TDD):** A camada de Serviços deve possuir cobertura de testes.

## Dicas para IA
- Ao alterar o Frontend, certifique-se de usar Razor Pages e os Handlers assíncronos em `wwwroot/js/timer.js`. Evite transformá-lo em Blazor, a não ser que explicitamente requisitado.
- Para adicionar pacotes NuGet, sempre certifique-se da compatibilidade com o `.NET 9.0`.
- As notificações do sistema operacional são abstraídas em `INotificationManager`. No Windows, utilizam `CommunityToolkit.WinUI.Notifications`.
