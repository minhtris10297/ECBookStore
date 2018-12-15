using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Areas.AdminArea.Controllers
{
    public class KipXuController : BaseController
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: AdminArea/KipXu
        public ActionResult Index()
        {
            ViewBag.giatri = db.GiaTriKIPXus.FirstOrDefault().GiaTriXu;
            var listXu = db.LichSuGiaoDichXuCuaMerchants.ToList();
            return View(listXu);
        }
    }
}