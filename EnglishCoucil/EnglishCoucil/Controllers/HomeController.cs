using EnglishCoucil.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace EnglishCoucil.Controllers
{
    public class HomeController : Controller
    {
        dbQLtrungtamDataContext data = new dbQLtrungtamDataContext();
        public ActionResult Index()
        {

            var hocvien = from hv in data.HocViens select hv;
            return View(hocvien);

        }

            public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}