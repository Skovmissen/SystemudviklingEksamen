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
        public ActionResult Delete(int id)
        {
            return View();
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            DB.Delete(id);
            return RedirectToAction("Index");
        }
    }
}