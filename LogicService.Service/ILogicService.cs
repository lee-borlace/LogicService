using LogicService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicService.Service
{
    public interface ILogicService
    {
        AddFactResult AddFact(string fact);

        IList<LogicSolution> GetQuerySolutions(string query);

        bool QueryHasSolution(string query);
    }
}
