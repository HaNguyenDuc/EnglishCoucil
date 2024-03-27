using EnglishCoucil.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnglishCoucil.Areas.Admin.Controllers
{
    public class QLChuongtrinhhocController : Controller
    {
        dbQLtrungtamDataContext data = new dbQLtrungtamDataContext();
        // GET: Admin/QLChuongtrinhhoc
        #region Show chương trình học

        public ActionResult Chuongtrinhhoc(int? IDmember, int? idaction, int? idlh)
        {
            ViewBag.idgv = IDmember;
            ViewBag.idaction = idaction;
            ViewBag.IDlh = idlh;
            var chuongtrinh = from ct in data.ChuongTrinhHocs select ct;
            return View(chuongtrinh);
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

        private bool CheckId(int id)
        {
            return data.HocViens.Count(x => x.IDHocvien == id) > 0;

        }

        [HttpGet]
        public ActionResult Themchuongtrinh()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Themchuongtrinh(FormCollection collection, ChuongTrinhHoc chuongtrinh)
        {
            var tenchuongtrinh = collection["Tenchuongtrinh"];
            var sobuoihoc = collection["Sobuoihoc"];
            var thoiluong = collection["Thoiluong"];
            var giatien = collection["Giatien"];
            var mota = collection["Mota"];

           if (string.IsNullOrEmpty(tenchuongtrinh) && tenchuongtrinh.Length > 20)
            {
                ViewData["Loi2"] = "Vui lòng nhập tên chương trình";
            }
            else if (string.IsNullOrEmpty(sobuoihoc))
            {
                ViewData["Loi3"] = "Vui lòng nhập số buổi học";
            }
            else if (string.IsNullOrEmpty(thoiluong))
            {
                ViewData["Loi4"] = "Vui lòng nhập thời lượng";
            }
            else if (string.IsNullOrEmpty(giatien) && giatien.Length > 20)
            {
                ViewData["Loi5"] = "Vui lòng nhập giá tiền";
            }
            else if (string.IsNullOrEmpty(mota))
            {
                ViewData["Loi6"] = "Vui lòng nhập mô tả";
            }
            else if
                 (
                !string.IsNullOrEmpty(sobuoihoc) && !string.IsNullOrEmpty(thoiluong) && !string.IsNullOrEmpty(giatien) &&
                !string.IsNullOrEmpty(mota))
            {
               
                chuongtrinh.TenChuongTrinh = tenchuongtrinh;
                chuongtrinh.SoBuoiHoc = sobuoihoc;
                chuongtrinh.ThoiLuong = thoiluong;
                chuongtrinh.GiaTien = int.Parse(giatien);
                chuongtrinh.MoTa = mota;
                data.ChuongTrinhHocs.InsertOnSubmit(chuongtrinh);
                data.SubmitChanges();
                return RedirectToAction("Chuongtrinhhoc");
            }
            return this.Themchuongtrinh();
        }
        //Xóa chương trình
        public ActionResult Xoachuongtrinh(int IDct)
        {
            var chuongTrinh = data.ChuongTrinhHocs.FirstOrDefault(c => c.IDChuongTrinh == IDct);
            if (chuongTrinh == null)
            {
                return HttpNotFound();
            }

            var lopHoc = data.LopHocs.FirstOrDefault(l => l.IDChuongTrinh == chuongTrinh.IDChuongTrinh);
            if (lopHoc != null)
            {
                TempData["ErrorMessage"] = "Không thể xóa chương trình học" +
                    " vì chương trình này đã có trong lớp học, hãy tạo mới";
                return RedirectToAction("Chuongtrinhhoc", "QLChuongtrinhhoc");
            }

            var chiTietLopHocs = data.ChiTietLopHocs.Where(c => c.IDLophoc == chuongTrinh.IDChuongTrinh);
            data.ChiTietLopHocs.DeleteAllOnSubmit(chiTietLopHocs);

            data.ChuongTrinhHocs.DeleteOnSubmit(chuongTrinh);
            data.SubmitChanges();

            TempData["SuccessMessage"] = "Đã xóa!";
            return RedirectToAction("Chuongtrinhhoc", "QLChuongtrinhhoc");
        }

        public ActionResult Xemchitiet(int IDct)
        {

            var show1chuongtrinh = data.ChuongTrinhHocs.FirstOrDefault(s => s.IDChuongTrinh == IDct);
            if (show1chuongtrinh == null)
            {
                return HttpNotFound();
            }
            return View(show1chuongtrinh);

        }
        public ActionResult Suachuongtrinh(int IDct)
        {

            ChuongTrinhHoc chuongtrinh = data.ChuongTrinhHocs.SingleOrDefault(c => c.IDChuongTrinh == IDct);
            if (chuongtrinh == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(chuongtrinh);
            }
        }

        [HttpPost]
        public ActionResult Suachuongtrinh(ChuongTrinhHoc editchuongtrinh)
        {
            if (ModelState.IsValid)
            {
                ChuongTrinhHoc chuongtrinh = data.ChuongTrinhHocs.SingleOrDefault(c => c.IDChuongTrinh == editchuongtrinh.IDChuongTrinh);
                if (chuongtrinh != null)
                {
                    if (editchuongtrinh.GiaTien > 20)
                    {
                        chuongtrinh.TenChuongTrinh = editchuongtrinh.TenChuongTrinh;
                    chuongtrinh.SoBuoiHoc = editchuongtrinh.SoBuoiHoc;
                    chuongtrinh.ThoiLuong = editchuongtrinh.ThoiLuong;
                    chuongtrinh.GiaTien = editchuongtrinh.GiaTien;
                    chuongtrinh.MoTa = editchuongtrinh.MoTa;
                    data.SubmitChanges();
                    return RedirectToAction("Chuongtrinhhoc");
                    }
                    else
                    {
                        ViewBag.SuccessMessage = "Nhập lại giá tiền";
                        return View(editchuongtrinh);
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return View(editchuongtrinh);
            }
        }
        #endregion
    }
}