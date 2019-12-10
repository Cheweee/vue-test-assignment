using System;
using System.Data.SqlClient;
using VueTest.Shared;
using CommandLine;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace VueTest.Utilities.Actions
{
    [Verb("drop", HelpText = "Drop the DB")]
    public class DropOptions { }
    
    public class Drop
    {
        public static int Run(ILogger logger, DatabaseConnectionSettings settings)
        {
            try
            {
                logger.LogInformation($"Try to drop \"{settings.DatabaseName}\" database");

                using (var connection = new NpgsqlConnection(settings.PostgresConnectionString))
                {
                    var comma = new NpgsqlCommand($"drop database if exists {settings.DatabaseName}", connection);

                    connection.Open();
                    comma.ExecuteNonQuery();
                    connection.Close();
                }
                
                logger.LogInformation($"{settings.DatabaseName} database successfully dropped");
                return 0;
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                return 1;
            }
        }
    }
}