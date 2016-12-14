using System;
using System.Linq.Expressions;
using FakeUp.Config;
using FakeUp.Extensions;

namespace FakeUp.Fluent.Implementation
{
    internal class WithAbsolutePath<TFakeObject, TMember> : IWith<TFakeObject, TMember>
    {
        private readonly Expression<Func<TFakeObject, TMember>> memberPath;
        private readonly FakeUpConfig<TFakeObject> config;

        public WithAbsolutePath(FakeUpConfig<TFakeObject> config, Expression<Func<TFakeObject, TMember>> memberPath)
        {
            this.config = config;
            this.memberPath = memberPath;
        }

        public IFakeUpConfig<TFakeObject> With(TMember constant)
        {
            var callPath = this.memberPath.ToCallPath();
            this.config.AbsolutePathFillers[callPath] = () => constant;
            return this.config;
        }

        public IFakeUpConfig<TFakeObject> With(Func<TMember> func)
        {
            var callPath = this.memberPath.ToCallPath();
            this.config.AbsolutePathFillers[callPath] = () => func();
            return this.config;
        }
    }
}