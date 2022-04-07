using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HF.CLI.DataAccess.Types;

public class Sending
{ 
    public Int64         Id               { get; set; }
    public String        RecipientAddress { get; set; } = String.Empty;
    public SendingStatus Status           { get; set; }
    public Decimal       SentAmount       { get; set; }
    public DateTime      SentDate         { get; set; }
    public String        TransactionHash  { get; set; } = String.Empty;
}

internal class SendingConfiguration : IEntityTypeConfiguration<Sending>
{
    public void Configure(EntityTypeBuilder<Sending> builder)
    {
        builder
            .HasKey(x => x.Id);
    }
}
