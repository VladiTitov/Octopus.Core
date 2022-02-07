﻿using System;
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
        private IEnumerable<DynamicProperty> _dynamicProperties;
        private readonly IJsonDeserializer _jsonDeserializer;
        private readonly IDynamicTypeFactory _dynamicTypeFactory;

        public DynamicObjectCreateService(IJsonDeserializer jsonDeserializer, IDynamicTypeFactory dynamicTypeFactory)
        {
            _dynamicTypeFactory = dynamicTypeFactory;
            _jsonDeserializer = jsonDeserializer;
            _dynamicProperties = new List<DynamicProperty>();
        }

        public Type CreateTypeByDescription(DynamicEntityWithProperties dynamicEntity) 
        {
            _dynamicProperties = dynamicEntity.Properties;
            return _dynamicTypeFactory.
                CreateNewTypeWithDynamicProperty(
                typeof(DynamicEntity), 
                dynamicEntity.EntityName, 
                _dynamicProperties);
            _dynamicProperties = ConfigureDynamicProperties(dynamicPropertiesFilePath);
            return _dynamicTypeFactory.GetTypeWithDynamicProperty(typeof(DynamicEntity), _dynamicProperties);
        }
            

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
