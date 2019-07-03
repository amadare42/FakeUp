using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FakeUpLib.RelativePathing
{
    public class CallChain
    {
        private readonly Type rootType;

        public CallChain(IList<CallInfo> calls, Type rootType)
        {
            this.rootType = rootType;
            this.calls = calls.ToList();
        }

        private List<CallInfo> calls { get; }

        public int GetMatchScore(IEnumerable<PropertyInfo> propertyChain)
        {
            var score = 0;
            var chain = propertyChain.Reverse().SkipWhile(c => c.DeclaringType != this.rootType).ToList();

            for (var i = 0; i < this.calls.Count; i++)
            {
                if ((chain.Count <= i) || !this.calls[i].IsSameCall(chain[i]))
                {
                    return 0;
                }
                score++;
            }

            return score;
        }

        public override string ToString()
        {
            return $"CallChain[{string.Join(".", this.calls.Select(c => c.PropName))}]";
        }
    }
}