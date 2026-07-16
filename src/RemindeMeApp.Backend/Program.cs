using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Photino.NET;
using RemindeMeApp.Backend.Data;
using RemindeMeApp.Backend.Services;
using RemindeMeApp.Backend.Interop;
using RemindeMeApp.Shared.Services;
using System;
using System.IO;
using System.Linq;

namespace RemindeMeApp.Backend;

class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        // 1. Create Photino Window early to register in DI
        var window = new PhotinoWindow()
            .SetTitle("RemindeMeApp")
            .SetUseOsDefaultSize(false)
            .SetSize(1024, 768)
            .SetMinSize(800, 600)
            .Center();

        window.WindowClosing += (sender, e) =>
        {
            window.Minimized = true;
            return true; // true cancels the close operation in Photino
        };

        // 2. Setup ASP.NET Core
        var builder = WebApplication.CreateBuilder(args);
        
        // Run on random available port
        builder.WebHost.ConfigureKestrel(serverOptions =>
        {
            serverOptions.Listen(System.Net.IPAddress.Loopback, 0);
        });

        // Configure database path
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                               ?? "Data Source=remindemeapp.db";
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var dbPath = Path.Combine(appData, "RemindeMeApp", "remindemeapp.db");
        Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
        connectionString = connectionString.Replace("remindemeapp.db", dbPath);

        // Register DbContext
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));

        // Register Services
        builder.Services.AddSingleton(TimeProvider.System);
        builder.Services.AddScoped<ITaskService, TaskService>();
        builder.Services.AddScoped<ITagService, TagService>();
        // ISubtaskService to be added if created
        builder.Services.AddScoped<ITimeTrackerService, TimeTrackingService>();
        builder.Services.AddScoped<IPomodoroService, TimeTrackingService>();

        // Register Interop
        builder.Services.AddSingleton(window);
        builder.Services.AddSingleton<INotificationManager, NotificationManager>();
        builder.Services.AddSingleton<ITrayIconManager, TrayIconManager>();

        builder.Services.AddRazorPages()
            .AddApplicationPart(typeof(RemindeMeApp.Frontend.Pages.IndexModel).Assembly);

        var app = builder.Build();

        app.UseStaticFiles();
        app.UseRouting();
        app.MapRazorPages();

        // Run migrations
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate();
        }

        // 3. Start ASP.NET Core
        app.StartAsync().Wait();

        // Get the assigned random port URL
        var server = app.Services.GetRequiredService<IServer>();
        var addressFeature = server.Features.Get<IServerAddressesFeature>();
        var url = addressFeature?.Addresses.FirstOrDefault();
        if (url != null)
        {
            window.Load(url);
        }

        // Initialize Tray Icon
        var trayManager = app.Services.GetRequiredService<ITrayIconManager>();
        trayManager.Initialize();

        // 4. Run Photino Window (blocks until closed)
        window.WaitForClose();

        // 5. Cleanup
        app.StopAsync().Wait();
    }
}
