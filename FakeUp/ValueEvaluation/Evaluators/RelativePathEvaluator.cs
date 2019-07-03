using System;
using FakeUpLib.RelativePathing;

namespace FakeUpLib.ValueEvaluation.Evaluators
{
    internal class RelativePathEvaluator : IValueEvaluator
    {
        public EvaluationResult Evaluate(Type type, IObjectCreationContext context)
        {
            var bestMemberInfo = context.Config.RelativeTypeFillers.GetBestMatch(context);

            if (bestMemberInfo != null)
            {
                var result = bestMemberInfo.Evaluate(context);
                return new EvaluationResult(result);
            }
            return EvaluationResult.Empty;
        }
    }
}