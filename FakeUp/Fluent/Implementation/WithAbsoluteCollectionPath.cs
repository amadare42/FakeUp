using System;
using System.Linq.Expressions;
using FakeUpLib.Config;
using FakeUpLib.Extensions;

namespace FakeUpLib.Fluent.Implementation
{
    internal class WithAbsoluteCollectionPath<TFakeObject, TCollection> : ICollectionWith<TFakeObject>
    {
        private readonly Expression<Func<TFakeObject, TCollection>> memberPath;
        private readonly FakeUpConfig<TFakeObject> config;

        public WithAbsoluteCollectionPath(Expression<Func<TFakeObject, TCollection>> memberPath,
            FakeUpConfig<TFakeObject> config)
        {
            this.memberPath = memberPath;
            this.config = config;
        }

        public IFakeUpConfig<TFakeObject> With(Func<int, object> func)
        {
            var callPath = this.memberPath.ToCallPath();
            this.config.AbsoluteElementsFillers[callPath] = func;
            return this.config;
        }

        public IFakeUpConfig<TFakeObject> With(object constant)
        {
            var callPath = this.memberPath.ToCallPath();
            this.config.AbsoluteElementsFillers[callPath] = index => constant;
            return this.config;
        }

        public IFakeUpConfig<TFakeObject> With(Func<object> func)
        {
            var callPath = this.memberPath.ToCallPath();
            this.config.AbsoluteElementsFillers[callPath] = index => func();
            return this.config;
        }
    }
}