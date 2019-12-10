using System;
using Microsoft.Extensions.Logging;
using VueTest.Data.Interfaces;

namespace VueTest.Data.DataAccessObjects.Postgres
{
    public class DaoFactory : IDaoFactory
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;

        public DaoFactory(string connectionString, ILogger logger)
        {
            _connectionString = connectionString ?? throw new ArgumentException(nameof(connectionString));
            _logger = logger ?? throw new ArgumentException(nameof(logger));            
        }

        public IUserDao UserDao => new UserDao(_connectionString, _logger);
    }
}