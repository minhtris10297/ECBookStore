using KnowledgeStore.Common;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KnowledgeStore.Areas.AdminArea.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USERMADMIN_SESSION];
            if (session == null )
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccountsAdmin", action = "Login"}));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}