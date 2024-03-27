using EnglishCoucil.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace EnglishCoucil.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account
        dbQLtrungtamDataContext data = new dbQLtrungtamDataContext();

        public ActionResult UserAccount()
        {
            var query = from tk in data.TaiKhoans
                       // join để kết hợp các bản ghi từ bảng TaiKhoan với bảng HocVien dựa trên điều kiện equals và gom vào (into) hvGroup
                        join hv in data.HocViens on tk.IDTaiKhoan equals hv.IDTaiKhoan into hvGroup
                        from hv in hvGroup.DefaultIfEmpty()
                        select new UserAccount
                        {
                            //? là điều kiện để lọc
                            TenHocVien = hv != null ? hv.TenHocVien : null,
                            SoDienThoai = hv != null ? hv.SoDienThoai : null,
                            Email = hv != null ? hv.Email : null,
                            IDTaiKhoan = tk.IDTaiKhoan,
                            TaiKhoan = tk.TaiKhoan1,
                        };

            var userAccounts = query.ToList();

            return View(userAccounts);
        }
        public ActionResult DeleteUser(int id)
        {
            // Lấy thông tin tài khoản và học viên cần xóa từ cơ sở dữ liệu
            var userToDelete = data.TaiKhoans.FirstOrDefault(tk => tk.IDTaiKhoan == id);

            // Kiểm tra xem tài khoản và học viên có tồn tại hay không
            if (userToDelete == null )
            {
                return HttpNotFound(); // Hoặc bạn có thể trả về một view thông báo lỗi
            }

            // Xóa tài khoản
            data.TaiKhoans.DeleteOnSubmit(userToDelete);

            // Lưu các thay đổi vào cơ sở dữ liệu
            data.SubmitChanges();

            return RedirectToAction("UserAccount");
        }


    }
}

   