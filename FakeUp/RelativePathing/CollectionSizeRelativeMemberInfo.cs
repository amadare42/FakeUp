using System;

namespace FakeUp.RelativePathing
{
    public class CollectionSizeRelativeMemberInfo : BaseRelativeMemberInfo
    {
        public int Size { get; }

        public CollectionSizeRelativeMemberInfo(int size, Type rootType, Type targetType, CallChain callChain) : base(rootType, targetType, callChain)
        {
            this.Size = size;
        }
    }
}