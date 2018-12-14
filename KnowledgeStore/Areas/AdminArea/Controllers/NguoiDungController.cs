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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KnowledgeStore.Areas.AdminArea.Controllers
{
    public class NguoiDungController : Controller
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: AdminArea/NguoiDung
        public ActionResult Index()
        {
            var listCUS = db.Merchants.ToList();
            return View(listCUS);
        }
        [HttpPost]
        public ActionResult Index(string SoDienThoai, string TenNguoiDung, string EmailNguoiDung)
        {
            var listNhaBuon = db.Merchants.ToList();

            var result = db.Merchants.Where(p => p.HoTen.Contains(TenNguoiDung ?? "") || p.SoDienThoai.Equals(SoDienThoai ?? "") || p.Email.Equals(EmailNguoiDung ?? ""))
                .ToList();
            return View(result);
        }

        public ActionResult ExportExcel()
        {
            //var gv = new GridView();
            //gv.DataSource = db.Merchants.ToList();
            //gv.DataBind();

            //Response.ClearContent();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment; filename=merchants.xls");
            //Response.ContentType = "application/ms-excel";
            //Response.Charset = "";
            //StringWriter objStringWriter = new StringWriter();
            //HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            //gv.RenderControl(objHtmlTextWriter);
            //Response.Output.Write(objStringWriter.ToString());
            //Response.Flush();
            //Response.End();
            //return View("Index");

            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[11] { new DataColumn("MerchantID"),
                                            new DataColumn("Email"),
                                            new DataColumn("MatKhauMaHoa"),
                                            new DataColumn("HoTen"),
                                            new DataColumn("DiaChi"),
                                            new DataColumn("GioiTinhID"),
                                            new DataColumn("TenCuaHang"),
                                            new DataColumn("SoDienThoai"),
                                            new DataColumn("SoLuongKIPXu"),
                                            new DataColumn("TrangThai"),
                                            new DataColumn("NgayTao"),});

            foreach (var merchant in db.Merchants.ToList())
            {
                dt.Rows.Add(merchant.MerchantID, merchant.Email, merchant.MatKhauMaHoa, merchant.HoTen, merchant.DiaChi,
                    merchant.GioiTinhID, merchant.TenCuaHang, merchant.SoDienThoai,
                    merchant.SoLuongKIPXu, merchant.TrangThai, merchant.NgayTao
                    );
            }

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
                        Merchant merchant = new Merchant()
                        {

                            //MerchantID = (int)double.Parse(reader.GetString(0)),
                            Email = reader.GetString(1),
                            MatKhauMaHoa = reader.GetString(2),
                            HoTen = reader.GetString(3),
                            DiaChi = reader.GetString(4),
                            GioiTinhID = int.Parse(reader.GetString(5)),
                            TenCuaHang = reader.GetString(6),
                            SoDienThoai = reader.GetString(7),
                            SoLuongKIPXu = int.Parse(reader.GetString(8)),
                            TrangThai = bool.Parse(reader.GetString(9)),
                            NgayTao = DateTime.Parse(reader.GetString(10))
                        };
                        db.Merchants.Add(merchant);
                        db.SaveChanges();
                    }
                    count++;
                }
                var result = db.Merchants.ToList();




                return View("Index", result);
            }
        }

        [HttpPost]
        public ActionResult KhoaTaiKhoan(int id)
        {
            var listCUS = db.Merchants.ToList();
            db.Merchants.Where(p => p.MerchantID == id).First().TrangThai = false;
            db.SaveChanges();
            return View("Index", listCUS);
        }
        public ActionResult KichHoat(int id)
        {
            var listCUS = db.Merchants.ToList();
            db.Merchants.Where(p => p.MerchantID == id).First().TrangThai = true;
            db.SaveChanges();
            return View("Index", listCUS);
        }
    }
}