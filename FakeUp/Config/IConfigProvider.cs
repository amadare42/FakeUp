using System;

namespace FakeUp.Config
{
    public interface IConfigProvider
    {
        Action<IFakeUpConfig<T>> Create<T>(Action<IFakeUpConfig<T>> conf);
    }
}