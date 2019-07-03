using System;
using System.Collections;
using System.Linq;
using FakeUp.Extensions;

namespace FakeUp.ValueEvaluation.Evaluators
{
    internal class ArrayEvaluator : IValueEvaluator
    {
        public EvaluationResult Evaluate(Type type, IObjectCreationContext context)
        {
            if (!CanBeArray(type))
            {
                return EvaluationResult.Empty;
            }

            var elementsInCollections = context.GetCollectionSize(type);
            var elementType = type.GetGenericArguments().FirstOrDefault();
            Array array;
            if (elementType != null)
            {
                array = Array.CreateInstance(elementType, elementsInCollections);
            }
            else
            {
                array = (Array)Activator.CreateInstance(type, elementsInCollections);
                elementType = type.GetElementType();
            }

            for (var i = 0; i < elementsInCollections; i++)
            {
                object value;
                if (context.Config.TypeElementsFillers.TryGetValue(type, out var filler))
                {
                    value = filler(i);
                }
                else
                {
                    if (context.Config.AbsoluteElementsFillers.TryGetValue(context.InvocationPath, out filler))
                    {
                        value = filler(i);
                    }
                    else
                    {
                        value = context.NewObject(elementType);
                    }
                }
                array.SetValue(value, i);
            }
            return new EvaluationResult(array);
        }

        private static bool CanBeArray(Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type);
        }
    }
}