//using LogicService.Model;
//using Prolog;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LogicService.Service
//{
//    public class PrologNetLogicService : ILogicService
//    {
//        Program _program;

//        public PrologNetLogicService()
//        {
//            _program = new Program();
//        }

//        public void AddFact(string fact)
//        {
//            var codeSentence = Parser.Parse(fact)[0];
//            _program.Add(codeSentence);
//        }

//        public Solution Query(string query)
//        {
//            throw new NotImplementedException();
//        }

//        IList<LogicSolution> ILogicService.Query(string query)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
