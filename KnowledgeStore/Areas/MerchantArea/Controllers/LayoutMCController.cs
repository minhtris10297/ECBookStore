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
            var merchant= db.Merchants.Where(m => m.Email == sessionUser.Email).FirstOrDefault();
            var merchantID = merchant.MerchantID;
            ViewBag.MerchantID = merchantID;
            ViewBag.MerchantName = merchant.HoTen;
            ViewBag.MerchantEmail = merchant.Email;
            ViewBag.MerchantTenCuaHang = merchant.TenCuaHang;
            ViewBag.MerchantSoDienThoai = merchant.SoDienThoai;
            ViewBag.MerchantDiaChi = merchant.DiaChi;
            ViewBag.MerchantNgayTao = merchant.NgayTao.ToString("dd/MM/yyyy");

            return PartialView();
        }
    }
}