using System;
using System.Collections.Generic;
using FakeUpLib.Exceptions;

namespace FakeUpLib.States
{
    internal class StatesConfig
    {
        private readonly Dictionary<Type, Dictionary<string, Func<object>>> dict = new Dictionary<Type, Dictionary<string, Func<object>>>();

        public void Add<T>(string tag, Func<T> factory)
        {
            var type = typeof(T);
            if (this.dict.TryGetValue(type, out var states))
            {
                if (states.ContainsKey(tag))
                {
                    throw new StateAlreadyPresentException(type, tag);
                }

                states[tag] = () => factory();
                return;
            }

            this.dict[type] = new Dictionary<string, Func<object>>
            {
                {tag, () => factory()}
            };
        }

        public StatesRepository GetRepository() => new StatesRepository(this.dict);
    }
}