using System;
using System.Collections;
using System.Linq;

namespace Playground.ObjectFaker
{
    public static class FakeUp
    {
        public const int DefaultCollectionElementCount = 1;

        public static T NewObject<T>()
        {
            var options = new FakeUpOptions<T>();
            var instance = (T)NewObject(typeof(T), new ObjectCreationContext<T> { TypedOptions = options });
            return instance;
        }

        public static object NewObject(Type type, Action<IFakeUpOptions<object>> opt)
        {
            var options = GetOptions(opt);
            var instance = NewObject(type, new ObjectCreationContext<object> { TypedOptions = options });
            return instance;
        }

        public static T NewObject<T>(Action<IFakeUpOptions<T>> opt)
        {
            var options = GetOptions(opt);
            var instance = (T)NewObject(typeof(T), new ObjectCreationContext<T> { TypedOptions = options });
            return instance;
        }

        private static FakeUpOptions<T> GetOptions<T>(Action<IFakeUpOptions<T>> action)
        {
            var opt = new FakeUpOptions<T>();
            action?.Invoke(opt);
            return opt;
        }

        private static object NewObject<T>(Type type, ObjectCreationContext<T> context)
        {
            object instance = null;
            try
            {
                //Root
                Func<object> fillEvaluator;
                if (context.TypedOptions.RootMemberFillers.TryGetValue(context.InvocationPath, out fillEvaluator))
                {
                    return fillEvaluator();
                }

                //Relative
                var bestMemberInfo = context.TypedOptions.RelativeTypeFillers
                    .ToLookup(info => info.CallChain.GetMatchScore(context.InvocationStack))
                    .Where(pair => pair.Key > 0)
                    .OrderByDescending(pair => pair.Key)
                    .Select(pair => pair.First())
                    .FirstOrDefault();

                if (bestMemberInfo != null)
                {
                    return bestMemberInfo.Evaluate();
                }

                //Type
                if (context.TypedOptions.TypeFillers.TryGetValue(type, out fillEvaluator))
                {
                    return fillEvaluator();
                }

                //String
                if (type == typeof(string))
                {
                    return string.Empty;
                }

                //Collections
                if (TryCreateCollection(type, context, out instance))
                {
                    return instance;
                }

                //Default
                instance = Activator.CreateInstance(type);

                //Set root
                if (context.TypedRootObject == null)
                {
                    context.TypedRootObject = (T)instance;
                }
            }
            catch (MissingMethodException) //no parameterless c-tor
            {
                return instance;
            }
            // TODO: get only properties with setters
            var propertyInfos = type.GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.CanWrite)
                {
                    context.InvocationStack.Push(propertyInfo);

                    var value = NewObject(propertyInfo.PropertyType, context);
                    propertyInfo.SetValue(instance, value);

                    context.InvocationStack.Pop();
                }
            }
            return instance;
        }

        private static bool TryCreateCollection<T>(Type type, ObjectCreationContext<T> context, out object instance)
        {
            instance = TryCreateList(type, context) ?? TryCreateArray(type, context);
            return instance != null;
        }

        private static Array TryCreateArray<T>(Type type, ObjectCreationContext<T> context)
        {
            if (!typeof(IEnumerable).IsAssignableFrom(type))
            {
                return null;
            }

            var elementsInCollections = context.Options.ElementsInCollections;
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

            for (int i = 0; i < elementsInCollections; i++)
            {
                Func<int, object> filler;
                object value;
                if (context.TypedOptions.ElementsTypeFillers.TryGetValue(type, out filler))
                {
                    value = filler(i);
                }
                else
                {
                    if (context.TypedOptions.AbsoluteElementsFillers.TryGetValue(context.InvocationPath, out filler))
                    {
                        value = filler(i);
                    }
                    else
                    {
                        value = NewObject(elementType, context);
                    }
                }
                array.SetValue(value, i);
            }
            return array;
        }

        private static IList TryCreateList<T>(Type type, ObjectCreationContext<T> context)
        {
            if (!type.IsGenericList())
            {
                return null;
            }

            var elementType = type.HasElementType
                ? type.GetElementType()
                : type.GenericTypeArguments.First();
            var list = (IList)Activator.CreateInstance(type);
            var elementsInCollections = context.Options.ElementsInCollections;
            for (var i = 0; i < elementsInCollections; i++)
            {
                var value = NewObject(elementType, context);
                list.Add(value);
            }
            return list;
        }
    }
}