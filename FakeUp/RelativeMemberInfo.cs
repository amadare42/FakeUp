using System;

namespace FakeUp
{
    internal class RelativeMemberInfo
    {
        private readonly Func<object> factoryFunc;

        public RelativeMemberInfo(Func<object> factoryFunc, CallChain chain, Type rootType, Type targetType)
        {
            this.CallChain = chain;
            this.RootType = rootType;
            this.TargetType = targetType;
            this.factoryFunc = factoryFunc;
        }

        public Type RootType { get; set; }
        public Type TargetType { get; set; }
        public CallChain CallChain { get; set; }

        public object Evaluate()
        {
            return this.factoryFunc();
        }
    }
}