using System;
using FakeUp.RelativePathing;

namespace FakeUp.ValueEvaluation.Evaluators
{
    internal class RelativePathEvaluator : IValueEvaluator
    {
        public EvaluationResult Evaluate(Type type, IObjectCreationContext context)
        {
            var bestMemberInfo = RelativeTypeHelper.GetBestMatch(context.Config.RelativeTypeFillers, context);

            if (bestMemberInfo != null)
            {
                var result = bestMemberInfo.Evaluate();
                return new EvaluationResult(result);
            }
            return EvaluationResult.Empty;
        }
    }
}