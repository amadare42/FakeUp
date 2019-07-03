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
            this.config.AbsolutePathFillers[callPath] = (_) => constant;
            return this.config;
        }

        public IFakeUpConfig<TFakeObject> With(Func<TMember> func)
        {
            var callPath = this.memberPath.ToCallPath();
            this.config.AbsolutePathFillers[callPath] = (_) => func();
            return this.config;
        }

        public IFakeUpConfig<TFakeObject> With(Func<IObjectCreationContext, TMember> func)
        {
            var callPath = this.memberPath.ToCallPath();
            this.config.AbsolutePathFillers[callPath] = (ctx) => func(ctx);
            return this.config;
        }

        public IFakeUpConfig<TFakeObject> With(Action<IFakeUpConfig<TMember>> configOverride)
        {
            var callPath = this.memberPath.ToCallPath();
            this.config.AbsolutePathFillers[callPath] = (_) => FakeUp.NewObject(configOverride);
            return this.config;
        }
    }
}