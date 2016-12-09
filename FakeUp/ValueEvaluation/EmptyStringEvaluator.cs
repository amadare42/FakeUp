using System;

namespace FakeUp
{
    internal class EmptyStringEvaluator : IValueEvaluator
    {
        public bool TryEvaluate(Type type, IObjectCreationContext context, out object result)
        {
            if (type == typeof(string))
            {
                result = string.Empty;
                return true;
            }
            result = null;
            return false;
        }
    }
}