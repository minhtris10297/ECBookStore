 using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using KnowledgeStore.Common;
using Model.ViewModel;
using System.Data.Entity;
using System.Net;

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
            ViewBag.GiaNangTin = db.GiaTiens.Where(m => m.GiaTienID == 1).Select(m => m.TyGia).FirstOrDefault();

            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "AccountsMerchant");
            }
            var id = db.Merchants.Where(m => m.Email == sessionUser.Email).Select(m => m.MerchantID).FirstOrDefault();
            var listSach = db.Saches.Where(m => m.MerchantID == id).OrderByDescending(m => m.LichSuNangTins.Max(n => n.NgayNang)).ToList();
            return View(listSach);
        }
        [HttpPost]
        public ActionResult Index(int? dropDownNXB, int? searchId, string nameSach, int? dropDownTheLoai)
        {
            ViewBag.dropDownNXB = new SelectList(db.NhaXuatBans, "NhaXuatBanID", "TenNXB");
            ViewBag.dropDownTheLoai = new SelectList(db.TheLoais, "TheLoaiID", "TenTheLoai");
            ViewBag.GiaNangTin = db.GiaTiens.Where(m => m.GiaTienID == 1).Select(m => m.TyGia).FirstOrDefault();

            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            var id = db.Merchants.Where(m => m.Email == sessionUser.Email).Select(m => m.MerchantID).FirstOrDefault();

            var listSach = db.Saches.Where(m => m.MerchantID == id).OrderByDescending(m => m.LichSuNangTins.Max(n => n.NgayNang)).ToList();
            if (ModelState.IsValid)
            {

                if (dropDownNXB != null)
                {
                    //listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id&m.DonHang.NgayDat==searchTime).OrderByDescending(m => m.DonHang.NgayDat);
                    listSach = listSach.Where(m => m.NhaXuatBanID == dropDownNXB).ToList();
                }
                if (dropDownTheLoai != null)
                {
                    //listCTDH= db.ChiTietDonHangs.Where(m => m.MerchantID == id&m.TinhTrangDonHangID==searchId).OrderByDescending(m => m.DonHang.NgayDat);
                    listSach = listSach.Where(m => m.TheLoaiID == dropDownTheLoai).ToList();
                }
                if (nameSach != null)
                {
                    //listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id & m.DonHang.Customer.HoTen.Contains(nameCus)).OrderByDescending(m => m.DonHang.NgayDat);
                    listSach = listSach.Where(m => m.TenSach.Contains(nameSach)).ToList();
                }
                if (searchId != null)
                {
                    //listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id & m.TinhTrangDonHangID == DropdownStatus).OrderByDescending(m => m.DonHang.NgayDat);
                    listSach = listSach.Where(m => m.SachID == searchId).ToList();
                }
            }
            return View(listSach);

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiBiaID = new SelectList(db.LoaiBias, "LoaiBiaID", "LoaiBia1", sach.LoaiBiaID);
            ViewBag.MerchantID = new SelectList(db.Merchants, "MerchantID", "Email", sach.MerchantID);
            ViewBag.NhaXuatBanID = new SelectList(db.NhaXuatBans, "NhaXuatBanID", "TenNXB", sach.NhaXuatBanID);
            ViewBag.TheLoaiID = new SelectList(db.TheLoais, "TheLoaiID", "TenTheLoai", sach.TheLoaiID);
            return View(sach);
        }

        // POST: AdminArea/Saches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SachID,TenSach,TacGia,NhaXuatBanID,NgayXuatBan,SoTrang,LoaiBiaID,MerchantID,TrangThai,GiaTien,GiaKhuyenMai,MoTa,SoLuong,TheLoaiID")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoaiBiaID = new SelectList(db.LoaiBias, "LoaiBiaID", "LoaiBia1", sach.LoaiBiaID);
            ViewBag.MerchantID = new SelectList(db.Merchants, "MerchantID", "Email", sach.MerchantID);
            ViewBag.NhaXuatBanID = new SelectList(db.NhaXuatBans, "NhaXuatBanID", "TenNXB", sach.NhaXuatBanID);
            ViewBag.TheLoaiID = new SelectList(db.TheLoais, "TheLoaiID", "TenTheLoai", sach.TheLoaiID);
            return View(sach);
        }
    }

}