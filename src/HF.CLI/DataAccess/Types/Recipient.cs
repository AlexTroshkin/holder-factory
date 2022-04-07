using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HF.CLI.DataAccess.Types;

public class Recipient
{
    public Int64  Id            { get; set; }
    public Int64  MnemonicId    { get; set; }
    public Int64  AccountNumber { get; set; }
    public String PrivateKey    { get; set; } = String.Empty;
    public String Address       { get; set; } = String.Empty;
}

internal class RecipientConfiguration : IEntityTypeConfiguration<Recipient>
{
    public void Configure(EntityTypeBuilder<Recipient> builder)
    {
        
    }
}
