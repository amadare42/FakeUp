using System.Collections.Generic;
using System.Linq;

namespace FakeUpLib.RelativePathing
{
    internal static class RelativeTypeHelper
    {
        public static TMemberInfo GetBestMatch<TMemberInfo>(this IEnumerable<TMemberInfo> relativeMemberInfos, IObjectCreationContext context)
            where TMemberInfo : BaseRelativeMemberInfo
        {
            return relativeMemberInfos
                .ToLookup(context.GetMatchScore)
                .Where(pair => pair.Key > 0)
                .OrderByDescending(pair => pair.Key)
                .Select(pair => pair.First())
                .FirstOrDefault();
        }
    }
}