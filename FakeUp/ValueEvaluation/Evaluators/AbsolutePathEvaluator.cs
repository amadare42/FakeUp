using System;

namespace FakeUp.ValueEvaluation.Evaluators
{
    internal class AbsolutePathEvaluator : IValueEvaluator
    {
        public EvaluationResult Evaluate(Type type, IObjectCreationContext context)
        {
            Func<object> fillEvaluator;
            if (context.Config.AbsolutePathFillers.TryGetValue(context.InvocationPath, out fillEvaluator))
            {
                var result = fillEvaluator();
                return new EvaluationResult(result);
            }

            return EvaluationResult.Empty;
        }
    }
}