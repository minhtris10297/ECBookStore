 using Common;
using KnowledgeStore.Common;
using Model.Dao;
using Model.EntityFramework;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KnowledgeStore.Areas.MerchantArea.Controllers
{
    public class AccountsMerchantController : Controller
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
                var result = dao.LoginMerchant(model.Email, Encryptor.SHA256Encrypt(model.Password));
                if (result == 1)
                {
                    var merchant=db.Merchants.Where(m => m.Email == model.Email).FirstOrDefault();
                    var userSession = new UserLogin();
                    userSession.UserName = merchant.HoTen;
                    userSession.Email = model.Email;
                    Session[CommonConstants.USERMERCHANT_SESSION] = null;
                    Session.Add(CommonConstants.USERMERCHANT_SESSION, userSession);
                    return RedirectToAction("OverallPage", "MCHome");
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
        public ActionResult Register([Bind(Include = "MerchantID,Email,MatKhauMaHoa,HoTen,DiaChi,GioiTinhID,TenCuaHang,SoDienThoai")] Merchant merchant, string AuthenticationCode, HttpPostedFileBase imageAvatar)
        {
            //check Ho ten
            if (merchant.HoTen.Trim().Any(char.IsNumber))
            {
                ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh", "1");
                ModelState.AddModelError("", "Họ tên không hợp lệ!");
                return View(merchant);
            }
            //check password
            if (merchant.MatKhauMaHoa.Trim().Length < 5)
            {
                ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh", "1");
                ModelState.AddModelError("", "Mật khẩu không hợp lệ! Mật khẩu hợp lệ phải chứa ít nhất 5 ký tự bao gồm chữ và số");
                return View(merchant);
            }
            if (merchant.MatKhauMaHoa.Trim().All(char.IsLetter))
            {
                ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh", "1");
                ModelState.AddModelError("", "Mật khẩu không hợp lệ! Mật khẩu hợp lệ phải chứa ít nhất 1 ký tự số và chữ");
                return View(merchant);
            }
            var authenticationEmail = (AuthenticationEmail)Session[CommonConstants.AUTHENTICATIONEMAILMERCHANT_SESSION];

            if (ModelState.IsValid & authenticationEmail != null)
            {
                if (merchant.Email == authenticationEmail.Email & authenticationEmail.AuthenticationCode == AuthenticationCode)
                {

                    merchant.MatKhauMaHoa = Encryptor.SHA256Encrypt(merchant.MatKhauMaHoa);
                    merchant.TrangThai = false;
                    merchant.NgayTao = System.DateTime.Now;
                    merchant.SoLuongKIPXu = 0;
                    db.Merchants.Add(merchant);
                    db.SaveChanges();

                    if (imageAvatar != null)
                    {
                        //Resize Image
                        WebImage img = new WebImage(imageAvatar.InputStream);
                        //img.Resize(500, 1000);

                        var filePathOriginal = Server.MapPath("/Assets/Image/Merchant/");
                        var fileName = merchant.MerchantID+ ".jpg";
                        string savedFileName = Path.Combine(filePathOriginal, fileName);
                        img.Save(savedFileName);
                    }
                    
                    Session[CommonConstants.USERMERCHANT_SESSION] = null;

                    return RedirectToAction("MerchantRegSuccess", "Home",new { area=""});
                }
                else
                {
                    ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh");
                    ModelState.AddModelError("", "Mã xác thực không hợp lệ");
                    return View(merchant);
                }
            }
            ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh", 1);
            return View(merchant);
        }
        public JsonResult GetAuthenticationInEmail(string Email)
        {
            var findThisEmail = db.Merchants.Where(m => m.Email == Email).Select(m => m.Email).FirstOrDefault();

            if (findThisEmail == null)
            {
                Session[CommonConstants.AUTHENTICATIONEMAILMERCHANT_SESSION] = null;
                int authCode = new Random().Next(1000, 9999);
                AuthenticationEmail authenticationEmail = new AuthenticationEmail();
                authenticationEmail.Email = Email;
                authenticationEmail.AuthenticationCode = authCode.ToString();
                Session[CommonConstants.AUTHENTICATIONEMAILMERCHANT_SESSION] = authenticationEmail;

                MailHelper.SendMailAuthentication(Email, "Mã xác thực từ Knowledge Store", authCode.ToString());

                return Json(new { status = true });
            }
            else
                return Json(new { status = false });
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.AUTHENTICATIONEMAILMERCHANT_SESSION] = null;
            return RedirectToAction("Login", "AccountsMerchant");
        }
    }
}