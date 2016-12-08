using System;
using System.Collections.Generic;

namespace Playground.ObjectFaker
{
    public static class TypeExtensions
    {
        public static bool IsGenericList(this Type type)
        {
            return type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(List<>));
        }
    }
}