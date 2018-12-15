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
    public class NguoiMuaController : BaseController
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: AdminArea/NguoiMua
        public ActionResult Index()
        {
            var listCUS = db.Customers.ToList();
            return View(listCUS);
        }

        [HttpPost]
        public ActionResult Index(string SoDienThoai, string TenNguoiMua, string EmailNguoiMua)
        {
            var listNhaBuon = db.Merchants.ToList();

            var result = db.Merchants.Where(p => p.HoTen.Contains(TenNguoiMua ?? "") || p.SoDienThoai.Equals(SoDienThoai ?? "") || p.Email.Equals(EmailNguoiMua ?? ""))
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
            dt.Columns.AddRange(new DataColumn[11] { new DataColumn("CustomerID"),
                                            new DataColumn("Email"),
                                            new DataColumn("IDGoogle"),
                                            new DataColumn("IDFacebook"),
                                            new DataColumn("HoTen"),
                                            new DataColumn("DiaChi"),
                                            new DataColumn("SoDienThoai"),
                                            new DataColumn("MatKhauMaHoa"),
                                            new DataColumn("GioiTinhID"),
                                            new DataColumn("NgayTao"),
                                            new DataColumn("TrangThai"),
                                            });

            foreach (var customer in db.Customers.ToList())
            {
                dt.Rows.Add(customer.CustomerID, customer.Email, customer.IDGoogle, customer.IDFacebook, customer.HoTen,
                    customer.DiaChi, customer.SoDienThoai, customer.MatKhauMaHoa, customer.GioiTinhID,
                    customer.NgayTao, customer.TrangThai
                    );
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Customers.xlsx");
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
                        Customer customer = new Customer()
                        {

                            //MerchantID = (int)double.Parse(reader.GetString(0)),
                            Email = reader.GetString(1),
                            IDGoogle = reader.GetString(2),
                            IDFacebook = reader.GetString(3),
                            HoTen = reader.GetString(4),
                            DiaChi = reader.GetString(5),
                            SoDienThoai = reader.GetString(6),
                            MatKhauMaHoa = reader.GetString(7),
                            GioiTinhID = int.Parse(reader.GetString(8)),
                            NgayTao = DateTime.Parse(reader.GetString(9)),
                            TrangThai = bool.Parse(reader.GetString(10))
                        };
                        db.Customers.Add(customer);
                        db.SaveChanges();
                    }
                    count++;
                }
                var result = db.Customers.ToList();




                return View("Index", result);
            }
        }

        [HttpPost]
        public ActionResult KhoaTaiKhoan(int id)
        {
            var listCUS = db.Customers.ToList();
            db.Customers.Where(p => p.CustomerID == id).First().TrangThai = false;
            db.SaveChanges();
            return View("Index", listCUS);
        }
        [HttpPost]
        public ActionResult KichHoat(int id)
        {
            var listCUS = db.Customers.ToList();
            db.Customers.Where(p => p.CustomerID == id).First().TrangThai = true;
            db.SaveChanges();
            return View("Index", listCUS);
        }
    }
}