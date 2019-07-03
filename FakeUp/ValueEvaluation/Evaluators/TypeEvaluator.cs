using System;

namespace FakeUpLib.ValueEvaluation.Evaluators
{
    internal class TypeEvaluator : IValueEvaluator
    {
        public EvaluationResult Evaluate(Type type, IObjectCreationContext context)
        {
            if (context.Config.TypeFillers.TryGetValue(type, out var filler))
            {
                var result = filler(context);
                return new EvaluationResult(result);
            }
            return EvaluationResult.Empty;
        }
    }
}