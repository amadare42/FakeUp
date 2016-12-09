using System;

namespace FakeUp
{
    internal interface IValueEvaluator
    {
        bool TryEvaluate(Type type, IObjectCreationContext context, out object result);
    }
}