using Model.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Controllers
{
    public class MerchantShopController : Controller
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: MerchantShop
        public ActionResult Index(int id,int? page,string type)
        {
            var listBook = db.Saches.Where(m => m.MerchantID == id).ToList();
            if (type == "SachGiamGia")
            {
                listBook = db.Saches.OrderByDescending(m => (m.GiaKhuyenMai / m.GiaTien)).Where(m => m.TrangThai == true).ToList();
            }
            else if (type == "SachMoiPhatHanh")
            {
                listBook = db.Saches.OrderByDescending(m => m.NgayXuatBan).Where(m => m.TrangThai == true).ToList();
            }
            ViewBag.MerchantID = id;
            ViewBag.NameShop = db.Saches.Select(m => m.Merchant.TenCuaHang).FirstOrDefault();
            ViewBag.CountBook = listBook.Count();

            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(listBook.ToPagedList(pageNumber, pageSize));
        }
    }
}