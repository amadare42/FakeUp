using System;

namespace Playground.ObjectFaker
{
    internal class WithType<TFakeObject, TMember> : IWith<TFakeObject, TMember>
    {
        private readonly FakeUpOptions<TFakeObject> options;

        public WithType(FakeUpOptions<TFakeObject> options)
        {
            this.options = options;
        }

        public IFakeUpOptions<TFakeObject> With(TMember constant)
        {
            this.options.TypeFillers.Add(typeof(TMember), () => constant);
            return this.options;
        }

        public IFakeUpOptions<TFakeObject> With(Func<TMember> func)
        {
            this.options.TypeFillers.Add(typeof(TMember), () => func());
            return this.options;
        }
    }
}