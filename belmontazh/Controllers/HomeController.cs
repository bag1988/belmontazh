using belmontazh.Areas.Admin.Models;
using belmontazh.Models;
using belmontazh.Models.Repositor;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace belmontazh.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {                      
            return View();
        }

        public ActionResult Price()
        {
            var p = new Price();
            ViewBag.Kategories = p.GetKategories();
            ViewBag.Units = new Units().Get();
            return View(p.GetByKategories().ToList());
        }

        [ChildActionOnly]
        public ActionResult resultComment()
        {
            var r = new Comment();
            return PartialView(r.Get(1, 3).ToList());
        }
    }
}