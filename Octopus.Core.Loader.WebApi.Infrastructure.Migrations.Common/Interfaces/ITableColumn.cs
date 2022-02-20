namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Interfaces
{
    public interface ITableColumn
    {
        public string PropertyName { get; set; }
        public string PropertyTypeName { get; set; }
        public bool PropertyIsNullable { get; set; }
    }
}
