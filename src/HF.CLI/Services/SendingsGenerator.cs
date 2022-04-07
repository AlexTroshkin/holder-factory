using HF.CLI.DataAccess.Types;

namespace HF.CLI.Services;

public class SendingsGenerator
{
    private static readonly Random rng = new();

    public static List<Sending> From(Int16 workingHours, Int16 minTxPerHour, Int16 maxTxPerHour, Decimal minAmount, Decimal maxAmount)
    {
        var sendings  = new List<Sending>();
        var startTime = DateTime.UtcNow;

        for (int hourNumber = 0; hourNumber < workingHours; hourNumber++)
        {
            var throughput       = rng.Next(minTxPerHour, maxTxPerHour + 1);
            var shiftedStartTime = startTime.AddHours(hourNumber);

            for (int i = 0; i < throughput; i++)
            {
                var timeShiftInSeconds = rng.Next(0, 3600);
                var sending           = new Sending
                {
                    RecipientAddress = "",
                    SentAmount       = rng.NextDecimal(minAmount, maxAmount),
                    SentDate         = shiftedStartTime.AddSeconds(timeShiftInSeconds),
                    Status           = SendingStatus.Pending,
                    TransactionHash  = null,
                };
            }
        }

        return sendings;
    }
}
