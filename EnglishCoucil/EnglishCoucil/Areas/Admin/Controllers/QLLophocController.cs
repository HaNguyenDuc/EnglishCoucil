using EnglishCoucil.Areas.Admin.Data;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnglishCoucil.Areas.Admin.Controllers
{
    public class QLLophocController : Controller
    {
        // GET: Admin/QLLophoc
        dbQLtrungtamDataContext data = new dbQLtrungtamDataContext();
        #region Show lớp học
        public ActionResult Lophoc()
        {
            var lophoc = from lh in data.LopHocs select lh;
            return View(lophoc);
        }
        #endregion
        #region funtion
        private bool checkid(int id)
        {
            return data.LopHocs.Count(x => x.IDLophoc == id) > 0;
        }


        #endregion
        #region Thêm xóa sửa
        //hiện form thêm lớp học
        public ActionResult Themlophoc()
        {
            return View();
        }
        //xử lý yêu cầu để hiện qua danh sách giảng viên
        public ActionResult Themgvvaolop(int? idaction, int? idlh)
        {
            return RedirectToAction("Giangvien", "QLGiangvien", new { idaction, idlh });
        }
        //xử lý yêu cầu để hiện qua danh sách các chương trình học( có đem theo giá trị của giảng viên)
        public ActionResult Themctvaolop(int? Idremen, int? idaction, int? idlh)
        {

            return RedirectToAction("Chuongtrinhhoc", "QLChuongtrinhhoc", new { IDmember = Idremen, idaction, idlh });

        }

        //xử lý yêu cầu khi đã lấy được giảng viên
        public ActionResult Themlophocdacogv(int IDgv)
        {

            var namegv = data.GiangViens.FirstOrDefault(x => x.IDGiangvien == IDgv).TenGiangVien;
            ViewBag.idgv = IDgv;
            ViewBag.namegv = namegv;
            return View();
        }

        [HttpGet]
        public ActionResult Themlophocdacoct(int Idremen, int IDct)
        {
            var namegv = data.GiangViens.FirstOrDefault(x => x.IDGiangvien == Idremen).TenGiangVien;
            var namect = data.ChuongTrinhHocs.FirstOrDefault(x => x.IDChuongTrinh == IDct).TenChuongTrinh;
            ViewBag.idgv = Idremen;
            ViewBag.namegv = namegv;
            ViewBag.idct = IDct;
            ViewBag.namect = namect;
            return View();
        }
        [HttpPost]
        public ActionResult Themlophocdacoct(FormCollection collection, LopHoc lophoc, int Idremen, int IDct)
        {

            var tenlophoc = collection["Tenlophoc"];

           if (string.IsNullOrEmpty(tenlophoc))
            {
                ViewData["Loi2"] = "Vui lòng nhập tên lớp học";
            }
            else
            {
                lophoc.TenLopHoc = tenlophoc;
                lophoc.IDGiangVien = Idremen;
                lophoc.IDChuongTrinh = IDct;
                data.LopHocs.InsertOnSubmit(lophoc);
                data.SubmitChanges();
                return RedirectToAction("Lophoc");
            }
            return this.Themlophoc();
        }
        //xóa lớp học 
        public ActionResult Xoalophoc(int IDlh)
        {
            var lophoc = data.LopHocs.FirstOrDefault(c => c.IDLophoc == IDlh);
            if (lophoc == null)
            {
                TempData["ErrorMessage"] = "Lớp học không tồn tại.";
                return RedirectToAction("Lophoc", "QLLophoc");
            }

            // Kiểm tra xem lớp học có dữ liệu liên quan trong bảng ChiTietLopHoc hay không
            var chiTietLopHocs = data.ChiTietLopHocs.Where(c => c.IDLophoc == lophoc.IDLophoc).ToList();
            var chiTietLichHocs = data.ChiTietLichHocs.Where(c => c.IDLophoc == lophoc.IDLophoc).ToList();
            if (chiTietLopHocs.Count > 0 || chiTietLichHocs.Count > 0)
            {
                TempData["ErrorMessage"] = "Không thể xóa lớp học vì có dữ liệu, hãy thêm mới";
                return RedirectToAction("Lophoc", "QLLophoc");
            }

            // Xóa bản ghi trong bảng LopHoc
            data.LopHocs.DeleteOnSubmit(lophoc);
            data.SubmitChanges();

            TempData["SuccessMessage"] = "Đã xóa!";
            return RedirectToAction("Lophoc", "QLLophoc");
        }


        //SỬA LỚP HỌC 


        // xử lý yêu cầu khi có được giảng viên trong view sửa
        public ActionResult Sualophoccogv(int IDlh, int IDgv)
        {
            ViewBag.Idlh = IDlh;
            var namegv = data.GiangViens.FirstOrDefault(x => x.IDGiangvien == IDgv).TenGiangVien;
            ViewBag.idgv = IDgv;
            ViewBag.namegv = namegv;
            LopHoc lophoc = data.LopHocs.SingleOrDefault(c => c.IDLophoc == IDlh);
            if (lophoc == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(lophoc);
            }

        }
        //hiển thị 1 lớp học yêu cầu để sửa

        public ActionResult Sualophoc(int IDlh)
        {
            ViewBag.IDlh = IDlh;
            LopHoc lophoc = data.LopHocs.SingleOrDefault(c => c.IDLophoc == IDlh);
            if (lophoc == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(lophoc);
            }
        }
        //Hiển thị về view danh sách đã có lưu id của giảng viên và cả lớp học và chương trình
        [HttpGet]
        public ActionResult Sualophoccoct(int IDlh, int? IDct, int? Idremen)
        {
            ViewBag.Idlh = IDlh;
            var namegv = data.GiangViens.FirstOrDefault(x => x.IDGiangvien == Idremen).TenGiangVien;
            var namect = data.ChuongTrinhHocs.FirstOrDefault(x => x.IDChuongTrinh == IDct).TenChuongTrinh;
            ViewBag.idgv = Idremen;
            ViewBag.namegv = namegv;
            ViewBag.idct = IDct;
            ViewBag.namect = namect;
            LopHoc lophoc = data.LopHocs.SingleOrDefault(c => c.IDLophoc == IDlh);
            if (lophoc == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(lophoc);
            }

        }

        [HttpPost]
        public ActionResult Sualophoccoct(LopHoc editlophoc, int? IDct, int? Idremen, int IDlh)
        {

            if (ModelState.IsValid)
            {
                LopHoc lophoc = data.LopHocs.SingleOrDefault(c => c.IDLophoc == IDlh);
                if (lophoc != null)
                {
                    lophoc.IDLophoc = editlophoc.IDLophoc;
                    lophoc.TenLopHoc = editlophoc.TenLopHoc;
                    lophoc.IDGiangVien = Idremen ?? lophoc.IDGiangVien;
                    lophoc.IDChuongTrinh = IDct ?? lophoc.IDChuongTrinh;
                    data.SubmitChanges();
                    return RedirectToAction("Lophoc");
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return View(editlophoc);
            }
        }

        //thêm học viên vào lớp học
        public ActionResult themhvvaolop(int IDlh)
        {
            ViewBag.Idlh = IDlh;
            string namelh = data.LopHocs.FirstOrDefault(x => x.IDLophoc == IDlh).TenLopHoc;
            ViewBag.namelh = namelh;
            int? slMax = data.LopHocs.FirstOrDefault(x => x.IDLophoc == IDlh).SoLuong;
            bool checkSL = data.ChiTietLopHocs.Count(x => x.IDLophoc == IDlh) >= slMax;
            if (checkSL)
            {
                TempData["AlertMessage"] = "Lớp đầy";
                return RedirectToAction("Lophoc");
            }
            else
            {
                //!data.ChiTietLopHocs.Any(ct => cxt.IDHocVien == hv.IDHocvien) để lọc ra các học viên không có bản ghi tương ứng trong bảng ChiTietLopHoc.
                var hocvienList = data.HocViens.Where(hv => !data.ChiTietLopHocs.Any(ct => ct.IDHocVien == hv.IDHocvien || (ct.IDHocVien == hv.IDHocvien && ct.DiemTB == null))).ToList();
                return View(hocvienList); 
            }
        }
        // nút xem danh sách học sinh trong lớp
        public ActionResult Xemhvtronglop(int IDlh)
        {
            ViewBag.Idlh = IDlh;

            var namelh = data.LopHocs.FirstOrDefault(x => x.IDLophoc == IDlh)?.TenLopHoc;
            ViewBag.namelh = namelh;
            List<ChiTietLopHoc> hocvienList = new List<ChiTietLopHoc>();
            List<ChiTietLopHoc> listIdHocVien = data.ChiTietLopHocs.Where(item => item.IDLophoc == IDlh).ToList();
            foreach (ChiTietLopHoc item in listIdHocVien)
            {
                ChiTietLopHoc hocVien = data.ChiTietLopHocs.FirstOrDefault(x => x.IDHocVien == item.IDHocVien);
                hocvienList.Add(hocVien);
            }
            int soHocSinh = hocvienList.Count;
            ViewBag.count = soHocSinh;
            return View(hocvienList);
        }
        public ActionResult Status(int? IDlh,int? IDhv)
        {

            ChiTietLopHoc chiTietLopHoc = data.ChiTietLopHocs.FirstOrDefault(x => x.IDHocVien == IDhv && x.IDLophoc == IDlh);
            //dùng toán tử 3 ngôi biến = biểu thức?biểu thức 2: biểu thức 3; (nếu biểu thức 1 đúng trả về biểu thức 2 cỏn sai thì trả bt3 ) 
            chiTietLopHoc.DaThanhToan = (chiTietLopHoc.DaThanhToan == false) ? true : false;
            if (chiTietLopHoc.DaThanhToan == false)
            {
                chiTietLopHoc.NgayNopTien = null;
                chiTietLopHoc.HocVien.IDTrangThai = 1;
            }
            else
            {
                chiTietLopHoc.NgayNopTien = DateTime.Now;
                chiTietLopHoc.HocVien.IDTrangThai = 2;
            }
            
            UpdateModel(chiTietLopHoc);
            data.SubmitChanges();
            return RedirectToAction("Xemhvtronglop", new { IDlh });
        }



        //nút action AddToClass
        public ActionResult addtoclass(int IDlh, int IDhv)
        {
            ChiTietLopHoc hocvien = new ChiTietLopHoc();
            hocvien.IDLophoc = IDlh;
            hocvien.IDHocVien = IDhv;
            hocvien.DaThanhToan = false;
            data.ChiTietLopHocs.InsertOnSubmit(hocvien);
            data.SubmitChanges();
            return RedirectToAction("themhvvaolop", new { IDlh });
        }

        //nút xóa học sinh ra khỏi lớp
        public ActionResult DeleteStudent(int? IDlh, int? IDhv)
        {
            if (IDlh != null && IDhv != null)
            {
                ChiTietLopHoc hocvien = data.ChiTietLopHocs.FirstOrDefault(x => x.IDHocVien == IDhv && x.IDLophoc == IDlh);
                if (hocvien != null)
                {
                    data.ChiTietLopHocs.DeleteOnSubmit(hocvien);
                    data.SubmitChanges();
                }
            }
            return RedirectToAction("Xemhvtronglop", new { IDlh });
        }

        //nút thêm điểm cho học viên
        [HttpGet]
        public ActionResult addScore(int IDlh, int IDhv, int IDaction)
        {
            ViewBag.IDcation = IDaction;
            ViewBag.IDlh = IDlh;
            ViewBag.IDhv = IDhv;
            string namelh = data.LopHocs.FirstOrDefault(x => x.IDLophoc == IDlh).TenLopHoc;
            ViewBag.namelh = namelh;
            string namehv = data.HocViens.FirstOrDefault(x => x.IDHocvien == IDhv).TenHocVien;
            ViewBag.namehv = namehv;
            return View();
        }

        [HttpPost]
        public ActionResult addScore(FormCollection collection, int IDlh, int IDhv, int IDaction)
        {
            var hocVien = data.HocViens.FirstOrDefault(x => x.IDHocvien == IDhv);
            var lopHoc = data.LopHocs.FirstOrDefault(x => x.IDLophoc == IDlh);

            float diemnghe = float.Parse(collection["Diemnghe"]);
            float diemnoi = float.Parse(collection["Diemnoi"]);
            float diemdoc = float.Parse(collection["Diemdoc"]);
            float diemviet = float.Parse(collection["Diemviet"]);

            if (diemnghe > 10 && diemnoi > 10 && diemdoc > 10 && diemviet > 10)
            {
                ViewData["Loi1"] = "Điểm phải nhỏ hơn 10";
            }
            else if (hocVien != null && lopHoc != null)
            {
                // Kiểm tra xem đã tồn tại bản ghi trong ChiTietLopHoc có cùng IDHocVien và IDLophoc hay chưa
                var chiTietLopHoc = data.ChiTietLopHocs.FirstOrDefault(x => x.IDHocVien == hocVien.IDHocvien && x.IDLophoc == lopHoc.IDLophoc);

                if (chiTietLopHoc == null)
                {
                    TempData["AlertMessage"] = "Không tìn thấy!";
                    return RedirectToAction("Xemhvtronglop", new { IDlh });
                }
                // Cập nhật các cột phụ trong bản ghi hiện có
                chiTietLopHoc.DiemNghe = diemnghe;
                chiTietLopHoc.DiemNoi = diemnoi;
                chiTietLopHoc.DiemDoc = diemdoc;
                chiTietLopHoc.DiemViet = diemviet;
                // Lưu các thay đổi vào cơ sở dữ liệu
                data.SubmitChanges();
                return RedirectToAction("Xemhvtronglop", new { IDlh });
            }

            // Nếu không tìm thấy hocVien hoặc lopHoc
            return this.addScore(IDlh, IDhv, IDaction);
        }

        //xem điểm của học viên
        public ActionResult viewScore(int IDlh, int IDhv)
        {
            ViewBag.IDlh = IDlh;
            ViewBag.IDhv = IDhv;

            string namelh = data.LopHocs.FirstOrDefault(x => x.IDLophoc == IDlh)?.TenLopHoc;
            ViewBag.namelh = namelh;

            string namehv = data.HocViens.FirstOrDefault(x => x.IDHocvien == IDhv)?.TenHocVien;
            ViewBag.namehv = namehv;

            var score = data.ChiTietLopHocs.Where(sc => sc.IDLophoc == IDlh && sc.HocVien.IDHocvien == IDhv).ToList();
            return View(score);
        }


        //thêm lớp học vào ca học 
        [HttpGet]
        public ActionResult ThemlophocvaoCaHoc(int IDlh, int? IDCaHoc)
        {

            string Ngay = data.LichHocs.FirstOrDefault(x => x.IDLichhoc == IDCaHoc)?.Ngay.Value.ToString("dd/MM/yyyy");
            ViewBag.Ngay = Ngay;

            var lichHoc = data.LichHocs.FirstOrDefault(x => x.IDLichhoc == IDCaHoc);
            string TGBatdau = lichHoc?.TGBatDau?.ToString("hh\\:mm");
            ViewBag.TGBatdau = TGBatdau;

            string TGKetthuc = lichHoc?.TGKetThuc?.ToString("hh\\:mm");
            ViewBag.TGKetthuc = TGKetthuc;


            ViewBag.Idlh = IDlh;
            LopHoc lophoc = data.LopHocs.SingleOrDefault(c => c.IDLophoc == IDlh);
            if (lophoc == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Lấy danh sách các ca học
                var caHocList = data.LichHocs.ToList();
                ViewBag.CaHocList = caHocList;

                return View(lophoc);
            }
        }

        [HttpPost]
        public ActionResult ThemlophocvaoCaHoc(int IDlh, int IDCaHoc)
        {
            ViewBag.Idlh = IDlh;
            var lopHoc = data.LopHocs.FirstOrDefault(c => c.IDLophoc == IDlh);
            var caHoc = data.LichHocs.FirstOrDefault(c => c.IDLichhoc == IDCaHoc);

            if (lopHoc == null || caHoc == null)
            {
                return HttpNotFound();
            }

            // Kiểm tra xem lớp học đã được gán vào ca học hay chưa
            if (data.ChiTietLichHocs.Any(ct => ct.IDLophoc == IDlh && ct.IDLichhoc == IDCaHoc))
            {
                TempData["ErrorMessage"] = "Lớp học đã được gán vào ca học.";
                return RedirectToAction("ThemlophocvaoCaHoc", new { IDlh, IDCaHoc });
            }
            else
            {
                // Tạo một bản ghi ChiTietLichHoc mới và gán thông tin lớp học và ca học
                var chiTietLichHoc = new ChiTietLichHoc();
                chiTietLichHoc.IDLophoc = IDlh;
                chiTietLichHoc.IDLichhoc = IDCaHoc;

                // Thêm bản ghi vào cơ sở dữ liệu
                data.ChiTietLichHocs.InsertOnSubmit(chiTietLichHoc);
                data.SubmitChanges();

                TempData["SuccessMessage"] = "Thêm lớp học vào ca học thành công.";
            }

            return RedirectToAction("Lophoc");
        }

        //xem ca học có trong lớp 
        public ActionResult ShiftinClass()
        {
            var lophoc = from lh in data.ChiTietLichHocs select lh;
            return View(lophoc);
        }
        #endregion
    }
}