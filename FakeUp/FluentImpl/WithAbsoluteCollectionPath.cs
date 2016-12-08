using System;
using System.Linq.Expressions;

namespace Playground.ObjectFaker
{
    class WithAbsoluteCollectionPath<TFakeObject, TCollection> : ICollectionWith<TFakeObject>
    {
        private readonly Expression<Func<TFakeObject, TCollection>> memberPath;
        private readonly FakeUpOptions<TFakeObject> options;

        public WithAbsoluteCollectionPath(Expression<Func<TFakeObject, TCollection>> memberPath, 
            FakeUpOptions<TFakeObject> options)
        {
            this.memberPath = memberPath;
            this.options = options;
        }

        public IFakeUpOptions<TFakeObject> With(Func<int, object> func)
        {
            var callPath = this.memberPath.ToCallPath();
            this.options.AbsoluteElementsFillers.Add(callPath, func);
            return this.options;
        }

        public IFakeUpOptions<TFakeObject> With(object constant)
        {
            var callPath = this.memberPath.ToCallPath();
            this.options.AbsoluteElementsFillers.Add(callPath, (index) => constant);
            return this.options;
        }

        public IFakeUpOptions<TFakeObject> With(Func<object> func)
        {
            var callPath = this.memberPath.ToCallPath();
            this.options.AbsoluteElementsFillers.Add(callPath, (index) => func());
            return this.options;
        }
    }
}