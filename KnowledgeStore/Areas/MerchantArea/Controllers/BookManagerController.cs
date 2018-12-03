using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace KnowledgeStore.Areas.MerchantArea.Controllers
{
    public class BookManagerController : Controller
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: MerchantArea/BookManager
        public ActionResult Index(int id, int? page,string type)
        {
            var list = db.Saches.Where(m => m.MerchantID == id).ToList();
            ViewBag.MerchantID = id;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }
    }
}