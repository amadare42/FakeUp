using System;

namespace Playground.ObjectFaker
{
    class WithCollectionType<TFakeObject, TCollection> : ICollectionWith<TFakeObject>
    {
        private readonly FakeUpOptions<TFakeObject> options;

        public WithCollectionType(FakeUpOptions<TFakeObject> options)
        {
            this.options = options;
        }

        public IFakeUpOptions<TFakeObject> With(Func<int, object> func)
        {
            this.options.ElementsTypeFillers.Add(typeof(TCollection), func);
            return this.options;
        }

        public IFakeUpOptions<TFakeObject> With(object constant)
        {
            this.options.ElementsTypeFillers.Add(typeof(TCollection), (index) => constant);
            return this.options;
        }

        public IFakeUpOptions<TFakeObject> With(Func<object> func)
        {
            this.options.ElementsTypeFillers.Add(typeof(TCollection), (index) => func());
            return this.options;
        }
    }
}