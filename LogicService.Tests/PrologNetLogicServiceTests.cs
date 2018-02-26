using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicService.Service;
using LogicService.Model;

namespace LogicService.Tests
{
    [TestClass]
    public class PrologNetLogicServiceTests
    {
        [TestMethod]
        public void BackwardsChaining_EnumerateVariables()
        {
            var service = GetLogicService();

            var addFactResult = service.AddFact("dog(ripley)");
            AssertResponseOk(addFactResult);

            addFactResult = service.AddFact("dog(charlie)");
            AssertResponseOk(addFactResult);

            addFactResult = service.AddFact("mammal(X):-dog(X)");
            AssertResponseOk(addFactResult);

            var queryResult = service.GetQuerySolutions("mammal(X)");

            Assert.AreEqual(2, queryResult.Count);

            Assert.AreEqual(1, queryResult[0].VariableSubstitutions.Count);
            Assert.AreEqual("X", queryResult[0].VariableSubstitutions[0].Variable);
            Assert.AreEqual("ripley", queryResult[0].VariableSubstitutions[0].TextValue);

            Assert.AreEqual(1, queryResult[1].VariableSubstitutions.Count);
            Assert.AreEqual("X", queryResult[1].VariableSubstitutions[0].Variable);
            Assert.AreEqual("charlie", queryResult[1].VariableSubstitutions[0].TextValue);
        }


        [TestMethod]
        public void BackwardsChaining_MathTest1()
        {
            var service = GetLogicService();

            var addFactResult = service.AddFact("match(input1,nn1,0.75)");
            AssertResponseOk(addFactResult);

            addFactResult = service.AddFact("match(input2,nn1,0.80)");
            AssertResponseOk(addFactResult);

            addFactResult = service.AddFact("match(input3,nn1,0.99)");
            AssertResponseOk(addFactResult);

            addFactResult = service.AddFact("match(input4,nn1,1)");
            AssertResponseOk(addFactResult);

            addFactResult = service.AddFact("match(input5,nn1,0.25)");
            AssertResponseOk(addFactResult);

            addFactResult = service.AddFact("match(input6,nn1,-0.25)");
            AssertResponseOk(addFactResult);

            addFactResult = service.AddFact("strongmatch(Result,NeuralNet):-(match(Result,NeuralNet,ActivationLevel),ActivationLevel > 0.5)");
            AssertResponseOk(addFactResult);

            var queryHasSolution = service.QueryHasSolution("strongmatch(input1,nn1)");
            Assert.IsTrue(queryHasSolution);

            queryHasSolution = service.QueryHasSolution("strongmatch(input5,nn1)");
            Assert.IsFalse(queryHasSolution);

            var queryResult = service.GetQuerySolutions("strongmatch(X,nn1)");
            Assert.AreEqual(4, queryResult.Count);

            Assert.AreEqual("input1", queryResult[0].VariableSubstitutions[0].TextValue);
            Assert.AreEqual("X", queryResult[0].VariableSubstitutions[0].Variable);

            Assert.AreEqual("input2", queryResult[1].VariableSubstitutions[0].TextValue);
            Assert.AreEqual("X", queryResult[0].VariableSubstitutions[0].Variable);

            Assert.AreEqual("input3", queryResult[2].VariableSubstitutions[0].TextValue);
            Assert.AreEqual("X", queryResult[0].VariableSubstitutions[0].Variable);

            Assert.AreEqual("input4", queryResult[3].VariableSubstitutions[0].TextValue);
            Assert.AreEqual("X", queryResult[0].VariableSubstitutions[0].Variable);

            queryResult = service.GetQuerySolutions("match(input6,nn1,X)");
            Assert.AreEqual(1, queryResult.Count);
            Assert.AreEqual("-0.25", queryResult[0].VariableSubstitutions[0].TextValue);
            Assert.AreEqual("X", queryResult[0].VariableSubstitutions[0].Variable);
        }


        [TestMethod]
        public void BackwardsChaining_DoesNotBindVariable()
        {
            var service = GetLogicService();

            service.AddFact("mammal(X):-dog(X)");

            var queryResult = service.GetQuerySolutions("mammal(X)");

            Assert.AreEqual(0, queryResult.Count);
        }


        [TestMethod]
        public void BackwardsChaining_NoSolution()
        {
            var service = GetLogicService();

            service.AddFact("dog(ripley)");
            service.AddFact("cat(misterkitty)");
            service.AddFact("friendly(X):-dog(X)");

            var queryHasSolution = service.QueryHasSolution("friendly(misterkitty)");
            Assert.IsFalse(queryHasSolution);
        }


        private void AssertResponseOk(AddFactResult result)
        {
            Assert.IsTrue(result.Success);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.Message));
        }


        private ILogicService GetLogicService()
        {
            return new CSPrologLogicService();
        }

    }
}
