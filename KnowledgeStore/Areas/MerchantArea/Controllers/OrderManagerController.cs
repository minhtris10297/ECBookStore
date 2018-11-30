using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Areas.MerchantArea.Controllers
{
    public class OrderManagerController : BaseController
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: Merchant/OrderManager
        public ActionResult Index( int id)
        {
            ViewBag.IdMerchant = id;
            ViewBag.DropdownStatus = new SelectList(db.TinhTrangDonHangs, "TinhTrangDonHangID", "TinhTrangDonHang1");

            var listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id).OrderByDescending(m=>m.DonHang.NgayDat).ToList();
            return View(listCTDH);
        }

        [HttpPost]
        public ActionResult Index(int id, System.DateTime? searchTime,int? searchId, string nameCus,int? DropdownStatus)
        {
            ViewBag.IdMerchant = id;
            ViewBag.DropdownStatus = new SelectList(db.TinhTrangDonHangs, "TinhTrangDonHangID", "TinhTrangDonHang1");
            ViewBag.SearchTime = searchTime.GetValueOrDefault(System.DateTime.Now).ToString("yyyy-MM-dd");

            var listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id).OrderByDescending(m => m.DonHang.NgayDat).ToList();
            if (ModelState.IsValid)
            {
                
                if (searchTime != null)
                {
                    //listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id&m.DonHang.NgayDat==searchTime).OrderByDescending(m => m.DonHang.NgayDat);
                    listCTDH = listCTDH.Where(m=>m.DonHang.NgayDat.Day==searchTime.GetValueOrDefault().Day& m.DonHang.NgayDat.Month== searchTime.GetValueOrDefault().Month& m.DonHang.NgayDat.Year== searchTime.GetValueOrDefault().Year).ToList();
                }
                if (searchId != null)
                {
                    //listCTDH= db.ChiTietDonHangs.Where(m => m.MerchantID == id&m.TinhTrangDonHangID==searchId).OrderByDescending(m => m.DonHang.NgayDat);
                    listCTDH = listCTDH.Where(m => m.TinhTrangDonHangID == searchId).ToList();
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

        public ActionResult ChangeDeliveryStatus(int id,int idCtdh)
        {
            ViewBag.IdMerchant = id;

            var ctdh = db.ChiTietDonHangs.Find(idCtdh);
            ctdh.TinhTrangDonHangID = 2;
            db.SaveChanges();

            var listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id).OrderByDescending(m => m.DonHang.NgayDat).ToList();
            return RedirectToAction("Index","OrderManager",new { id=id});
        }
    }
}