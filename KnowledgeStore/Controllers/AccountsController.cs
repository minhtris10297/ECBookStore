using Common;
using Facebook;
using KnowledgeStore.Common;
using Model.Dao;
using Model.EntityFramework;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace KnowledgeStore.Controllers
{
    public class AccountsController : Controller
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
            //check Ho ten
            if (customer.HoTen.Trim().Any(char.IsNumber))
            {
                ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh", "1");
                ModelState.AddModelError("", "Họ tên không hợp lệ!");
                return View(customer);
            }
            //check password
            if (customer.MatKhauMaHoa.Trim().Length < 5)
            {
                ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh", "1");
                ModelState.AddModelError("", "Mật khẩu không hợp lệ! Mật khẩu hợp lệ phải chứa ít nhất 5 ký tự bao gồm chữ và số");
                return View(customer);
            }
            if (customer.MatKhauMaHoa.Trim().All(char.IsLetter))
            {
                ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh", "1");
                ModelState.AddModelError("", "Mật khẩu không hợp lệ! Mật khẩu hợp lệ phải chứa ít nhất 1 ký tự số và chữ");
                return View(customer);
            }
            var authenticationEmail = (AuthenticationEmail)Session[CommonConstants.AUTHENTICATIONEMAIL_SESSION];

            if (ModelState.IsValid & authenticationEmail!=null)
            {
                if (customer.Email == authenticationEmail.Email & authenticationEmail.AuthenticationCode == AuthenticationCode)
                {

                    customer.MatKhauMaHoa = Encryptor.SHA256Encrypt(customer.MatKhauMaHoa);
                    customer.TrangThai = true;
                    customer.NgayTao = System.DateTime.Now;
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
            ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh",1);
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


        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        public ActionResult LoginFB()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secrect = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?field=email,id,name");
                string emailFB = me.email;
                string nameFB = me.name;
                string idFB = me.id;

                //Create member account add database
                var customerAccount = new Customer();
                customerAccount.Email = emailFB;
                customerAccount.HoTen = nameFB;
                customerAccount.IDFacebook = idFB;

                var resultInsertFb = new UserDao().InsertUserFb(customerAccount);

                //Add Session to display view
                UserLogin userLogin = new UserLogin();
                userLogin.Email = customerAccount.Email;
                userLogin.UserName = customerAccount.HoTen;

                Session.Remove(CommonConstants.USER_SESSION);
                Session.Add(CommonConstants.USER_SESSION, userLogin);

            }
            return Redirect("/");
        }

        public JsonResult LoginGoogle(string googleACModel)
        {
            var accountSocialsList = new JavaScriptSerializer().Deserialize<List<AccountSocial>>(googleACModel);
            var accountSocials = accountSocialsList.FirstOrDefault();
            var customerAccount = new Customer();
            customerAccount.Email = accountSocials.Email;
            customerAccount.HoTen = accountSocials.FullName;
            customerAccount.IDGoogle = accountSocials.AccountId;
            var resultInsertGg = new UserDao().InsertUserGg(customerAccount);

            //Add Session to display view
            UserLogin userLogin = new UserLogin();
            userLogin.Email = customerAccount.Email;
            userLogin.UserName = customerAccount.HoTen;

            Session.Remove(CommonConstants.USER_SESSION);
            Session.Add(CommonConstants.USER_SESSION, userLogin);

            var trangThai = db.Customers.Where(m => m.CustomerID == resultInsertGg).Select(m => m.TrangThai).FirstOrDefault();

            return Json(new { status = true,id= resultInsertGg, valueTrangThai= trangThai });
        }

        public ActionResult AdditionalInfoGg(int id)
        {
            ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh");
            var customer = db.Customers.Find(id);
            ViewBag.CusId = id;
            return View(customer);
        }

        [HttpPost]
        public ActionResult AdditionalInfoGg(int id,Customer customer, string AuthenticationCode)
        {
            ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh");
            var customerFind = db.Customers.Find(id);
            var authenticationEmail = (AuthenticationEmail)Session[CommonConstants.AUTHENTICATIONEMAIL_SESSION];
            
            if ((ModelState.IsValid & authenticationEmail != null)|| (ModelState.IsValid &customerFind.IDGoogle!=null))
            {
                    customerFind.HoTen=customer.HoTen;
                    customerFind.GioiTinhID=customer.GioiTinhID;
                    customerFind.SoDienThoai=customer.SoDienThoai;
                    customerFind.DiaChi = customer.DiaChi;
                    customerFind.TrangThai=true;
                    

                    var userSession = new UserLogin();
                    userSession.UserName = customer.HoTen;
                    userSession.Email = customer.Email;
                    Session[CommonConstants.USER_SESSION] = null;
                    Session[CommonConstants.USER_SESSION] = userSession;

                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(customer);
        }

        public ActionResult Edit()
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USER_SESSION];

            if (sessionUser == null)
            {
                return RedirectToAction("Login", "Accounts");
            }
            var id = db.Customers.Where(m => m.Email == sessionUser.Email).Select(m => m.CustomerID).FirstOrDefault();
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh", customer.GioiTinhID);
            return View(customer);
        }

        // POST: AdminArea/Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,Email,IDGoogle,IDFacebook,HoTen,DiaChi,SoDienThoai,MatKhauMaHoa,GioiTinhID,NgayTao,TrangThai")] Customer customer)
        {
            if (ModelState.IsValid)
            {

                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                var userSession = new UserLogin();
                userSession.UserName = customer.HoTen;
                userSession.Email = customer.Email;
                Session[CommonConstants.USER_SESSION] = null;
                Session[CommonConstants.USER_SESSION] = userSession;
                return RedirectToAction("Index");
            }
            ViewBag.GioiTinhID = new SelectList(db.GioiTinhs, "GioiTinhID", "TenGioiTinh", customer.GioiTinhID);
            return View(customer);
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