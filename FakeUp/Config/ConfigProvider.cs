using System;

namespace FakeUpLib.Config
{
    internal class ConfigProvider : IConfigProvider
    {
        public Action<IFakeUpConfig<T>> Create<T>(Action<IFakeUpConfig<T>> conf)
        {
            return conf;
        }
    }
}