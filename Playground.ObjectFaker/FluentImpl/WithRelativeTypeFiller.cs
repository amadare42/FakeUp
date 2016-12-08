using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Playground.ObjectFaker
{
    internal class WithRelativeTypeFiller<TFakeObject, TMember, TMetaMember> : IWith<TFakeObject, TMetaMember>
    {
        private readonly FakerOptions<TFakeObject> options;
        private readonly Expression<Func<TMember, TMetaMember>> memberExpr;
        private readonly PropertyInfo rootPropertyInfo;

        public WithRelativeTypeFiller(FakerOptions<TFakeObject> options, Expression<Func<TMember, TMetaMember>> memberExpr)
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

        public IOptions<TFakeObject> With(TMetaMember constant)
        {
            var relativeMemberInfo = new RelativeMemberInfo(() => constant)
            {
                RelativePath = this.memberExpr.ToCallPath(),
                RootType = typeof(TMember)
            };
            this.options.RelativeTypeFillers.Add(typeof(TMetaMember), relativeMemberInfo);
            return this.options;
        }

        public IOptions<TFakeObject> With(Func<TMetaMember> func)
        {
            throw new NotImplementedException();
        }
    }
}