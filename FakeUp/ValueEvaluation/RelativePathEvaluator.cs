using System;
using System.Linq;

namespace FakeUp.ValueEvaluation
{
    internal class RelativePathEvaluator : IValueEvaluator
    {
        public EvaluationResult Evaluate(Type type, IObjectCreationContext context)
        {
            var bestMemberInfo = context.Config.RelativeTypeFillers
                .ToLookup(info => context.GetMatchScore(info.CallChain))
                .Where(pair => pair.Key > 0)
                .OrderByDescending(pair => pair.Key)
                .Select(pair => pair.First())
                .FirstOrDefault();

            if (bestMemberInfo != null)
            {
                var result = bestMemberInfo.Evaluate();
                return new EvaluationResult(result);
            }
            return EvaluationResult.Empty;
        }
    }
}