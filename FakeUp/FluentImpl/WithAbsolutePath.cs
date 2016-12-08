using System;
using System.Linq.Expressions;

namespace Playground.ObjectFaker
{
    internal class WithAbsolutePath<TFakeObject, TMember> : IWith<TFakeObject, TMember>
    {
        private readonly FakeUpOptions<TFakeObject> options;
        private readonly Expression<Func<TFakeObject, TMember>> memberPath;

        public WithAbsolutePath(FakeUpOptions<TFakeObject> options, Expression<Func<TFakeObject, TMember>> memberPath)
        {
            this.options = options;
            this.memberPath = memberPath;
        }

        public IFakeUpOptions<TFakeObject> With(TMember constant)
        {
            var callPath = this.memberPath.ToCallPath();
            this.options.RootMemberFillers.Add(callPath, () => constant);
            return this.options;
        }

        public IFakeUpOptions<TFakeObject> With(Func<TMember> func)
        {
            var callPath = this.memberPath.ToCallPath();
            this.options.RootMemberFillers.Add(callPath, () => func());
            return this.options;
        }
    }
}