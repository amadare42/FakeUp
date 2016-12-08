using System;

namespace Playground.ObjectFaker
{
    internal class AbsoluteMemberInfo
    {
        private readonly Func<object> factoryFunc;
        public Type RootType { get; set; }
        public string AbsolutePath { get; set; }

        public AbsoluteMemberInfo(Func<object> factoryFunc, string absolutePath, Type rootType)
        {
            this.AbsolutePath = absolutePath;
            this.RootType = rootType;
            this.factoryFunc = factoryFunc;
        }

        public object Evaluate()
        {
            return this.factoryFunc();
        }
    }
}