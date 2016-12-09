using System;
using System.Reflection;

namespace FakeUp
{
    public static class FakeUp
    {
        public const int DefaultCollectionElementCount = 1;


        public static T NewObject<T>()
        {
            var config = new FakeUpConfig<T>();
            var instance = (T)NewObject(typeof(T), new ObjectCreationContext<T>(config));
            return instance;
        }

        public static object NewObject(Type type, Action<IFakeUpConfig<object>> conf)
        {
            var config = GetConfig(conf);
            var instance = NewObject(type, new ObjectCreationContext<object>(config));
            return instance;
        }

        public static T NewObject<T>(Action<IFakeUpConfig<T>> conf)
        {
            var config = GetConfig(conf);
            var instance = (T)NewObject(typeof(T), new ObjectCreationContext<T>(config));
            return instance;
        }

        public static IConfigProvider Config
        {
            get { throw new NotImplementedException(); }
        }

        private static FakeUpConfig<T> GetConfig<T>(Action<IFakeUpConfig<T>> action)
        {
            var opt = new FakeUpConfig<T>();
            action?.Invoke(opt);
            return opt;
        }

        internal static object NewObject(Type type, IObjectCreationContext context)
        {
            object instance;
            foreach (var evaluator in context.Evaluators)
            {
                if (evaluator.TryEvaluate(type, context, out instance))
                {
                    return instance;
                }
            }

            // TODO: move to activator evaluator
            instance = CreateByActivator(type);

            if (context.RootObject == null)
            {
                context.RootObject = instance;
            }

            var propertyInfos = GetProperties(type);
            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.CanWrite)
                {
                    // TODO: handle cyclic references
                    context.InvocationStack.Push(propertyInfo);

                    var value = NewObject(propertyInfo.PropertyType, context);
                    propertyInfo.SetValue(instance, value);

                    context.InvocationStack.Pop();
                }
            }
            return instance;
        }

        private static object CreateByActivator(Type type)
        {
            object instance;
            try
            {
                instance = Activator.CreateInstance(type);
            }
            catch (MissingMethodException) //no parameterless c-tor
            {
                instance = null;
            }
            return instance;
        }

        private static PropertyInfo[] GetProperties(Type type)
        {
            // TODO: add ability to set non-public properties
            // TODO: add ability to set fields
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);
        }
    }
}