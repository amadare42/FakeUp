using System;
using System.Linq.Expressions;
using FakeUp.Config;
using FakeUp.Extensions;
using FakeUp.RelativePathing;

namespace FakeUp.Fluent.Implementation
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
                () => constant,
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
                () => func(),
                this.memberExpr.ToCallChain(),
                typeof(TMember),
                typeof(TMetaMember)
            );
            this.config.RelativeTypeFillers.Add(relativeMemberInfo);
            return this.config;
        }
    }
}