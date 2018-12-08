using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PayPal.Api;

namespace KnowledgeStore.Areas.MerchantArea.Controllers
{
    public class KPIXuControllerController : Controller
    {
        
        // GET: MerchantArea/KPIXuController
        public ActionResult Index()
        {
            var config = ConfigManager.Instance.GetProperties();
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);
            return View();
        }
    }
}