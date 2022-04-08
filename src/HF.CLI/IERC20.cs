using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Numerics;

namespace HF.CLI;

public static class IERC20
{
    [Function("transfer", "bool")]
    public class TransferFunction : FunctionMessage
    {
        [Parameter("address", "_to", 1)]
        public String To { get; set; } = String.Empty;

        [Parameter("uint256", "_value", 2)]
        public BigInteger TokenAmount { get; set; }
    }
}
