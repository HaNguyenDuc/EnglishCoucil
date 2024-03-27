using EnglishCoucil.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace EnglishCoucil.Areas.Admin.Controllers
{
    public class QLGiangvienController : Controller
    {
        // GET: Admin/QLGiangvien
        dbQLtrungtamDataContext data = new dbQLtrungtamDataContext();
        // GET: QLHocvien
        #region Show Giảng viên

        public ActionResult Giangvien(int? idaction, int? idlh)
        {
            ViewBag.idaction = idaction;
            ViewBag.IDlh = idlh;
            var giangvien = from gv in data.GiangViens select gv;
            return View(giangvien);
        }
        #endregion
        #region Thêm xóa sửa
        public bool CheckInput(string input)
        {
            // Kiểm tra độ dài của dãy
            if (input.Length > 8)
            {
                return false;
            }

            // Kiểm tra từng ký tự trong dãy
            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }

 
        private bool checkemail(string email)
        {
            return data.GiangViens.Any(x => x.Email == email);
        }
        private bool IsValidEmail(string email)
        {
            // Biểu thức chính quy kiểm tra định dạng email
            string pattern = @"^[a-zA-Z0-9.-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Kiểm tra xem email có trùng khớp với biểu thức chính quy hay không
            return Regex.IsMatch(email, pattern);
        }
        //được sử dụng để upload hình ảnh
        public string ProcessUpload(HttpPostedFileBase file)
        {  //kiểm tra file nếu không có tệp tin nào được tải lên, và phương thức trả về một chuỗi rỗng ""
            if (file == null)
            {
                return "";
            }
            //nếu không thì sẽ lưu vào đường dẫn và thực hiện phương thức SaveAs ( với đường dẫn là "~/Areas/Admin/Content/images/" + file.FileName)
            file.SaveAs(Server.MapPath("~/Areas/Admin/Content/images/" + file.FileName));
            //Sau khi tệp tin được lưu thành công
            // trả về đường dẫn của tệp tin hình ảnh đã lưu, bắt đầu bằng "/Content/images/" + tên tệp tin file.FileName.
            return "/Areas/Admin/Content/images/" + file.FileName;
        }
       
      
        [HttpGet]
        public ActionResult Themgiangvien()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Themgiangvien(FormCollection collection, GiangVien giangvien)
        {
            var tengiangvien = collection["Tengiangvien"];
            var diachi = collection["DiaChi"];
            var sodt = collection["Phone"];
            var email = collection["Email"];
            var luong = collection["Luong"];

           if (string.IsNullOrEmpty(tengiangvien))
            {
                ViewData["Loi2"] = "Vui lòng nhập tên giảng viên";
            }
            else if (string.IsNullOrEmpty(diachi))
            {
                ViewData["Loi3"] = "Vui lòng nhập địa chỉ";
            }
            else if (string.IsNullOrEmpty(sodt))
            {
                ViewData["Loi4"] = "Vui lòng nhập số điện thoại";
            }
            else if (string.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Vui lòng nhập email";
            }
            else if (checkemail(email))
            {
                ViewData["Loi5"] = "Email đã có";
            }
            else if (!IsValidEmail(email))
            {
                ViewData["Loi5"] = "Email không hợp lệ";
            }
            else if (string.IsNullOrEmpty(luong) || luong.Length > 20)
            {
                ViewData["Loi6"] = "Vui lòng nhập lại lương";
            }
            else
            {
                giangvien.TenGiangVien = tengiangvien;
                giangvien.DiaChi = diachi;
                giangvien.SoDienThoai = sodt;
                giangvien.Email = email;
                giangvien.Hinh = collection["Hinh"];
                giangvien.BangCap = collection["BangCap"];
                giangvien.Luong = int.Parse(luong);
                data.GiangViens.InsertOnSubmit(giangvien);
                data.SubmitChanges();
                return RedirectToAction("Giangvien");
            }
            return this.Themgiangvien();
        }
        public ActionResult Xoagiangvien(int IDgv)
        {
            var giangvien = data.GiangViens.FirstOrDefault(c => c.IDGiangvien == IDgv);
            if (giangvien == null)
            {
                return HttpNotFound();
            }
            var lopHoc = data.LopHocs.FirstOrDefault(l => l.IDGiangVien == giangvien.IDGiangvien);
            if (lopHoc != null)
            {
                TempData["ErrorMessage"] = "Không thể xóa giảng viên vì giảng viên này đã có trong lớp học, hãy tạo mới";
                return RedirectToAction("Giangvien", "QLGiangvien");
            }


            data.GiangViens.DeleteOnSubmit(giangvien);
            data.SubmitChanges();

            TempData["SuccessMessage"] = "Đã xóa!";
            return RedirectToAction("Giangvien", "QLGiangvien");
        }


        public ActionResult Xemchitiet(int IDgv)
        {

            var show1giangvien = data.GiangViens.FirstOrDefault(s => s.IDGiangvien == IDgv);
            if (show1giangvien == null)
            {
                return HttpNotFound();
            }
            return View(show1giangvien);

        }
        public ActionResult Suagiangvien(int IDgv)
        {

            GiangVien giangvien = data.GiangViens.SingleOrDefault(c => c.IDGiangvien == IDgv);
            if (giangvien == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(giangvien);
            }
        }

        [HttpPost]
        public ActionResult Suagiangvien(GiangVien editGiangvien)
        {
            if (ModelState.IsValid)
            {
                string hinhUrl = Request.Form["Hinh"];
                string bangCapUrl = Request.Form["BangCap"];
                GiangVien giangvien = data.GiangViens.SingleOrDefault(c => c.IDGiangvien == editGiangvien.IDGiangvien);
                if (giangvien != null)
                {
                    giangvien.IDGiangvien = editGiangvien.IDGiangvien;
                    giangvien.TenGiangVien = editGiangvien.TenGiangVien;
                    giangvien.DiaChi = editGiangvien.DiaChi;
                    giangvien.SoDienThoai = editGiangvien.SoDienThoai;
                    giangvien.Email = editGiangvien.Email;
                    giangvien.Hinh = editGiangvien.Hinh;
                    giangvien.BangCap = editGiangvien.BangCap;
                    giangvien.Luong = editGiangvien.Luong;
                    data.SubmitChanges();
                    return RedirectToAction("Giangvien");
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return View(editGiangvien);
            }
        }
        public ActionResult add(int IDgv)
        {
            TempData["IDgv"] = IDgv;
            return RedirectToAction("showid", "QLLophoc");
        }

        #endregion
    }
}