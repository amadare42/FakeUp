using System;
using System.Collections;
using System.Linq;

namespace FakeUp
{
    class ArrayEvaluator : IValueEvaluator
    {
        public bool TryEvaluate(Type type, IObjectCreationContext context, out object result)
        {
            if (!typeof(IEnumerable).IsAssignableFrom(type))
            {
                result = null;
                return false;
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
                array = (Array)Activator.CreateInstance(type, elementsInCollections);
                elementType = type.GetElementType();
            }

            for (var i = 0; i < elementsInCollections; i++)
            {
                Func<int, object> filler;
                object value;
                if (context.Config.ElementsTypeFillers.TryGetValue(type, out filler))
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
            result = array;
            return true;
        }
    }
}