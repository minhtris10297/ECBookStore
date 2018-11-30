using KnowledgeStore.Common;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KnowledgeStore.Areas.MerchantArea.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            if (session == null )
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccountsMerchant", action = "Login", Area = "MerchantArea" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}