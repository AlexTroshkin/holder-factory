using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HF.CLI.DataAccess.Types;

public class Mnemonic
{
    public Int64  Id    { get; set; }
    public String Words { get; set; } = String.Empty;
}

internal class MnemonicConfiguration : IEntityTypeConfiguration<Mnemonic>
{
    public void Configure(EntityTypeBuilder<Mnemonic> builder)
    {
        builder
            .HasKey(x => x.Id);
    }
}
