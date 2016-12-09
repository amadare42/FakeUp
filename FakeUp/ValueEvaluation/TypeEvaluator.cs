using System;

namespace FakeUp.ValueEvaluation
{
    internal class TypeEvaluator : IValueEvaluator
    {
        public bool TryEvaluate(Type type, IObjectCreationContext context, out object result)
        {
            Func<object> filler;
            if (context.Config.TypeFillers.TryGetValue(type, out filler))
            {
                result = filler();
                return true;
            }
            result = null;
            return false;
        }
    }
}