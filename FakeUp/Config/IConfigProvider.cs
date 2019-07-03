using System;

namespace FakeUpLib.Config
{
    public interface IConfigProvider
    {
        Action<IFakeUpConfig<T>> Create<T>(Action<IFakeUpConfig<T>> conf);
    }
}