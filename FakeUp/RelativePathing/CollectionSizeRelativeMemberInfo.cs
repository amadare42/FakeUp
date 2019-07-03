using System;

namespace FakeUpLib.RelativePathing
{
    public class CollectionSizeRelativeMemberInfo : BaseRelativeMemberInfo
    {
        public int Size { get; }

        public CollectionSizeRelativeMemberInfo(int size, Type rootType, Type targetType, CallChain callChain) : base(rootType, targetType, callChain)
        {
            this.Size = size;
        }

        public override string ToString()
        {
            return $"RelativeMember[{this.RootType} {this.CallChain}]";
        }
    }
}