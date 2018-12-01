using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EntityFramework;

namespace KnowledgeStore.Controllers
{
    public class MerchantsController : Controller
    {
        private KnowledgeStoreEntities db = new KnowledgeStoreEntities();

        // GET: Merchants
        public ActionResult Index()
        {
            var merchants = db.Merchants.Include(m => m.GioiTinh).Include(m => m.GioiTinh1);
            return View(merchants.ToList());
        }

        // GET: Merchants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Merchant merchant = db.Merchants.Find(id);
            if (merchant == null)
            {
                return HttpNotFound();
            }
            return View(merchant);
        }

        // GET: Merchants/Create
        public ActionResult Create()
        {
            ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh");
            ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh");
            return View();
        }

        // POST: Merchants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MerchantID,Email,HoTen,DiaChi,GioiTinhID,TenCuaHang,SoLuongKIPXu,NgayTao")] Merchant merchant)
        {
            if (ModelState.IsValid)
            {
                db.Merchants.Add(merchant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh", merchant.GioiTinhID);
            ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh", merchant.GioiTinhID);
            return View(merchant);
        }

        // GET: Merchants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Merchant merchant = db.Merchants.Find(id);
            if (merchant == null)
            {
                return HttpNotFound();
            }
            ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh", merchant.GioiTinhID);
            ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh", merchant.GioiTinhID);
            return View(merchant);
        }

        // POST: Merchants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MerchantID,Email,HoTen,DiaChi,GioiTinhID,TenCuaHang,SoLuongKIPXu,NgayTao")] Merchant merchant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(merchant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh", merchant.GioiTinhID);
            ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh", merchant.GioiTinhID);
            return View(merchant);
        }

        // GET: Merchants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Merchant merchant = db.Merchants.Find(id);
            if (merchant == null)
            {
                return HttpNotFound();
            }
            return View(merchant);
        }

        // POST: Merchants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Merchant merchant = db.Merchants.Find(id);
            db.Merchants.Remove(merchant);
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
