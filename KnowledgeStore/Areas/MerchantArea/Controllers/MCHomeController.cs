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
using System.Web.Helpers;
using System.IO;

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

        [HttpPost]
        public ActionResult Index(string id, int? page, HttpPostedFileBase image,string name)
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "AccountsMerchant");
            }
            var merchantID = db.Merchants.Where(m => m.Email == sessionUser.Email).Select(m => m.MerchantID).FirstOrDefault();
            ViewBag.MerchantID = merchantID;

            var listSach = db.Saches.Where(m => m.MerchantID == merchantID).ToList();
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
            ViewBag.SoThang = System.DateTime.Now.Month - merchant.Select(m => m.NgayTao).FirstOrDefault().Month;
            ViewBag.SoSanPham = db.Saches.Where(m => m.MerchantID == merchantID).Count();
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            //Sua thong tin merchant
            if (image != null)
            {
                //Resize Image
                WebImage img = new WebImage(image.InputStream);
                //img.Resize(500, 1000);

                var filePathOriginal = Server.MapPath("/Assets/Image/Merchant/");
                var fileName =  +merchantID + ".jpg";
                string savedFileName = Path.Combine(filePathOriginal, fileName);
                img.Save(savedFileName);
            }

            merchant.FirstOrDefault().TenCuaHang = name;
            db.SaveChanges();

            return View(listSach.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult OverallPage()
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "AccountsMerchant");
            }
            var merchant = db.Merchants.Where(m => m.Email == sessionUser.Email);
            var merchantID = merchant.Select(m => m.MerchantID).FirstOrDefault();
            var date = System.DateTime.Now;
            ViewBag.MonthNow = date.Month;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var lastDayOfLastMonth = firstDayOfMonth.AddDays(-1);
            var firstDayOfLastMonth = new DateTime(lastDayOfLastMonth.Year,lastDayOfLastMonth.Month,1);
            var dayFirst = firstDayOfMonth.Day;
            var dayLast = lastDayOfMonth.Day;

            var listDT = db.ChiTietDonHangs.Where(m => m.TinhTrangDonHangID == 4);
            List<DataPoint> dataPoints = new List<DataPoint>();
            double tongDoanhThuThang = 0;
            
            var tienHoaHong = (float)(db.HoaHongs.OrderByDescending(m => m.HoaHongID).Select(m => m.PhanTranHoaHong).FirstOrDefault())/100;
            
            for (int i=dayFirst;i<=dayLast;i++)
            {
                    double count2 = 0;
                    var dateTemp = firstDayOfMonth;
                    var countOrderSuccess = listDT.Where(m => m.DonHang.NgayDat.Year == dateTemp.Year & m.DonHang.NgayDat .Month==dateTemp.Month & m.DonHang.NgayDat.Day==dateTemp.Day& m.MerchantID==merchantID).Count();
                    if(countOrderSuccess>0)
                    {
                        var listtemp= listDT.Where(m =>  m.DonHang.NgayDat.Year == dateTemp.Year & m.DonHang.NgayDat .Month==dateTemp.Month & m.DonHang.NgayDat.Day == dateTemp.Day & m.MerchantID==merchantID).Select(m => m.Sach.GiaKhuyenMai ?? m.Sach.GiaTien);
                        foreach(var item in listtemp)
                        {
                            if ((((float)item * tienHoaHong) % 1000) < 500)
                            {
                                tongDoanhThuThang += (float)item - (float)item * tienHoaHong - ((float)item * tienHoaHong % 1000);
                                count2+= (float)item - (float)item * tienHoaHong - ((float)item * tienHoaHong % 1000);
                        }
                            else
                            {
                                tongDoanhThuThang += (float)item - ((float)item * tienHoaHong - ((float)item * tienHoaHong % 1000) + 1000);
                                count2+= (float)item - (float)item * tienHoaHong - ((float)item * tienHoaHong % 1000) + 1000;
                        }
                        }
                    }
                    
                    dataPoints.Add(new DataPoint( i, count2));
                    firstDayOfMonth=firstDayOfMonth.AddDays( 1);
            }

            

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            ViewBag.TongDoanhThuThang = tongDoanhThuThang;

            float tongDoanhThuThangTruoc = 0;
            var dayFirstLastMonth = firstDayOfLastMonth.Day;
            var lastDayLastMonth = lastDayOfLastMonth.Day;
            for (int i = dayFirstLastMonth; i <= lastDayLastMonth; i++)
            {
                    var dateTemp = firstDayOfLastMonth;
                    var countOrderSuccess = listDT.Where(m =>  m.DonHang.NgayDat.Year == dateTemp.Year & m.DonHang.NgayDat .Month==dateTemp.Month & m.DonHang.NgayDat.Day == dateTemp.Day).Count();
                    if(listDT.Where(m =>  m.DonHang.NgayDat.Year == dateTemp.Year & m.DonHang.NgayDat .Month==dateTemp.Month & m.DonHang.NgayDat.Day == dateTemp.Day) != null)
                    {
                        var listtemp = listDT.Where(m =>  m.DonHang.NgayDat.Year == dateTemp.Year & m.DonHang.NgayDat .Month==dateTemp.Month & m.DonHang.NgayDat.Day == dateTemp.Day & m.MerchantID==merchantID).Select(m => m.Sach.GiaKhuyenMai ?? m.Sach.GiaTien);
                        foreach (var item in listtemp)
                        {
                            tongDoanhThuThangTruoc += (float)item;
                        }
                    }
                    
                    firstDayOfLastMonth.AddDays( 1);
            }
            

            if (tongDoanhThuThangTruoc == 0)
            {
                ViewBag.DoanhThuTang = "Tháng trước chưa có doanh thu";
            }
            else if (tongDoanhThuThang == 0)
            {
                ViewBag.DoanhThuTang = "Tháng này chưa có doanh thu";
            }
            else if (tongDoanhThuThangTruoc > tongDoanhThuThang)
            {
                var phantram = 100 - (tongDoanhThuThang * 100 / tongDoanhThuThangTruoc);
                ViewBag.DoanhThuTang = "Doanh thu tháng này giảm " + phantram + " % so với tháng trước";
            }
            else if (tongDoanhThuThangTruoc < tongDoanhThuThang)
            {
                var phantram = 100 - (tongDoanhThuThangTruoc * 100 / tongDoanhThuThang);
                ViewBag.DoanhThuTang = "Doanh thu tháng này tăng " + phantram + " % so với tháng trước";
            }
            var listCTDBanDuoc = db.ChiTietDonHangs.Where(m => m.DonHang.NgayDat.Year == date.Year & m.DonHang.NgayDat.Month == date.Month & m.DonHang.NgayDat.Day == date.Day & m.TinhTrangDonHangID == 4);

            ViewBag.SoXuMerchant =  merchant.FirstOrDefault().SoLuongKIPXu;
            
            ViewBag.TienHoaHong = tienHoaHong;

            return View(listCTDBanDuoc.ToList());
        }
    }
}