using FakeUp.Tests.Data;
using FluentAssertions;
using Xunit;

namespace FakeUp.Tests.Filling
{
    public class TypeFillingTests
    {
        [Fact]
        public void FilAll_FillMembersWithConstant()
        {
            // Act
            var holder = FakeUpLib.FakeUp.NewObject<ValuesHolder<int>>(options => options
                .FillAll<int>().With(42)
            );

            // Assert
            holder.Value1.Should().Be(42);
            holder.Value2.Should().Be(42);
        }

        [Fact]
        public void FilAll_FillMembersWithFunc()
        {
            // Arrange
            var i = 0;

            // Act
            var holder = FakeUpLib.FakeUp.NewObject<ValuesHolder<int>>(options => options
                .FillAll<int>().With(() => i++)
            );

            // Assert
            holder.Value1.Should().Be(0);
            holder.Value2.Should().Be(1);
        }
    }
}