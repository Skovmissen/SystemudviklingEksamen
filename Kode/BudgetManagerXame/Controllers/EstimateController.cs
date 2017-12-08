using BudgetManagerXame.Classes;
using BudgetManagerXame.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

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
        public ActionResult Create(Estimate estimate, int budgetId, int periodId)
        {

            estimate.Fap = DB.GetAllFinanceAccountsEstimates(budgetId, periodId);
            estimate.Period = DB.GetAllPeriods();
            estimate.FinanceGroup = DB.GetAllFinanceGroups();
            estimate.FinanceAccount = DB.GetAllFinanceAccounts(budgetId);
            //estimate.Fap = DB.GetAllFinanceAccountsEstimates(budgetId, periodId);
            ViewBag.PeriodId = estimate.Period[periodId - 1].Name;
            ViewBag.BudgetId = budgetId;
            ViewBag.FiscalId = DB.GetFiscalId(budgetId);
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
            return RedirectToAction("Create", "Estimate", new { budgetId = budgetId, periodId = periodId });


        }
        
        public void test()
        {
            int accountId = Convert.ToInt32(Request["AccountId"]);
            int budgetId = Convert.ToInt32(Request["BudgetId"]);
            int periodId = Convert.ToInt32(Request["PeriodId"]);
            double estimate = Convert.ToDouble(Request["Estimate"]);


            DB.UpdateFinanceAccountsPeriod(accountId,periodId,budgetId,estimate);
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
            int total = 0;
            estimate.TotalDic = new Dictionary<int, int>();
            estimate.TotalSumGroup = new Dictionary<string, int>();
            estimate.Period = DB.GetAllPeriods();
            estimate.Fap = DB.GetAllFinanceAccountsEstimates(budgetId);
            estimate.FinanceGroup = DB.GetAllFinanceGroups();
            estimate.FinanceAccount = DB.GetAllFinanceAccounts(budgetId);
            ViewBag.Year = DB.GetBudgetYear(budgetId);
            ViewBag.BudgetId = budgetId;
            ViewBag.FiscalId = DB.GetFiscalId(budgetId);
            foreach (var item in estimate.FinanceAccount)
            {
                estimate.TotalDic.Add(item.AccountId, DB.GetSumOfEstimates(budgetId, item.AccountId));
            }
            foreach (var item in estimate.FinanceGroup)
            {
                estimate.TotalSumGroup.Add(item.Name, 0);

            }
            foreach (var item in estimate.FinanceAccount)
            {
                if (estimate.TotalSumGroup.ContainsKey(item.FinanceGroup))
                {
                    estimate.TotalSumGroup[item.FinanceGroup] = estimate.TotalSumGroup[item.FinanceGroup] + DB.GetSumOfEstimatesOnGroups(budgetId, item.AccountId);
                }
                else
                {
                    estimate.TotalSumGroup.Add(item.FinanceGroup, DB.GetSumOfEstimatesOnGroups(budgetId, item.AccountId));
                }

            }


            return View(estimate);
        }
        [HttpPost]
        public ActionResult Show()
        {

            return View();
        }
    }
}
