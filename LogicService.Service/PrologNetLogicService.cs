using LogicService.Model;
using Prolog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicService.Service
{
    public class PrologNetLogicService : ILogicService
    {
        Program _program;

        public PrologNetLogicService()
        {
            _program = new Program();
        }

        public void AddFact(string fact)
        {
            var codeSentence = Parser.Parse(fact)[0];
            _program.Add(codeSentence);
        }

        public LogicQueryResult Query(string query)
        {
            var prologQuery = new Query(Parser.Parse(query)[0]);
            var machine = PrologMachine.Create(_program, prologQuery);

            var retVal = new LogicQueryResult();
            retVal.VariableSubstitutions = new List<VariableSubstitution>();

            ExecutionResults results = machine.RunToSuccess();

            retVal.Success = results == ExecutionResults.Success;

            if (machine.QueryResults != null && machine.QueryResults.Variables != null && machine.QueryResults.Variables.Count > 0)
            {
                foreach (var result in machine.QueryResults.Variables)
                {
                    retVal.VariableSubstitutions.Add(new VariableSubstitution() { Variable = result.Name, TextValue = result.Text });
                }
            }

            return retVal;
        }
    }
}
