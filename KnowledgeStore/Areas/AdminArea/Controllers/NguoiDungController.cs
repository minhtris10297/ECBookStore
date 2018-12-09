using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Areas.AdminArea.Controllers
{
    public class NguoiDungController : Controller
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: AdminArea/NguoiDung
        public ActionResult Index()
        {
            var listCUS = db.Customers.ToList();

            return View(listCUS);
        }
    }
}