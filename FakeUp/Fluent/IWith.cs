using System;

namespace Playground.ObjectFaker
{
    public interface IWith<TFakeObject, in TMember>
    {
        IFakeUpOptions<TFakeObject> With(TMember constant);

        IFakeUpOptions<TFakeObject> With(Func<TMember> func);
    }
}