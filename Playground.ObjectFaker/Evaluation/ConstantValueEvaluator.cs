using System;

namespace Playground.ObjectFaker
{
    class ConstantValueEvaluator<TInput, TOutput> : IValueEvaluator<TInput, TOutput>
    {
        private readonly TOutput value;
        public Type OutType => typeof (TOutput);

        public ConstantValueEvaluator(TOutput value)
        {
            this.value = value;
        }

        public object Evaluate(TInput input)
        {
            return value;
        }

        public TOutput EvaluateWithType(TInput input)
        {
            return value;
        }
    }
}