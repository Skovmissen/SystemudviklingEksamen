using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetManagerXame.Models
{
    public class FinanceAccountPeriod
    {
        public int AccountId { get; set; }
        public int BudgetId { get; set; }
        public int PeriodId { get; set; }
        public int Estimate { get; set; }
    }
}