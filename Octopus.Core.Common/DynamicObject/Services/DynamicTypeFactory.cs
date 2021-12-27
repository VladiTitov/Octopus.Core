using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Emit;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.DynamicObject.Services.Interfaces;
using Octopus.Core.Common.Extensions;

namespace Octopus.Core.Common.DynamicObject.Services
{
    public class DynamicTypeFactory : IDynamicTypeFactory
    {
        private TypeBuilder _typeBuilder;

        private readonly AssemblyBuilder _assemblyBuilder;
        private readonly ModuleBuilder _moduleBuilder;

        public DynamicTypeFactory()
        {
            var uniqueIdentifier = Guid.NewGuid().ToString();
            var assemblyName = new AssemblyName(uniqueIdentifier);

            _assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
            _moduleBuilder = _assemblyBuilder.DefineDynamicModule(uniqueIdentifier);
        }

        public Type CreateNewTypeWithDynamicProperty(Type parentType, IEnumerable<DynamicProperty> dynamicProperties)
        {
            _typeBuilder = _moduleBuilder.DefineType(parentType.Name, TypeAttributes.Public);
            _typeBuilder.SetParent(parentType);

            foreach (var property in dynamicProperties)
                AddDynamicPropertyToType(property);

            return _typeBuilder.CreateType();
        }

        private void AddDynamicPropertyToType(DynamicProperty property)
        {
            var propertyType = property.SystemType;
            var propertyName = $"{property.PropertyName}";
            var fieldName = $"_{propertyName.ToCamelCase()}";

            var fieldBuilder = _typeBuilder.DefineField(fieldName, propertyType, FieldAttributes.Public);
            var getSetAttributes = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

            var getMethodBuilder = _typeBuilder.DefineMethod($"get_{propertyName}", getSetAttributes, propertyType, Type.EmptyTypes);
            var propertyGetGenerator = getMethodBuilder.GetILGenerator();
            propertyGetGenerator.Emit(OpCodes.Ldarg_0);
            propertyGetGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
            propertyGetGenerator.Emit(OpCodes.Ret);

            var setMethodBuilder = _typeBuilder.DefineMethod($"set_{propertyName}", getSetAttributes, null, new[] { propertyType });
            var propertySetGenerator = setMethodBuilder.GetILGenerator();
            propertySetGenerator.Emit(OpCodes.Ldarg_0);
            propertySetGenerator.Emit(OpCodes.Ldarg_1);
            propertySetGenerator.Emit(OpCodes.Stfld, fieldBuilder);
            propertySetGenerator.Emit(OpCodes.Ret);

            var propertyBuilder = _typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            propertyBuilder.SetGetMethod(getMethodBuilder);
            propertyBuilder.SetSetMethod(setMethodBuilder);

            var attributeType = typeof(DisplayNameAttribute);
            var attributeBuilder = new CustomAttributeBuilder(
                attributeType.GetConstructor(new[] { typeof(string) }),
                new object[] { property.PropertyName },
                new PropertyInfo[] { },
                new object[] { }
                );
            propertyBuilder.SetCustomAttribute(attributeBuilder);
        }
    }
}
