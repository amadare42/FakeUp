using System;

namespace Playground.ObjectFaker
{
    public interface ICollectionWith<TFakeObject>
    {
        IFakeUpOptions<TFakeObject> With(Func<int, object> func);

        IFakeUpOptions<TFakeObject> With(object constant);

        IFakeUpOptions<TFakeObject> With(Func<object> func);
    }
}