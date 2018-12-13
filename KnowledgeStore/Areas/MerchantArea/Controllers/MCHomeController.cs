using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Model.ViewModel;
using KnowledgeStore.Common;
using Model.EntityFramework;
using Newtonsoft.Json;

namespace KnowledgeStore.Areas.MerchantArea.Controllers
{
    public class MCHomeController : Controller
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: MerchantArea/MCHome
        public ActionResult Index(string id, int? page)
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "AccountsMerchant");
            }
            var merchantID = db.Merchants.Where(m => m.Email == sessionUser.Email).Select(m => m.MerchantID).FirstOrDefault();
            ViewBag.MerchantID = merchantID;

            var listSach = db.Saches.Where(m=>m.MerchantID== merchantID).ToList();
            if (id != null)
            {
                if (id == "SachGiamGia")
                {
                    listSach = db.Saches.OrderByDescending(m => (m.GiaKhuyenMai / m.GiaTien)).Where(m => m.TrangThai == true).ToList();
                }
                else if (id == "SachMoiPhatHanh")
                {
                    listSach = db.Saches.OrderByDescending(m => m.NgayXuatBan).Where(m => m.TrangThai == true).ToList();
                }
                else if (id == "SachBanChay")
                {
                    listSach = db.Saches.OrderByDescending(m => m.LichSuNangTins.Max(n => n.NgayNang)).Where(m => m.TrangThai == true).ToList();
                }
            }
            var merchant = db.Merchants.Where(m => m.MerchantID == merchantID);
            ViewBag.TenChu = merchant.Select(m => m.HoTen).FirstOrDefault();
            ViewBag.TenShop = merchant.Select(m => m.TenCuaHang).FirstOrDefault();
            ViewBag.SoThang = System.DateTime.Now.Month- merchant.Select(m => m.NgayTao).FirstOrDefault().Month;
            ViewBag.SoSanPham = db.Saches.Where(m => m.MerchantID == merchantID).Count();
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(listSach.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult OverallPage()
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "AccountsMerchant");
            }
            var date = System.DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var dayFirst = firstDayOfMonth.Day;
            var dayLast = lastDayOfMonth.Day;

            var listDT = db.ChiTietDonHangs.Where(m => m.TinhTrangDonHangID == 4);
            List<DataPoint> dataPoints = new List<DataPoint>();
            var count = 0;
            for(int i=dayFirst;i<=dayLast;i++)
            {
                if (count <= dayLast)
                {
                    var dateTemp = firstDayOfMonth;
                    var countOrderSuccess = listDT.Where(m => m.DonHang.NgayDat == dateTemp).Count();
                    count++;
                    dataPoints.Add(new DataPoint( count, countOrderSuccess));
                    firstDayOfMonth.AddDays(count + 1);
                }
            }

            

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return View();
        }
    }
}