﻿using System;
using FakeUp.Config;

namespace FakeUp
{
    public static class FakeUp
    {
        public const int DefaultCollectionElementCount = 1;


        public static T NewObject<T>()
        {
            var config = new FakeUpConfig<T>();
            var instance = (T) NewObject(typeof(T), new ObjectCreationContext<T>(config));
            return instance;
        }

        public static object NewObject(Type type, Action<IFakeUpConfig<object>> conf)
        {
            var config = GetConfig(conf);
            var instance = NewObject(type, new ObjectCreationContext<object>(config));
            return instance;
        }

        public static T NewObject<T>(Action<IFakeUpConfig<T>> conf)
        {
            var config = GetConfig(conf);
            var instance = (T) NewObject(typeof(T), new ObjectCreationContext<T>(config));
            return instance;
        }

        public static IConfigProvider Config
        {
            get { return new ConfigProvider(); }
        }

        private static FakeUpConfig<T> GetConfig<T>(Action<IFakeUpConfig<T>> action)
        {
            var opt = new FakeUpConfig<T>();
            action?.Invoke(opt);
            return opt;
        }

        internal static object NewObject(Type type, IObjectCreationContext context)
        {
            foreach (var evaluator in context.Evaluators)
            {
                object instance;
                if (evaluator.TryEvaluate(type, context, out instance))
                {
                    return instance;
                }
            }

            throw new FillingException($"Cannot fill member '{context.InvocationPath}' of type '{type.FullName}'.");
        }
    }
}