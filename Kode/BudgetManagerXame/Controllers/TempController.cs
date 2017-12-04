using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BudgetManagerXame.Models;
using BudgetManagerXame.Classes;

namespace BudgetManagerXame.Controllers
{
    public class TempController : Controller
    {
        // GET: Temp
        public ActionResult Delete(Budget budget)
        {
            
            return View();
        }
    }
}