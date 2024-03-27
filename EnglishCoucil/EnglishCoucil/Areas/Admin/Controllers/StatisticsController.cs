using EnglishCoucil.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Schema;

namespace EnglishCoucil.Areas.Admin.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Admin/Statistics
        dbQLtrungtamDataContext data = new dbQLtrungtamDataContext();
        public ActionResult StaticView()
        {
            double? totalsPrice = 0;
          var statics = data.ChiTietLopHocs.Where(x => x.DaThanhToan == true);
            foreach (ChiTietLopHoc price in statics)
            { 
                totalsPrice += price.LopHoc.ChuongTrinhHoc.GiaTien;
            }
            ViewBag.total = totalsPrice;
                return View(statics);
        }
    }
}