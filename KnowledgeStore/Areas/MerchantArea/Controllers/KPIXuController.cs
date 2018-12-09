using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnowledgeStore.Common;
using Model.EntityFramework;
using Model.ViewModel;
using PayPal.Api;

namespace KnowledgeStore.Areas.MerchantArea.Controllers
{
    public class KPIXuController : Controller
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: MerchantArea/KPIXuController
        public ActionResult Index()
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "AccountsMerchant");
            }
            var id = db.Merchants.Where(m => m.Email == sessionUser.Email).Select(m => m.MerchantID).FirstOrDefault();

            var listLSGDXu = db.LichSuGiaoDichXuCuaMerchants.Where(m => m.MerchantID == id);
            ViewBag.SoLuongXu = db.Merchants.Where(m => m.MerchantID == id).Select(m => m.SoLuongKIPXu).FirstOrDefault();
            return View(listLSGDXu.ToList());
        }

        public ActionResult AddKPIXu(int id)
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            var merchant = db.Merchants.Where(m => m.Email == sessionUser.Email).FirstOrDefault();
            ViewBag.XuNap = 0;
            if (id == 1)
            {
                merchant.SoLuongKIPXu = merchant.SoLuongKIPXu + 20;
                ViewBag.XuNap = 20;
                db.LichSuGiaoDichXuCuaMerchants.Add(new LichSuGiaoDichXuCuaMerchant { MerchantID = merchant.MerchantID, NgayGiaoDich = System.DateTime.Now, PhuongThucSuDung = "Paypal", SoXu = 20 });
            }
            else if (id == 2)
            {
                merchant.SoLuongKIPXu = merchant.SoLuongKIPXu + 45;
                ViewBag.XuNap = 45;
                db.LichSuGiaoDichXuCuaMerchants.Add(new LichSuGiaoDichXuCuaMerchant { MerchantID = merchant.MerchantID, NgayGiaoDich = System.DateTime.Now, PhuongThucSuDung = "Paypal", SoXu = 45 });
            }
            else if (id == 3)
            {
                merchant.SoLuongKIPXu = merchant.SoLuongKIPXu + 75;
                ViewBag.XuNap = 75;
                db.LichSuGiaoDichXuCuaMerchants.Add(new LichSuGiaoDichXuCuaMerchant { MerchantID = merchant.MerchantID, NgayGiaoDich = System.DateTime.Now, PhuongThucSuDung = "Paypal", SoXu = 75 });
            }
            db.SaveChanges();

            return View();
        }
    }
}