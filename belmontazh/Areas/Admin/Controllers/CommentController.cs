using belmontazh.Models;
using belmontazh.Models.Repositor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace belmontazh.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class CommentController : Controller
    {
        // GET: Admin/Comment       
        public ActionResult Index(int page = 1, string name = "")
        {
            int pageSize = 50;
            var p = new Comment();
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = p.GetCount(name) };
            ViewBag.PageInfo = pageInfo;
            return View(p.Get(page, pageSize, name).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(commentModel comment)
        {
            if (ModelState.IsValid)
            {
                comment.dateComment = DateTime.Now.ToUniversalTime();
                var p = new Comment();
                p.Save(comment);

                return RedirectToAction("Index");
            }
            return View(comment);
        }
        
        public ActionResult Delete(string id)
        {
            var p = new Comment();
            p.Delete(int.Parse(id));
            return RedirectToAction("Index");
        }
    }
}