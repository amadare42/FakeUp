using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Playground.ObjectFaker
{
    internal class WithRelativePath<TFakeObject, TMember, TMetaMember> : IWith<TFakeObject, TMetaMember>
    {
        private readonly FakeUpOptions<TFakeObject> options;
        private readonly Expression<Func<TMember, TMetaMember>> memberExpr;
        private readonly PropertyInfo rootPropertyInfo;

        public WithRelativePath(FakeUpOptions<TFakeObject> options, Expression<Func<TMember, TMetaMember>> memberExpr)
        {
            this.options = options;
            this.memberExpr = memberExpr;
            try
            {
                var memberExpression = (MemberExpression)memberExpr.Body;
                this.rootPropertyInfo = (PropertyInfo)memberExpression.Member;
            }
            catch (Exception e)
            {
                // TODO: extend exception info
                throw new Exception("Wrong expression", e);
            }
        }

        public IFakeUpOptions<TFakeObject> With(TMetaMember constant)
        {
            var relativeMemberInfo = new RelativeMemberInfo(
                () => constant, 
                this.memberExpr.ToCallChain(), 
                typeof (TMember),
                typeof (TMetaMember)
            );
            this.options.RelativeTypeFillers.Add(relativeMemberInfo);
            return this.options;
        }

        public IFakeUpOptions<TFakeObject> With(Func<TMetaMember> func)
        {
            var relativeMemberInfo = new RelativeMemberInfo(
                () => func(),
                this.memberExpr.ToCallChain(),
                typeof(TMember),
                typeof(TMetaMember)
            );
            this.options.RelativeTypeFillers.Add(relativeMemberInfo);
            return this.options;
        }
    }
}