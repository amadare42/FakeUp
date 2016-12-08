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
            var instance = FakeUp.NewObject<ValuesHolder<int>>(opt =>
                opt.Fill(holder => holder.Value1).With(42)
            );

            // assert
            instance.Value1.Should().Be(42);
        }

        [TestMethod]
        public void ShouldFillMemberWithFuncByExpressionPath()
        {
            // arrange
            int i = 0;

            // act
            var instance = FakeUp.NewObject<ValuesHolder<int>>(opt =>
                opt.Fill(holder => holder.Value1).With(() => i++)
                   .Fill(holder => holder.Value2).With(() => i++)
            );

            // assert
            instance.Value1.Should().Be(0);
            instance.Value2.Should().Be(1);
        }

        [TestMethod]
        public void ShouldPrioritizeMemberFillingOverTypeFilling()
        {
            // act
            var instance = FakeUp.NewObject<ValuesHolder<string>>(opt =>
                opt.FillAll<string>().With("FillAll")
                   .Fill(holder => holder.Value2).With("Fill")
            );

            // assert
            instance.Value1.Should().Be("FillAll");
            instance.Value2.Should().Be("Fill");
        }

        [TestMethod]
        public void ShouldFillMemberWithConstantWhenUsingRelativePath()
        {
            // act
            var instance = FakeUp.NewObject<ValuesHolder<MetaIntHolder>>(opt =>
                opt.Fill((MetaIntHolder metaIntHolder) => metaIntHolder.Holder.IntValue1)
                   .With(42)
            );

            // assert
            instance.Value1.Holder.IntValue1.Should().Be(42);
            instance.Value2.Holder.IntValue1.Should().Be(42);
        }

        [TestMethod]
        public void ShouldFillMemberWithConstantWhenUsingNestedRelativePaths()
        {
            // act
            var instance = FakeUp.NewObject<ValuesHolder<MetaIntHolder>>(opt =>
            {
                opt.Fill((MetaIntHolder metaIntHolder) => metaIntHolder.Holder.IntValue1)
                   .With(1);
                opt.Fill((IntHolder intHolder) => intHolder.IntValue2)
                   .With(2);
            });

            // assert
            instance.Value1.Holder.IntValue1.Should().Be(1);
            instance.Value1.Holder.IntValue2.Should().Be(2);
        }

        [TestMethod]
        public void ShouldFillMemberWithFuncUsingRelativePath()
        {
            // act
            int i = 0;
            var instance = FakeUp.NewObject<MetaIntHolder>(opt => opt
                .Fill((IntHolder holder) => holder.IntValue1).With(() => i++)
                .Fill((IntHolder holder) => holder.IntValue2).With(() => i++)
            );

            // assert
            instance.Holder.IntValue1.Should().Be(0);
            instance.Holder.IntValue2.Should().Be(1);
        }
    }
}