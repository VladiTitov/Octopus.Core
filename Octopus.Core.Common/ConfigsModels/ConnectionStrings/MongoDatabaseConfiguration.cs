namespace Octopus.Core.Common.ConfigsModels.ConnectionStrings
{
    public class MongoDatabaseConfiguration
    {
        public string DatabaseName { get; set; }
        public string Server { get; set; }
        public string Port { get; set; }
        public string ConnectionString => $"mongodb://{Server}:{Port}/{DatabaseName}";
    }
}
