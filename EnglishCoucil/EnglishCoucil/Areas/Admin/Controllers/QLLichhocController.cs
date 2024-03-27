using EnglishCoucil.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnglishCoucil.Areas.Admin.Controllers
{
    public class QLLichhocController : Controller
    {
        // GET: Admin/QLLichhoc
        dbQLtrungtamDataContext data = new dbQLtrungtamDataContext();
        #region Show lớp học
        public ActionResult Cahoc(int? IDaction, int? IDlh)
        {
            ViewBag.IDaction = IDaction;
            ViewBag.IDlh = IDlh;
            var lichHocs = data.LichHocs.OrderBy(lh => lh.Ngay).ThenBy(lh => lh.TGBatDau);
            return View(lichHocs);
        }

        #endregion
        #region Thêm xóa sửa
        //Thêm ca học mới
        [HttpGet]
        public ActionResult Themcahoc()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Themcahoc(FormCollection collection, LichHoc cahoc/*,int IDNgay*/)
        {

            var tgbatdau = collection["TGbatdau"];
            var tgketthuc = collection["TGketthuc"];
            var ngay = DateTime.ParseExact(collection["Ngay"], "dd/MM/yyyy", CultureInfo.InvariantCulture);

            TimeSpan tgBatDau;
            TimeSpan tgKetThuc;

            bool isValidTGBatDau = TimeSpan.TryParse(tgbatdau, out tgBatDau);
            bool isValidTGKetThuc = TimeSpan.TryParse(tgketthuc, out tgKetThuc);
            //DayOfWeek là chuyển đổi thứ sang số từ 0-6 tương ứng chủ nhật - thứ 7
            var idNgay = ((int)ngay.DayOfWeek + 6) % 7 + 1;

            var existingLichHoc = data.LichHocs.FirstOrDefault(lh => lh.Ngay == cahoc.Ngay && lh.TGBatDau == cahoc.TGBatDau && lh.TGKetThuc == cahoc.TGKetThuc);
            if (existingLichHoc != null)
            {
                ViewData["Loi1"] = "Ca học đã có";
            }
            else if (!isValidTGBatDau || !isValidTGKetThuc)
            {
                ViewData["Loi1"] = "Nhập lại giờ";
            }
            else if (!isValidTGBatDau || !isValidTGKetThuc)
            {
                ViewData["Loi2"] = "Nhập lại giờ";
            }
            else if (tgBatDau > tgKetThuc)
            {
                ViewData["Loi2"] = "Giờ kết thúc phải sau giờ bắt đầu";
            }
            else if (!DateTime.TryParseExact(Request.Form["Ngay"], "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                ViewData["Loi3"] = "Ngày không hợp lệ";
            }
            else if (string.IsNullOrEmpty(Request.Form["Ngay"]))
            {
                ViewData["Loi3"] = "Vui lòng nhập ngày";
            }
            else
            {
                cahoc.TGBatDau = cahoc.TGBatDau;
                cahoc.TGKetThuc = cahoc.TGKetThuc;
                cahoc.Ngay = ngay;
                cahoc.IDNgay = idNgay;
                data.LichHocs.InsertOnSubmit(cahoc);
                data.SubmitChanges();
                return RedirectToAction("Cahoc");
            }
            return this.Themcahoc();
        }
        //Xóa ca học
        public ActionResult Xoacahoc(int IDch)
        {
            var cahoc = data.LichHocs.FirstOrDefault(c => c.IDLichhoc == IDch);
            if (cahoc == null)
            {
                return HttpNotFound();
            }

            // Xóa các bản ghi liên quan trong bảng ChiTietLichHoc
            var chiTietLichHocs = data.ChiTietLichHocs.Where(c => c.IDLichhoc == cahoc.IDLichhoc);
            data.ChiTietLichHocs.DeleteAllOnSubmit(chiTietLichHocs);
            data.SubmitChanges();
            // Xóa bản ghi trong bảng LichHoc
            data.LichHocs.DeleteOnSubmit(cahoc);
            data.SubmitChanges();

            TempData["SuccessMessage"] = "Đã xóa!";
            return RedirectToAction("Cahoc", "QLLichhoc");
        }

        [HttpGet]
        public ActionResult Suacahoc(int IDCaHoc)
        {

            LichHoc cahoc = data.LichHocs.SingleOrDefault(c => c.IDLichhoc == IDCaHoc);
            if (cahoc == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(cahoc);
            }
        }

        [HttpPost]
        public ActionResult Suacahoc(LichHoc editcahoc)
        {
            if (ModelState.IsValid)
            {
                LichHoc cahoc = data.LichHocs.SingleOrDefault(c => c.IDLichhoc == editcahoc.IDLichhoc);
                if (cahoc != null)
                {
                    cahoc.TGBatDau = editcahoc.TGBatDau;
                    cahoc.TGKetThuc = editcahoc.TGKetThuc;
                    cahoc.Ngay = editcahoc.Ngay;
                    data.SubmitChanges();
                    return RedirectToAction("Cahoc");
                }
                else
                {
                    return View(editcahoc);
                }
            }
            else
            {
                return HttpNotFound();
            }

            }
        }
        #endregion
    }
