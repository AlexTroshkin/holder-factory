using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text.Json;

namespace HF.CLI.Services;

public static class ReportGenerator
{
    public async static ValueTask GenerateAndSaveAsync(List<Sender.Receipt> receipts, Settings settings, DateTime startTime)
    {
        var configuration = new CsvConfiguration(CultureInfo.CurrentCulture)
        {            
        };

        using var fs        = new StreamWriter($"./report-{startTime:yyyy-MM-dd-HH-mm-ss}-receipts.csv");
        using var csvWriter = new CsvWriter(fs, configuration);

              csvWriter.WriteHeader<Sender.Receipt>();
        await csvWriter.NextRecordAsync();
        await csvWriter.WriteRecordsAsync(receipts);

        var settingsJson = JsonSerializer.Serialize(settings, new JsonSerializerOptions
        {
            WriteIndented = true,
        });

        using var settingsFs = new StreamWriter($"./report-{startTime:yyyy-MM-dd-HH-mm-ss}-settings.json");
        await settingsFs.WriteAsync(settingsJson);
    }
}
