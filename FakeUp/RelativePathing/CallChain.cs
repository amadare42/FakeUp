using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FakeUp.RelativePathing
{
    public class CallChain
    {
        public CallChain(List<CallInfo> calls)
        {
            this.Calls = calls;
        }

        public List<CallInfo> Calls { get; }

        public int GetMatchScore(IEnumerable<PropertyInfo> propertyChain)
        {
            var score = 0;
            var reverseCalls = Enumerable.Reverse(this.Calls).ToList();
            var otherReverseCalls = propertyChain.ToList();

            for (var i = 0; i < reverseCalls.Count; i++)
            {
                if ((otherReverseCalls.Count <= i) || !reverseCalls[i].IsSameCall(otherReverseCalls[i]))
                {
                    return 0;
                }
                score++;
            }

            return score;
        }
    }
}