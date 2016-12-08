using System;

namespace Playground.ObjectFaker
{
    internal class RelativeMemberInfo
    {
        private readonly Func<object> factoryFunc;
        public Type RootType { get; set; }
        public Type TargetType { get; set; }
        public CallChain CallChain { get; set; }

        public RelativeMemberInfo(Func<object> factoryFunc, CallChain chain, Type rootType, Type targetType)
        {
            CallChain = chain;
            RootType = rootType;
            TargetType = targetType;
            this.factoryFunc = factoryFunc;
        }

        public object Evaluate()
        {
            return this.factoryFunc();
        }
    }
}