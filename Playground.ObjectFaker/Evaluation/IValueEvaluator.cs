using System;

namespace Playground.ObjectFaker
{
    public interface IValuePopulator<in TInput>
    {
        Type OutType { get; }
        object Evaluate(TInput input);
    }

    public interface IValueEvaluator<in TInput, out TOutput> : IValuePopulator<TInput>
    {
        TOutput EvaluateWithType(TInput input);
    }
}