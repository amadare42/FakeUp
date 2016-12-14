using System.Collections.Generic;
using System.Linq;
using FakeUp.Tests.Data;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FakeUp.Tests.Filling
{
    [TestClass]
    public class CollectionsFillingTests
    {
        [TestMethod]
        public void ShouldCreateArrayWithOneElementByDefault()
        {
            // act
            var holder = FakeUp.NewObject<ValuesHolder<int[]>>();

            // assert
            holder.Value1.Length.Should().Be(1);
        }

        [TestMethod]
        public void ShouldFillValuesInArrays()
        {
            // act
            var holder = FakeUp.NewObject<ValuesHolder<int[]>>(opt =>
                        opt.FillAll<int>().With(42)
            );

            // assert
            holder.Value1[0].Should().Be(42);
        }

        [TestMethod]
        public void ShouldCreateListWithOneElementByDefault()
        {
            // act
            var holder = FakeUp.NewObject<ValuesHolder<List<int>>>();

            // assert
            holder.Value1.Count.Should().Be(1);
        }

        [TestMethod]
        public void ShouldFillValuesInLists()
        {
            // act
            var holder = FakeUp.NewObject<ValuesHolder<List<int>>>(opt =>
                        opt.FillAll<int>().With(42)
            );

            // assert
            holder.Value1[0].Should().Be(42);
        }

        [TestMethod]
        public void ShouldCreateArrayForIEnumerable()
        {
            // act
            var holder = FakeUp.NewObject<ValuesHolder<IEnumerable<int>>>(opt =>
                        opt.FillAll<int>().With(42)
            );

            // assert
            holder.Value1.GetType().IsArray.Should().BeTrue();
            holder.Value1.First().Should().Be(42);
        }

        [TestMethod]
        public void ShouldFillCollectionWithSpecifiedElementsCount()
        {
            // act
            var instance = FakeUp.NewObject<ValuesHolder<int[]>>(o => o
                    .WithCollectionsSize(42)
            );

            // assert
            instance.Value1.Length.Should().Be(42);
        }

        [TestMethod]
        public void ShouldFillCollectionsWithAbsolutePathSpecifiedSize()
        {
            // act
            var instance = FakeUp.NewObject<ValuesHolder<int[]>>(o => o
                    .WithCollectionsSize(3)
                    .WithCollectionsSize(holder => holder.Value1, 1)
                    .WithCollectionsSize(holder => holder.Value2, 2)
            );

            // assert
            instance.Value1.Length.Should().Be(1);
            instance.Value2.Length.Should().Be(2);
            instance.Value3.Length.Should().Be(3);
        }

        [TestMethod]
        public void ShouldFillCollectionWithTypeSpecifiedSize()
        {
            // act
            var instance = FakeUp.NewObject<ValuesHolder<int[], long[]>>(o => o
                    .WithCollectionsSize<int[]>(1)
                    .WithCollectionsSize<long[]>(2)
            );

            // assert
            instance.Value1.Length.Should().Be(1);
            instance.Value2.Length.Should().Be(2);
        }

        [TestMethod]
        public void ShouldFillCollectionWithRelativePathSpecifiedSize()
        {
            // act
            var instance = FakeUp.NewObject<ValuesHolder<MetaIntArrayHolder>>(o => o
                   .WithCollectionsSize((IntArrayHolder holder) => holder.IntArray1, 1)
                   .WithCollectionsSize((IntArrayHolder holder) => holder.IntArray2, 2)
                   .WithCollectionsSize<int[]>(3)
            );

            // assert
            instance.Value1.Holder.IntArray1.Length.Should().Be(1);
            instance.Value1.Holder.IntArray2.Length.Should().Be(2);
            instance.Value1.Holder.IntArray3.Length.Should().Be(3);
            instance.Value1.IntArray.Length.Should().Be(3);
        }
    }
}