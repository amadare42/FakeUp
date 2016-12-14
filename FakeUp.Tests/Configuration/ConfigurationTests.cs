using FakeUp.Tests.Data;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FakeUp.Tests.Configuration
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void ShouldUsePreviouslyDefinedConfigurations()
        {
            // arrange
            var fillingConfig1 = FakeUp.Config.Create<ValuesHolder<int>>(options => options
                    .Fill(holder => holder.Value1).With(1)
            );

            var fillingConfig2 = FakeUp.Config.Create<ValuesHolder<int>>(options => options
                    .Fill(holder => holder.Value2).With(2)
            );

            // act
            var instance = FakeUp.NewObject<ValuesHolder<int>>(config => config
                    .WithConfiguration(fillingConfig1, fillingConfig2)
                    .Fill(holder => holder.Value3).With(3)
            );

            // assert
            instance.Value1.Should().Be(1);
            instance.Value2.Should().Be(2);
            instance.Value3.Should().Be(3);
        }
    }
}