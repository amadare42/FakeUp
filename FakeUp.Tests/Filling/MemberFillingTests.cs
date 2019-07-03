using FakeUp.Tests.Data;
using FluentAssertions;
using Xunit;

namespace FakeUp.Tests.Filling
{
    public class MemberFillingTests
    {
        [Fact]
        public void Fill_Constant_FillMemberByPath()
        {
            // Act
            var instance = FakeUp.NewObject<ValuesHolder<int>>(opt =>
                opt.Fill(holder => holder.Value1).With(42)
            );

            // Assert
            instance.Value1.Should().Be(42);
        }

        [Fact]
        public void Fill_Func_FillMemberByPath()
        {
            // Arrange
            var i = 0;

            // Act
            var instance = FakeUp.NewObject<ValuesHolder<int>>(opt => opt
                .Fill(holder => holder.Value1).With(() => i++)
                .Fill(holder => holder.Value2).With(() => i++)
            );

            // Assert
            instance.Value1.Should().Be(0);
            instance.Value2.Should().Be(1);
        }

        [Fact]
        public void Fill_PrioritizedOverFillAll()
        {
            // Act
            var instance = FakeUp.NewObject<ValuesHolder<string>>(opt => opt
                .FillAll<string>().With("FillAll")
                .Fill(holder => holder.Value2).With("Fill")
            );

            // Assert
            instance.Value1.Should().Be("FillAll");
            instance.Value2.Should().Be("Fill");
        }

        [Fact]
        public void Fill_RelativePath_FillWithConstant()
        {
            // Act
            var instance = FakeUp.NewObject<ValuesHolder<MetaIntHolder>>(opt => opt
                .Fill((MetaIntHolder metaIntHolder) => metaIntHolder.Holder.IntValue1)
                .With(42)
            );

            // Assert
            instance.Value1.Holder.IntValue1.Should().Be(42);
            instance.Value2.Holder.IntValue1.Should().Be(42);
        }

        [Fact]
        public void Fill_NestedRelativePath_FillConstants()
        {
            // Act
            var instance = FakeUp.NewObject<ValuesHolder<MetaIntHolder>>(opt =>
            {
                opt.Fill((MetaIntHolder metaIntHolder) => metaIntHolder.Holder.IntValue1)
                    .With(1);
                opt.Fill((IntHolder intHolder) => intHolder.IntValue2)
                    .With(2);
            });

            // Assert
            instance.Value1.Holder.IntValue1.Should().Be(1);
            instance.Value1.Holder.IntValue2.Should().Be(2);
        }

        [Fact]
        public void Fill_RelativePath_FillByFunc()
        {
            // Act
            var i = 0;
            var instance = FakeUp.NewObject<MetaIntHolder>(opt => opt
                .Fill((IntHolder holder) => holder.IntValue1).With(() => i++)
                .Fill((IntHolder holder) => holder.IntValue2).With(() => i++)
            );

            // Assert
            instance.Holder.IntValue1.Should().Be(0);
            instance.Holder.IntValue2.Should().Be(1);
        }
    }
}