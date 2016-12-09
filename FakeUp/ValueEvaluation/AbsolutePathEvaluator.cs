using System;

namespace FakeUp
{
    internal class AbsolutePathEvaluator : IValueEvaluator
    {
        public bool TryEvaluate(Type type, IObjectCreationContext context, out object result)
        {
            Func<object> fillEvaluator;
            if (context.Config.RootMemberFillers.TryGetValue(context.InvocationPath, out fillEvaluator))
            {
                result = fillEvaluator();
                return true;
            }

            result = null;
            return false;
        }
    }
}