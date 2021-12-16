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
    public class OknaController : Controller
    {
        // GET: Admin/Okna
        public ActionResult Index()
        {
            var p = new Okno();
            ViewBag.list = p.Get();
            return View();
        }

        [HttpPost]
        public ActionResult Index(oknaForm project)
        {
            var p = new Okno();
            if (ModelState.IsValid)
            {
                double width = 0.0, height = 0.0, count = 0, col = 0;
                double cteklo = 0.0, prof = 0.0, hardware = 0.0, types = 0.0;

                width = (float)project.width / 1000;
                height = (float)project.height / 1000;

                col = p.GetTypes(project.oknaTypeModel).col;
                count = p.GetTypes(project.oknaTypeModel).count;
                types = p.GetTypes(project.oknaTypeModel).cost;
                cteklo = p.GetCteklo(project.oknaCtekloModel).cost;
                prof = p.GetProf(project.oknaProfModel).cost;
                hardware = p.GetHardware(project.oknaHardwareModel).cost;

                ViewBag.Cost = Math.Ceiling((width * 2 + height * (col + 1)) * prof + (width / col * 2 + height * 2) * count * prof + width * height * cteklo + hardware * count) * types;
            }
            ViewBag.list = p.Get();
            return View(project);
        }

        public ActionResult CreateCteklo()
        {
            var p = new Okno();
            ViewBag.List = p.GetCteklo();
            ViewBag.Units = new SelectList(new Units().Get(), "id", "name");
            return View();
        }
        public ActionResult DeleteCteklo(int id)
        {
            var p = new Okno();
            p.DeleteCteklo(id);
            return RedirectToAction("CreateCteklo");
        }
        [HttpPost]
        public ActionResult CreateCteklo(oknaCtekloModel project)
        {
            var p = new Okno();
            if (ModelState.IsValid)
            {
                if (project.id != 0)
                {
                    p.EdditCteklo(project);
                }
                else
                {
                    p.SaveCteklo(project);
                }
                
                return RedirectToAction("CreateCteklo", "Okna");
            }
            ViewBag.List = p.GetCteklo();
            ViewBag.Units = new SelectList(new Units().Get(), "id", "name");
            return View(project);
        }

        public ActionResult CreateProf()
        {
            var p = new Okno();
            ViewBag.List = p.GetProf();
            ViewBag.Units = new SelectList(new Units().Get(), "id", "name");
            return View();
        }
        public ActionResult DeleteProf(int id)
        {
            var p = new Okno();
            p.DeleteProf(id);
            return RedirectToAction("CreateProf");
        }
        [HttpPost]
        public ActionResult CreateProf(oknaProfModel project)
        {
            var p = new Okno();
            if (ModelState.IsValid)
            {
                if (project.id != 0)
                {
                    p.EdditProf(project);
                }
                else
                {
                    p.SaveProf(project);
                }                
                return RedirectToAction("CreateProf", "Okna");
            }
            ViewBag.List = p.GetProf();
            ViewBag.Units = new SelectList(new Units().Get(), "id", "name");
            return View(project);
        }

        public ActionResult CreateHardware()
        {
            var p = new Okno();
            ViewBag.List = p.GetHardware();
            ViewBag.Units = new SelectList(new Units().Get(), "id", "name");
            return View();
        }
        public ActionResult DeleteHardware(int id)
        {
            var p = new Okno();
            p.DeleteHardware(id);
            return RedirectToAction("CreateHardware");
        }
        [HttpPost]
        public ActionResult CreateHardware(oknaHardwareModel project)
        {
            var p = new Okno();
            if (ModelState.IsValid)
            {
                if (project.id != 0)
                {
                    p.EdditHardware(project);
                }
                else
                {
                    p.SaveHardware(project);
                }
                
                return RedirectToAction("CreateHardware", "Okna");
            }
            ViewBag.List = p.GetHardware();
            ViewBag.Units = new SelectList(new Units().Get(), "id", "name");
            return View(project);
        }

        public ActionResult CreateType()
        {
            var p = new Okno();
            ViewBag.List = p.GetTypes();
            ViewBag.Units = new SelectList(new Units().Get(), "id", "name");
            return View();
        }
        public ActionResult DeleteTypes(int id)
        {
            var p = new Okno();
            p.DeleteTypes(id);
            return RedirectToAction("CreateType");
        }
        [HttpPost]
        public ActionResult CreateType(oknaTypeModel project, HttpPostedFileBase image = null)
        {
            var p = new Okno();


            string fName = "";
            if (image != null)
            {
                FileInfo fInfo = new FileInfo(image.FileName);

                fName = project.name.ToTranslit();
                Directory.CreateDirectory(Server.MapPath("~/images/"));
                var sName = Directory.EnumerateFiles(Server.MapPath("~/images/"), fName + ".*", SearchOption.AllDirectories);
                if (sName.Count() > 0)
                {
                    for (int i = 1; i > 0; i++)
                    {
                        if ((Directory.EnumerateFiles(Server.MapPath("~/images/"), fName + "-" + i.ToString() + ".*", SearchOption.AllDirectories).Count() == 0))
                        {
                            fName += "-" + i.ToString();
                            break;
                        }
                    }
                }
                SaveImage save = new SaveImage();
                project.urlImage = "/images/" + save.Upload(image, 800, 800, fName, Server.MapPath("~/images/"));
            }


            if (ModelState.IsValid)
            {
                if (project.id != 0)
                {
                    p.EdditType(project);
                }
                else
                {
                    p.SaveType(project);
                }
                
                return RedirectToAction("CreateType", "Okna");
            }
            ViewBag.List = p.GetTypes();
            ViewBag.Units = new SelectList(new Units().Get(), "id", "name");
            return View(project);
        }

        public ActionResult CreateMontazh()
        {
            var p = new Okno();            
            return View(p.GetMontazh());
        }

        [HttpPost]
        public ActionResult CreateMontazh(oknaMontazhCostModel project)
        {
            var p = new Okno();
            if (ModelState.IsValid)
            {
                p.EdditMontazh(project);
                return RedirectToAction("CreateMontazh", "Okna");
            }            
            return View(project);
        }
    }
}
