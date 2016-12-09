using System;

namespace FakeUp.ValueEvaluation
{
    internal interface IValueEvaluator
    {
        bool TryEvaluate(Type type, IObjectCreationContext context, out object result);
    }
}