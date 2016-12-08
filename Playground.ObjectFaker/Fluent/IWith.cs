using System;

namespace Playground.ObjectFaker
{
    public interface IWith<TFakeObject, in TMember>
    {
        IOptions<TFakeObject> With(TMember constant);

        IOptions<TFakeObject> With(Func<TMember> func);
    }
}