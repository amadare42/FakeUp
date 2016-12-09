using System;

namespace FakeUp
{
    public interface IConfigProvider
    {
        Action<IFakeUpConfig<T>> Create<T>(Action<IFakeUpConfig<T>> conf);
    }

    internal class ConfigProvider : IConfigProvider
    {
        public Action<IFakeUpConfig<T>> Create<T>(Action<IFakeUpConfig<T>> conf)
        {
            return conf;
        }
    }
}