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
        public ActionResult Delete(Budget budget)
        {
            budget.Fiscalid = "a";
            DB.GetBudgetID(budget);
            return View(budget);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            DB.DeleteBudget(id);
            return RedirectToAction("Index");
        }
    }
}