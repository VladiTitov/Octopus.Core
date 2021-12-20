using Octopus.Core.Common.Extensions;
using Octopus.Core.Common.Helpers.JsonDeserializer;
using Octopus.Core.Common.Models;
using Octopus.Core.Parser.WorkerService.Interfaces.Services.DynamicModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Octopus.Core.Parser.WorkerService.Services.DynamicModels
{
    public class DynamicObjectCreateService : IDynamicObjectCreateService
    {
        private IList<DynamicProperty> _dynamicProperties;
        private readonly IDynamicTypeFactory _dynamicTypeFactory;
        private readonly IJsonDeserializer _jsonDeserializer;

        public DynamicObjectCreateService(IJsonDeserializer jsonDeserializer, IDynamicTypeFactory dynamicTypeFactory)
        {
            _dynamicTypeFactory = dynamicTypeFactory;
            _jsonDeserializer = jsonDeserializer;
            _dynamicProperties = new List<DynamicProperty>();
        }

        public void ConfugureDynamicProperties(string dynamicPropertiesFilePath)
        {
            _dynamicProperties = _jsonDeserializer.GetDynamicProperties<DynamicProperty>(dynamicPropertiesFilePath);
        }

        public IEnumerable<object> AddValuesToDynamicObject(IEnumerable<string[]> values)
        {
            var extendedType = _dynamicTypeFactory.CreateNewTypeWithDynamicProperty(typeof(DynamicEntity), _dynamicProperties);
            return values.Select(value => GetObjectWithProperty(extendedType, value)).ToList();
        }

        private object GetObjectWithProperty(Type dynamicType, string[] objValues)
        {
            var obj = Activator.CreateInstance(dynamicType);
            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                var index = _dynamicProperties.FirstOrDefault(i => i.PropertyName.Equals(property.Name)).ValueIndex;

                switch (property.PropertyType.Name)
                {
                    case "Int32":
                        property.SetValue(obj, objValues[index].GetValidIntProperty());
                        break;
                    case "DateTime":
                        property.SetValue(obj, objValues[index].GetValidDateTimeProperty());
                        break;
                    default:
                        property.SetValue(obj, objValues[index]);
                        break;
                }
            }

            return obj;
        }
    }
}
