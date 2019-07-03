using System;
using FakeUp.Config;

namespace FakeUp.Fluent
{
    public interface IWith<TFakeObject, TMember>
    {
        IFakeUpConfig<TFakeObject> With(TMember constant);

        IFakeUpConfig<TFakeObject> With(Func<TMember> func);
        
        IFakeUpConfig<TFakeObject> With(Func<IObjectCreationContext, TMember> func);

        IFakeUpConfig<TFakeObject> With(Action<IFakeUpConfig<TMember>> configOverride);

        // TODO: add ability to use previously created config to fill using With
        /*  so following calls should be possible:
         *
         *  .Fill<Foo>().With(config);
         *
         */
    }
}