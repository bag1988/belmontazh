using belmontazh.Models;
using belmontazh.Models.Repositor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace belmontazh.Controllers
{
    public class projectsController : Controller
    {
        // GET: projects
        public ActionResult Index(int page = 1, string name = "")
        {
            int pageSize = 50;
            var p = new Project();
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = p.GetCount(name) };
            ViewBag.PageInfo = pageInfo;
            return View(p.Get(page, pageSize, name).ToList());
        }

        public ActionResult detalis(int id)
        {
            var p = new Project();
            if(p.Get(id)==null)
                return RedirectToAction("index", "projects");
            List<projectImg> images = new List<projectImg>();
            images = p.GetImg(id, 1, 200);
            ViewBag.NameProject = p.Get(id).name;            
            return View(images);
        }
    }
}