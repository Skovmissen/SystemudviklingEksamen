using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetManagerXame.Models
{
    public class Estimate
    {
        public List<Period> Period { get; set; }
        public List<Budget> Budget { get; set; }
        public List<FinanceAccount> FinanceAccount { get; set; }
        public List<FinanceGroup> FinanceGroup { get; set; }
        public List<FinanceAccountPeriod> FinanceAccountPeriod { get; set; }

    }
}