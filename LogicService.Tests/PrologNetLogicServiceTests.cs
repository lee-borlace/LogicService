using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicService.Service;

namespace LogicService.Tests
{
    [TestClass]
    public class PrologNetLogicServiceTests
    {
        [TestMethod]
        public void BackwardsChaining_Success_1()
        {
            var service = new PrologNetLogicService();

            service.AddFact("dog(ripley)");
            service.AddFact("mammal(X):-dog(X)");

            var queryResult = service.Query("mammal(X)");

            Assert.IsTrue(queryResult.Success);
            Assert.AreEqual(1, queryResult.VariableSubstitutions.Count);
            Assert.AreEqual("X", queryResult.VariableSubstitutions[0].Variable);
            Assert.AreEqual("ripley", queryResult.VariableSubstitutions[0].TextValue);
        }


        [TestMethod]
        public void BackwardsChaining_Success_2()
        {
            var service = new PrologNetLogicService();

            service.AddFact("dog(ripley)");
            service.AddFact("mammal(X):-dog(X)");

            var queryResult = service.Query("mammal(ripley)");

            Assert.IsTrue(queryResult.Success);
            Assert.AreEqual(0, queryResult.VariableSubstitutions.Count);
        }

        [TestMethod]
        public void BackwardsChaining_Success_3()
        {
            var service = new PrologNetLogicService();

            //service.AddFact("match(input1,nn1,0.75)");
            service.AddFact("strongmatch(Result,NeuralNet):-match(Result,NeuralNet,ActivationLevel),greaterThan(ActivationLevel,0.5)");
            

            var queryResult = service.Query("strongmatch(input1,nn1)");

            //Assert.IsTrue(queryResult.Success);
            //Assert.AreEqual(0, queryResult.VariableSubstitutions.Count);
        }


        [TestMethod]
        public void BackwardsChaining_Failure_1()
        {
            var service = new PrologNetLogicService();

            service.AddFact("mammal(X):-dog(X)");

            var queryResult = service.Query("mammal(X)");

            Assert.IsFalse(queryResult.Success);
            Assert.AreEqual(0, queryResult.VariableSubstitutions.Count);
        }

        [TestMethod]
        public void BackwardsChaining_Failure_2()
        {
            var service = new PrologNetLogicService();

            service.AddFact("dog(ripley)");
            service.AddFact("cat(misterwhiskers)");
            service.AddFact("mammal(X):-dog(X)");

            var queryResult = service.Query("mammal(misterwhiskers)");

            Assert.IsFalse(queryResult.Success);
            Assert.AreEqual(0, queryResult.VariableSubstitutions.Count);
        }
    }
}
