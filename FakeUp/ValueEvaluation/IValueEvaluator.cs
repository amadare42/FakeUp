using System;

namespace FakeUp.ValueEvaluation
{
    internal interface IValueEvaluator
    {
        EvaluationResult Evaluate(Type type, IObjectCreationContext context);
    }
}