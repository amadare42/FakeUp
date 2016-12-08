using System;

namespace Playground.ObjectFaker
{
    public interface IFluentPopulator<TFakeObject, in TMember>
    {
        FakerOptions<TFakeObject> With(TMember value);
        FakerOptions<TFakeObject> WithFunc(Func<TFakeObject, TMember> evaluatorFunc);
    }
}