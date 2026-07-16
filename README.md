# RemindeMeApp

RemindeMeApp é uma aplicação desktop moderna para Windows, projetada para ajudar no gerenciamento de tarefas, controle de tempo (Pomodoro) e produtividade sem distrações (Modo Foco e Não Perturbe).

## Stack Tecnológica

- **Linguagem:** C# 13, .NET 9.0
- **Desktop Host:** Photino.NET (cross-platform nativo via webview)
- **Frontend Web:** ASP.NET Core Razor Pages (HTML, CSS, JS puros)
- **Estilização:** TailwindCSS (via CDN) com design system centralizado em Dark Mode
- **Banco de Dados:** SQLite embutido
- **ORM:** Entity Framework Core 9.0
- **Testes:** xUnit + Moq
- **Notificações:** Integração nativa no SO usando Toast Notifications (`CommunityToolkit.WinUI.Notifications`).

## Arquitetura do Projeto

A solução está estruturada nas seguintes camadas:
- `src/RemindeMeApp.Backend/`: Projeto Host que inicializa a janela do Photino, configura os serviços do ASP.NET Core e gerencia as abstrações do Sistema Operacional.
- `src/RemindeMeApp.Frontend/`: Biblioteca Razor contendo toda a Interface de Usuário (Razor Pages `.cshtml`, CSS, Scripts e arquivos estáticos em `wwwroot`).
- `src/RemindeMeApp.Shared/`: Biblioteca de classes contendo Modelos (Entities), Interfaces de Serviços e abstrações que trafegam entre Frontend e Backend.
- `src/RemindeMeApp.Tests/`: Projeto de testes unitários com foco na camada de serviços (TDD).

Para mais detalhes sobre arquitetura e contexto para IA, veja [AI Context](docs/ai-context.md) e [Integration README](docs/INTEGRATION_README.md).

## Como Executar (Desenvolvimento)

Certifique-se de ter o [.NET 9.0 SDK](https://dotnet.microsoft.com/download) instalado.

1. Navegue até a raiz do repositório (onde o arquivo `.sln` está localizado).

2. Restaure as dependências e compile a solução completa:
   ```bash
   dotnet build
   ```

3. Execute a aplicação (Backend + Frontend embutido):
   ```bash
   dotnet run --project src/RemindeMeApp.Backend/RemindeMeApp.Backend.csproj
   ```
   *Nota: O projeto Backend é o Host do Photino. Ao executá-lo, a janela desktop nativa será aberta carregando toda a interface construída no projeto Frontend.*

## Testes

Os testes são focados nos serviços de negócio na camada Shared. Para executar os testes:
```bash
dotnet test src/RemindeMeApp.Tests/RemindeMeApp.Tests.csproj
```

## Como fazer o Build (Deploy / Pacote)

Para gerar um executável `self-contained` para Windows, que pode ser distribuído sem necessidade de instalação do SDK na máquina destino:

```bash
dotnet publish src/RemindeMeApp.Backend/RemindeMeApp.Backend.csproj -c Release -r win-x64 --self-contained -p:PublishSingleFile=true
```

Isso gerará um arquivo executável principal em `src/RemindeMeApp.Backend/bin/Release/net9.0-windows10.0.19041.0/win-x64/publish/`.

## Padrão de Controle de Versão

O repositório segue a adoção de:
- **Conventional Commits:** Todas as mensagens de commit devem seguir o formato `tipo(escopo): descrição`, ex: `feat(ui): adicionar temporizador`, `fix(db): resolver erro na migração`.
- **GitHub Flow:** Criação de branches a partir da `main` (ex: `feature/xyz` ou `fix/xyz`), revisão por Pull Request e merge na `main` sem necessidade de complexidade extra.
