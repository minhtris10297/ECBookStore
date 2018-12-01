using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Model.ViewModel;
using KnowledgeStore.Common;

namespace KnowledgeStore.Controllers
{
    public class ListBooksController : Controller
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        private const string CartSession = "CartSession";
        
        // GET: ListBooks
        public ActionResult Index(string id, int? page,string theLoai)
        {
            ViewBag.TypeID = id;
            var listSach = db.Saches.ToList();
            if (id != null)
            {
                if (id == "SachGiamGia")
                {
                    listSach = db.Saches.OrderByDescending(m => m.GiaKhuyenMai / m.GiaTien).ToList();
                }
                else if (id == "SachMoiPhatHanh")
                {
                    listSach = db.Saches.OrderByDescending(m => m.NgayXuatBan).ToList();
                }
                else if (id == "SachBanChay")
                {
                    listSach = db.Saches.OrderByDescending(m => m.LichSuNangTins.Max(n => n.NgayNang)).ToList();
                }
            }
            if (theLoai != null)
            {
                listSach = db.Saches.Where(m => m.TheLoai.TenTheLoai==theLoai).ToList();
            }
            
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(listSach.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult BookDetail(int id)
        {
            var book = db.Saches.Find(id);
            ViewBag.BookId = id;
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
                if (list.Exists(x => x.Sach.SachID == id))
                {

                    foreach (var item in list)
                    {
                        ViewBag.QuantityMax =book.SoLuong- item.Quantity;
                    }
                }
                else
                {
                    ViewBag.QuantityMax = book.SoLuong;
                }
            }
            else
            {
                ViewBag.QuantityMax = book.SoLuong;
            }

            var product = db.Saches.Find(id);
            ViewBag.RelatedProduct = db.Saches.Where(p => p.TheLoaiID == product.TheLoaiID).ToList();


            var sumStar = db.DanhGiaCuaCustomers.Where(p => p.SachID == product.SachID).ToList();
            if(sumStar.Count == 0)
            {
                ViewBag.sumStar = 0;
            }
            else { ViewBag.sumStar = db.DanhGiaCuaCustomers.Where(p => p.SachID == product.SachID).Sum(p => p.SoSao); }

            ViewBag.countStar = db.DanhGiaCuaCustomers.Where(p => p.SachID == product.SachID).Count();

            ViewBag.comment = db.DanhGiaCuaCustomers.Where(p => p.SachID == id).ToList();

            return View(book);
        }
        [HttpPost]
        public ActionResult Rating(DanhGiaCuaCustomer comment)
        {
            var session = Session[CommonConstants.USER_SESSION];
          

            if (session == null)
                return View("Chua dang nhap");
            else
            {
                db.DanhGiaCuaCustomers.Add(comment);                
            }
            return View();
        }
    }
}