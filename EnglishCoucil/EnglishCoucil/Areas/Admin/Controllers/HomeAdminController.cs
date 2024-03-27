using EnglishCoucil.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnglishCoucil.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        dbQLtrungtamDataContext data = new dbQLtrungtamDataContext();
        // GET: Admin/Home
        public ActionResult HomeQL()
        {   if (Session["Admin"] != null)
            { 
                return View();
            }else {
                return  RedirectToAction("Login", "HomeAdmin"); 
            }
            
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var taikhoan = collection["TaiKhoan"];
            var matkhau = collection["Matkhau"];

            if (String.IsNullOrEmpty(taikhoan))
            {
                ViewData["Loi1"] = "Tài khoản không được để trống";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Mật khẩu không được để trống";
            }
            else
            {
                var admin = data.Admins.SingleOrDefault(n => n.TaiKhoan == taikhoan && n.MatKhau == matkhau);
                if (admin != null)
                {
                    Session["Admin"] = admin;
                    Session["Admin_Username"] = admin.TaiKhoan;
                    return RedirectToAction("HomeQL", "HomeAdmin");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng ";
            }
            return this.Login();
        }
        #region Log Off And Change Info when Login
        public ActionResult Logoff()
        {
            Session.Clear();
            return RedirectToAction("Login", "HomeAdmin");
        }

        #endregion
    }
}