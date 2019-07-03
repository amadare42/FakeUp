using System;

namespace FakeUp.ValueEvaluation
{
    public interface IValueEvaluator
    {
        EvaluationResult Evaluate(Type type, IObjectCreationContext context);
    }
}