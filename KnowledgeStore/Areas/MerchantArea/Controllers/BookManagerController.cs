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
using System.Web.Helpers;
using Common;
using System.IO;

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
        // GET: AdminArea/Saches/Create
        public ActionResult Create()
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "AccountsMerchant");
            }
            ViewBag.LoaiBiaID = new SelectList(db.LoaiBias, "LoaiBiaID", "LoaiBia1");
            ViewBag.NhaXuatBanID = new SelectList(db.NhaXuatBans, "NhaXuatBanID", "TenNXB");
            ViewBag.TheLoaiID = new SelectList(db.TheLoais, "TheLoaiID", "TenTheLoai");
            return View();
        }

        // POST: AdminArea/Saches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenSach,TacGia,NhaXuatBanID,NgayXuatBan,SoTrang,LoaiBiaID,GiaTien,GiaKhuyenMai,MoTa,SoLuong,TheLoaiID")] Sach sach, HttpPostedFileBase image1, HttpPostedFileBase image2, HttpPostedFileBase image3)
        {
            if (ModelState.IsValid)
            {
                
                var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
                if (sessionUser == null)
                {
                    return RedirectToAction("Login", "AccountsMerchant");
                }
                var id = db.Merchants.Where(m => m.Email == sessionUser.Email).Select(m => m.MerchantID).FirstOrDefault();
                sach.MerchantID = id;
                sach.TrangThai = false;
                db.LichSuNangTins.Add(new LichSuNangTin() { SachID = sach.SachID, NgayNang = System.DateTime.Now });
                db.Saches.Add(sach);
                db.SaveChanges();
                if (image1 != null)
                {
                    //Resize Image
                    WebImage img = new WebImage(image1.InputStream);
                    //img.Resize(500, 1000);

                    var filePathOriginal = Server.MapPath("/Assets/Image/ImageBook/" );
                    var fileName = sach.SachID + "_1"  + ".jpg";
                    string savedFileName = Path.Combine(filePathOriginal, fileName);
                    img.Save(savedFileName);
                }
                if (image2 != null)
                {
                    //Resize Image
                    WebImage img = new WebImage(image2.InputStream);
                    //img.Resize(500, 1000);

                    var filePathOriginal = Server.MapPath("/Assets/Image/ImageBook/" + sach.SachID + "/");
                    var fileName = sach.SachID + "_2" + ".jpg";
                    string savedFileName = Path.Combine(filePathOriginal, fileName);
                    img.Save(savedFileName);
                }
                if (image3 != null)
                {
                    //Resize Image
                    WebImage img = new WebImage(image3.InputStream);
                    //img.Resize(500, 1000);

                    var filePathOriginal = Server.MapPath("/Assets/Image/ImageBook/" + sach.SachID + "/");
                    var fileName = sach.SachID + "_3" + ".jpg";
                    string savedFileName = Path.Combine(filePathOriginal, fileName);
                    img.Save(savedFileName);
                }
                return RedirectToAction("Index");
            }

            ViewBag.LoaiBiaID = new SelectList(db.LoaiBias, "LoaiBiaID", "LoaiBia1", sach.LoaiBiaID);
            ViewBag.NhaXuatBanID = new SelectList(db.NhaXuatBans, "NhaXuatBanID", "TenNXB", sach.NhaXuatBanID);
            ViewBag.TheLoaiID = new SelectList(db.TheLoais, "TheLoaiID", "TenTheLoai", sach.TheLoaiID);
            return View(sach);
        }

        public JsonResult NangTin(int id)
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            var idMer = db.Merchants.Where(m => m.Email == sessionUser.Email).Select(m => m.MerchantID).FirstOrDefault();
            var merchant = db.Merchants.Where(m => m.MerchantID == idMer).FirstOrDefault();
            var soLuongKPI = merchant.SoLuongKIPXu - 10;
            merchant.SoLuongKIPXu = soLuongKPI;

            db.LichSuNangTins.Add(new LichSuNangTin() { SachID = id, NgayNang = System.DateTime.Now });
            db.SaveChanges();
            return Json(new { status = true });
        }
        public JsonResult ThemSach(int id,int num)
        {
            var sach = db.Saches.Find(id);
            sach.SoLuong = sach.SoLuong + num;
            db.SaveChanges();
            return Json(new { status = true });
        }
    }

}