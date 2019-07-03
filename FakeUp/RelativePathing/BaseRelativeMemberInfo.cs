using System;

namespace FakeUp.RelativePathing
{
    public abstract class BaseRelativeMemberInfo
    {
        protected BaseRelativeMemberInfo(Type rootType, Type targetType, CallChain callChain)
        {
            this.RootType = rootType;
            this.TargetType = targetType;
            this.CallChain = callChain;
        }

        public Type RootType { get; set; }
        public Type TargetType { get; set; }
        public CallChain CallChain { get; set; }
    }
}