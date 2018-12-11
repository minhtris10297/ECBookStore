using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Areas.AdminArea.Controllers
{
    public class DonHangController : Controller
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();

        // GET: AdminArea/DonHang
        public ActionResult Index()
        {
            ViewBag.HoaHong = db.HoaHongs.Where(p => p.TrangThai == true).First().PhanTranHoaHong;
            ViewBag.DropdownStatus = new SelectList(db.TinhTrangDonHangs, "TinhTrangDonHangID", "TinhTrangDonHang1");
            var listCTDH = db.ChiTietDonHangs.OrderByDescending(m => m.DonHang.NgayDat).ToList();
            return View(listCTDH);
        }
        [HttpPost]
        public ActionResult DangGiaoHang(int id)
        {
            db.ChiTietDonHangs.Find(id).TinhTrangDonHangID = 3;//thay doi thanh dang giao hang
            db.SaveChanges();
            ViewBag.HoaHong = db.HoaHongs.Where(p => p.TrangThai == true).First().PhanTranHoaHong;
            ViewBag.DropdownStatus = new SelectList(db.TinhTrangDonHangs, "TinhTrangDonHangID", "TinhTrangDonHang1");
            var listCTDH = db.ChiTietDonHangs.OrderByDescending(m => m.DonHang.NgayDat).ToList();
            return View("Index",listCTDH);
        }

        [HttpPost]
        public ActionResult Index(System.DateTime? searchTime, int? searchId, string nameCus, int? DropdownStatus)
        {         
            ViewBag.DropdownStatus = new SelectList(db.TinhTrangDonHangs, "TinhTrangDonHangID", "TinhTrangDonHang1");


            var listCTDH = db.ChiTietDonHangs.OrderByDescending(m => m.DonHang.NgayDat).ToList();
            if (ModelState.IsValid)
            {

                if (searchTime != null)
                {
                    ViewBag.SearchTime = searchTime.GetValueOrDefault(System.DateTime.Now).ToString("yyyy-MM-dd");
                    //listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id&m.DonHang.NgayDat==searchTime).OrderByDescending(m => m.DonHang.NgayDat);
                    listCTDH = listCTDH.Where(m => m.DonHang.NgayDat.Day == searchTime.GetValueOrDefault().Day & m.DonHang.NgayDat.Month == searchTime.GetValueOrDefault().Month & m.DonHang.NgayDat.Year == searchTime.GetValueOrDefault().Year).ToList();
                }
                if (searchId != null)
                {
                    //listCTDH= db.ChiTietDonHangs.Where(m => m.MerchantID == id&m.TinhTrangDonHangID==searchId).OrderByDescending(m => m.DonHang.NgayDat);
                    listCTDH = listCTDH.Where(m => m.ChiTietDonHangID == searchId).ToList();
                }
                if (nameCus != null)
                {
                    //listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id & m.DonHang.Customer.HoTen.Contains(nameCus)).OrderByDescending(m => m.DonHang.NgayDat);
                    listCTDH = listCTDH.Where(m => m.DonHang.Customer.HoTen.Contains(nameCus)).ToList();
                }
                if (DropdownStatus != null)
                {
                    //listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id & m.TinhTrangDonHangID == DropdownStatus).OrderByDescending(m => m.DonHang.NgayDat);
                    listCTDH = listCTDH.Where(m => m.TinhTrangDonHangID == DropdownStatus).ToList();
                }
            }
            return View(listCTDH);

        }
    }
}