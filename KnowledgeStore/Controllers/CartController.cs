using Model.EntityFramework;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace KnowledgeStore.Controllers
{
    public class CartController : Controller
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();

        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            decimal tamTinh = 0;
            foreach(var item in list)
            {
                tamTinh += item.Quantity * item.Sach.GiaTien;
            }
            ViewBag.TamTinh = tamTinh.ToString("N0");
            ViewBag.ThanhTien= tamTinh.ToString("N0");
            return View(list);
        }

        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(int id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Sach.SachID == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Sach.SachID == item.Sach.SachID);
                if (jsonItem != null)
                {
                    if (jsonItem.Quantity == 0)
                    {
                        sessionCart.RemoveAll(x=>x.Sach.SachID== item.Sach.SachID);
                    }
                    else
                    {
                        item.Quantity = jsonItem.Quantity;

                    }
                    
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddItem(int id, int quantity)
        {
            var product = db.Saches.Find(id);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Sach.SachID == id))
                {

                    foreach (var item in list)
                    {
                        if (item.Sach.SachID == id)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.Sach = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gán vào session
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.Sach = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Success()
        {
            return View();
        }
    }
}