using System;
using System.Linq;
using System.Collections.Generic;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.DynamicObject.Services.Interfaces;
using Octopus.Core.Common.Extensions;
using Octopus.Core.Common.Helpers.JsonDeserializer;

namespace Octopus.Core.Common.DynamicObject.Services
{
    public class DynamicObjectCreateService : IDynamicObjectCreateService
    {
        private IList<DynamicProperty> _dynamicProperties;
        private readonly IDynamicTypeFactory _dynamicTypeFactory;

        public DynamicObjectCreateService(IJsonDeserializer jsonDeserializer, IDynamicTypeFactory dynamicTypeFactory)
        {
            _dynamicTypeFactory = dynamicTypeFactory;
            _dynamicProperties = jsonDeserializer.GetDynamicProperties<DynamicProperty>(@"Configs\dynamicProperties.json");
        }

        public Type CreateTypeByDescription() => 
            _dynamicTypeFactory.CreateNewTypeWithDynamicProperty(typeof(DynamicEntity), _dynamicProperties);

        public IEnumerable<object> AddValuesToDynamicObject(Type extendedType, IEnumerable<string[]> values) => 
            values.Select(value => GetObjectWithProperty(extendedType, value)).ToList();

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
