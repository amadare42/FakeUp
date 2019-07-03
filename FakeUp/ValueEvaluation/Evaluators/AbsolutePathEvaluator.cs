using System;

namespace FakeUpLib.ValueEvaluation.Evaluators
{
    internal class AbsolutePathEvaluator : IValueEvaluator
    {
        public EvaluationResult Evaluate(Type type, IObjectCreationContext context)
        {
            if (context.Config.AbsolutePathFillers.TryGetValue(context.InvocationPath, out var fillEvaluator))
            {
                var result = fillEvaluator(context);
                return new EvaluationResult(result);
            }

            return EvaluationResult.Empty;
        }
    }
}