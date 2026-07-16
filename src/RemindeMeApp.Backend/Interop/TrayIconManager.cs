using System;
using System.Drawing;
using System.Windows.Forms;
using Photino.NET;
using RemindeMeApp.Shared.Services;
using Application = System.Windows.Forms.Application;

namespace RemindeMeApp.Backend.Interop;

public class TrayIconManager : ITrayIconManager, IDisposable
{
    private readonly PhotinoWindow _window;
    private NotifyIcon? _notifyIcon;

    public TrayIconManager(PhotinoWindow window)
    {
        _window = window;
    }

    public void Initialize()
    {
        _notifyIcon = new NotifyIcon
        {
            Icon = SystemIcons.Application, // Fallback icon, could load from file
            Visible = true,
            Text = "RemindeMeApp"
        };

        var contextMenu = new ContextMenuStrip();
        var exitMenuItem = new ToolStripMenuItem("Sair");
        
        exitMenuItem.Click += (s, e) =>
        {
            var result = MessageBox.Show("Deseja realmente sair do RemindeMeApp?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _notifyIcon.Visible = false;
                Environment.Exit(0);
            }
        };

        contextMenu.Items.Add(exitMenuItem);
        _notifyIcon.ContextMenuStrip = contextMenu;

        _notifyIcon.DoubleClick += (s, e) =>
        {
            // Restore window on double click
            _window.Minimized = false;
            // Photino currently might not have a BringToFront, but un-minimizing helps.
        };
    }

    public void ShowNotification(string title, string message)
    {
        _notifyIcon?.ShowBalloonTip(3000, title, message, ToolTipIcon.Info);
    }

    public void Dispose()
    {
        if (_notifyIcon != null)
        {
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
        }
    }
}
