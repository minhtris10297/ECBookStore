using Common;
using KnowledgeStore.Common;
using Model.EntityFramework;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Areas.MerchantArea.Controllers
{
    public class CustomerController : Controller
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: MerchantArea/Customer
        public ActionResult Index()
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "AccountsMerchant");
            }
            var id = db.Merchants.Where(m => m.Email == sessionUser.Email).Select(m => m.MerchantID).FirstOrDefault();
            var listCus = db.ChiTietDonHangs.Where(m => m.MerchantID == id).Select(m => m.DonHang.CustomerID);
            var list = db.Customers.Where(m => listCus.Contains(m.CustomerID));
            return View(list.ToList());
        }

        [HttpPost]
        public ActionResult Index(string chuDe,string noiDung)
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            var id = db.Merchants.Where(m => m.Email == sessionUser.Email).Select(m => m.MerchantID).FirstOrDefault();
            var name= db.Merchants.Where(m => m.Email == sessionUser.Email).Select(m => m.TenCuaHang).FirstOrDefault();
            var listCus = db.ChiTietDonHangs.Where(m => m.MerchantID == id).Select(m => m.DonHang.CustomerID);
            var list = db.Customers.Where(m => listCus.Contains(m.CustomerID));

            foreach(var item in list)
            {
                MailHelper.SendMailOfMer(name, item.Email, chuDe, noiDung);
            }
            return View(list.ToList());
        }

        public ActionResult DanhGia(int id)
        {
            return View();
        }
    }
}