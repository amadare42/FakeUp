using System;

namespace FakeUp.RelativePathing
{
    internal class FillerRelativeMemberInfo : BaseRelativeMemberInfo
    {
        private readonly Func<object> factoryFunc;

        public FillerRelativeMemberInfo(Func<object> factoryFunc, CallChain chain, Type rootType, Type targetType) : base(rootType, targetType, chain)
        {
            this.factoryFunc = factoryFunc;
        }

        public object Evaluate()
        {
            return this.factoryFunc();
        }
    }
}