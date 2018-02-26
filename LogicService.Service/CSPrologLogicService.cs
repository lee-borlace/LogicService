using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicService.Model;
using Prolog;

namespace LogicService.Service
{
    public class CSPrologLogicService : ILogicService
    {
        private PrologEngine _engine;

        public CSPrologLogicService()
        {
            _engine = new PrologEngine();
        }

        public AddFactResult AddFact(string fact)
        {
            fact = fact.Trim();

            // Make sure fact doesn't end in period.
            if (fact.EndsWith("."))
            {
                fact = fact.Substring(0, fact.Length - 1);
            }

            _engine.Query = $"assert({fact}).";

            foreach (var s in _engine.SolutionIterator)
            {
                if(!s.Solved)
                {
                    return new AddFactResult()
                    {
                        Success = false,
                        Message = s.ToString()
                    };
                }
            }

            return new AddFactResult()
            {
                Success = true,
                Message = null
            };
        }

        public IList<LogicSolution> GetQuerySolutions(string query)
        {
            query = query.Trim();
            query = query.AddEndDot();
            _engine.Query = query;

            var retVal = new List<LogicSolution>();
            
            foreach (var solution in _engine.SolutionIterator)
            {
                var returnedSolution = new LogicSolution();

                if (solution.Solved)
                {

                    returnedSolution.VariableSubstitutions = new List<VariableSubstitution>();

                    foreach (var variableBinding in solution.VarValuesIterator)
                    {
                        returnedSolution.VariableSubstitutions.Add(new VariableSubstitution()
                        {
                            Variable = variableBinding.Name,
                            TextValue = variableBinding.Value.ToString(),
                            DataType = variableBinding.DataType
                        });
                    }

                    retVal.Add(returnedSolution);
                }
            }


            return retVal;
        }

        public bool QueryHasSolution(string query)
        {
            query = query.Trim();
            query = query.AddEndDot();
            _engine.Query = query;

            foreach (var solution in _engine.SolutionIterator)
            {
                return solution.Solved;
            }

            return false;
        }
    }
}
