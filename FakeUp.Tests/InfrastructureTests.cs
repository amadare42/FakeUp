using FakeUp.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FakeUp.Tests
{
    [TestClass]
    public class InfrastructureTests
    {
        [TestMethod]
        public void ShouldFillObjectsWithCyclicReferences()
        {
            var instance = FakeUp.NewObject<CyclicHolder<int>>(o => o
                    .FillAll<int>().With(5)
            );
        }
    }
}