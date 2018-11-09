using Common;
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
    public class AccountsController : Controller
    {
        KnowledgeStoreContext db = new KnowledgeStoreContext();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.Email, Encryptor.SHA256Encrypt(model.Password));
                if (result == 1)
                {
                    var userSession = new UserLogin();
                    userSession.UserName = db.Customers.Where(m => m.Email == model.Email).Select(m=>m.HoTen).FirstOrDefault();
                    userSession.Email = model.Email;
                    Session[CommonConstants.USER_SESSION] = null;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
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
        public ActionResult Register()
        {
            ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "CustomerID,Email,HoTen,DiaChi,MatKhauMaHoa,GioiTinhID,SoDienThoai")] Customer customer,string AuthenticationCode)
        {
            var authenticationEmail = (AuthenticationEmail)Session[CommonConstants.AUTHENTICATIONEMAIL_SESSION];

            if (ModelState.IsValid & authenticationEmail!=null)
            {
                if (customer.Email == authenticationEmail.Email & authenticationEmail.AuthenticationCode == AuthenticationCode)
                {

                    customer.MatKhauMaHoa = Encryptor.SHA256Encrypt(customer.MatKhauMaHoa);
                    customer.TrangThai = true;
                    db.Customers.Add(customer);
                    db.SaveChanges();

                    var userSession = new UserLogin();
                    userSession.UserName = customer.HoTen;
                    userSession.Email = customer.Email;
                    Session[CommonConstants.USER_SESSION] = null;
                    Session[CommonConstants.USER_SESSION] = userSession;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh");
                    ModelState.AddModelError("", "Mã xác thực không hợp lệ");
                    return View(customer);
                }
            }
            ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh");
            return View(customer);
        }
        public JsonResult GetAuthenticationInEmail(string Email)
        {
            var findThisEmail = db.Customers.Where(m => m.Email == Email).Select(m => m.Email).FirstOrDefault();

            if (findThisEmail == null)
            {
                Session[CommonConstants.AUTHENTICATIONEMAIL_SESSION] = null;
                int authCode = new Random().Next(1000, 9999);
                AuthenticationEmail authenticationEmail = new AuthenticationEmail();
                authenticationEmail.Email = Email;
                authenticationEmail.AuthenticationCode = authCode.ToString();
                Session[CommonConstants.AUTHENTICATIONEMAIL_SESSION] = authenticationEmail;

                MailHelper.SendMailAuthentication(Email, "Mã xác thực từ Knowledge Store", authCode.ToString());

                return Json(new { status = true });
            }
            else
                return Json(new { status = false });
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return RedirectToAction("Index", "Home");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);

        }
    }
}