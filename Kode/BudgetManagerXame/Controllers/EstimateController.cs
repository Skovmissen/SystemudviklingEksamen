using BudgetManagerXame.Classes;
using BudgetManagerXame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetManagerXame.Controllers
{
    public class EstimateController : Controller
    {
        // GET: Estimate
        public ActionResult Index()
        {
            return View();
        }

        // GET: Estimate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Estimate/Create
        public ActionResult Create(Estimate estimate ,int budgetId , int periodId)
        {
          
            estimate.Fap = DB.GetAllFinanceAccountsEstimates(budgetId, periodId);
            estimate.Period = DB.GetAllPeriods();
            estimate.FinanceGroup = DB.GetAllFinanceGroups();
            estimate.FinanceAccount = DB.GetAllFinanceAccounts(budgetId);
            //estimate.Fap = DB.GetAllFinanceAccountsEstimates(budgetId, periodId);
            ViewBag.PeriodId = estimate.Period[periodId -1].Name;
            return View(estimate);
        }

        // POST: Estimate/Create
        [HttpPost, ActionName("Create")]
        public ActionResult UpdateBudget(Estimate estimate)
        {
            int budgetId = 0;
            int periodId = 0;
            foreach (var item in estimate.Fap)
            {
                DB.UpdateFinanceAccountsPeriod(item.AccountId, item.PeriodId, item.BudgetId, item.Estimate);
                 budgetId = item.BudgetId;
                 periodId = item.PeriodId;

            }
            string fiscalId = DB.GetFiscalId(budgetId);
            return RedirectToAction("BudgetList", "Budget", new { id = fiscalId });


        }

        // GET: Estimate/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: Estimate/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Estimate/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Estimate/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Show(Estimate estimate, int budgetId)
        {
            estimate.Period = DB.GetAllPeriods();
            estimate.Fap = DB.GetAllFinanceAccountsEstimates(budgetId);


            estimate.FinanceGroup = DB.GetAllFinanceGroups();
            estimate.FinanceAccount = DB.GetAllFinanceAccounts(budgetId);
            //estimate.Fap = DB.GetAllFinanceAccountsEstimates(budgetId, periodId);
            //ViewBag.PeriodId = estimate.Period[periodId - 1].Name;
            return View(estimate);
        }
        [HttpPost]
        public ActionResult Show()
        {
           
            return View();
        }
    }
}
