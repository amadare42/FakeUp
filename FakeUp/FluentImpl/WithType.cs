using System;
using FakeUp.Fluent;

namespace FakeUp.FluentImpl
{
    internal class WithType<TFakeObject, TMember> : IWith<TFakeObject, TMember>
    {
        private readonly FakeUpConfig<TFakeObject> config;

        public WithType(FakeUpConfig<TFakeObject> config)
        {
            this.config = config;
        }

        public IFakeUpConfig<TFakeObject> With(TMember constant)
        {
            // TODO: override type filler if it already exists
            this.config.TypeFillers.Add(typeof(TMember), () => constant);
            return this.config;
        }

        public IFakeUpConfig<TFakeObject> With(Func<TMember> func)
        {
            this.config.TypeFillers.Add(typeof(TMember), () => func());
            return this.config;
        }
    }
}