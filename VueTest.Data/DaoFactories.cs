
using Microsoft.Extensions.Logging;
using VueTest.Data.Enumerations;
using VueTest.Data.Interfaces;

public class DaoFactories
{
    public static IDaoFactory GetFactory(DatabaseProvider provider, string connectionString, ILogger logger)
    {
        switch (provider)
        {
            case DatabaseProvider.Postgres:
                return new VueTest.Data.DataAccessObjects.Postgres.DaoFactory(connectionString, logger);
            default:
                return new VueTest.Data.DataAccessObjects.Postgres.DaoFactory(connectionString, logger);
        }
    }
}