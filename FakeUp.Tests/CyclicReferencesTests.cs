using FakeUp.Tests.Data;
using FluentAssertions;
using Xunit;

namespace FakeUp.Tests
{
    public class CyclicReferencesTests
    {
        [Fact]
        public void FillAll_CyclicReferences_FillFirst4ByDefault()
        {
            // Act
            var instance = FakeUp.NewObject<CyclicHolder<int>>(o => o
                .FillAll<int>().With(5)
            );
            
            // Assert

            var holder = instance;
            var nestingDepth = 0;
            while (holder != null)
            {
                holder = holder.InnerHolder;
                nestingDepth++;
            }

            nestingDepth.Should().Be(4);
        }
    }
}