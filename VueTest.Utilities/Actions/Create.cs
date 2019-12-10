using System;
using System.Data.SqlClient;
using VueTest.Shared;
using CommandLine;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace VueTest.Utilities.Actions
{
    [Verb("create", HelpText = "Create the DB")]
    public class CreateOptions { }

    public class Create
    {
        public static int Run(ILogger logger, DatabaseConnectionSettings settings)
        {
            try
            {
                logger.LogInformation($"Try to create \"{settings.DatabaseName}\" database");

                using (var connection = new NpgsqlConnection(settings.PostgresConnectionString))
                {
                    var comma = new NpgsqlCommand($@"
                        create database {settings.DatabaseName}
                    ", connection);

                    connection.Open();
                    comma.ExecuteNonQuery();
                    connection.Close();
                }

                logger.LogInformation($"{settings.DatabaseName} database successfully created");
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