using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FakeUp.Extensions;

namespace FakeUp.ValueEvaluation.Evaluators
{
    internal class ListEvaluator : IValueEvaluator
    {
        public EvaluationResult Evaluate(Type type, IObjectCreationContext context)
        {
            if (!IsGenericList(type))
            {
                return EvaluationResult.Empty;
            }

            var elementType = type.HasElementType
                ? type.GetElementType()
                : type.GenericTypeArguments.First();
            var elementsInCollections = context.GetCollectionSize();

            var list = (IList)Activator.CreateInstance(type);
            for (var i = 0; i < elementsInCollections; i++)
            {
                var value = context.NewObject(elementType);
                list.Add(value);
            }

            return new EvaluationResult(list);
        }

        public static bool IsGenericList(Type type)
        {
            return type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(List<>));
        }
    }
}