using System;
using FakeUp.Config;
using FakeUp.RelativePathing;

namespace FakeUp.Extensions
{
    public static class ConfigExtensions
    {
        public static int GetCollectionSize(this IObjectCreationContext context, Type type)
        {
            var config = context.Config;
            int size;

            // absolute
            if (config.AbsolutePathCollectionSizes.TryGetValue(context.InvocationPath, out size))
            {
                return size;
            }

            // relative
            var bestMemberInfo = config.RelativeCollectionSizes.GetBestMatch(context);
            if (bestMemberInfo != null)
            {
                return bestMemberInfo.Size;
            }


            // type
            if (config.TypeCollectionSizes.TryGetValue(type, out size))
            {
                return size;
            }

            //generic
            return config.DefaultCollectionsSize;
        }

        public static T FakeUp<T>(this Action<IFakeUpConfig<T>> config)
        {
            return global::FakeUp.FakeUp.NewObject(config);
        }
    }
}