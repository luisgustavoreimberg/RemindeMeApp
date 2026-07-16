using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using RemindeMeApp.Backend.Data;

namespace RemindeMeApp.Tests.Infrastructure;

public abstract class TestBase : IDisposable
{
    protected readonly ApplicationDbContext DbContext;
    protected readonly Mock<TimeProvider> TimeProviderMock;
    private readonly SqliteConnection _connection;

    protected TestBase()
    {
        // Configura conexão SQLite In-Memory que deve permanecer aberta durante o teste
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_connection)
            .Options;

        DbContext = new ApplicationDbContext(options);
        
        // Garante a criação do esquema no banco in-memory
        DbContext.Database.EnsureCreated();

        // Configura o Mock do TimeProvider para testes de resiliência e timers determinísticos
        TimeProviderMock = new Mock<TimeProvider>();
        TimeProviderMock.Setup(x => x.GetUtcNow()).Returns(DateTimeOffset.UtcNow);
        TimeProviderMock.Setup(x => x.LocalTimeZone).Returns(TimeZoneInfo.Local);
    }

    public void Dispose()
    {
        DbContext.Dispose();
        _connection.Dispose();
        GC.SuppressFinalize(this);
    }
}
