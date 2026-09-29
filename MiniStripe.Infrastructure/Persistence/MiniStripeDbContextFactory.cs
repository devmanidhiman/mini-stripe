using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

namespace MiniStripe.Infrastructure.Persistence;

public class MiniStripeDbContextFactory : IDesignTimeDbContextFactory<MiniStripeDbContext>
{
    public MiniStripeDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("MINISTRIPE_DB_CONNECTION")
            ?? "Host=localhost;Port=5432;Database=ministripe;Username=ministripe;Password=ministripe123";

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        dataSourceBuilder.ConnectionStringBuilder.SslMode = SslMode.Disable;
        
        var dataSource = dataSourceBuilder.Build();

        var options = new DbContextOptionsBuilder<MiniStripeDbContext>()
            .UseNpgsql(dataSource)
            .Options;

        return new MiniStripeDbContext(options);
    }
}