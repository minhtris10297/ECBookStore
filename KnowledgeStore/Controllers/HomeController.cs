using Model.EntityFramework;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Controllers
{
    public class HomeController : Controller
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        public ActionResult Index()
        {
            return View(db.Saches.ToList());
        }

        public JsonResult ViewMore(int xPosition, int height)
        {
            var listSach = new List<BookHomeVM>();
            var list = db.Saches.ToList();
            for (int i = 0; i <= xPosition + 8; i++)
            {
                BookHomeVM bookHomeVM = new BookHomeVM() { SachID = list[i].SachID, TenSach = list[i].TenSach, TenTheLoai = list[i].TheLoai.TenTheLoai, GiaTien = list[i].GiaTien, GiaKhuyenMai = list[i].GiaKhuyenMai, MoTa = list[i].MoTa, TrangThai = list[i].TrangThai, SoLuong = list[i].SoLuong, TenCuaHang = list[i].Merchant.TenCuaHang };
                listSach.Add(bookHomeVM);
            }
            return Json(new { data = listSach, position = xPosition + 8, heightStyle = height + 610, top = 0 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}