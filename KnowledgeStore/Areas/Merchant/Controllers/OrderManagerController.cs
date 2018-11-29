using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Areas.Merchant.Controllers
{
    public class OrderManagerController : Controller
    {
        // GET: Merchant/OrderManager
        public ActionResult Index()
        {
            return View();
        }
    }
}