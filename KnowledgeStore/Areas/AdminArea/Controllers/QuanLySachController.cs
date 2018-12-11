using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Areas.AdminArea.Controllers
{
    public class QuanLySachController : Controller
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: AdminArea/QuanLySach
        public ActionResult Index()
        {
            var listSach = db.Saches.ToList();
            return View(listSach);
        }
    }
}