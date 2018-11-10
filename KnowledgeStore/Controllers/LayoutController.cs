using KnowledgeStore.Common;
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
        // GET: Layout
        public PartialViewResult TopbarUserDisplay()
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (sessionUser != null)
            {
                ViewBag.DisplayUserName = sessionUser.UserName;
            }
            return PartialView("_TopbarUserDisplay");
        }
    }
}