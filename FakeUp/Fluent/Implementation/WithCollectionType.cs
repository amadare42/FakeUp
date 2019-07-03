using System;
using FakeUpLib.Config;

namespace FakeUpLib.Fluent.Implementation
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
            this.config.TypeElementsFillers[typeof(TCollection)] = func;
            return this.config;
        }

        public IFakeUpConfig<TFakeObject> With(object constant)
        {
            this.config.TypeElementsFillers[typeof(TCollection)] = index => constant;
            return this.config;
        }

        public IFakeUpConfig<TFakeObject> With(Func<object> func)
        {
            this.config.TypeElementsFillers[typeof(TCollection)] = index => func();
            return this.config;
        }
    }
}