using System.Collections.Generic;
using FakeUp.Extensions;
using FakeUp.Tests.Data;
using FluentAssertions;
using Xunit;

namespace FakeUp.Tests.ExtensionsTests
{
    public class ExpressionExtensionsTests
    {
        [Fact]
        public void SplitToCalls_SplitToCallInfos()
        {
            // Act
            var calls =
                ExpressionExtensions.SplitToCalls<ValuesHolder<MetaIntHolder>, int>(h => h.Value1.Holder.IntValue2);

            //assert
            var expected = new List<CallInfo>
            {
                new CallInfo(typeof(MetaIntHolder), "Value1"),
                new CallInfo(typeof(IntHolder), "Holder"),
                new CallInfo(typeof(int), "IntValue2")
            };
            calls.ShouldBeEquivalentTo(expected, opt => opt.WithStrictOrdering());
        }
    }
}