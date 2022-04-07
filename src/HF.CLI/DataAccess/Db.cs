using HF.CLI.DataAccess.Types;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HF.CLI.DataAccess;

public class Db : DbContext
{
    private String DbPath { get; set; }

    public Db()
    {
        var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        var file = "hf.db";

        DbPath = Path.Combine(path, file);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder builder) 
        => builder.ApplyConfigurationsFromAssembly(typeof(Db).Assembly);

    public DbSet<Mnemonic> Mnemonic { get; set; }
    public DbSet<Sending>  Sendings { get; set; }
}
