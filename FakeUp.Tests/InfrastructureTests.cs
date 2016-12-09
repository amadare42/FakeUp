using System;
using FakeUp.Tests.Data;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FakeUp.Tests
{
    [TestClass]
    public class InfrastructureTests
    {
        [TestMethod]
        public void ShouldThrowWhenObjectHaveCyclicReferences()
        {
            // act
            Action act = () => FakeUp.NewObject<CyclicHolder<int>>();

            // assert
            act.ShouldThrow<ArgumentException>()
                .And.Message.Should().Contain("cyclic reference");
        }
    }
}