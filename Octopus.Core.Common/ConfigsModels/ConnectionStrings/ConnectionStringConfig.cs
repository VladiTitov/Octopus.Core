namespace Octopus.Core.Common.ConfigsModels.ConnectionStrings
{
    public class ConnectionStringConfig
    {
        public const string Position = "ConnectionString";

        public string DbType { get; set; }
        public string DbScheme { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public override string ToString() => $"Host={Server};" +
                                             $"Port={Port};" +
                                             $"Database={Database};" +
                                             $"Username={UserName};" +
                                             $"Password={Password}";
    }
}
