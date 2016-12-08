using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Playground.ObjectFaker
{
    public class CallChain
    {
        public List<CallInfo> Calls { get; }

        public CallChain(List<CallInfo> calls)
        {
            Calls = calls;
        }

        public int GetMatchScore(IEnumerable<PropertyInfo> propertyChain)
        {
            int score = 0;
            var reverseCalls = Enumerable.Reverse(Calls).ToList();
            var otherReverseCalls = propertyChain.ToList();

            for (int i = 0; i < reverseCalls.Count; i++)
            {
                if (otherReverseCalls.Count <= i || !reverseCalls[i].IsSameCall(otherReverseCalls[i]))
                {
                    return 0;
                }
                score++;
            }

            return score;
        }
    }
}