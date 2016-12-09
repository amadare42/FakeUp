using FakeUp.Tests.Data;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FakeUp.Tests
{
    [TestClass]
    public class TypeFillingTests
    {
        [TestMethod]
        public void FilAll_ShouldFillMembersWithConstant()
        {
            // act
            var holder = FakeUp.NewObject<ValuesHolder<int>>
            (options => options.FillAll<int>().With(42)
            );

            // assert
            holder.Value1.Should().Be(42);
            holder.Value2.Should().Be(42);
        }

        [TestMethod]
        public void FilAll_ShouldFillMembersWithFunc()
        {
            // arrange
            var i = 0;

            // act
            var holder = FakeUp.NewObject<ValuesHolder<int>>
            (options => options.FillAll<int>().With(() => i++)
            );

            // assert
            holder.Value1.Should().Be(0);
            holder.Value2.Should().Be(1);
        }
    }
}