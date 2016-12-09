using System;

namespace FakeUp.Fluent
{
    public interface IWith<TFakeObject, in TMember>
    {
        IFakeUpConfig<TFakeObject> With(TMember constant);

        IFakeUpConfig<TFakeObject> With(Func<TMember> func);
    }
}