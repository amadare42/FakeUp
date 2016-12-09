using System;
using FakeUp.Config;

namespace FakeUp.Fluent
{
    public interface IWith<TFakeObject, in TMember>
    {
        IFakeUpConfig<TFakeObject> With(TMember constant);

        IFakeUpConfig<TFakeObject> With(Func<TMember> func);

        // TODO: add ability to use previously created config to fill using With
        /*  so following calls should be possible:
         *
         *  .Fill<Foo>().With(config);
         *
         */
    }
}