﻿using LogicService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicService.Service
{
    public interface ILogicService
    {
        void AddFact(string fact);

        LogicQueryResult Query(string query);
    }
}
