using System.Collections.Generic;
using System.Linq;
using FakeUp.Tests.Data;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FakeUp.Tests
{
    [TestClass]
    public class CollectionElementsFillingTests
    {
        [TestMethod]
        public void ShouldFillElementsWithValuesFromConstantByType()
        {
            // act
            var instance = FakeUp.NewObject<ValuesHolder<int[], List<int>>>(options =>
                        options.FillElementsOf<int[]>().With(42)
            );

            // assert
            instance.Value1.ShouldAllBeEquivalentTo(42);
        }

        [TestMethod]
        public void ShouldPrioritizeFillElementsByTypeOverTypeFill()
        {
            // act
            var instance = FakeUp.NewObject<ValuesHolder<int[], List<int>>>(options => options
                    .FillAll<int>().With(1)
                    .FillElementsOf<int[]>().With(2)
            );

            // assert
            instance.Value1.ShouldAllBeEquivalentTo(2);
            instance.Value2.ShouldAllBeEquivalentTo(1);
        }

        [TestMethod]
        public void ShouldFillElementsWithValuesFromIndexFuncByType()
        {
            // arrange
            var collectionsSize = 10;

            // act
            var instance = FakeUp.NewObject<ValuesHolder<int[], List<int>>>(options => options
                    .WithCollectionsSize(collectionsSize)
                    .FillElementsOf<int[]>().With(index => index + 100)
            );

            // assert
            var expectedValues = Enumerable.Range(0, collectionsSize).Select(i => i + 100);
            instance.Value1.ShouldBeEquivalentTo(expectedValues, opt => opt.WithStrictOrdering());
        }

        [TestMethod]
        public void ShouldFillElementsWithValuesFromFuncByType()
        {
            // arrange
            var i = 0;

            // act
            var instance = FakeUp.NewObject<ValuesHolder<int[]>>(options =>
                        options.FillElementsOf<int[]>().With(() => i++)
            );

            // assert
            instance.Value1.ShouldAllBeEquivalentTo(0);
            instance.Value2.ShouldAllBeEquivalentTo(1);
        }

        [TestMethod]
        public void ShouldFillElementsWithValuesFromConstantByAbsolutePath()
        {
            // act
            var instance = FakeUp.NewObject<ValuesHolder<int[]>>(options => options
                    .FillElementsOf(holder => holder.Value1).With(1)
                    .FillElementsOf(holder => holder.Value2).With(2)
            );

            // assert
            instance.Value1.ShouldAllBeEquivalentTo(1);
            instance.Value2.ShouldAllBeEquivalentTo(2);
            instance.Value3.ShouldAllBeEquivalentTo(0);
        }

        [TestMethod]
        public void ShouldFillElementsWithValuesFromFuncByAbsolutePath()
        {
            // arrange
            var i = 0;

            // act
            var instance = FakeUp.NewObject<ValuesHolder<int[]>>(options => options
                    .FillElementsOf(holder => holder.Value1).With(() => ++i)
                    .FillElementsOf(holder => holder.Value2).With(() => ++i)
            );

            // assert
            instance.Value1.ShouldAllBeEquivalentTo(1);
            instance.Value2.ShouldAllBeEquivalentTo(2);
            instance.Value3.ShouldAllBeEquivalentTo(0);
        }

        [TestMethod]
        public void ShouldFillElementsWithValuesFromIndexFuncByAbsolutePath()
        {
            // arrange
            var collectionSize = 10;

            // act
            var instance = FakeUp.NewObject<ValuesHolder<int[]>>(options => options
                    .WithCollectionsSize(collectionSize)
                    .FillElementsOf(holder => holder.Value1).With(index => index * 2)
            );

            // assert
            var expectedValues = Enumerable.Range(0, collectionSize).Select(i => i * 2);
            instance.Value1.ShouldBeEquivalentTo(expectedValues);
        }
    }
}