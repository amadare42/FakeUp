using System.Collections.Generic;
using FakeUp.Extensions;
using FakeUp.Tests.Data;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FakeUp.Tests.ExtensionsTests
{
    [TestClass]
    public class ExpressionExtensionsTests
    {
        [TestMethod]
        public void ShouldSplitToCallInfosSuccessfully()
        {
            // act
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