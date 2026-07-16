# RemindeMeApp.Backend

Este é o projeto Host principal do RemindeMeApp. Ele é o ponto de entrada da aplicação (`Program.cs`) e é responsável por iniciar tanto o contêiner do ASP.NET Core quanto a janela nativa do Photino.

## Descrição

O Backend não expõe uma API RESTful externa, mas sim atua como um WebHost embutido na aplicação desktop, servindo as páginas Razor do Frontend e provendo a Injeção de Dependência dos serviços.

## Dependências Principais
- `Photino.NET`: Para hospedar o frontend web dentro de uma janela OS-nativa sem overhead do Electron.
- `Microsoft.EntityFrameworkCore.Sqlite`: Banco de dados relacional embarcado em arquivo local.
- `CommunityToolkit.WinUI.Notifications`: Para chamadas nativas de Toast Notifications no Windows.
- Dependência de projeto: `RemindeMeApp.Frontend` (a UI) e `RemindeMeApp.Shared` (Modelos e Contratos).

## Estrutura da Camada
- `Program.cs`: Setup do WebApplication Builder, registro de serviços (DI), configuração do banco de dados (SQLite) e inicialização do loop da janela Photino (`PhotinoWindow`).
- `Data/`: Contexto do Entity Framework (`AppDbContext`) e possíveis Migrations.
- Integrações de SO: Implementações concretas de serviços como `INotificationManager` ou `ITrayIconManager`.
