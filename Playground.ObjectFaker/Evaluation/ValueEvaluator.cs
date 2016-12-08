using System;

namespace Playground.ObjectFaker
{
    class ValueEvaluator<TInput, TOutput> : IValueEvaluator<TInput, TOutput>
    {
        private readonly Func<TInput, TOutput> func;

        public ValueEvaluator(Func<TInput, TOutput> func)
        {
            this.func = func;
        }

        public Type OutType => typeof (TOutput);

        public object Evaluate(TInput input)
        {
            return func(input);
        }

        public TOutput EvaluateWithType(TInput input)
        {
            return func(input);
        }
    }
}