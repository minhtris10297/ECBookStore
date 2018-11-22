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

        public JsonResult ViewMore(int height, int xPosition)
        {
            var listSach = new List<BookHomeVM>();
            var list = db.Saches.OrderByDescending(p => p.SachID).ToList();

            for (int i = 0; i < xPosition + 8; i++)
            {
                BookHomeVM bookHomeVM = new BookHomeVM() { SachID = list[i].SachID, TenSach = list[i].TenSach, TenTheLoai = list[i].TheLoai.TenTheLoai, GiaTien = list[i].GiaTien, GiaKhuyenMai = list[i].GiaKhuyenMai, MoTa = list[i].MoTa, TrangThai = list[i].TrangThai, SoLuong = list[i].SoLuong, TenCuaHang = list[i].Merchant.TenCuaHang };
                listSach.Add(bookHomeVM);
            }
            return Json(new { data = listSach, heightStyle = height + 610, position = xPosition + 8, top = 0 }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ViewNew(int height, int xPosition)
        {
            var listSach = new List<BookHomeVM>();
            var list = db.Saches.OrderByDescending(p => p.NgayXuatBan).ToList();

            for (int i = 0; i < xPosition + 8; i++)
            {
                BookHomeVM bookHomeVM = new BookHomeVM() { SachID = list[i].SachID, TenSach = list[i].TenSach, TenTheLoai = list[i].TheLoai.TenTheLoai, GiaTien = list[i].GiaTien, GiaKhuyenMai = list[i].GiaKhuyenMai, MoTa = list[i].MoTa, TrangThai = list[i].TrangThai, SoLuong = list[i].SoLuong, TenCuaHang = list[i].Merchant.TenCuaHang };
                listSach.Add(bookHomeVM);
            }
            return Json(new { data = listSach, heightStyle = height + 610, position = xPosition + 8, top = 0 }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ViewBestSales(int height, int xPosition)
        {
            var listSach = new List<BookHomeVM>();
            var list = db.Saches.Where(p => p.GiaKhuyenMai != null)
                .OrderBy(p => p.GiaKhuyenMai).ToList();

            for (int i = 0; i < xPosition + 8; i++)
            {
                if (i > list.Count - 1)
                    break;
                BookHomeVM bookHomeVM = new BookHomeVM() { SachID = list[i].SachID, TenSach = list[i].TenSach, TenTheLoai = list[i].TheLoai.TenTheLoai, GiaTien = list[i].GiaTien, GiaKhuyenMai = list[i].GiaKhuyenMai, MoTa = list[i].MoTa, TrangThai = list[i].TrangThai, SoLuong = list[i].SoLuong, TenCuaHang = list[i].Merchant.TenCuaHang };
                listSach.Add(bookHomeVM);


            }
            return Json(new { data = listSach, heightStyle = height + 610, position = xPosition + 8, top = 0 }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ViewLowPrice(int height, int xPosition)
        {
            var listSach = new List<BookHomeVM>();
            var list = db.Saches
                .OrderBy(p => p.GiaTien).ToList();

            for (int i = 0; i < xPosition + 8; i++)
            {

                BookHomeVM bookHomeVM = new BookHomeVM() { SachID = list[i].SachID, TenSach = list[i].TenSach, TenTheLoai = list[i].TheLoai.TenTheLoai, GiaTien = list[i].GiaTien, GiaKhuyenMai = list[i].GiaKhuyenMai, MoTa = list[i].MoTa, TrangThai = list[i].TrangThai, SoLuong = list[i].SoLuong, TenCuaHang = list[i].Merchant.TenCuaHang };
                listSach.Add(bookHomeVM);


            }
            return Json(new { data = listSach, heightStyle = height + 610, position = xPosition + 8, top = 0 }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ViewHighPrice(int height, int xPosition)
        {
            var listSach = new List<BookHomeVM>();
            var list = db.Saches
                .OrderByDescending(p => p.GiaTien).ToList();

            for (int i = 0; i < xPosition + 8; i++)
            {

                BookHomeVM bookHomeVM = new BookHomeVM() { SachID = list[i].SachID, TenSach = list[i].TenSach, TenTheLoai = list[i].TheLoai.TenTheLoai, GiaTien = list[i].GiaTien, GiaKhuyenMai = list[i].GiaKhuyenMai, MoTa = list[i].MoTa, TrangThai = list[i].TrangThai, SoLuong = list[i].SoLuong, TenCuaHang = list[i].Merchant.TenCuaHang };
                listSach.Add(bookHomeVM);


            }
            return Json(new { data = listSach, heightStyle = height + 610, position = xPosition + 8, top = 0 }, JsonRequestBehavior.AllowGet);

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