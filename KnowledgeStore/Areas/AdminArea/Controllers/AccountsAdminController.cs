using KnowledgeStore.Common;
using Model.Dao;
using Model.EntityFramework;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Areas.AdminArea.Controllers
{
    public class AccountsAdminController : Controller
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
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
                var result = dao.LoginAdmin(model.Email, Encryptor.SHA256Encrypt(model.Password));
                if (result == 1)
                { 
                    var admin = db.Admins.Where(m => m.TenDangNhap == model.Email).FirstOrDefault();
                    var userSession = new UserLogin();
                    userSession.UserName = admin.TenHienThi;
                    userSession.Email = admin.TenDangNhap;
                    Session[CommonConstants.USERMADMIN_SESSION] = null;
                    Session.Add(CommonConstants.USERMADMIN_SESSION, userSession);
                    return RedirectToAction("Index", "ADHome");
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
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "AdminID,TenDangNhap,MatKhauMaHoa,TenHienThi")] Admin admin)
        {
            //check Ho ten
            if (admin.TenHienThi.Trim().Any(char.IsNumber))
            {
                ModelState.AddModelError("", "Họ tên không hợp lệ!");
                return View(admin);
            }
            //check password
            if (admin.MatKhauMaHoa.Trim().Length < 5)
            {
                ModelState.AddModelError("", "Mật khẩu không hợp lệ! Mật khẩu hợp lệ phải chứa ít nhất 5 ký tự bao gồm chữ và số");
                return View(admin);
            }
            if (admin.MatKhauMaHoa.Trim().All(char.IsLetter))
            {
                ModelState.AddModelError("", "Mật khẩu không hợp lệ! Mật khẩu hợp lệ phải chứa ít nhất 1 ký tự số và chữ");
                return View(admin);
            }

            if (ModelState.IsValid)
            {

                    admin.MatKhauMaHoa = Encryptor.SHA256Encrypt(admin.MatKhauMaHoa);
                    admin.TrangThai = true;
                    db.Admins.Add(admin);
                    db.SaveChanges();

                    var userSession = new UserLogin();
                    userSession.UserName = admin.TenHienThi;
                    userSession.Email = admin.TenDangNhap;
                    Session[CommonConstants.USERMADMIN_SESSION] = null;
                    Session[CommonConstants.USERMADMIN_SESSION] = userSession;

                    return RedirectToAction("Index", "ADHome");
            }
            ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh", 1);
            return View(admin);
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USERMADMIN_SESSION] = null;
            return RedirectToAction("Login", "AccountsAdmin");
        }
    }
}