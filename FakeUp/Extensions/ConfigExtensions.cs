using FakeUp.RelativePathing;

namespace FakeUp.Extensions
{
    internal static class ConfigExtensions
    {
        public static int GetCollectionSize(this IObjectCreationContext context)
        {
            var config = context.Config;
            int size;

            // absolute
            if (config.AbsolutePathCollectionSizes.TryGetValue(context.InvocationPath, out size))
            {
                return size;
            }

            // relative
            var bestMemberInfo = RelativeTypeHelper.GetBestMatch(config.RelativeCollectionSizes, context);
            if (bestMemberInfo != null)
            {
                return bestMemberInfo.Size;
            }


            // type
            if (config.TypeCollectionSizes.TryGetValue(context.CurrentPropertyType, out size))
            {
                return size;
            }

            //generic
            return config.DefaultCollectionsSize;
        }
    }
}