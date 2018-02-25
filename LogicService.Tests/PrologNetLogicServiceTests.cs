using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicService.Service;

namespace LogicService.Tests
{
    [TestClass]
    public class PrologNetLogicServiceTests
    {
        [TestMethod]
        public void BackwardsChaining_Success()
        {
            var service = new PrologNetLogicService();

            service.AddFact("dog(ripley)");
            service.AddFact("mammal(X):-dog(X)");

            var queryResult = service.Query(":-mammal(X)");

            Assert.IsTrue(queryResult.Success);
            Assert.AreEqual(1, queryResult.VariableSubstitutions.Count);
            Assert.AreEqual("X", queryResult.VariableSubstitutions[0].Variable);
            Assert.AreEqual("ripley", queryResult.VariableSubstitutions[0].TextValue);
        }

        [TestMethod]
        public void BackwardsChaining_Failure()
        {
            var service = new PrologNetLogicService();

            service.AddFact("mammal(X):-dog(X)");

            var queryResult = service.Query(":-mammal(X)");

            Assert.IsFalse(queryResult.Success);
            Assert.AreEqual(0, queryResult.VariableSubstitutions.Count);
        }
    }
}
