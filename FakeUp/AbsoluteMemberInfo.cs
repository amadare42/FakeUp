using System;

namespace FakeUp
{
    internal class AbsoluteMemberInfo
    {
        private readonly Func<object> factoryFunc;

        public AbsoluteMemberInfo(Func<object> factoryFunc, string absolutePath, Type rootType)
        {
            this.AbsolutePath = absolutePath;
            this.RootType = rootType;
            this.factoryFunc = factoryFunc;
        }

        public Type RootType { get; set; }
        public string AbsolutePath { get; set; }

        public object Evaluate()
        {
            return this.factoryFunc();
        }
    }
}