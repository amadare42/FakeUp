using System;

namespace FakeUp.ValueEvaluation.Evaluators
{
    internal class TypeEvaluator : IValueEvaluator
    {
        public EvaluationResult Evaluate(Type type, IObjectCreationContext context)
        {
            Func<object> filler;
            if (context.Config.TypeFillers.TryGetValue(type, out filler))
            {
                var result = filler();
                return new EvaluationResult(result);
            }
            return EvaluationResult.Empty;
        }
    }
}