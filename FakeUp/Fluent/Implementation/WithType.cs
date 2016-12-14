using System;
using FakeUp.Config;

namespace FakeUp.Fluent.Implementation
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
            this.config.TypeFillers[typeof(TMember)] = () => constant;
            return this.config;
        }

        public IFakeUpConfig<TFakeObject> With(Func<TMember> func)
        {
            this.config.TypeFillers[typeof(TMember)] = () => func();
            return this.config;
        }
    }
}