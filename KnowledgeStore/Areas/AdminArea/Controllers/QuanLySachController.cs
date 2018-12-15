using ClosedXML.Excel;
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
    public class QuanLySachController : BaseController
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: AdminArea/QuanLySach
        public ActionResult Index()
        {
            ViewBag.DropdownStatus = new SelectList(db.TheLoais, "TenTheLoai", "TenTheLoai");
            ViewBag.DropdownStatusNXB = new SelectList(db.NhaXuatBans, "TenNXB", "TenNXB");

            var listSach = db.Saches.OrderByDescending(m=>m.LichSuNangTins.Min(n=>n.NgayNang)).ToList();
            return View(listSach);
        }

        [HttpPost]
        public ActionResult Index(string TenSach, int? DropdownStatusNXB, int? DropdownStatus)
        {
            ViewBag.DropdownStatus = new SelectList(db.TheLoais, "TheLoaiID", "TenTheLoai");
            ViewBag.DropdownStatusNXB = new SelectList(db.NhaXuatBans, "NhaXuatBanID", "TenNXB");
            //var results = db.Saches.ToList();
            //if(TenSach != null)
            //{
            //    results = results.Where(p => p.TenSach.Contains(TenSach)).ToList();
            //}
            //else if (DropdownStatus != null)
            //{
            //    results = results.Where(p => p.TheLoai.TheLoaiID == DropdownStatus).ToList();
            //}
            //else if(DropdownStatusNXB != null)
            //{
            //    results = results.Where(p => p.NhaXuatBan.NhaXuatBanID == DropdownStatusNXB).ToList();
            //}
            var results = db.Saches.OrderByDescending(m => m.LichSuNangTins.Min(n => n.NgayNang)).Where(p => p.NhaXuatBan.NhaXuatBanID == DropdownStatusNXB || p.TenSach.Contains(TenSach ?? "") || p.TheLoai.TheLoaiID == DropdownStatus).ToList();
            return View(results);
        }
        [HttpPost]
        public ActionResult DuyetSach(int id)
        {
            ViewBag.DropdownStatus = new SelectList(db.TheLoais, "TenTheLoai", "TenTheLoai");
            ViewBag.DropdownStatusNXB = new SelectList(db.NhaXuatBans, "TenNXB", "TenNXB");

            var listSach = db.Saches.ToList();
            db.Saches.Where(p => p.SachID == id).First().TrangThai = true;
            db.SaveChanges();
            return View("Index", listSach);
        }

        public ActionResult ExportExcel()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[14] { new DataColumn("SachID"),
                                            new DataColumn("TenSach"),
                                            new DataColumn("TacGia"),
                                            new DataColumn("NhaXuatBanID"),
                                            new DataColumn("NgayXuatBan"),
                                            new DataColumn("SoTrang"),
                                            new DataColumn("LoaiBiaID"),
                                            new DataColumn("MerchantID"),
                                            new DataColumn("TrangThai"),
                                            new DataColumn("GiaTien"),
                                            new DataColumn("GiaKhuyenMai"),
                                            new DataColumn("MoTa"),
                                            new DataColumn("SoLuong"),
                                            new DataColumn("TheLoaiID")});

            foreach (var sachs in db.Saches.ToList())
            {
                dt.Rows.Add(sachs.SachID, sachs.TenSach, sachs.TacGia, sachs.NhaXuatBanID, sachs.NgayXuatBan, sachs.SoTrang, sachs.LoaiBiaID, sachs.MerchantID,
                    sachs.TrangThai, sachs.GiaTien, sachs.GiaKhuyenMai, sachs.MoTa, sachs.SoLuong, sachs.TheLoaiID
                    );
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sachs.xlsx");
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
                        Sach sach = new Sach();


                        //MerchantID = (int)double.Parse(reader.GetString(0)),
                        //SachID = int.Parse(reader.GetString(0)),
                        sach.TenSach = reader.GetString(1);
                        sach.TacGia = reader.GetString(2);
                        sach.NhaXuatBanID = int.Parse(reader.GetString(3));
                        sach.NgayXuatBan = DateTime.Parse(reader.GetString(4));
                        sach.SoTrang = int.Parse(reader.GetString(5));
                        sach.LoaiBiaID = int.Parse(reader.GetString(6));
                        sach.MerchantID = int.Parse(reader.GetString(7));
                        sach.TrangThai = bool.Parse(reader.GetString(8));
                        sach.GiaTien = (decimal)decimal.Parse(reader.GetString(9));
                        if (reader.GetString(10) == null)
                        {
                            sach.GiaKhuyenMai = 0;
                        }
                        else
                        {
                            sach.GiaKhuyenMai = decimal.Parse(reader.GetString(10));
                        }
                        sach.MoTa = reader.GetString(11);
                        if (reader.GetString(12) == null)
                        {
                            sach.SoLuong = 0;
                        }
                        else
                        {
                            sach.SoLuong = int.Parse(reader.GetString(12));
                        }
                        sach.TheLoaiID = int.Parse(reader.GetString(13));

                        db.Saches.Add(sach);
                        db.SaveChanges();
                    }
                    count++;
                }
                var result = db.Saches.ToList();



                ViewBag.DropdownStatus = new SelectList(db.TheLoais, "TenTheLoai", "TenTheLoai");
                ViewBag.DropdownStatusNXB = new SelectList(db.NhaXuatBans, "TenNXB", "TenNXB");

                var listSach = db.Saches.ToList();
                return View("Index", listSach);
            }
        }
    }
}