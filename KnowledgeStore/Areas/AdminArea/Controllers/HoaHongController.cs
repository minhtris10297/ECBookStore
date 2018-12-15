using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Areas.AdminArea.Controllers
{
    public class HoaHongController : BaseController
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: AdminArea/HoaHong
        public ActionResult Index()
        {
            ViewBag.HoaHong = db.HoaHongs.Where(p => p.TrangThai == true).First().PhanTranHoaHong.ToString();
            var listHoaHong = db.HoaHongs.OrderByDescending(p => p.NgayBatDau).ToList();
            return View(listHoaHong);
        }
        public ActionResult NewHoaHong(int NewHoaHong)
        {
            db.HoaHongs.Where(p => p.TrangThai == true).First().TrangThai = false;
            var hoahong = new HoaHong();
            hoahong.NgayBatDau = DateTime.Now;
            hoahong.PhanTranHoaHong = NewHoaHong;
            hoahong.TrangThai = true;
            db.HoaHongs.Add(hoahong);
            db.SaveChanges();
            ViewBag.HoaHong = db.HoaHongs.Where(p => p.TrangThai == true).First().PhanTranHoaHong.ToString();
            var listHoaHong = db.HoaHongs.OrderByDescending(p => p.NgayBatDau).ToList();
            return View("Index", listHoaHong);
        }
    }
}