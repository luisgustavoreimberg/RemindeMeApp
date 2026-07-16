## Context

A aplicação foi planejada como um aplicativo desktop offline que utiliza tecnologias web para a interface de usuário (Razor Pages, CSS, JS) através de um Webview (Photino), mas que requer interação com recursos do sistema operacional Windows, como notificações Toast (Centro de Notificações do Windows) e ícone na bandeja do sistema (System Tray).

Para que as camadas de domínio e aplicação não fiquem presas à infraestrutura do Windows ou do Photino diretamente, adotaremos um padrão de injeção de dependência provendo implementações concretas de interoperabilidade.

## Goals / Non-Goals

**Goals:**
- Implementar as interfaces de integração com o SO Windows.
- Omitir o processo quando a janela do Photino for fechada, delegando o ciclo de vida ao ícone do System Tray.
- Utilizar pacote adequado para Windows Toast Notifications (ex: `Microsoft.Toolkit.Uwp.Notifications` ou equivalente suportado via pacote nativo do Windows).
- Integrar a injeção de dependência e inicialização de banco com a janela do Photino.

**Non-Goals:**
- Implementação de integração com macOS ou Linux.
- Desenvolvimento das telas de UI.
- Autenticação e Sincronização em nuvem.

## Decisions

1. **Host Integrado**: O `Program.cs` criará o Kestrel usando a Injeção de Dependências e configurará os serviços. Em seguida, iniciará o Servidor Web e no final chamará o Photino de forma assíncrona/na thread principal.
2. **Ciclo de vida do System Tray**:
   - `PhotinoWindow.WindowClosing`: Interceptar e impedir o encerramento do processo (`args.Cancel = true`), apenas minimizando ou ocultando a janela.
   - Um System Tray será criado usando as APIs fornecidas pelo Photino.NET (ex: `window.SetIconFile()`, e menus contextuais se suportados). Caso Photino não suporte System Tray nativo de forma fluída em C#, usaremos pacotes adicionais como `H.NotifyIcon` se necessário, mas primeiro tentaremos a API nativa ou deixaremos a casca pronta. (Nota: A especificação diz para encapsular em `TrayIconManager`).
3. **Notificações**:
   - Usar biblioteca suportada para Toast no .NET 9 (ex: `CommunityToolkit.WinUI.Notifications`).

## Risks / Trade-offs

- [Risk] Falha ao enviar notificações caso o Windows bloqueie ou as APIs mudem. → Mitigação: Capturar exceções na classe `NotificationManager` para evitar crash e adicionar logging.
- [Risk] O Photino tem controle limitado de Tray nativo no Windows. → Mitigação: Projetar a interface `TrayIconManager` para ser extensível ou utilizar um pacote Windows Forms / Win32Interop caso necessário, mas tentar a API base do Photino (se existir) primeiro.
