using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicService.Model
{
    /// <summary>
    /// TODO : Add other members for numbers etc.
    /// </summary>
    public class LogicQueryResult
    {
        public bool Success { get; set; }

        public IList<VariableSubstitution> VariableSubstitutions { get; set; }
    }
}
