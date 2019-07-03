using System;
using FakeUpLib.Config;

namespace FakeUpLib.Fluent
{
    public static class Extensions
    {
        public static IFakeUpConfig<TFakeObject> WithGuid<TFakeObject>(this IWith<TFakeObject, string> with)
        {
            return with.With(() => Guid.NewGuid().ToString());
        }
    }
}