using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.DynamicObject.Services.Interfaces;
using Octopus.Core.Common.Extensions;
using Octopus.Core.Common.Helpers.JsonDeserializer;
using Octopus.Core.Parser.WorkerService.Interfaces.Services.DynamicModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Octopus.Core.Parser.WorkerService.Services.DynamicModels
{
    public class DynamicObjectCreateService_2 : IDynamicObjectCreateService_2
    {
        private IList<DynamicProperty> _dynamicProperties;
        private readonly IDynamicTypeFactory _dynamicTypeFactory;
        private readonly IJsonDeserializer _jsonDeserializer;

        public DynamicObjectCreateService_2(IJsonDeserializer jsonDeserializer, IDynamicTypeFactory dynamicTypeFactory)
        {
            _dynamicTypeFactory = dynamicTypeFactory;
            _jsonDeserializer = jsonDeserializer;
            _dynamicProperties = new List<DynamicProperty>();
        }

        public Type CreateTypeByDescription(string dynamicPropertiesFilePath)
        {
            ConfigureDynamicProperties(dynamicPropertiesFilePath);

            return _dynamicTypeFactory.CreateNewTypeWithDynamicProperty(typeof(DynamicEntity), _dynamicProperties);
        }

        public IEnumerable<object> AddValuesToDynamicObject(Type extendedType, IEnumerable<string[]> values)
        {
            return values.Select(value => GetObjectWithProperty(extendedType, value)).ToList();
        }

        private void ConfigureDynamicProperties(string dynamicPropertiesFilePath)
        {
            _dynamicProperties = _jsonDeserializer.GetDynamicProperties<DynamicProperty>(dynamicPropertiesFilePath);
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
