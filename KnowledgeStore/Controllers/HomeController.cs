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
        public ActionResult Index( string sort, string searchProduct)
        {
            var listSach = db.Saches.Where(m=>m.TrangThai==true).OrderByDescending(m=>m.LichSuNangTins.Max(n=>n.NgayNang)).ToList();
            if (!String.IsNullOrEmpty(searchProduct))
            {
                ViewBag.Search = searchProduct;
                listSach = listSach.Where(m => m.TenSach.ToLower().Contains(searchProduct.ToLower())
                                         || m.NhaXuatBan.TenNXB.ToLower().Contains(searchProduct.ToLower())).ToList();
            }
            if (!String.IsNullOrEmpty(sort))
            {
                ViewBag.Sort = sort;
                if (sort == "HangMoi")
                {
                    listSach = listSach.OrderByDescending(m => m.NgayXuatBan).ToList();
                }
                else if(sort == "GiaGiamDan")
                {
                    listSach = listSach.OrderByDescending(m => m.GiaTien).ToList();
                }
                else if (sort == "GiaTangDan")
                {
                    listSach = listSach.OrderBy(m => m.GiaTien).ToList();
                }
            }
            //if (id != null)
            //{
            //    if (id == "SachMoiPhatHanh")
            //    {
            //        listSach = listSach.OrderByDescending(m => m.NgayXuatBan).ToList();
            //    }
            //    else if (id == "SachBanChay")
            //    {
            //        listSach = listSach.OrderByDescending(m => m.NangTins.Max(n => n.NgayNang)).ToList();
            //    }
            //}
            //if (theLoai != null)
            //{
            //    listSach = listSach.Where(m => m.TheLoai.TenTheLoai == theLoai).ToList();
            //}
            //if (listSach.Count > 8)
            //{
            //    return View("Index", listSach.Take(8));
            //}
            
            return View("Index", listSach);
        }

        public JsonResult ViewMore(int xPosition, int height,string sort)
        {
            var listSach = new List<BookHomeVM>();
            var list = db.Saches.Where(m=>m.TrangThai==true).OrderByDescending(m => m.LichSuNangTins.Max(n => n.NgayNang)).ToList();
            if (!String.IsNullOrEmpty(sort))
            {
                if (sort == "HangMoi")
                {
                    list = list.OrderByDescending(m => m.NgayXuatBan).ToList();
                }
                else if (sort == "GiaGiamDan")
                {
                    list = list.OrderByDescending(m => m.GiaTien).ToList();
                }
                else if (sort == "GiaTangDan")
                {
                    list = list.OrderBy(m => m.GiaTien).ToList();
                }
            }
            for (int i = 0; i <= xPosition + 8; i++)
            {
                if (i<list.Count())
                {
                    BookHomeVM bookHomeVM = new BookHomeVM() { SachID = list[i].SachID, TenSach = list[i].TenSach, TenTheLoai = list[i].TheLoai.TenTheLoai, GiaTien = list[i].GiaTien, GiaKhuyenMai = list[i].GiaKhuyenMai, MoTa = list[i].MoTa, TrangThai = list[i].TrangThai, SoLuong = list[i].SoLuong, TenCuaHang = list[i].Merchant.TenCuaHang };
                    listSach.Add(bookHomeVM);
                }
                else
                {
                    break;
                }
                
            }
            return Json(new { data = listSach, position = xPosition + 8, heightStyle = height + 610, top = 0 }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchProduct(  string searchProduct)
        {
            var listSach = db.Saches.OrderByDescending(m => m.LichSuNangTins.Max(n => n.NgayNang)).ToList();
            if (!String.IsNullOrEmpty(searchProduct))
            {
                listSach = listSach.Where(m => m.TenSach.ToLower().Contains(searchProduct.ToLower())
                                         || m.NhaXuatBan.TenNXB.ToLower().Contains(searchProduct.ToLower())).Select(x => new Sach
                                         {
                                             SachID = x.SachID,
                                             TenSach = x.TenSach,
                                             GiaTien = x.GiaTien
                                         }).ToList();
            }
            int height = 610;
            if (listSach.Count() > 8)
            {
                if (listSach.Count() % 8 != 0)
                {
                    height = 610 * ((listSach.Count() ) / 8)+610;
                }
                else
                {
                    height = 610 * (listSach.Count() / 8);
                }
            }
            

            return Json(new { data= listSach ,heightStyle= height }, JsonRequestBehavior.AllowGet);


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

        public ActionResult MerchantRegSuccess()
        {
            return View();
        }
        public ActionResult ChinhSachNguoiDung()
        {
            return View();
        }
    }
}