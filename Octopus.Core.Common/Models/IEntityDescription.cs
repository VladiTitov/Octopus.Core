namespace Octopus.Core.Common.Models
{
    public interface IEntityDescription
    {
        string EntityType { get; set; }
        string EntityFilePath { get; set; }
    }
}
