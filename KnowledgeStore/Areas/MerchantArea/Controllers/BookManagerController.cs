 using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using KnowledgeStore.Common;
using Model.ViewModel;

namespace KnowledgeStore.Areas.MerchantArea.Controllers
{
    public class BookManagerController : Controller
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: MerchantArea/BookManager
        public ActionResult Index()
        {
            ViewBag.dropDownNXB = new SelectList(db.NhaXuatBans, "NhaXuatBanID", "TenNXB");
            ViewBag.dropDownTheLoai = new SelectList(db.TheLoais, "TheLoaiID", "TenTheLoai");

            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            var id = db.Merchants.Where(m => m.Email == sessionUser.Email).Select(m => m.MerchantID).FirstOrDefault();
            var listSach = db.Saches.Where(m => m.MerchantID == id).OrderByDescending(m => m.LichSuNangTins.Max(n => n.NgayNang)).ToList();
            return View(listSach);
        }
        [HttpPost]
        public ActionResult Index(int? dropDownNXB, int? searchId, string nameSach, int? dropDownTheLoai)
        {
            ViewBag.dropDownNXB = new SelectList(db.NhaXuatBans, "NhaXuatBanID", "TenNXB");
            ViewBag.dropDownTheLoai = new SelectList(db.TheLoais, "TheLoaiID", "TenTheLoai");

            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            var id = db.Merchants.Where(m => m.Email == sessionUser.Email).Select(m => m.MerchantID).FirstOrDefault();

            var listSach = db.Saches.Where(m => m.MerchantID == id).OrderByDescending(m => m.LichSuNangTins.Max(n=>n.NgayNang)).ToList();
            if (ModelState.IsValid)
            {

                if (dropDownNXB != null)
                {
                    //listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id&m.DonHang.NgayDat==searchTime).OrderByDescending(m => m.DonHang.NgayDat);
                    listSach = listSach.Where(m => m.NhaXuatBanID==dropDownNXB).ToList();
                }
                if (dropDownTheLoai != null)
                {
                    //listCTDH= db.ChiTietDonHangs.Where(m => m.MerchantID == id&m.TinhTrangDonHangID==searchId).OrderByDescending(m => m.DonHang.NgayDat);
                    listSach = listSach.Where(m => m.TheLoaiID==dropDownTheLoai).ToList();
                }
                if (nameSach != null)
                {
                    //listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id & m.DonHang.Customer.HoTen.Contains(nameCus)).OrderByDescending(m => m.DonHang.NgayDat);
                    listSach = listSach.Where(m => m.TenSach.Contains(nameSach)).ToList();
                }
                if (searchId != null)
                {
                    //listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id & m.TinhTrangDonHangID == DropdownStatus).OrderByDescending(m => m.DonHang.NgayDat);
                    listSach = listSach.Where(m => m.SachID== searchId).ToList();
                }
            }
            return View(listSach);

        }
    }

}