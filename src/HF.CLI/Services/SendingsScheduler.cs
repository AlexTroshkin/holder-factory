using Nethereum.Web3;
using System.Numerics;

namespace HF.CLI.Services;

public static class SendingsScheduler
{
    private readonly static Random rng = new();

    public class Sending
    {
        public UInt64     OrderNumber { get; set; }
        public BigInteger Amount      { get; set; }
        public DateTime   Time        { get; set; }
    }

    public static List<Sending> Schedule(
        UInt16  sendingDurationInHous,
        UInt64  recipientsCount,
        UInt16  tokenDecimals,
        Decimal minSendAmount,
        Decimal maxSendAmount)
    {
        var sendings                 = new List<Sending>();
        var startTime                = DateTime.UtcNow;
        var sendingDurationInSeconds = sendingDurationInHous * 60 * 60;

        for (UInt64 recipientNumber = 0; recipientNumber < recipientsCount; recipientNumber++)
        {
            var humanReadableAmount = rng.NextDecimal(minSendAmount, maxSendAmount);
            var amount              = Web3.Convert.ToWei(humanReadableAmount, tokenDecimals);

            var timeShift = rng.NextInt64(0, sendingDurationInSeconds);
            var time      = startTime.AddSeconds(timeShift);

            var sending = new Sending()
            {
                OrderNumber = recipientNumber,
                Amount      = amount,
                Time        = time,
            };

            sendings.Add(sending);
        }

        return sendings
            .OrderBy(sending => sending.Time)
            .ToList();
    }
}
