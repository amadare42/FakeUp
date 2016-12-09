using System;
using FakeUp.Fluent;

namespace FakeUp.FluentImpl
{
    internal class WithCollectionType<TFakeObject, TCollection> : ICollectionWith<TFakeObject>
    {
        private readonly FakeUpConfig<TFakeObject> config;

        public WithCollectionType(FakeUpConfig<TFakeObject> config)
        {
            this.config = config;
        }

        public IFakeUpConfig<TFakeObject> With(Func<int, object> func)
        {
            this.config.ElementsTypeFillers.Add(typeof(TCollection), func);
            return this.config;
        }

        public IFakeUpConfig<TFakeObject> With(object constant)
        {
            this.config.ElementsTypeFillers.Add(typeof(TCollection), index => constant);
            return this.config;
        }

        public IFakeUpConfig<TFakeObject> With(Func<object> func)
        {
            this.config.ElementsTypeFillers.Add(typeof(TCollection), index => func());
            return this.config;
        }
    }
}