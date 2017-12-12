﻿using BudgetManagerXame.Models;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using BudgetManagerXame.Classes;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BudgetManagerXame.Controllers
{
    public class BudgetController : Controller
    {
        // Lavet af Nikolaj
        // GET: Budget

        public async Task<ActionResult> Index(Budget budget)
        {
            var FiscalId = "";
            var FirmName = "";
            var content = await GetJsonString();
            Dictionary<string, int> FirmList = new Dictionary<string, int>();
            JObject jsonContent = JObject.Parse(content);

            for (int i = 0; i < jsonContent.Count; i++)
            {
                FiscalId = jsonContent["Entities"][i].ToString();
                FiscalId = jsonContent["Entities"][i]["FiscalSetupId"].ToString();

                FirmName = jsonContent["Entities"][i].ToString();
                FirmName = jsonContent["Entities"][i]["FiscalSetupName"].ToString();
                FirmList.Add(FirmName, Convert.ToInt32(FiscalId));

            }
            budget.firmList = FirmList;
            return View(budget);

        }
        public async Task<string> GetJsonString()
        {
            string token = Request.Cookies["access_token"].Value;

            HttpClient _client = new HttpClient();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string content = await _client.GetStringAsync("https://my.xena.biz/Api/User/XenaUserMembership?ForceNoPaging=true&Page=0&PageSize=10&ShowDeactivated=false");

            return content;

        }
        public async Task<ActionResult> BudgetList(Budget budget, string id)
        {
            var FiscalId = "";
            var FirmName = "";
            var content = await GetJsonString();
            budget.firmId = Convert.ToInt32(id);
            JObject jsonContent = JObject.Parse(content);

            for (int i = 0; i < jsonContent.Count; i++)
            {
                //FiscalId = jsonContent["Entities"][i].ToString();

                if (jsonContent["Entities"][i]["FiscalSetupId"].ToString() == id)
                {
                    FiscalId = jsonContent["Entities"][i]["FiscalSetupId"].ToString();
                    //FirmName = jsonContent["Entities"][i].ToString();
                    FirmName = jsonContent["Entities"][i]["FiscalSetupName"].ToString();
                }
                else
                {

                }
            }

            budget.Fiscalid = FiscalId.ToString();
            budget.FirmName = FirmName.ToString();
            ViewBag.Name = FirmName;

            DataTable dt = DB.GetAllBudgets(FiscalId.ToString());
            budget.BudgetList = dt.AsEnumerable().ToList();


            return View(budget);
        }
        // GET: Budget/Details/5
        public ActionResult Details(int id)
        {
            return View();

        }

        // GET: Budget/Create
        public ActionResult Create(Budget budget)
        {


            return View(budget);
        }
        public async Task<string> GetFinanceAccounts(Budget budget)
        {
            string token = Request.Cookies["access_token"].Value;

            HttpClient _client = new HttpClient();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string content = await _client.GetStringAsync("https://my.xena.biz/Api/Fiscal/" + budget.Fiscalid + "/LedgerSearch/FullList?ForceNoPaging=true&Page=0&PageSize=10&ShowDeactivated=false&_=1512459901335");

            return content;

        }

        // POST: Budget/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection, Budget budget, int id)
        {
            Estimate estimate = new Estimate();
            estimate.Period = DB.GetAllPeriods();
            try
            {

                budget.Year = int.Parse(Request.Form["Year"]);
                budget.Description = Request.Form["Description"];

                budget.Id = DB.CreateBudget(budget);

                await AddAccountsToBudget(budget, estimate);

                return RedirectToAction("BudgetList", "Budget", new { @id = id });
            }
            catch (Exception e)
            {

                return RedirectToAction("Error", e);
            }
        }
        
        public async Task<ActionResult> Sync(Budget budget, int id, string FiscalId, int siteId)
        {
            Estimate estimate = new Estimate();
            estimate.Period = DB.GetAllPeriods();
            budget.Id = id;
            budget.Fiscalid = FiscalId;
            await AddAccountsToBudget(budget, estimate);
            if (siteId == 1)
            {
                return RedirectToAction("Create", "Estimate", new { @budgetid = id, @periodId = 1 });
            }
            else
            {
                return RedirectToAction("Show", "Estimate", new { @budgetid = id});
            }
            
        }

        private async Task AddAccountsToBudget(Budget budget, Estimate estimate)
        {

            var FinanceAccountId = "";
            var LedgerAccoount = "";
            var FinanceAccountDesc = "";
            var Moms = "";
            var ArticleId = "";
            var content = await GetFinanceAccounts(budget);

            JObject jsonContent = JObject.Parse(content);
            int items = jsonContent["Entities"].Count();

            for (int i = 0; i < items; i++)
            {

                FinanceAccountId = jsonContent["Entities"][i]["AccountNumber"].ToString();
                FinanceAccountDesc = jsonContent["Entities"][i]["Description"].ToString();
                LedgerAccoount = jsonContent["Entities"][i]["LedgerAccount"].ToString();
                Moms = jsonContent["Entities"][i]["LongDescription"].ToString();
                ArticleId = jsonContent["Entities"][i]["ArticleGroupId"].ToString();
                if (ArticleId == "")
                {
                    ArticleId = jsonContent["Entities"][i]["LedgerTagId"].ToString();
                }
                LedgerAccoount = DB.GetFinanceGroupName(LedgerAccoount);
                if (FinanceAccountId == "" || FinanceAccountDesc == "" || LedgerAccoount == "" || LedgerAccoount == "tom" || Moms.Contains("Momsfri"))
                {
                    //ingen oprettelse
                }
                else
                {
                    DB.CreateFinanceAccounts(int.Parse(FinanceAccountId.ToString()), FinanceAccountDesc.ToString(), LedgerAccoount.ToString(), ArticleId, budget);
                }
            }
            estimate.FinanceAccount = DB.GetAllFinanceAccounts(budget.Id);
            foreach (var period in estimate.Period)
            {
                foreach (var financeAccount in estimate.FinanceAccount)
                {
                    DB.CreateFinanceAccountsPeriod(financeAccount.AccountId, period.Id, budget);
                }
            }
        }

        public ActionResult Error(Exception e)
        {
            ViewBag.error = e;
            return View();
        }
        

        // GET: Budget/Edit/5
        public ActionResult Edit(int id)
        {
            Estimate estimate = new Estimate();

            estimate.Period = DB.GetAllPeriods();
            estimate.FinanceAccount = DB.GetAllFinanceAccounts(id);
            estimate.FinanceGroup = DB.GetAllFinanceGroups();


            return View(estimate);
        }

        // POST: Budget/Edit/5
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

        // GET: Budget/Delete/5
        public ActionResult Delete(int id)
        {
            Budget budget = DB.GetBudget(id);
            return View(budget);
        }

        // POST: Budget/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            string fiscalId = DB.GetFiscalId(id);
            DB.DeleteBudget(id);
            

            return RedirectToAction("BudgetList", "Budget", new { id = fiscalId });
        }
    }
}
