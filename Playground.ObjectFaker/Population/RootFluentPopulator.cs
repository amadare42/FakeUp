using System;
using System.Linq.Expressions;

namespace Playground.ObjectFaker
{
    class RootFluentPopulator<TFakeObject, TMember> : IFluentPopulator<TFakeObject, TMember>
    {
        private readonly FakerOptions<TFakeObject> options;
        private readonly Expression<Func<TFakeObject, TMember>> expr;

        public RootFluentPopulator(FakerOptions<TFakeObject> options, Expression<Func<TFakeObject, TMember>> expr)
        {
            this.options = options;
            this.expr = expr;
        }

        public FakerOptions<TFakeObject> With(TMember constantValue)
        {
            var callPath = expr.ToCallPath();
            var evaluator = new ConstantValueEvaluator<TFakeObject, TMember>(constantValue);
            options.PopulateRootExpressions.Add(callPath, evaluator);
            return options;
        }

        public FakerOptions<TFakeObject> WithFunc(Func<TFakeObject, TMember> evaluatorFunc)
        {
            var callPath = expr.ToCallPath();
            var evaluator = new ValueEvaluator<TFakeObject, TMember>(evaluatorFunc);
            options.PopulateRootExpressions.Add(callPath, evaluator);
            return options;
        }
    }
}