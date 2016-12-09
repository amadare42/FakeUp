using System;

namespace FakeUp.Config
{
    internal class ConfigProvider : IConfigProvider
    {
        public Action<IFakeUpConfig<T>> Create<T>(Action<IFakeUpConfig<T>> conf)
        {
            return conf;
        }
    }
}