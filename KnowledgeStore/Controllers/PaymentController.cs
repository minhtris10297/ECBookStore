using KnowledgeStore.Common;
using Model.Dao;
using Model.EntityFramework;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Controllers
{
    public class PaymentController : Controller
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        private const string CartSession = "CartSession";
        // GET: Payment
        public ActionResult Index()
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (sessionUser != null)
            {
                ViewBag.NameCus = sessionUser.UserName;
                var user = db.Customers.Where(m => m.Email == sessionUser.Email).FirstOrDefault();
                ViewBag.AddressCus = user.DiaChi;
                ViewBag.PhoneCus = user.SoDienThoai;
            }
            else
            {
                return RedirectToAction("LoginPayment", "Payment");
            }
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            decimal tamTinh = 0;
            foreach (var item in list)
            {
                tamTinh += item.Quantity * item.Sach.GiaTien;
            }
            ViewBag.TamTinh = tamTinh.ToString("N0");
            ViewBag.ThanhTien = tamTinh.ToString("N0");
            return View(list);
        }

        public ActionResult LoginPayment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            ViewBag.ListCart = list;
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginPayment(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.Email, Encryptor.SHA256Encrypt(model.Password));
                if (result == 1)
                {
                    var userSession = new UserLogin();
                    userSession.UserName = db.Customers.Where(m => m.Email == model.Email).Select(m => m.HoTen).FirstOrDefault();
                    userSession.Email = model.Email;
                    Session[CommonConstants.USER_SESSION] = null;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Payment");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản bị khóa.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
            }
            return View(model);
        }

        public ActionResult PaymentSuccess()
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USER_SESSION];
            var user = db.Customers.Where(m => m.Email == sessionUser.Email).FirstOrDefault();

            var cart = Session[CartSession];
            var listCart = new List<CartItem>();
            if (cart != null)
            {
                listCart = (List<CartItem>)cart;
            }

            decimal tamTinh = 0;
            foreach (var item in listCart)
            {
                tamTinh += item.Quantity * item.Sach.GiaTien;
            }
            var donHang = new DonHang() { CustomerID = user.CustomerID, NgayDat = System.DateTime.Now, TongTien = tamTinh, DiaChi = user.DiaChi, TinhTrangDonHangID = 1 };
            foreach(var item in listCart)
            {
                var chiTietDH = new ChiTietDonHang() { DonHangID = donHang.DonHangID, SachID = item.Sach.SachID, MerchantID = item.Sach.MerchantID.GetValueOrDefault(0), SoLuong = item.Quantity, ThanhTien = item.Quantity * item.Sach.GiaTien };
                db.ChiTietDonHangs.Add(chiTietDH);
            }
            db.DonHangs.Add(donHang);
            db.SaveChanges();
            return View();
        }
    }
}