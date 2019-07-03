using System.Collections.Generic;
using System.Linq;
using FakeUp.Tests.Data;
using FluentAssertions;
using Xunit;

namespace FakeUp.Tests.Filling
{
    public class CollectionsFillingTests
    {
        [Fact]
        public void NewObject_Array_SingleElementByDefault()
        {
            // Act
            var holder = FakeUpLib.FakeUp.NewObject<ValuesHolder<int[]>>();

            // Assert
            holder.Value1.Should().HaveCount(1);
        }

        [Fact]
        public void FillAll_Array_FillValues()
        {
            // Act
            var holder = FakeUpLib.FakeUp.NewObject<ValuesHolder<int[]>>(opt =>
                opt.FillAll<int>().With(42)
            );

            // Assert
            holder.Value1[0].Should().Be(42);
        }

        [Fact]
        public void NewObject_List_SingleElementByDefault()
        {
            // Act
            var holder = FakeUpLib.FakeUp.NewObject<ValuesHolder<List<int>>>();

            // Assert
            holder.Value1.Should().HaveCount(1);
        }

        [Fact]
        public void FillAll_List_FillValues()
        {
            // Act
            var holder = FakeUpLib.FakeUp.NewObject<ValuesHolder<List<int>>>(opt =>
                opt.FillAll<int>().With(42)
            );

            // Assert
            holder.Value1.Should().BeEquivalentTo(new[] {42});
        }

        [Fact]
        public void FillAll_IEnumerable_ArrayCreated()
        {
            // Act
            var holder = FakeUpLib.FakeUp.NewObject<ValuesHolder<IEnumerable<int>>>(opt =>
                opt.FillAll<int>().With(42)
            );

            // Assert
            holder.Value1.GetType().IsArray.Should().BeTrue();
            holder.Value1.First().Should().Be(42);
        }

        [Fact]
        public void WithCollectionsSize_CreateCollectionsWithSpecifiedSize()
        {
            // Act
            var instance = FakeUpLib.FakeUp.NewObject<ValuesHolder<int[]>>(o => o.WithCollectionsSize(42));

            // Assert
            instance.Value1.Should().HaveCount(42);
        }

        [Fact]
        public void WithCollectionSize_AbsolutePath_CreateCollectionsWithSpecifiedSize()
        {
            // Act
            var instance = FakeUpLib.FakeUp.NewObject<ValuesHolder<int[]>>(o => o
                .WithCollectionsSize(3)
                .WithCollectionsSize(holder => holder.Value1, 1)
                .WithCollectionsSize(holder => holder.Value2, 2)
            );

            // Assert
            instance.Value1.Should().HaveCount(1);
            instance.Value2.Should().HaveCount(2);
            instance.Value3.Should().HaveCount(3);
        }

        [Fact]
        public void WithCollectionsSize_Type_CreateCollectionsWithSpecifiedSize()
        {
            // Act
            var instance = FakeUpLib.FakeUp.NewObject<ValuesHolder<int[], long[]>>(o => o
                .WithCollectionsSize<int[]>(1)
                .WithCollectionsSize<long[]>(2)
            );

            // Assert
            instance.Value1.Should().HaveCount(1);
            instance.Value2.Should().HaveCount(2);
        }

        [Fact]
        public void WithCollectionsSize_RelativePaths_CreateCollectionsWithSpecifiedSize()
        {
            // Act
            var instance = FakeUpLib.FakeUp.NewObject<ValuesHolder<MetaIntArrayHolder>>(o => o
               .WithCollectionsSize((IntArrayHolder holder) => holder.IntArray1, 1)
               .WithCollectionsSize((IntArrayHolder holder) => holder.IntArray2, 2)
               .WithCollectionsSize<int[]>(3)
            );

            // Assert
            instance.Value1.Holder.IntArray1.Should().HaveCount(1);
            instance.Value1.Holder.IntArray2.Should().HaveCount(2);
            instance.Value1.Holder.IntArray3.Should().HaveCount(3);
            instance.Value1.IntArray.Should().HaveCount(3);
        }
    }
}