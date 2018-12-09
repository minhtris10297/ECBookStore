using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Model.ViewModel;
using KnowledgeStore.Common;
using Model.EntityFramework;

namespace KnowledgeStore.Areas.MerchantArea.Controllers
{
    public class MCHomeController : Controller
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: MerchantArea/MCHome
        public ActionResult Index(string id, int? page)
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "AccountsMerchant");
            }
            var merchantID = db.Merchants.Where(m => m.Email == sessionUser.Email).Select(m => m.MerchantID).FirstOrDefault();

            var listSach = db.Saches.Where(m=>m.MerchantID== merchantID).ToList();
            if (id != null)
            {
                if (id == "SachGiamGia")
                {
                    listSach = db.Saches.OrderByDescending(m => (m.GiaKhuyenMai / m.GiaTien)).Where(m => m.TrangThai == true).ToList();
                }
                else if (id == "SachMoiPhatHanh")
                {
                    listSach = db.Saches.OrderByDescending(m => m.NgayXuatBan).Where(m => m.TrangThai == true).ToList();
                }
                else if (id == "SachBanChay")
                {
                    listSach = db.Saches.OrderByDescending(m => m.LichSuNangTins.Max(n => n.NgayNang)).Where(m => m.TrangThai == true).ToList();
                }
            }
            var merchant = db.Merchants.Where(m => m.MerchantID == merchantID);
            ViewBag.TenShop = merchant.Select(m => m.TenCuaHang).FirstOrDefault();
            ViewBag.SoThang = System.DateTime.Now.Month- merchant.Select(m => m.NgayTao).FirstOrDefault().Month;
            ViewBag.SoSanPham = db.Saches.Where(m => m.MerchantID == merchantID).Count();
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(listSach.ToPagedList(pageNumber, pageSize));
        }
    }
}