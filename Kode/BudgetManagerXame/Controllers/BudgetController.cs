using BudgetManagerXame.Models;
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
            Dictionary<string, string> FirmList = new Dictionary<string, string>();
        JObject jsonContent = JObject.Parse(content);

            for (int i = 0; i < jsonContent.Count; i++)
            {
                FiscalId = jsonContent["Entities"][i].ToString();
                FiscalId = jsonContent["Entities"][i]["FiscalSetupId"].ToString();

                FirmName = jsonContent["Entities"][i].ToString();
                FirmName = jsonContent["Entities"][i]["FiscalSetupName"].ToString();
                FirmList.Add(FirmName, FiscalId);
                
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
        public async Task<ActionResult> BudgetList(Budget budget, int id)
        {
            var FiscalId = "";
            var FirmName = "";
            var content = await GetJsonString();

            JObject jsonContent = JObject.Parse(content);

           
                FiscalId = jsonContent["Entities"][id].ToString();
                FiscalId = jsonContent["Entities"][id]["FiscalSetupId"].ToString();

                FirmName = jsonContent["Entities"][id].ToString();
                FirmName = jsonContent["Entities"][id]["FiscalSetupName"].ToString();
                
            

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
        public async Task<string> GetFinanceAccounts()
        {
            string token = Request.Cookies["access_token"].Value;

            HttpClient _client = new HttpClient();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string content = await _client.GetStringAsync("https://my.xena.biz/Api/Fiscal/98512/LedgerSearch/FullList?ForceNoPaging=true&Page=0&PageSize=10&ShowDeactivated=false&_=1512459901335");

            return content;

        }

        // POST: Budget/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection, Budget budget)
        {
            Estimate estimate = new Estimate();
            estimate.Period = DB.GetAllPeriods();
            try
            {

                budget.Year = int.Parse(Request.Form["Year"]);
                budget.Description = Request.Form["Description"];

                budget.Id = DB.CreateBudget(budget);

                var FinanceAccountId = "";
                var LedgerAccoount = "";
                var FinanceAccountDesc = "";
                var content = await GetFinanceAccounts();

                JObject jsonContent = JObject.Parse(content);
                int items = jsonContent["Entities"].Count();

                for (int i = 0; i < items; i++)
                {

                    FinanceAccountId = jsonContent["Entities"][i]["AccountNumber"].ToString();
                    FinanceAccountDesc = jsonContent["Entities"][i]["Description"].ToString();
                    LedgerAccoount = jsonContent["Entities"][i]["LedgerAccount"].ToString();
                    LedgerAccoount = DB.GetFinanceGroupName(LedgerAccoount);
                    if (FinanceAccountId == "" || FinanceAccountDesc == "" || LedgerAccoount == "" || LedgerAccoount == "tom")
                    {
                        //ingen oprettelse uden _ID
                    }
                    else
                    {
                        DB.CreateFinanceAccounts(int.Parse(FinanceAccountId.ToString()), FinanceAccountDesc.ToString(), LedgerAccoount.ToString(), budget);
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


                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                return RedirectToAction("Error", e);
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
        public ActionResult Delete(int? id)
        {
            Budget budget = DB.GetBudget(id);
            return View(budget);
        }

        // POST: Budget/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirm(int? id)
        {
            DB.DeleteBudget(id);
            return View("Index");
        }
    }
}
