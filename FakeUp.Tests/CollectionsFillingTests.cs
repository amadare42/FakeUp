using System.Collections.Generic;
using System.Linq;
using FakeUp.Tests.Data;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FakeUp.Tests
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

            //assert
            instance.Value1.Length.Should().Be(42);
        }
    }
}