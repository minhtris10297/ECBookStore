using ClosedXML.Excel;
using Common;
using ExcelDataReader;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Areas.AdminArea.Controllers
{
    public class DonHangController : BaseController
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();

        // GET: AdminArea/DonHang
        public ActionResult Index()
        {
            ViewBag.HoaHong = db.HoaHongs.Where(p => p.TrangThai == true).First().PhanTranHoaHong;
            ViewBag.DropdownStatus = new SelectList(db.TinhTrangDonHangs, "TinhTrangDonHangID", "TinhTrangDonHang1");
            var listCTDH = db.ChiTietDonHangs.OrderByDescending(m => m.DonHang.NgayDat).ToList();
            return View(listCTDH);
        }
        [HttpPost]
        public ActionResult DangGiaoHang(int id)
        {
            db.ChiTietDonHangs.Find(id).TinhTrangDonHangID = 3;//thay doi thanh dang giao hang
            db.SaveChanges();
            ViewBag.HoaHong = db.HoaHongs.Where(p => p.TrangThai == true).First().PhanTranHoaHong;
            ViewBag.DropdownStatus = new SelectList(db.TinhTrangDonHangs, "TinhTrangDonHangID", "TinhTrangDonHang1");
            var listCTDH = db.ChiTietDonHangs.OrderByDescending(m => m.DonHang.NgayDat).ToList();
            return View("Index", listCTDH);
        }

        [HttpPost]
        public ActionResult Index(System.DateTime? searchTime, int? searchId, string nameCus, int? DropdownStatus)
        {
            ViewBag.DropdownStatus = new SelectList(db.TinhTrangDonHangs, "TinhTrangDonHangID", "TinhTrangDonHang1");


            var listCTDH = db.ChiTietDonHangs.OrderByDescending(m => m.DonHang.NgayDat).ToList();
            if (ModelState.IsValid)
            {

                if (searchTime != null)
                {
                    ViewBag.SearchTime = searchTime.GetValueOrDefault(System.DateTime.Now).ToString("yyyy-MM-dd");
                    //listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id&m.DonHang.NgayDat==searchTime).OrderByDescending(m => m.DonHang.NgayDat);
                    listCTDH = listCTDH.Where(m => m.DonHang.NgayDat.Day == searchTime.GetValueOrDefault().Day & m.DonHang.NgayDat.Month == searchTime.GetValueOrDefault().Month & m.DonHang.NgayDat.Year == searchTime.GetValueOrDefault().Year).ToList();
                }
                if (searchId != null)
                {
                    //listCTDH= db.ChiTietDonHangs.Where(m => m.MerchantID == id&m.TinhTrangDonHangID==searchId).OrderByDescending(m => m.DonHang.NgayDat);
                    listCTDH = listCTDH.Where(m => m.ChiTietDonHangID == searchId).ToList();
                }
                if (nameCus != null)
                {
                    //listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id & m.DonHang.Customer.HoTen.Contains(nameCus)).OrderByDescending(m => m.DonHang.NgayDat);
                    listCTDH = listCTDH.Where(m => m.DonHang.Customer.HoTen.Contains(nameCus)).ToList();
                }
                if (DropdownStatus != null)
                {
                    //listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id & m.TinhTrangDonHangID == DropdownStatus).OrderByDescending(m => m.DonHang.NgayDat);
                    listCTDH = listCTDH.Where(m => m.TinhTrangDonHangID == DropdownStatus).ToList();
                }
            }
            return View(listCTDH);

        }

        public ActionResult ExportExcel()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("ChiTietDonHangID"),
                                            new DataColumn("TenNguoiNhan"),
                                            new DataColumn("DiaChiNguoiNhan"),
                                            new DataColumn("DiaChiNguoiBan"),
                                            new DataColumn("Tensach"),
                                            new DataColumn("SoLuong"),
                                            new DataColumn("ThanhTien"),
                                            new DataColumn("TrangThai")});

            foreach (var donhang in db.ChiTietDonHangs.Where(p => p.TinhTrangDonHangID == 2).ToList())
            {
                dt.Rows.Add(donhang.ChiTietDonHangID, donhang.DonHang.Customer.HoTen, donhang.DonHang.DiaChi,
                    donhang.Sach.Merchant.DiaChi, donhang.Sach.TenSach, donhang.SoLuong, donhang.ThanhTien, donhang.TinhTrangDonHangID
                    );
                donhang.TinhTrangDonHangID = 3;
                MailHelper.SendMailOrderReceived(donhang.DonHang.Customer.Email, "KnowledgeStore thông báo tình trạng đơn hàng", donhang.ChiTietDonHangID.ToString(), donhang.DonHang.Customer.HoTen, "đang được giao", System.DateTime.Now.ToString("dd/MM/yyyy"));
            }
            db.SaveChanges();

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Merchants.xlsx");
                }
            }
        }

        [HttpPost]
        public ActionResult ImportExcel(HttpPostedFileBase upload)
        {
            using (var stream = upload.InputStream)
            {
                IExcelDataReader reader;

                reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream);

                var conf = new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };


                var dataSet = reader.AsDataSet(conf);
                var dataTable = dataSet.Tables[0];
                int count = 0;
                while (reader.Read())
                {
                    if (count > 0)
                    {
                        int chiTietDonHangID = Int32.Parse(reader.GetString(0)),
                            statusOrder = (int)reader.GetDouble(7);
                        var ctdonhang = db.ChiTietDonHangs.Find(chiTietDonHangID);

                        ctdonhang.TinhTrangDonHangID = statusOrder;
                        if (statusOrder == 4)
                        {
                            MailHelper.SendMailOrderReceived(ctdonhang.DonHang.Customer.Email, "KnowledgeStore thông báo tình trạng đơn hàng", ctdonhang.ChiTietDonHangID.ToString(), ctdonhang.DonHang.Customer.HoTen, "giao thành công!!!", System.DateTime.Now.ToString("dd/MM/yyyy"));
                            ctdonhang.NgayXuat = System.DateTime.Now;
                        }
                        else if (statusOrder == 5){
                            MailHelper.SendMailOrderReceived(ctdonhang.DonHang.Customer.Email, "KnowledgeStore thông báo tình trạng đơn hàng", ctdonhang.ChiTietDonHangID.ToString(), ctdonhang.DonHang.Customer.HoTen, "đã giao thất bại, xu hoa hồng đc hoàn trả cho quí khách", System.DateTime.Now.ToString("dd/MM/yyyy"));
                            var xuHoaHong = db.LichSuHoaHongs.Where(m => m.ChiTietDonHangID == ctdonhang.ChiTietDonHangID).Select(m=>m.GiaTriXu).FirstOrDefault();
                            db.LichSuHoaHongs.Add(new LichSuHoaHong() { ChiTietDonHangID = ctdonhang.ChiTietDonHangID, GiaTriXu = -xuHoaHong, ThoiDiem = System.DateTime.Now });
                            Merchant mer = db.Merchants.Find(ctdonhang.MerchantID);
                            mer.SoLuongKIPXu = mer.SoLuongKIPXu - (xuHoaHong);
                            ctdonhang.NgayXuat = System.DateTime.Now;
                        }
                        db.SaveChanges();
                    }
                    count++;
                }
                var result = db.ChiTietDonHangs.ToList();



                ViewBag.HoaHong = db.HoaHongs.Where(p => p.TrangThai == true).First().PhanTranHoaHong;
                ViewBag.DropdownStatus = new SelectList(db.TinhTrangDonHangs, "TinhTrangDonHangID", "TinhTrangDonHang1");
                var listCTDH = db.ChiTietDonHangs.OrderByDescending(m => m.DonHang.NgayDat).ToList();
                return View("Index", listCTDH);
            }
        }

    }
}
