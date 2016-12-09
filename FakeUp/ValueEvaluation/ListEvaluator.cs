using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FakeUp.ValueEvaluation
{
    internal class ListEvaluator : IValueEvaluator
    {
        public bool TryEvaluate(Type type, IObjectCreationContext context, out object result)
        {
            if (!IsGenericList(type))
            {
                result = null;
                return false;
            }

            var elementType = type.HasElementType
                ? type.GetElementType()
                : type.GenericTypeArguments.First();
            var elementsInCollections = context.Config.ElementsInCollections;

            var list = (IList) Activator.CreateInstance(type);
            for (var i = 0; i < elementsInCollections; i++)
            {
                var value = context.NewObject(elementType);
                list.Add(value);
            }

            result = list;
            return true;
        }

        public static bool IsGenericList(Type type)
        {
            return type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(List<>));
        }
    }
}