using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BudgetManagerXame.Models
{
    public class Estimate
    {
        public IEnumerable<Period> Period { get; set; }
        public IEnumerable<Budget> Budget { get; set; }
        public IEnumerable<FinanceAccount> FinanceAccount { get; set; }
        public IEnumerable<FinanceGroup> FinanceGroup { get; set; }
        public IEnumerable<FinanceAccountPeriod> FinanceAccountPeriod { get; set; }

    }
}