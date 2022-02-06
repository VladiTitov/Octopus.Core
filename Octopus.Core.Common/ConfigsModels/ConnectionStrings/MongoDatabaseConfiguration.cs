namespace Octopus.Core.Common.ConfigsModels.ConnectionStrings
{
    public class MongoDatabaseConfiguration
    {
        public string CollectionName { get; set; }
        public string DataBaseName { get; set; }
        public string Server { get; set; }
        public string Port { get; set; }
        public string ConnectionString => $"mongodb://{Server}:{Port}/{DataBaseName}";
    }
}
