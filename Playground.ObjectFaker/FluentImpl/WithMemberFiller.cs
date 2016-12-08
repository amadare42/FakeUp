using System;
using System.Linq.Expressions;

namespace Playground.ObjectFaker
{
    internal class WithMemberFiller<TFakeObject, TMember> : IWith<TFakeObject, TMember>
    {
        private readonly FakerOptions<TFakeObject> options;
        private readonly Expression<Func<TFakeObject, TMember>> memberPath;

        public WithMemberFiller(FakerOptions<TFakeObject> options, Expression<Func<TFakeObject, TMember>> memberPath)
        {
            this.options = options;
            this.memberPath = memberPath;
        }

        public IOptions<TFakeObject> With(TMember constant)
        {
            var callPath = this.memberPath.ToCallPath();
            this.options.RootMemberFillers.Add(callPath, () => constant);
            return this.options;
        }

        public IOptions<TFakeObject> With(Func<TMember> func)
        {
            var callPath = this.memberPath.ToCallPath();
            this.options.RootMemberFillers.Add(callPath, () => func());
            return this.options;
        }
    }
}