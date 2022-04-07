using HF.CLI.DataAccess;
using HF.CLI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HF.CLI;

public class Program
{
    public static async Task Main(String[] args)
    {
        var services = new ServiceCollection();
            
        services
            .AddDbContext<Db>()
            .AddSingleton<RecipientsGenerator>()
            .AddSingleton<SendingsGenerator>();

        var serviceProvider = services.BuildServiceProvider();
        
        var db = serviceProvider.GetRequiredService<Db>();
            db.Database.EnsureCreated();
        
        await db.Database.MigrateAsync();

        var senderPrivateKey       = "<HEX STR FROM INPUT>";
        var tokenAddress           = "<HEX STR FROM INPUT>";
        var minTransactionsPerHour = 5;
        var maxTransactionsPerHour = 100;
        var minSendAmount          = 1000;
        var maxSendAmount          = 50000;


    }
}

