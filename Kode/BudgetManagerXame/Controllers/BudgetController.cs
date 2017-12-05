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

namespace BudgetManagerXame.Controllers
{
    public class BudgetController : Controller
    {
        // Lavet af Nikolaj
        // GET: Budget

        public async Task<ActionResult> Index(Budget budget)
        {
            var content = await GetJsonString();

            JObject jsonContent = JObject.Parse(content);

            var FiscalId = jsonContent["Entities"][0];
            FiscalId = jsonContent["Entities"][0]["FiscalSetupId"];

            budget.Fiscalid = FiscalId.ToString();
            ViewBag.ID = FiscalId;

            DataTable dt = DB.GetAllBudgets(FiscalId.ToString());
            budget.BudgetList = dt.AsEnumerable().ToList();

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

        // GET: Budget/Details/5
        public ActionResult Details(int id)
        {
            return View();

        }

        // GET: Budget/Create
        public async Task<ActionResult> Create(Budget budget)
        {
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
                LedgerAccoount = DB.GetFinanceGroup(LedgerAccoount);
                if (FinanceAccountId == "")
                {
                    FinanceAccountId = "1";
                    DB.CreateFinanceAccounts(int.Parse(FinanceAccountId.ToString()), FinanceAccountDesc.ToString(), LedgerAccoount.ToString(), budget);
                }
                else
                {
                    DB.CreateFinanceAccounts(int.Parse(FinanceAccountId.ToString()), FinanceAccountDesc.ToString(), LedgerAccoount.ToString(), budget);
                }


            }
            ViewBag.test = FinanceAccountId;
            ViewBag.Ledger = LedgerAccoount;
            ViewBag.count = items;
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
            try
            {
                budget.Year = int.Parse(Request.Form["Year"]);
                budget.Description = Request.Form["Description"];

                budget.Id = DB.CreateBudget(budget);



                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Budget/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
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
            return View();
        }

        // POST: Budget/Delete/5
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
