using System;
using System.Collections;
using System.Linq;

namespace Playground.ObjectFaker
{
    public static class EmptyObjectFaker
    {
        public static T Create<T>()
        {
            var opt = new FakerOptions<T>();
            var instanse = (T)Create(typeof(T), new ObjectCreationContext<T> { TypedOptions = opt });
            return instanse;
        }

        public static T Create<T>(Action<IOptions<T>> options)
        {
            var opt = GetOptions(options);
            var instanse = (T)Create(typeof(T), new ObjectCreationContext<T> { TypedOptions = opt });
            return instanse;
        }

        private static FakerOptions<T> GetOptions<T>(Action<IOptions<T>> action)
        {
            var opt = new FakerOptions<T>();
            action?.Invoke(opt);
            return opt;
        }

        private static object Create<T>(Type type, ObjectCreationContext<T> context)
        {
            object instanse = null;
            try
            {
                Func<object> fillEvaluator;
                if (context.TypedOptions.RootMemberFillers.TryGetValue(context.InvocationPath, out fillEvaluator))
                {
                    return fillEvaluator();
                }

                RelativeMemberInfo memberInfo;
                if (context.TypedOptions.RelativeTypeFillers.TryGetValue(type, out memberInfo))
                {
                    var absolutePath = memberInfo.GetAbsolutePath(context.InvocationPath);
                    if (absolutePath == context.InvocationPath)
                    {
                        return memberInfo.Evaluate();
                    }
                }

                if (context.TypedOptions.TypeFillers.TryGetValue(type, out fillEvaluator))
                {
                    return fillEvaluator();
                }

                if (type == typeof(string))
                {
                    return string.Empty;
                }

                if (TryCreateCollection(type, context, out instanse))
                {
                    return instanse;
                }

                instanse = Activator.CreateInstance(type);
                if (context.TypedRootObject == null)
                {
                    context.TypedRootObject = (T)instanse;
                }
            }
            catch (MissingMethodException) //no parameterless c-tor
            {
                return instanse;
            }
            var propertyInfos = type.GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.CanWrite)
                {
                    context.InvocationStack.Push(propertyInfo);

                    var value = Create(propertyInfo.PropertyType, context);
                    propertyInfo.SetValue(instanse, value);

                    context.InvocationStack.Pop();
                }
            }
            return instanse;
        }

        private static bool TryCreateCollection<T>(Type type, ObjectCreationContext<T> context, out object instanse)
        {
            instanse = TryCreateList(type, context) ?? TryCreateArray(type, context);
            return instanse != null;
        }

        private static Array TryCreateArray<T>(Type type, ObjectCreationContext<T> context)
        {
            if (!typeof(IEnumerable).IsAssignableFrom(type))
            {
                return null;
            }

            var elementsInCollections = context.Options.ElementsInCollections;
            var array = (Array)Activator.CreateInstance(type, elementsInCollections);

            for (int i = 0; i < elementsInCollections; i++)
            {
                // TODO: element index
                var value = Create(type.GetElementType(), context);
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
                var value = Create(elementType, context);
                list.Add(value);
            }
            return list;
        }
    }
}