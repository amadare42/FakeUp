using System;
using System.Collections.Generic;
using System.Linq;
using FakeUp.Tests.Data;
using FakeUpLib.Extensions;
using FluentAssertions;
using Xunit;

namespace FakeUp.Tests.StatesTests
{
    public class StatesTests
    {
        [Fact]
        public void GetState_LocatesSameStateForDifferentFills()
        {
            // Act
            var result = FakeUpLib.FakeUp.NewObject<ValuesHolder<int[], string[]>>(o => o
                .AddState(() => new []{ 1,2,3 })
                .Fill(vh => vh.Value1).With(ctx => ctx.GetState<int[]>())
                .Fill(vh => vh.Value2).With(ctx => ctx.GetState<int[]>().Select(v => v + "!").ToArray())
            );
            
            // Assert
            result.Value1.Should().BeEquivalentTo(1, 2, 3);
            result.Value2.Should().BeEquivalentTo("1!", "2!", "3!");
        }
        
        [Fact]
        public void GetState_LocatesStatesWithSameTypeByTag()
        {
            // Act
            var result = FakeUpLib.FakeUp.NewObject<int>(o => o
                .AddState("number1", () => 42)
                .AddState("number2", () => 12)
                .FillAll<int>().With(ctx => ctx.GetState<int>("number1") + ctx.GetState<int>("number2"))
            );
            
            // Assert
            result.Should().Be(42 + 12);
        }
        
        [Fact]
        public void GetState_RecreatesStatesForEachObjectCreations()
        {
            // Arrange
            var stateValue = 0;
            
            // Act
            var config = FakeUpLib.FakeUp.Config.Create<int>(o => o
                .AddState(() => stateValue++)
                .FillAll<int>().With(ctx => ctx.GetState<int>())
            );
            var value1 = config.FakeUp();
            var value2 = config.FakeUp();
            
            // Assert
            value1.Should().Be(0);
            value2.Should().Be(1);
        }
    }
}