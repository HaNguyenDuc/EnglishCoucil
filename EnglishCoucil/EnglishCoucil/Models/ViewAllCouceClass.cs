using EnglishCoucil.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCoucil.Models
{
    public class ViewAllCouceClass
    {
        public int IDLopHoc { get; set; }
        public string TenLopHoc { get; set; }
        public int IDGiangVien { get; set; }
        public string TenChuongTrinh { get; set; }
       
        public string SoBuoiHoc { get; set; }
        public string ThoiLuong { get; set; }
        public int? SoLuong { get; set; }
        public string MoTa { get; set; }
        public double? GiaTien { get; set; }
        public int Count { get; set; }


    }
 }
