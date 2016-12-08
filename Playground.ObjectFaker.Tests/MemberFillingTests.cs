using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Playground.ObjectFaker.Tests
{
    [TestClass]
    public class MemberFillingTests
    {
        [TestMethod]
        public void ShouldFillMemberWithConstantByExpressionPath()
        {
            // act
            var instanse = EmptyObjectFaker.Create<ValuesHolder<int>>(opt =>
                opt.Fill(holder => holder.Value1).With(42)
            );

            // assert
            instanse.Value1.Should().Be(42);
        }

        [TestMethod]
        public void ShouldFillMemberWithFuncByExpressionPath()
        {
            // arrange
            int i = 0;

            // act
            var instanse = EmptyObjectFaker.Create<ValuesHolder<int>>(opt =>
                opt.Fill(holder => holder.Value1).With(() => i++)
                   .Fill(holder => holder.Value2).With(() => i++)
            );

            // assert
            instanse.Value1.Should().Be(0);
            instanse.Value2.Should().Be(1);
        }

        [TestMethod]
        public void ShouldPrioritizeMemberFillingOverTypeFilling()
        {
            // act
            var instanse = EmptyObjectFaker.Create<ValuesHolder<string>>(opt =>
                opt.FillAll<string>().With("FillAll")
                   .Fill(holder => holder.Value2).With("Fill")
            );

            // assert
            instanse.Value1.Should().Be("FillAll");
            instanse.Value2.Should().Be("Fill");
        }

        [TestMethod]
        public void ShouldFillMemberWithConstantWhenUsingRelativePath()
        {
            // act
            var instanse = EmptyObjectFaker.Create<ValuesHolder<MetaIntHolder>>(opt =>
                opt.Fill((MetaIntHolder metaIntHolder) => metaIntHolder.Holder.IntValue1)
                   .With(42)
            );

            // assert
            instanse.Value1.Holder.IntValue1.Should().Be(42);
        }

        [TestMethod]
        public void ShouldFillMemberWithConstantWhenUsingNestedRelativePaths()
        {
            // act
            var instanse = EmptyObjectFaker.Create<ValuesHolder<MetaIntHolder>>(opt =>
                opt.Fill((MetaIntHolder metaIntHolder) => metaIntHolder.Holder.IntValue1)
                    .With(1)
                   .Fill((IntHolder intHolder) => intHolder.IntValue2)
                    .With(2)
            );

            // assert
            instanse.Value1.Holder.IntValue1.Should().Be(1);
            instanse.Value1.Holder.IntValue2.Should().Be(2);
        }
    }
}