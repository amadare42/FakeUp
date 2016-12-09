using System;
using FakeUp.Config;

namespace FakeUp.Fluent
{
    public interface ICollectionWith<TFakeObject>
    {
        IFakeUpConfig<TFakeObject> With(Func<int, object> func);

        IFakeUpConfig<TFakeObject> With(object constant);

        IFakeUpConfig<TFakeObject> With(Func<object> func);
    }
}