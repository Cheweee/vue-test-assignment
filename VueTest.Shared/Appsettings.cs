namespace VueTest.Shared
{
    public class Appsettings
    {
        public string Secret { get; set; }
        public DatabaseConnectionSettings DatabaseConnectionSettings { get; set; }
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
    }
}