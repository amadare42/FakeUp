using System;

namespace Playground.ObjectFaker
{
    internal class WithTypeFiller<TFakeObject, TMember> : IWith<TFakeObject, TMember>
    {
        private readonly FakerOptions<TFakeObject> options;

        public WithTypeFiller(FakerOptions<TFakeObject> options)
        {
            this.options = options;
        }

        public IOptions<TFakeObject> With(TMember constant)
        {
            this.options.TypeFillers.Add(typeof(TMember), () => constant);
            return this.options;
        }

        public IOptions<TFakeObject> With(Func<TMember> func)
        {
            this.options.TypeFillers.Add(typeof(TMember), () => func());
            return this.options;
        }
    }
}