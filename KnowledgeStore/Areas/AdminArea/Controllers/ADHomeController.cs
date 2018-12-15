
using Model.EntityFramework;
using Model.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Areas.AdminArea.Controllers
{
    public class ADHomeController : BaseController
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: AdminArea/ADHome
        public ActionResult Index()
        {
            decimal tongXu = 0;
            if (db.LichSuGiaoDichXuCuaMerchants.Count()!=0)
            {
                tongXu = db.LichSuGiaoDichXuCuaMerchants.Sum(m => m.SoXu);
            }
            
            ViewBag.TongXu = tongXu;

            var date = System.DateTime.Now;
            ViewBag.MonthNow = date.Month;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var lastDayOfLastMonth = firstDayOfMonth.AddDays(-1);
            var firstDayOfLastMonth = new DateTime(lastDayOfLastMonth.Year, lastDayOfLastMonth.Month, 1);

            var dayFirst = firstDayOfMonth.Day;
            var dayLast = lastDayOfMonth.Day;

            var listLSGDXu = db.LichSuGiaoDichXuCuaMerchants.OrderByDescending(m => m.NgayGiaoDich);
            List<DataPoint> dataPoints = new List<DataPoint>();
            var tongXuThang = 0;
            for (int i = dayFirst; i <= dayLast; i++)
            {
                    var count2 = 0;
                    var dateTemp = firstDayOfMonth;
                    if(listLSGDXu.Where(m =>m.NgayGiaoDich.Year==dateTemp.Year&m.NgayGiaoDich.Month==dateTemp.Month&m.NgayGiaoDich.Day==dateTemp.Day) != null)
                    {
                        foreach(var item in listLSGDXu.Where(m => m.NgayGiaoDich.Year == dateTemp.Year & m.NgayGiaoDich.Month == dateTemp.Month & m.NgayGiaoDich.Day == dateTemp.Day))
                        {
                            count2 += item.SoXu;
                        }
                    tongXuThang += count2;
                    }
                    
                    dataPoints.Add(new DataPoint(i, count2));
                    firstDayOfMonth = firstDayOfMonth.AddDays(1);
            }
            ViewBag.TongXuThang = tongXuThang;
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            var dayFirstLastMonth = firstDayOfLastMonth.Day;
            var lastDayLastMonth = lastDayOfLastMonth.Day;
            var tongXuThangTruoc = 0;
            for (int i = dayFirstLastMonth; i <= lastDayLastMonth; i++)
            {
                var count2 = 0;
                var dateTemp = firstDayOfLastMonth;
                if (listLSGDXu.Where(m => m.NgayGiaoDich.Year == dateTemp.Year & m.NgayGiaoDich.Month == dateTemp.Month & m.NgayGiaoDich.Day == dateTemp.Day) != null)
                {
                    foreach (var item in listLSGDXu.Where(m => m.NgayGiaoDich.Year == dateTemp.Year & m.NgayGiaoDich.Month == dateTemp.Month & m.NgayGiaoDich.Day == dateTemp.Day))
                    {
                        count2 += item.SoXu;
                    }
                    tongXuThangTruoc += count2;
                }
            }

            if (tongXuThangTruoc == 0)
            {
                ViewBag.DoanhThuTang = "Tháng trước chưa có doanh thu";
            }
            else if(tongXuThang==0)
            {
                ViewBag.DoanhThuTang = "Tháng này chưa có doanh thu";
            }
            else if (tongXuThangTruoc > tongXuThang)
            {
                var phantram = 100 - (tongXuThang * 100 / tongXuThangTruoc);
                ViewBag.DoanhThuTang = "Doanh thu tháng này giảm "+phantram+" % so với tháng trước";
            }
            else if (tongXuThangTruoc < tongXuThang)
            {
                var phantram = 100 - (tongXuThangTruoc * 100 / tongXuThang);
                ViewBag.DoanhThuTang = "Doanh thu tháng này tăng " + phantram + " % so với tháng trước";
            }
            return View(listLSGDXu.ToList());
        }
    }
}