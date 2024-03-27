using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCoucil.Areas.Admin.Data
{
    public class UserAccount
    {
        public int IDHocvien { get; set; }
        public string TenHocVien { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public int IDTaiKhoan { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }

    }
}