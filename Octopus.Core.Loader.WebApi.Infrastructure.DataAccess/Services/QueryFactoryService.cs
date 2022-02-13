using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services
{
    public class QueryFactoryService : IQueryFactoryService
    {
        private readonly CreateCommentQueryModel _createCommentQueryModel;
        private readonly CreateSchemeQueryModel _createSchemeQueryModel;
        private readonly CreateTableQueryModel _createTableQueryModel;
        private readonly InsertQueryModel _insertQueryModel;

        public QueryFactoryService(CreateCommentQueryModel createCommentQueryModel,
            CreateSchemeQueryModel createSchemeQueryModel,
            CreateTableQueryModel createTableQueryModel,
            InsertQueryModel insertQueryModel)
        {
            _createCommentQueryModel = createCommentQueryModel;
            _createSchemeQueryModel = createSchemeQueryModel;
            _createTableQueryModel = createTableQueryModel;
            _insertQueryModel = insertQueryModel;
        }

        public string GetCreateSchemeQuery() 
            => _createSchemeQueryModel.GetQuery();

        public string GetInsertQuery(DynamicEntityWithProperties dynamicEntity) 
            => _insertQueryModel.GetQuery(dynamicEntity);

        public string GetCreateTableQuery(DynamicEntityWithProperties dynamicEntity) 
            => _createTableQueryModel.GetQuery(dynamicEntity);

        public string GetCreateCommentQuery(DynamicEntityWithProperties dynamicEntity) 
            => _createCommentQueryModel.GetQuery(dynamicEntity);
    }
}
