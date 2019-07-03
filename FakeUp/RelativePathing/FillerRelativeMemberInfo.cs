using System;

namespace FakeUpLib.RelativePathing
{
    public class FillerRelativeMemberInfo : BaseRelativeMemberInfo
    {
        private readonly Func<IObjectCreationContext, object> factoryFunc;

        public FillerRelativeMemberInfo(Func<IObjectCreationContext, object> factoryFunc, CallChain chain, Type rootType, Type targetType) : base(rootType, targetType, chain)
        {
            this.factoryFunc = factoryFunc;
        }

        public object Evaluate(IObjectCreationContext context)
        {
            return this.factoryFunc(context);
        }
    }
}