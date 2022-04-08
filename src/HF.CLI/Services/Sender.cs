using Nethereum.HdWallet;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Serilog.Core;

namespace HF.CLI.Services;

public static class Sender
{
    public class Receipt
    {
        public String  Amount              { get; set; } = String.Empty;
        public String? Note                { get; set; }
        public String  RecipientAddress    { get; set; } = String.Empty;
        public String  RecipientPrivateKey { get; set; } = String.Empty;
        public String  Status              { get; set; } = String.Empty;
        public String? TransactionHash     { get; set; }
    }

    public static async ValueTask<List<Receipt>> SendAsync(
        Logger                          logger,
        Account                         sender,
        List<SendingsScheduler.Sending> sendings,
        Settings                        settings,
        Web3                            web3)
    {
        var wallet       = new Wallet(settings.RecipientsMnemonicPhrase, String.Empty);
        var sendingTasks = sendings
            .ConvertAll(sending =>
            {
                logger.Information("scheduled sending: {time} - {amount}", sending.Time, Web3.Convert.FromWei(sending.Amount, settings.TokenDecimals));

                return Task.Run(async () =>
                {
                    var now         = DateTime.UtcNow;
                    var waitingTime = sending.Time - now;

                    await Task.Delay(waitingTime);

                    var recipientNumber = (Int32)(settings.RecipientsAccountStartNumber + sending.OrderNumber);
                    var recipient       = wallet.GetAccount(index: recipientNumber);
                    var transferHandler = web3.Eth.GetContractTransactionHandler<IERC20.TransferFunction>();
                    var transfer        = new IERC20.TransferFunction
                    {
                        FromAddress = sender.Address,
                        To          = recipient.Address,
                        TokenAmount = sending.Amount,
                    };

                    try
                    {
                        var receipt = await transferHandler.SendRequestAndWaitForReceiptAsync(settings.TokenAddress, transfer);

                        var transferSuccessfully = receipt.Status.Value == 1;
                        if (transferSuccessfully)
                        {
                            logger.Information("sending to {address} completed", recipient.Address);

                            return new Receipt
                            {
                                Amount              = Web3.Convert.FromWei(sending.Amount, settings.TokenDecimals).ToString(),
                                RecipientAddress    = recipient.Address,
                                RecipientPrivateKey = recipient.PrivateKey,
                                Status              = "completed",
                                TransactionHash     = receipt.TransactionHash
                            };
                        }
                        else
                        {
                            logger.Error("sending to {address} failed", recipient.Address);

                            return new Receipt
                            {
                                Amount              = Web3.Convert.FromWei(sending.Amount, settings.TokenDecimals).ToString(),
                                RecipientAddress    = recipient.Address,
                                RecipientPrivateKey = recipient.PrivateKey,
                                Status              = "failed",
                                TransactionHash     = receipt.TransactionHash
                            };
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error("sending to {address} failed", recipient.Address);

                        return new Receipt
                        {
                            Amount              = Web3.Convert.FromWei(sending.Amount, settings.TokenDecimals).ToString(),
                            Note                = e.Message,
                            RecipientAddress    = recipient.Address,
                            RecipientPrivateKey = recipient.PrivateKey,
                            Status              = "failed",
                            TransactionHash     = null,
                        };
                    }
                });
            });

        await Task.WhenAll(sendingTasks);

        return sendingTasks.ConvertAll(task => task.Result);
    }
}
