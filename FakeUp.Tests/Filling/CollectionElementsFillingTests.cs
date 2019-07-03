using System.Linq;
using FakeUp.Tests.Data;
using FluentAssertions;
using Xunit;

namespace FakeUp.Tests.Filling
{
    public class CollectionElementsFillingTests
    {
        [Fact]
        public void FillElementsOf_WithConstants_FillElements()
        {
            // Act
            var instance = FakeUpLib.FakeUp.NewObject<IntListValueHolder>(options =>
                options.FillElementsOf<int[]>().With(42)
            );

            // Assert
            instance.Value1.Should().BeEquivalentTo(new[] {42});
        }

        [Fact]
        public void FillElementsOf_FillAllSpecified_FillElementsOfPrioritized()
        {
            // Act
            var instance = FakeUpLib.FakeUp.NewObject<IntListValueHolder>(options => options
                .FillAll<int>().With(1)
                .FillElementsOf<int[]>().With(2)
            );

            // Assert
            instance.Value1.ShouldAllBeEquivalentTo(2);
            instance.Value2.ShouldAllBeEquivalentTo(1);
        }

        [Fact]
        public void FillElementsOf_WithFunc_FillSuccessfully()
        {
            // Arrange
            var collectionsSize = 10;

            // Act
            var instance = FakeUpLib.FakeUp.NewObject<IntListValueHolder>(options => options
                .WithCollectionsSize(collectionsSize)
                .FillElementsOf<int[]>().With(index => index + 100)
            );

            // Assert
            var expectedValues = Enumerable.Range(0, collectionsSize).Select(i => i + 100);
            instance.Value1.ShouldBeEquivalentTo(expectedValues, opt => opt.WithStrictOrdering());
        }

        [Fact]
        public void FillElementsOf_WithFuncWithoutArg_FillSuccessfully()
        {
            // Arrange
            var i = 0;

            // Act
            var instance = FakeUpLib.FakeUp.NewObject<ValuesHolder<int[]>>(options =>
                options.FillElementsOf<int[]>().With(() => i++)
            );

            // Assert
            instance.Value1.ShouldAllBeEquivalentTo(0);
            instance.Value2.ShouldAllBeEquivalentTo(1);
        }

        [Fact]
        public void FillElementsOf_AbsolutePath_FillConstants()
        {
            // Act
            var instance = FakeUpLib.FakeUp.NewObject<ValuesHolder<int[]>>(options => options
                .FillElementsOf(holder => holder.Value1).With(1)
                .FillElementsOf(holder => holder.Value2).With(2)
            );

            // Assert
            instance.Value1.ShouldAllBeEquivalentTo(1);
            instance.Value2.ShouldAllBeEquivalentTo(2);
            instance.Value3.ShouldAllBeEquivalentTo(0);
        }

        [Fact]
        public void FillElementsOf_AbsolutePath_FillFromParameterlessFunc()
        {
            // Arrange
            var i = 0;

            // Act
            var instance = FakeUpLib.FakeUp.NewObject<ValuesHolder<int[]>>(options => options
                .FillElementsOf(holder => holder.Value1).With(() => ++i)
                .FillElementsOf(holder => holder.Value2).With(() => ++i)
            );

            // Assert
            instance.Value1.ShouldAllBeEquivalentTo(1);
            instance.Value2.ShouldAllBeEquivalentTo(2);
            instance.Value3.ShouldAllBeEquivalentTo(0);
        }

        [Fact]
        public void FillElementsOf_AbsolutePath_FillFromFunc()
        {
            // Arrange
            var collectionSize = 10;

            // Act
            var instance = FakeUpLib.FakeUp.NewObject<ValuesHolder<int[]>>(options => options
                .WithCollectionsSize(collectionSize)
                .FillElementsOf(holder => holder.Value1).With(index => index * 2)
            );

            // Assert
            var expectedValues = Enumerable.Range(0, collectionSize).Select(i => i * 2);
            instance.Value1.ShouldBeEquivalentTo(expectedValues);
        }
    }
}