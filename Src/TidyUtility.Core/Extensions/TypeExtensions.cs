using System;
using System.Collections.Generic;
using System.Reflection;

namespace TidyUtility.Core.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetAllBaseTypes(this Type type)
        {
            Type? baseType = type.GetTypeInfo().BaseType;

            while (baseType != null)
            {
                yield return baseType;
                baseType = baseType.GetTypeInfo().BaseType;
            }
        }
    }
}
