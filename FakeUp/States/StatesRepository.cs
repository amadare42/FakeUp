using System;
using System.Collections.Generic;
using System.Linq;
using FakeUpLib.Exceptions;

namespace FakeUpLib.States
{
    internal class StatesRepository
    {
        private readonly Dictionary<Type, Dictionary<string, Lazy<object>>> lazyStates;
        
        public StatesRepository(Dictionary<Type, Dictionary<string, Func<object>>> factories)
        {
            this.lazyStates = factories.ToDictionary(
                typePair => typePair.Key,
                typePair => typePair.Value.ToDictionary(
                    factoryPair => factoryPair.Key,
                    factoryPair => new Lazy<object>(factoryPair.Value)
                )
            );
        }

        public T GetState<T>(string tag)
        {
            if (this.lazyStates.TryGetValue(typeof(T), out var lazies) && lazies.TryGetValue(tag, out var lazy))
            {
                return (T) lazy.Value;
            }

            throw new CannotLocateStateException(typeof(T), tag);
        }
    }
}