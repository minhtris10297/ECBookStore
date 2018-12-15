using Common;
using KnowledgeStore.Common;
using Model.EntityFramework;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Areas.MerchantArea.Controllers
{
    public class OrderManagerController : BaseController
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        // GET: Merchant/OrderManager
        public ActionResult Index()
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "AccountsMerchant");
            }
            var id = db.Merchants.Where(m => m.Email == sessionUser.Email).Select(m => m.MerchantID).FirstOrDefault();
            ViewBag.DropdownStatus = new SelectList(db.TinhTrangDonHangs, "TinhTrangDonHangID", "TinhTrangDonHang1");

            var listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id).OrderByDescending(m=>m.DonHang.NgayDat).ToList();

            return View(listCTDH);
        }

        [HttpPost]
        public ActionResult Index( System.DateTime? searchTime,int? searchId, string nameCus,int? DropdownStatus)
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "AccountsMerchant");
            }
            var id = db.Merchants.Where(m => m.Email == sessionUser.Email).Select(m => m.MerchantID).FirstOrDefault();

            ViewBag.DropdownStatus = new SelectList(db.TinhTrangDonHangs, "TinhTrangDonHangID", "TinhTrangDonHang1");
            

            var listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id).OrderByDescending(m => m.DonHang.NgayDat).ToList();
            if (ModelState.IsValid)
            {
                
                if (searchTime != null)
                {
                    ViewBag.SearchTime = searchTime.GetValueOrDefault(System.DateTime.Now).ToString("yyyy-MM-dd");
                    //listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id&m.DonHang.NgayDat==searchTime).OrderByDescending(m => m.DonHang.NgayDat);
                    listCTDH = listCTDH.Where(m=>m.DonHang.NgayDat.Day==searchTime.GetValueOrDefault().Day& m.DonHang.NgayDat.Month== searchTime.GetValueOrDefault().Month& m.DonHang.NgayDat.Year== searchTime.GetValueOrDefault().Year).ToList();
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

        public JsonResult CheckXu(int id)
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            var merchant = db.Merchants.Where(m => m.Email == sessionUser.Email).FirstOrDefault();

            var ctdh = db.ChiTietDonHangs.Find(id);
            var phantramHoaHong = (float)(db.HoaHongs.OrderByDescending(m => m.HoaHongID).Select(m => m.PhanTranHoaHong).FirstOrDefault()) / 100;
            float phiHoaHong = 0;
            if (ctdh.Sach.GiaKhuyenMai == null)
            {
                phiHoaHong = (float)ctdh.Sach.GiaTien * phantramHoaHong * ctdh.SoLuong;
            }
            else
            {
                phiHoaHong = (float)ctdh.Sach.GiaKhuyenMai * phantramHoaHong * ctdh.SoLuong;
            }
            if (phiHoaHong % 1000 != 0)
            {
                if (phiHoaHong % 1000 < 500)
                {
                    phiHoaHong = phiHoaHong - phiHoaHong % 1000;
                }
                else
                {
                    phiHoaHong = phiHoaHong - phiHoaHong % 1000 + 1000;
                }
            }
            int xuCanTru = (int)phiHoaHong / 1000;
            var sta = true;
            if (merchant.SoLuongKIPXu < xuCanTru)
            {
                sta = false;
            }
            return Json(new { xu=xuCanTru,status=sta,idCTDH=id});
        }

        public ActionResult ChangeDeliveryStatus(int idCtdh)
        {
            var sessionUser = (UserLogin)Session[CommonConstants.USERMERCHANT_SESSION];
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "AccountsMerchant");
            }
            var merchant = db.Merchants.Where(m => m.Email == sessionUser.Email);
            var id = merchant.Select(m => m.MerchantID).FirstOrDefault();
            ViewBag.IdMerchant = id;

            var ctdh = db.ChiTietDonHangs.Find(idCtdh);
            ctdh.TinhTrangDonHangID = 2;
            db.SaveChanges();
            MailHelper.SendMailOrderReceived(ctdh.DonHang.Customer.Email, "KnowledgeStore thông báo tình trạng đơn hàng", ctdh.ChiTietDonHangID.ToString(),ctdh.DonHang.Customer.HoTen);

            var phantramHoaHong = (float)(db.HoaHongs.OrderByDescending(m => m.HoaHongID).Select(m => m.PhanTranHoaHong).FirstOrDefault()) / 100;
            float phiHoaHong = 0;
            if (ctdh.Sach.GiaKhuyenMai == null)
            {
                phiHoaHong = (float)ctdh.Sach.GiaTien * phantramHoaHong * ctdh.SoLuong;
            }
            else
            {
                phiHoaHong = (float)ctdh.Sach.GiaKhuyenMai * phantramHoaHong * ctdh.SoLuong;
            }
            if(phiHoaHong%1000 != 0)
            {
                if(phiHoaHong % 1000 < 500)
                {
                    phiHoaHong = phiHoaHong - phiHoaHong % 1000;
                }
                else
                {
                    phiHoaHong = phiHoaHong - phiHoaHong % 1000 + 1000;
                }
            }
            int xuCanTru = -(int)phiHoaHong / 1000;
            LichSuHoaHong lshh = new LichSuHoaHong() { ChiTietDonHangID = idCtdh, GiaTriXu = xuCanTru, ThoiDiem = System.DateTime.Now };
            db.LichSuHoaHongs.Add(lshh);
            merchant.FirstOrDefault().SoLuongKIPXu = merchant.FirstOrDefault().SoLuongKIPXu + xuCanTru;
            db.SaveChanges();
            var listCTDH = db.ChiTietDonHangs.Where(m => m.MerchantID == id).OrderByDescending(m => m.DonHang.NgayDat).ToList();
            return RedirectToAction("Index","OrderManager",new { area="MerchantArea"});
        }
    } 
}