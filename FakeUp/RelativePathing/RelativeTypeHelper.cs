using System.Collections.Generic;
using System.Linq;

namespace FakeUp.RelativePathing
{
    internal static class RelativeTypeHelper
    {
        public static TMemberInfo GetBestMatch<TMemberInfo>(IEnumerable<TMemberInfo> relativeMemberInfos, IObjectCreationContext context)
            where TMemberInfo : BaseRelativeMemberInfo
        {
            return relativeMemberInfos
                .ToLookup(info => context.GetMatchScore(info.CallChain))
                .Where(pair => pair.Key > 0)
                .OrderByDescending(pair => pair.Key)
                .Select(pair => pair.First())
                .FirstOrDefault();
        }
    }
}