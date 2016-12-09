using System;
using System.Reflection;

namespace FakeUp.ValueEvaluation
{
    internal class ActivatorEvaluator : IValueEvaluator
    {
        public bool TryEvaluate(Type type, IObjectCreationContext context, out object result)
        {
            result = CreateByActivator(type);

            if (context.RootObject == null)
            {
                context.RootObject = result;
            }

            var propertyInfos = GetProperties(type);
            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.CanWrite)
                {
                    // TODO: handle cyclic references
                    context.InvocationStack.Push(propertyInfo);

                    var value = context.NewObject(propertyInfo.PropertyType);
                    propertyInfo.SetValue(result, value);

                    context.InvocationStack.Pop();
                }
            }
            return true;
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