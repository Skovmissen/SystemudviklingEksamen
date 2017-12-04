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
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            id = 1;
            Budget budget = DB.GetBudget(id);
            return View(budget);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id)
        {
            DB.DeleteBudget(id);
            return View("Index");
        }

    }
}