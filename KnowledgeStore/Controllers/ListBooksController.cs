using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace KnowledgeStore.Controllers
{
    public class ListBooksController : Controller
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: ListBooks
        public ActionResult Index(string id, int? page)
        {
            ViewBag.TypeID = id;
            var listSach = db.Saches.ToList();
            if (id == "SachGiamGia")
            {
                listSach = db.Saches.OrderByDescending(m => m.GiaKhuyenMai / m.GiaTien).ToList();
            }
            else if(id == "SachMoiPhatHanh")
            {
                listSach = db.Saches.OrderByDescending(m => m.NgayXuatBan).ToList();
            }
            else if (id == "SachBanChay")
            {
                listSach = db.Saches.OrderByDescending(m => m.NangTins.Max(n => n.NgayNang)).ToList();
            }
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(listSach.ToPagedList(pageNumber, pageSize));
        }
    }
}