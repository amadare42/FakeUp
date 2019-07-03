using FakeUp.Tests.Data;
using FluentAssertions;
using Xunit;

namespace FakeUp.Tests.Configuration
{
    public class ConfigurationTests
    {
        [Fact]
        public void WithConfiguration_ApplyConfigurations()
        {
            // Arrange
            var fillingConfig1 = FakeUpLib.FakeUp.Config.Create<IntValueHolder>(options => options
                .Fill(holder => holder.Value1).With(1)
            );

            var fillingConfig2 = FakeUpLib.FakeUp.Config.Create<IntValueHolder>(options => options
                .Fill(holder => holder.Value2).With(2)
            );

            // Act
            var instance = FakeUpLib.FakeUp.NewObject<IntValueHolder>(config => config
                .WithConfiguration(fillingConfig1, fillingConfig2)
                .Fill(holder => holder.Value3).With(3)
            );

            // Assert
            instance.Value1.Should().Be(1);
            instance.Value2.Should().Be(2);
            instance.Value3.Should().Be(3);
        }

        [Fact]
        public void With_Config_GeneratesValueByConfig()
        {
            // Arrange
            var fillingConfig = FakeUpLib.FakeUp.Config.Create<ValuesHolder<string>>(options => options
                .FillAll<string>().With(ctx => ctx.InvocationPath)
            );

            // Act
            var instance = FakeUpLib.FakeUp.NewObject<ValuesHolder<ValuesHolder<string>>>(o => o
                .FillAll<string>().With("constant")
                .Fill(vh => vh.Value2).With(fillingConfig)
            );

            // Assert
            var expected = new ValuesHolder<ValuesHolder<string>>()
            {
                Value1 = new ValuesHolder<string>()
                {
                    Value1 = "constant", Value2 = "constant", Value3 = "constant"
                },
                Value2 = new ValuesHolder<string>()
                {
                    Value1 = "Value1", Value2 = "Value2", Value3 = "Value3"
                },
                Value3 = new ValuesHolder<string>()
                {
                    Value1 = "constant", Value2 = "constant", Value3 = "constant"
                }
            };
            instance.ShouldBeEquivalentTo(expected);
        }
    }
}