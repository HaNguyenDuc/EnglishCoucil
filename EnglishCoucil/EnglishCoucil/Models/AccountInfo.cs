using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCoucil.Models
{
    public class AccountInfo
    {
        public int IDHocVien { get; set; }
        public string TenHocVien { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string Hinh { get; set; }
        public int IDTaiKhoan { get; set; }
        public DateTime? NgayDangKy { get; set; } 
        public string Username { get; set; }
    }
}