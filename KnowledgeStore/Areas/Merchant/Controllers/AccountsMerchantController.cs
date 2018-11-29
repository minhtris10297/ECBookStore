using KnowledgeStore.Common;
using Model.Dao;
using Model.EntityFramework;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Areas.Merchant.Controllers
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
                    return RedirectToAction("Index", "OrderManager",new { id=merchant.MerchantID});
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
    }
}