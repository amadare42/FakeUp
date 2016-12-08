using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Playground.ObjectFaker.Tests
{
    [TestClass]
    public class CollectionsFillingTests
    {
        [TestMethod]
        public void ShouldCreateArrayWithOneElementByDefault()
        {
            // act
            var holder = EmptyObjectFaker.Create<ValuesHolder<int[]>>();

            // assert
            holder.Value1.Length.Should().Be(1);
        }

        [TestMethod]
        public void ShouldFillValuesInArrays()
        {
            // act
            var holder = EmptyObjectFaker.Create<ValuesHolder<int[]>>(opt =>
                opt.FillAll<int>().With(42)
                );

            // assert
            holder.Value1[0].Should().Be(42);
        }

        [TestMethod]
        public void ShouldCreateListWithOneElementByDefault()
        {
            // act
            var holder = EmptyObjectFaker.Create<ValuesHolder<List<int>>>();

            // assert
            holder.Value1.Count.Should().Be(1);
        }

        [TestMethod]
        public void ShouldFillValuesInLists()
        {
            // act
            var holder = EmptyObjectFaker.Create<ValuesHolder<List<int>>>(opt =>
                opt.FillAll<int>().With(42)
                );

            // assert
            holder.Value1[0].Should().Be(42);
        }

        [TestMethod]
        public void ShouldCreateArrayForIEnumerable()
        {
            // act
            var holder = EmptyObjectFaker.Create<ValuesHolder<IEnumerable<int>>>(opt =>
                opt.FillAll<int>().With(42)
                );

            // assert
            holder.Value1.GetType().IsArray.Should().BeTrue();
            holder.Value1.First().Should().Be(42);
        }
    }
}