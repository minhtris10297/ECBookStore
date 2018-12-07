using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EntityFramework;

namespace KnowledgeStore.Areas.AdminArea.Controllers
{
    public class SachesController : Controller
    {
        private KnowledgeStoreEntities db = new KnowledgeStoreEntities();

        // GET: AdminArea/Saches
        public ActionResult Index()
        {
            var saches = db.Saches.Include(s => s.LoaiBia).Include(s => s.Merchant).Include(s => s.NhaXuatBan).Include(s => s.TheLoai);
            return View(saches.ToList());
        }

        // GET: AdminArea/Saches/Details/5
        public ActionResult Details(int? id)
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
            return View(sach);
        }

        // GET: AdminArea/Saches/Create
        public ActionResult Create()
        {
            ViewBag.LoaiBiaID = new SelectList(db.LoaiBias, "LoaiBiaID", "LoaiBia1");
            ViewBag.MerchantID = new SelectList(db.Merchants, "MerchantID", "Email");
            ViewBag.NhaXuatBanID = new SelectList(db.NhaXuatBans, "NhaXuatBanID", "TenNXB");
            ViewBag.TheLoaiID = new SelectList(db.TheLoais, "TheLoaiID", "TenTheLoai");
            return View();
        }

        // POST: AdminArea/Saches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SachID,TenSach,TacGia,NhaXuatBanID,NgayXuatBan,SoTrang,LoaiBiaID,MerchantID,TrangThai,GiaTien,GiaKhuyenMai,MoTa,SoLuong,TheLoaiID")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                db.Saches.Add(sach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoaiBiaID = new SelectList(db.LoaiBias, "LoaiBiaID", "LoaiBia1", sach.LoaiBiaID);
            ViewBag.MerchantID = new SelectList(db.Merchants, "MerchantID", "Email", sach.MerchantID);
            ViewBag.NhaXuatBanID = new SelectList(db.NhaXuatBans, "NhaXuatBanID", "TenNXB", sach.NhaXuatBanID);
            ViewBag.TheLoaiID = new SelectList(db.TheLoais, "TheLoaiID", "TenTheLoai", sach.TheLoaiID);
            return View(sach);
        }

        // GET: AdminArea/Saches/Edit/5
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

        // GET: AdminArea/Saches/Delete/5
        public ActionResult Delete(int? id)
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
            return View(sach);
        }

        // POST: AdminArea/Saches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sach sach = db.Saches.Find(id);
            db.Saches.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
