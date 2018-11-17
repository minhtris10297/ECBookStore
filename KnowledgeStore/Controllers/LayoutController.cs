using KnowledgeStore.Common;
using Model.EntityFramework;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Controllers
{
    public class LayoutController : Controller
    {
        private const string CartSession = "CartSession";
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: Layout
        public PartialViewResult TopbarUserDisplay()
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (sessionUser != null)
            {
                ViewBag.DisplayUserName = sessionUser.UserName;
            }
            return PartialView( );
        }

        public PartialViewResult CartModalDisplay()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            decimal sum = 0;
            if (cart != null)
            {
                list = (List<CartItem>)cart;
                foreach(var item in list)
                {
                    if (item.Sach.GiaKhuyenMai == null)
                    {
                        sum += item.Quantity * item.Sach.GiaTien;
                    }
                    else
                    {
                        sum+= item.Quantity * item.Sach.GiaKhuyenMai.GetValueOrDefault(0);
                    }
                }
                ViewBag.ThanhTien = sum.ToString("N0");
            }
            return PartialView(list);
        }

        public PartialViewResult CartIconDisplay()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            
            int sum = 0;
            if (cart != null)
            {
                list = (List<CartItem>)cart;
                foreach (var item in list)
                {
                    sum += item.Quantity;
                }
            }
            
            ViewBag.NumCart = sum;

            return PartialView();
        }
        public PartialViewResult TopbarTypeBookDisplay()
        {
            ViewBag.TheLoai = db.Saches.Select(m =>  m.TheLoai.TenTheLoai).Distinct();
            return PartialView();
        }
    }
}