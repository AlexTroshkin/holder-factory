using HF.CLI.DataAccess;

namespace HF.CLI.Services;

public class RecipientsGenerator
{
    private readonly Db db;

    public RecipientsGenerator(Db db)
    {
        this.db = db;
    }

    
}
