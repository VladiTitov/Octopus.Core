namespace Octopus.Core.Common.ConfigsModels.Rabbit.Interfaces
{
    public interface IConnectionConfiguration
    {
        string Hostname { get; set; }
        int Port { get; set; }
        string VirtualHost { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}
