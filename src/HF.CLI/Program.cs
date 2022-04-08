using HF.CLI.Services;
using Nethereum.Web3;
using Serilog;
using System.Text.Json;

namespace HF.CLI;

public class Program
{
    public static async Task Main(String[] args)
    {
        var now = DateTime.UtcNow;

        using var logger = new LoggerConfiguration()
            .WriteTo.Console(
                restrictedToMinimumLevel : Serilog.Events.LogEventLevel.Information)
            .WriteTo.File(
                path                     : $"./log-{now:yyyy-MM-dd-HH-mm-ss}.txt",
                restrictedToMinimumLevel : Serilog.Events.LogEventLevel.Information)
            .CreateLogger();

        Settings? settings = null;

        try
        {
            var settingsJson = await File.ReadAllTextAsync("./settings.json");
                settings     = JsonSerializer.Deserialize<Settings>(settingsJson);            
        }
        catch (Exception e)
        {
            logger.Error(e, "An error occurred while loading the settings");
        }

        var senderAccount = new Nethereum.Web3.Accounts.Account(settings!.SenderPrivateKey, settings.ChainId);
        var web3          = new Web3(senderAccount, settings.NodeRpc);
        
        web3.TransactionManager.UseLegacyAsDefault = true;

        var sendings = SendingsScheduler.Schedule(
            settings.SendingDurationInHours,
            settings.RecipientsCount,
            settings.TokenDecimals,
            settings.MinSendAmount,
            settings.MaxSendAmount);

        var receipts = await Sender.SendAsync(logger, senderAccount, sendings, settings, web3);

        await ReportGenerator.GenerateAndSaveAsync(receipts, settings, now);

        logger.Information("The report has been created and saved. Press any key to exit");
        Console.ReadKey();
    }
}

