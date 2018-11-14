using KnowledgeStore.Common;
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
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }

    }
}