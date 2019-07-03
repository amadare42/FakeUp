using System;
using System.Linq.Expressions;
using FakeUpLib.Config;
using FakeUpLib.Extensions;
using FakeUpLib.RelativePathing;

namespace FakeUpLib.Fluent.Implementation
{
    internal class WithRelativePath<TFakeObject, TMember, TMetaMember> : IWith<TFakeObject, TMetaMember>
    {
        private readonly Expression<Func<TMember, TMetaMember>> memberExpr;
        private readonly FakeUpConfig<TFakeObject> config;

        public WithRelativePath(FakeUpConfig<TFakeObject> config, Expression<Func<TMember, TMetaMember>> memberExpr)
        {
            this.config = config;
            this.memberExpr = memberExpr;
        }

        public IFakeUpConfig<TFakeObject> With(TMetaMember constant)
        {
            var relativeMemberInfo = new FillerRelativeMemberInfo(
                (_) => constant,
                this.memberExpr.ToCallChain(),
                typeof(TMember),
                typeof(TMetaMember)
            );
            this.config.RelativeTypeFillers.Add(relativeMemberInfo);
            return this.config;
        }

        public IFakeUpConfig<TFakeObject> With(Func<TMetaMember> func)
        {
            var relativeMemberInfo = new FillerRelativeMemberInfo(
                (_) => func(),
                this.memberExpr.ToCallChain(),
                typeof(TMember),
                typeof(TMetaMember)
            );
            this.config.RelativeTypeFillers.Add(relativeMemberInfo);
            return this.config;
        }

        public IFakeUpConfig<TFakeObject> With(Func<IObjectCreationContext, TMetaMember> func)
        {
            var relativeMemberInfo = new FillerRelativeMemberInfo(
                (ctx) => func(ctx),
                this.memberExpr.ToCallChain(),
                typeof(TMember),
                typeof(TMetaMember)
            );
            this.config.RelativeTypeFillers.Add(relativeMemberInfo);
            return this.config;
        }

        public IFakeUpConfig<TFakeObject> With(Action<IFakeUpConfig<TMetaMember>> configOverride)
        {
            var relativeMemberInfo = new FillerRelativeMemberInfo(
                (_) => FakeUp.NewObject(configOverride),
                this.memberExpr.ToCallChain(),
                typeof(TMember),
                typeof(TMetaMember)
            );
            this.config.RelativeTypeFillers.Add(relativeMemberInfo);
            return this.config;
        }
    }
}