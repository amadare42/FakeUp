using System;

namespace FakeUp.ValueEvaluation
{
    internal class EmptyStringEvaluator : IValueEvaluator
    {
        public EvaluationResult Evaluate(Type type, IObjectCreationContext context)
        {
            if (type == typeof(string))
            {
                return new EvaluationResult(string.Empty);
            }
            return EvaluationResult.Empty;
        }
    }
}