using KnowledgeStore.Common;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Areas.MerchantArea.Controllers
{
    public class LayoutMerchantController : Controller
    {
        public PartialViewResult TopbarMenuDisplay()
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            if (sessionUser != null)
            {
                ViewBag.DisplayUserName = sessionUser.UserName;
            }
            return PartialView();
        }
    }
}