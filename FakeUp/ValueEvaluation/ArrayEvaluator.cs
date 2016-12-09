using System;
using System.Collections;
using System.Linq;

namespace FakeUp.ValueEvaluation
{
    internal class ArrayEvaluator : IValueEvaluator
    {
        public EvaluationResult Evaluate(Type type, IObjectCreationContext context)
        {
            if (!CanBeArray(type))
            {
                return EvaluationResult.Empty;
            }

            var elementsInCollections = context.Config.ElementsInCollections;
            var elementType = type.GetGenericArguments().FirstOrDefault();
            Array array;
            if (elementType != null)
            {
                array = Array.CreateInstance(elementType, elementsInCollections);
            }
            else
            {
                array = (Array) Activator.CreateInstance(type, elementsInCollections);
                elementType = type.GetElementType();
            }

            for (var i = 0; i < elementsInCollections; i++)
            {
                Func<int, object> filler;
                object value;
                if (context.Config.TypeElementsFillers.TryGetValue(type, out filler))
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