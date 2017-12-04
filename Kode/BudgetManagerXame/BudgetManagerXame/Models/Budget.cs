using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetManagerXame.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string FiscalId { get; set; }
    }
}