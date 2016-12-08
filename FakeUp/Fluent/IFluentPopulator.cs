using System;

namespace Playground.ObjectFaker
{
    public interface IFluentPopulator<TFakeObject, in TMember>
    {
        FakeUpOptions<TFakeObject> With(TMember value);
        FakeUpOptions<TFakeObject> WithFunc(Func<TFakeObject, TMember> evaluatorFunc);
    }
}