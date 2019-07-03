using System;
using FakeUpLib.Config;

namespace FakeUpLib.Fluent
{
    public interface ICollectionWith<TFakeObject>
    {
        IFakeUpConfig<TFakeObject> With(Func<int, object> func);

        IFakeUpConfig<TFakeObject> With(object constant);

        IFakeUpConfig<TFakeObject> With(Func<object> func);
    }
}