using belmontazh.Areas.Admin.Models;
using belmontazh.Models;
using belmontazh.Models.Repositor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace belmontazh.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class knowBaseController : Controller
    {
        public ActionResult Index(int page=1, string name = "")
        {
            int pageSize = 50;
            var p = new KnowBase();
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
        public ActionResult Create(knowBaseModel knowBase, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                string fName = "";
                fName = knowBase.name.ToTranslit();
                knowBase.nameEn = fName;
                if (image != null)
                {
                    FileInfo fInfo = new FileInfo(image.FileName);
                    Directory.CreateDirectory(Server.MapPath("~/knowImages/"));
                    var sName = Directory.EnumerateFiles(Server.MapPath("~/knowImages/"), fName + ".*", SearchOption.AllDirectories);
                    if (sName.Count() > 0)
                    {
                        for (int i = 1; i > 0; i++)
                        {
                            if ((Directory.EnumerateFiles(Server.MapPath("~/knowImages/"), fName + "-" + i.ToString() + ".*", SearchOption.AllDirectories).Count() == 0))
                            {
                                fName += "-" + i.ToString();
                                break;
                            }
                        }
                    }
                    SaveImage save = new SaveImage();
                    knowBase.urlImage = "/knowImages/" + save.Upload(image, 1000, 600, fName, Server.MapPath("~/knowImages/"));
                }
                knowBase.date = DateTime.Now.ToUniversalTime();
                var p = new KnowBase();
                p.Save(knowBase);
                return RedirectToAction("Index");
            }
            return View(knowBase);
        }

        
        public ActionResult Delete(string id)
        {
            var p = new KnowBase();
            p.Delete(int.Parse(id));
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            var p = new KnowBase();
            return View(p.Get(int.Parse(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(knowBaseModel knowBase, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                string fName = "";
                fName = knowBase.name.ToTranslit();
                knowBase.nameEn = fName;
                if (image != null)
                {
                    FileInfo fInfo = new FileInfo(image.FileName);
                    Directory.CreateDirectory(Server.MapPath("~/knowImages/"));
                    var sName = Directory.EnumerateFiles(Server.MapPath("~/knowImages/"), fName + ".*", SearchOption.AllDirectories);
                    if (sName.Count() > 0)
                    {
                        for (int i = 1; i > 0; i++)
                        {
                            if ((Directory.EnumerateFiles(Server.MapPath("~/knowImages/"), fName + "-" + i.ToString() + ".*", SearchOption.AllDirectories).Count() == 0))
                            {
                                fName += "-" + i.ToString();
                                break;
                            }
                        }
                    }
                    SaveImage save = new SaveImage();                    
                    knowBase.urlImage = "/knowImages/" + save.Upload(image, 1000, 600, fName, Server.MapPath("~/knowImages/"));
                }
                knowBase.date = DateTime.Now.ToUniversalTime();
                var p = new KnowBase();
                p.Eddit(knowBase);
                return RedirectToAction("Index");
            }
            return View(knowBase);
        }

        public ActionResult imageResult(string id="0")
        {
            int i = int.Parse(id);
            List<string> url = new List<string>();
            ViewBag.Count = Math.Ceiling((Double)(new DirectoryInfo(Server.MapPath("~/knowImages/")).GetFiles().Length/20));
            url = new DirectoryInfo(Server.MapPath("~/knowImages/")).GetFiles().OrderByDescending(x => x.CreationTime).Skip(20 * (i - 1)).Take(20).Select(x => "/knowImages/" + x.Name).ToList();
            return PartialView("imageResult", url);
        }

        [HttpPost]
        public ActionResult SaveImage()
        {
            try
            {
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var image = System.Web.HttpContext.Current.Request.Files[0];
                    HttpPostedFileBase filebase = new HttpPostedFileWrapper(image);
                    string fName = DateTime.Now.Ticks.ToString();
                    SaveImage save = new SaveImage();
                    save.Upload(filebase, 1000, 600, fName, Server.MapPath("~/knowImages/"));
                    return Json("File Saved Successfully.");
                }
                else { return Json("No File Saved."); }
            }
            catch (Exception ex) { return Json(ex.Message); }            
        }
    }
}