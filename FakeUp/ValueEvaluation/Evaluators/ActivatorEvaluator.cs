using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FakeUp.ValueEvaluation.Evaluators
{
    internal class ActivatorEvaluator : IValueEvaluator
    {
        public const int MaxCyclicDepth = 3;

        public EvaluationResult Evaluate(Type type, IObjectCreationContext context)
        {
            var result = CreateByActivator(type);

            var propertyInfos = GetProperties(type);
            foreach (var propertyInfo in propertyInfos)
            {
                context.PushInvocation(propertyInfo);

                if (context.GetCyclicReferencesDepth() <= MaxCyclicDepth)
                {
                    var value = context.NewObject(propertyInfo.PropertyType);
                    propertyInfo.SetValue(result, value);
                }

                context.PopInvocation();
            }
            return new EvaluationResult(result);
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

        private static IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            // TODO: add ability to set non-public properties
            // TODO: add ability to set fields [mind props with backing field]
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty)
                .Where(prop => prop.CanWrite);
        }
    }
}