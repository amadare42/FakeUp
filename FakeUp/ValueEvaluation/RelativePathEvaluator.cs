using System;
using System.Linq;

namespace FakeUp
{
    internal class RelativePathEvaluator : IValueEvaluator
    {
        public bool TryEvaluate(Type type, IObjectCreationContext context, out object result)
        {
            var bestMemberInfo = context.Config.RelativeTypeFillers
                .ToLookup(info => info.CallChain.GetMatchScore(context.InvocationStack))
                .Where(pair => pair.Key > 0)
                .OrderByDescending(pair => pair.Key)
                .Select(pair => pair.First())
                .FirstOrDefault();

            if (bestMemberInfo != null)
            {
                result = bestMemberInfo.Evaluate();
                return true;
            }
            result = null;
            return false;
        }
    }
}