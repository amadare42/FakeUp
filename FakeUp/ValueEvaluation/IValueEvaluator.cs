using System;

namespace FakeUpLib.ValueEvaluation
{
    public interface IValueEvaluator
    {
        EvaluationResult Evaluate(Type type, IObjectCreationContext context);
    }
}