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
    public class LayoutMCController : Controller
    {
        // GET: MerchantArea/LayoutMC
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        public ActionResult LogoTop()
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "AccountsMerchant");
            }
            ViewBag.TenShop = db.Merchants.Where(m => m.Email == sessionUser.Email).Select(m => m.TenCuaHang).FirstOrDefault();
            return PartialView();
        }
        public ActionResult TopbarAccount()
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "AccountsMerchant");
            }
            var merchantID = db.Merchants.Where(m => m.Email == sessionUser.Email).Select(m => m.MerchantID).FirstOrDefault();
            ViewBag.MerchantID = merchantID;
            
            return PartialView();
        }
    }
}