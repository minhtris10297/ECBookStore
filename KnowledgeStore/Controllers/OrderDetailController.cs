using KnowledgeStore.Common;
using Model.EntityFramework;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Controllers
{
    public class OrderDetailController : Controller
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: OrderDetail
        public ActionResult Index()
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USER_SESSION];

            if (sessionUser == null)
            {
                return RedirectToAction("Login", "Accounts");
            }
            var cusId = db.Customers.Where(m => m.Email == sessionUser.Email).Select(m=>m.CustomerID).FirstOrDefault();
            var listOrder = db.ChiTietDonHangs.Where(m => m.DonHang.CustomerID == cusId).ToList();
            
            return View(listOrder);
        }
    }
}