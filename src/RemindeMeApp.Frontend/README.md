# RemindeMeApp.Frontend

Este projeto é uma **Razor Class Library** (configurada via `AddRazorSupportForMvc=true`) que empacota toda a UI web da aplicação. O Backend (Host) a referencia e serve suas páginas.

## Descrição

O Frontend é responsável puramente pela Apresentação (UI e interações). A lógica de negócio é delegada para os serviços injetados via Injeção de Dependência da camada `Shared`. Usamos **ASP.NET Core Razor Pages** (não Blazor), combinando renderização do servidor com hidratação via JavaScript mínimo para ações assíncronas.

## Dependências Principais
- SDK: `Microsoft.NET.Sdk.Razor`
- Dependência de projeto: `RemindeMeApp.Shared` (para acessar os Modelos e os Contratos/Interfaces dos Serviços que serão consumidos pelas Páginas Razor).

## Estrutura da Camada
- `Pages/`: Contém os arquivos `.cshtml` e seus respectivos PageModels `.cshtml.cs`.
  - `Shared/_Layout.cshtml`: O shell da aplicação (Sidebar, Topbar) extraído do protótipo Stitch.
  - `Index.cshtml`: Dashboard principal de tarefas.
  - `Focus.cshtml`: O Canvas do Modo Foco e Pomodoro Timer.
- `wwwroot/`: Arquivos estáticos.
  - `css/site.css`: Configuração raiz, incorporando o sistema de cores e variáveis Dark Mode copiados dos protótipos visuais.
  - `js/timer.js`: JavaScript dedicado para efetuar interações assíncronas (via `fetch`) com os Page Handlers sem gerar recarregamento da página, passando o token Anti-Forgery do ASP.NET.
