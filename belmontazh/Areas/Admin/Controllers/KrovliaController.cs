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
    public class KrovliaController : Controller
    {
        // GET: Admin/Krovlia
        public ActionResult Index()
        {
            var p = new Krovlia();
            ViewBag.Kategories = p.GetKategories();
            ViewBag.Property = p.GetTypes();
            ViewBag.Units = new Units().Get();
            return View(p.GetByKategories().ToList());
        }

        public ActionResult Create()
        {
            var p = new Krovlia();
            ViewBag.Kategories = p.GetKategories();
            ViewBag.Property = p.GetTypes();
            ViewBag.Units = new Units().Get();
            return View();
        }

        [HttpPost]
        public ActionResult Create(krovliaModel project)
        {
            var p = new Krovlia();
            if (ModelState.IsValid)
            {
                p.Save(project);
                return RedirectToAction("Index", "Krovlia");
            }
            ViewBag.Kategories = p.GetKategories();
            ViewBag.Property = p.GetTypes();
            ViewBag.Units = new Units().Get();
            return View(project);
        }


        public ActionResult Edit(int id)
        {
            var p = new Krovlia();
            ViewBag.Kategories = p.GetKategories();
            ViewBag.Property = p.GetTypes();
            ViewBag.Units = new Units().Get();
            return View(p.Get(id));
        }


        [HttpPost]
        public ActionResult Edit(krovliaModel project)
        {
            var p = new Krovlia();
            if (ModelState.IsValid)
            {
                p.Eddit(project);
                return RedirectToAction("Index", "Krovlia");
            }
            ViewBag.Kategories = p.GetKategories();
            ViewBag.Property = p.GetTypes();
            ViewBag.Units = new Units().Get();
            return View(project);
        }


        public ActionResult Delete(int id)
        {
            var p = new Krovlia();
            return View(p.Get(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, krovliaModel project)
        {
            var p = new Krovlia();
            p.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: Admin/Dveri/Create
        public ActionResult CreateProperty()
        {
            ViewBag.List = new Krovlia().GetTypes();
            return View();
        }
        public ActionResult DeleteProperty(int id)
        {
            var p = new Krovlia();
            p.DeleteTypes(id);
            return RedirectToAction("CreateProperty");
        }
        [HttpPost]
        public ActionResult CreateProperty(krovliaTypeModel project)
        {
            var p = new Krovlia();
            if (ModelState.IsValid)
            {
                if (project.id != 0)
                {
                    p.EdditTypes(project);
                }
                else
                {
                    p.SaveTypes(project);
                }
                return RedirectToAction("CreateProperty", "Krovlia");
            }
            ViewBag.List = p.GetTypes();
            return View(project);
        }

        public ActionResult CreateKategories()
        {
            ViewBag.List = new Krovlia().GetKategories();
            return View();
        }
        public ActionResult DeleteKategories(int id)
        {
            var p = new Krovlia();
            p.DeleteKategori(id);
            return RedirectToAction("CreateKategories");
        }
        [HttpPost]
        public ActionResult CreateKategories(krovliaKategoriesModel project)
        {
            var p = new Krovlia();
            if (ModelState.IsValid)
            {
                if (project.id != 0)
                {
                    p.EdditKategori(project);
                }
                else
                {
                    p.SaveKategori(project);
                }
                
                return RedirectToAction("CreateKategories", "Krovlia");
            }
            ViewBag.List = p.GetKategories();
            return View(project);
        }

        public ActionResult valuePropertyResult()
        {
            ViewBag.Property = new Krovlia().GetTypes();
            return PartialView("valuePropertyResult");
        }
    }
}