﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetManagerXame.Models
{
    public class FinanceAccount
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
        public string FinanceGroup { get; set; }
        public int BudgetId { get; set; }
    }
}