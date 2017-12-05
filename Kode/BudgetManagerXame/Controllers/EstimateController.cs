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
        public ActionResult Create(int id)
        {
            Estimate estimate = new Estimate();

            estimate.Period = DB.GetAllPeriods();
            estimate.FinanceAccount = DB.GetAllFinanceAccounts(id);
            estimate.FinanceGroup = DB.GetAllFinanceGroups();


            return View(estimate);
        }

        // POST: Estimate/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Estimate/Edit/5
        public ActionResult Edit(int id)
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
    }
}
