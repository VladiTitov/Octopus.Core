using Dapper;
using System;
using System.Linq;
using System.Reflection;
using System.Data.Common;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services
{
    public class QueryHandlerService : IQueryHandlerService
    {
        private readonly IDatabaseContext _context;
        private readonly IDatabaseExceptionFactory _exceptionFactory;

        public QueryHandlerService(IDatabaseContext context,
            IDatabaseExceptionFactory exceptionFactory)
        {
            _context = context;
            _exceptionFactory = exceptionFactory;
        }

        public async Task ExecuteAsync(string query)
        {
            try
            {
                using (var db = _context.CreateConnection())
                {
                    var pr = await db.ExecuteAsync(query);
                }
            }
            catch (DbException ex)
            {
                _exceptionFactory.GetDatabaseError(ex);
            }
        }

        public async Task ExecuteAsync(string query, IEnumerable<object> items)
        {
            try
            {
                using (var db = _context.CreateConnection())
                {
                    await db.ExecuteAsync(query, items);
                }
            }
            catch (DbException ex)
            {
                _exceptionFactory.GetDatabaseError(ex);
            }
        }

        public async Task<T> QueryAsync<T>(string query)
        {
            var result = default(T);
            try
            {
                using (var db = _context.CreateConnection())
                {
                    result = await db.QueryFirstAsync<T>(query);
                }
            }
            catch (DbException ex)
            {
                _exceptionFactory.GetDatabaseError(ex);
            }

            return result;
        }

        public async Task<IEnumerable<T>> QueryListAsync<T>(string query)
        {
            IEnumerable<T> result = null;
            try
            {
                using (var db = _context.CreateConnection())
                {
                    SqlMapper.SetTypeMap(typeof(T), GetTypeMap<T>());
                    var temp = await db.QueryAsync<T>(query);
                    result = temp.ToList();
                }
            }
            catch (DbException ex)
            {
                _exceptionFactory.GetDatabaseError(ex);
            }

            return result;
        }

        private CustomPropertyTypeMap GetTypeMap<T>()
        {
            return new CustomPropertyTypeMap(typeof(T), (type, columnName)
                => type
                    .GetProperties()
                    .FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName.ToLower()));
        }

        private string GetDescriptionFromAttribute(MemberInfo member)
        {
            if (member == null) return null;

            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(member, typeof(DescriptionAttribute), false);
            return (attribute?.Description ?? member.Name).ToLower();
        }
    }
}
