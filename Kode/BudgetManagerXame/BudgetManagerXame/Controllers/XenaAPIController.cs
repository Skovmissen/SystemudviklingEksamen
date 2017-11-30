using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using BudgetManagerXame.Models;
using Newtonsoft.Json;

namespace BudgetManagerXame.Controllers
{
    public class XenaAPIController : ApiController
    {
        readonly string uri = "https://my.xena.biz/Api/Fiscal/98512/Transaction/LedgerGroupDataDetail?ForceNoPaging=true&Page=0&PageSize=10&ShowDeactivated=false&fiscalPeriodId=172923719&FiscalDateFrom=17167&FiscalDateTo=17531&ledgerAccount=Xena_Domain_Income_Accounts_Net_Turn_Over&_=1511941562807";
        
        public async Task<List<XenaData>> GetFinansKonti()
        {

            using (HttpClient httpClient = new HttpClient())
            {

                return JsonConvert.DeserializeObject<List<XenaData>>(await httpClient.GetStringAsync(uri));
            }
        }
    }
}
