using belmontazh.Models;
using belmontazh.Models.Repositor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace belmontazh.Controllers
{
    public class articlesController : Controller
    {
        // GET: articles
        public ActionResult Index(int page = 1, string name = "")
        {
            int pageSize = 50;
            var p = new KnowBase();
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = p.GetCount(name) };
            ViewBag.PageInfo = pageInfo;
            return View(p.Get(page, pageSize, name).ToList());
        }

        public ActionResult detalis(string name)
        {
            if (name==null)
              return  RedirectToAction("index");
            var p = new KnowBase();
            return View(p.Get(name));
        }

        [ChildActionOnly]
        public ActionResult resultKnowBase(string name="")
        {
            var r = new KnowBase();
            return PartialView(r.Get(1, 3, name).ToList());
        }
    }
}