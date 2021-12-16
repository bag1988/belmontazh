using belmontazh.Areas.Admin.Models;
using belmontazh.Models;
using belmontazh.Models.Repositor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace belmontazh.Areas.Admin.Controllers
{
    [Authorize(Roles ="admin")]
    public class ProjectController : Controller
    {
        // GET: Admin/Project       
        public ActionResult Index(int page = 1, string name = "")
        {
            int pageSize = 50;
            var p = new Project();
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
        public ActionResult Create(projectModel project, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                string fName = "";
                if (image != null)
                {
                    FileInfo fInfo = new FileInfo(image.FileName);

                    fName = project.name.ToTranslit();
                    Directory.CreateDirectory(Server.MapPath("~/projectImg/"));
                    var sName = Directory.EnumerateFiles(Server.MapPath("~/projectImg/"), fName + ".*", SearchOption.AllDirectories);

                    if (sName.Count() > 0)
                    {
                        fName = fName + 0;
                        for (int i = 1; i > 0; i++)
                        {
                            if ((Directory.EnumerateFiles(Server.MapPath("~/projectImg/"), fName + ".*", SearchOption.AllDirectories).Count() == 0))
                            {
                                break;
                            }
                            fName = fName.Replace((i - 1).ToString(), i.ToString());
                        }
                    }
                    SaveImage save = new SaveImage();                    
                    project.urlImage = "/projectImg/" + save.Upload(image, 500, 390, fName, Server.MapPath("~/projectImg/"));
                }
                var p = new Project();
                p.Save(project);
                return RedirectToAction("Index", "Project");
            }
            return View(project);
        }

      
        public ActionResult Delete(string id)
        {            
            var p = new Project();            
            p.Delete(int.Parse(id));
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            var p = new Project();
            return View(p.Get(int.Parse(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(projectModel project, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                string fName = "";
                if (image != null)
                {
                    System.IO.File.Delete(Server.MapPath("~" + project.urlImage));
                    FileInfo fInfo = new FileInfo(image.FileName);

                    fName = project.name.ToTranslit();
                    Directory.CreateDirectory(Server.MapPath("~/projectImg/"));
                    var sName = Directory.EnumerateFiles(Server.MapPath("~/projectImg/"), fName + ".*", SearchOption.AllDirectories);
                    if (sName.Count() > 0)
                    {
                        for (int i = 1; i > 0; i++)
                        {
                            if ((Directory.EnumerateFiles(Server.MapPath("~/projectImg/"), fName + "-" + i.ToString() + ".*", SearchOption.AllDirectories).Count() == 0))
                            {
                                fName += "-" + i.ToString();
                                break;
                            }
                        }
                    }
                    SaveImage save = new SaveImage();
                    project.urlImage = "/projectImg/" + save.Upload(image, 500, 390, fName, Server.MapPath("~/projectImg/"));
                }
                var p = new Project();
                p.Eddit(project);
                return RedirectToAction("Index");
            }
            return View(project);
        }

        public ActionResult AddImg(string id)
        {
            var p = new Project();
            List<projectImg> images = new List<projectImg>();
            images = p.GetImg(int.Parse(id), 1, 200);
            ViewBag.Images = images;
            ViewBag.NameProject = p.Get(int.Parse(id)).name;
            projectImg pImage = new projectImg();
            pImage.idProject =int.Parse(id);            
            return View(pImage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddImg(projectImg pImage, IEnumerable<HttpPostedFileBase> image = null)
        {
            var p = new Project();
            string fName = "";
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    string nameTranslit = p.Get(pImage.idProject).name.ToTranslit();
                    FileInfo fInfo;
                    foreach (var file in image)
                    {
                        if (file != null)
                        {
                            p = new Project();
                            fInfo = new FileInfo(file.FileName);
                            fName = nameTranslit;
                            
                            Directory.CreateDirectory(Server.MapPath("~/projectImg/"));
                            var sName = Directory.EnumerateFiles(Server.MapPath("~/projectImg/"), fName+".*", SearchOption.AllDirectories);
                            if (sName.Count() > 0)
                            {
                                fName = fName + 0;
                                for (int i = 1; i > 0; i++)
                                {
                                    if ((Directory.EnumerateFiles(Server.MapPath("~/projectImg/"), fName + ".*", SearchOption.AllDirectories).Count() == 0))
                                    {
                                        break;
                                    }
                                    fName = fName.Replace((i - 1).ToString(), i.ToString());
                                }
                            }
                            SaveImage save = new SaveImage();
                            pImage.smalUrlImage = "/projectImg/" + save.Upload(file, 500, 390, "s" + fName, Server.MapPath("~/projectImg/"));
                            pImage.urlImage = "/projectImg/" + save.Upload(file, 1500, 700, fName, Server.MapPath("~/projectImg/"));
                            p.SaveImg(pImage);
                        }
                    }
                    return RedirectToAction("AddImg", new { id = pImage.idProject });
                }
            }
            List<projectImg> images = new List<projectImg>();
            images = p.GetImg(pImage.idProject, 1, 100);
            ViewBag.Images = images;
            return View(pImage);
        }

      
        public ActionResult DeleteImg(string id, string idProject)
        {
            var p = new Project();
            p.DeleteImage(int.Parse(id));
            return RedirectToAction("AddImg", new { id = idProject });
        }
    }
}