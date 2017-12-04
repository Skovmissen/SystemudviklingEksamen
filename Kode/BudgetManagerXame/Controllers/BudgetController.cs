using BudgetManagerXame.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BudgetManagerXame.Controllers
{
    public class BudgetController : Controller
    {

        // GET: Budget

        public async Task<ActionResult> Index(Budget budget)
        {
            var content = await GetJsonString();

            JObject jsonContent = JObject.Parse(content);

            var FiscalId = jsonContent["Entities"][0];
            FiscalId = jsonContent["Entities"][0]["FiscalSetupId"];
            budget.Fiscalid = FiscalId.ToString();
            ViewBag.ID = FiscalId;
            return View();

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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Budget/Create
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
