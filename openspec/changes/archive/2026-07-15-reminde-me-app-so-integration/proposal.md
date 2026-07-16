## Why

A infraestrutura base da aplicação (backend) está desenvolvida, mas para funcionar como um aplicativo desktop real, precisamos integrar o backend ASP.NET Core com a interface gráfica do Photino e com recursos nativos do sistema operacional (Windows). Isso resolverá a inicialização correta da aplicação, exibição da janela e a interação com notificações e a bandeja do sistema (System Tray).

## What Changes

- Criação de classes de interoperabilidade com o SO (`NotificationManager` e `TrayIconManager`) no backend.
- Configuração do `Program.cs` para subir o Kestrel em uma porta aleatória disponível e carregar a janela do Photino apontando para essa porta.
- Configuração do comportamento da janela (`PhotinoWindow`) para ocultar em vez de fechar, mantendo o ícone no System Tray.
- Adição de menu de contexto no System Tray com a opção de sair de fato, usando diálogos de confirmação nativos.
- Configuração de Injeção de Dependência (DI) completa no ponto de entrada.

## Capabilities

### New Capabilities
- `os-integration`: Envio de notificações nativas (Toast) com verificação de "Modo Não Perturbe", gerenciamento do System Tray e inicialização do servidor web (Kestrel) acoplado a uma janela desktop (Photino).

### Modified Capabilities
Nenhuma.

## Impact

- Ponto de entrada (`Program.cs`) será amplamente modificado.
- Novas dependências poderão ser injetadas (pacotes para Windows Toast Notifications).
- O ciclo de vida da aplicação mudará de um console simples para uma aplicação desktop com Webview2 e System Tray.
