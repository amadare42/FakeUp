using System.Collections.Generic;
using FakeUp.Tests.Data;
using FakeUpLib;
using FakeUpLib.Extensions;
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
                new CallInfo(typeof(ValuesHolder<MetaIntHolder>).GetProperty("Value1")),
                new CallInfo(typeof(MetaIntHolder).GetProperty("Holder")),
                new CallInfo(typeof(IntHolder).GetProperty("IntValue2"))
            };
            calls.ShouldBeEquivalentTo(expected, opt => opt.WithStrictOrdering());
        }
    }
}