using KnowledgeStore.Common;
using Model.EntityFramework;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Areas.AdminArea.Controllers
{
    public class ADHomeController : Controller
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: AdminArea/ADHome
        public ActionResult Index()
        {
           
           
            return View();
            
        }
    }
}