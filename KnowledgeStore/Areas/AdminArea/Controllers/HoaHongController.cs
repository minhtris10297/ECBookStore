using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Areas.AdminArea.Controllers
{
    public class HoaHongController : Controller
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: AdminArea/HoaHong
        public ActionResult Index()
        {
            return View();
        }
    }
}