namespace HF.CLI;

public class Settings
{
    public String  SenderPrivateKey             { get; set; } = String.Empty;
    public String  TokenAddress                 { get; set; } = String.Empty;
    public UInt16  TokenDecimals                { get; set; }
    public UInt16  SendingDurationInHours       { get; set; }
    public String  RecipientsMnemonicPhrase     { get; set; } = String.Empty;
    public UInt64  RecipientsAccountStartNumber { get; set; }
    public UInt64  RecipientsCount              { get; set; }
    public Decimal MinSendAmount                { get; set; }
    public Decimal MaxSendAmount                { get; set; }
    public UInt64  ChainId                      { get; set; }
    public String  NodeRpc                      { get; set; } = String.Empty;
}
