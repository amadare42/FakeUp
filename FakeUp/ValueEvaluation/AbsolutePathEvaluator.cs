using System;

namespace FakeUp.ValueEvaluation
{
    internal class AbsolutePathEvaluator : IValueEvaluator
    {
        public bool TryEvaluate(Type type, IObjectCreationContext context, out object result)
        {
            Func<object> fillEvaluator;
            if (context.Config.AbsolutePathFillers.TryGetValue(context.InvocationPath, out fillEvaluator))
            {
                result = fillEvaluator();
                return true;
            }

            result = null;
            return false;
        }
    }
}