using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Areas.Merchant.Controllers
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

            var listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id).OrderByDescending(m => m.DonHang.NgayDat);
            if (searchTime != null)
            {
                listCTDH.Where(m => m.DonHang.NgayDat == searchTime);
            }
            if (searchId != null)
            {
                listCTDH.Where(m => m.ChiTietDonHangID == searchId);
            }
            if (nameCus != null)
            {
                listCTDH.Where(m => m.DonHang.Customer.HoTen.Contains(nameCus));
            }
            if (DropdownStatus != null)
            {
                listCTDH.Where(m => m.TinhTrangDonHangID == DropdownStatus);
            }
            return View(listCTDH.ToList());
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