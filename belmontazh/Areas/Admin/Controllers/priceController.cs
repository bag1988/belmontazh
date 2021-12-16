using belmontazh.Areas.Admin.Models;
using belmontazh.Models;
using belmontazh.Models.Repositor;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace belmontazh.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class priceController : Controller
    {
        // GET: Admin/price
        public ActionResult Index()
        {
            var p = new Price();
            return View(p.Get().ToList());
        }
        // GET: Admin/price/Create
        public ActionResult Create()
        {
            var p = new Price();
            ViewBag.Kategori = new SelectList(p.GetKategories(), "id", "name");
            ViewBag.Units = new SelectList(new Units().Get(), "id", "name");
            return View();
        }

        // POST: Admin/price/Create
        [HttpPost]
        public ActionResult Create(PriceModel project)
        {
            var p = new Price();
            if (ModelState.IsValid)
            {
                p.Save(project);
                return RedirectToAction("Index", "Price");
            }
            ViewBag.Kategori = new SelectList(p.GetKategories(), "id", "name");
            ViewBag.Units = new SelectList(new Units().Get(), "id", "name");
            return View(project);
        }

        // GET: Admin/price/Edit/5
        public ActionResult Edit(int id)
        {
            var p = new Price();
            ViewBag.Kategori = new SelectList(p.GetKategories(), "id", "name");
            ViewBag.Units = new SelectList(new Units().Get(), "id", "name");
            return View(p.Get(id));
        }

        // POST: Admin/price/Edit/5
        [HttpPost]
        public ActionResult Edit(PriceModel project)
        {
            var p = new Price();
            if (ModelState.IsValid)
            {
                p.Eddit(project);
                return RedirectToAction("Index", "Price");
            }
            ViewBag.Kategori = new SelectList(p.GetKategories(), "id", "name");
            ViewBag.Units = new SelectList(new Units().Get(), "id", "name");
            return View(project);
        }

        // GET: Admin/price/Delete/5
        public ActionResult Delete(int id)
        {
            var p = new Price();
            return View(p.Get(id));
        }

        // POST: Admin/price/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, PriceModel project)
        {
            var p = new Price();
            p.Delete(id);
            return RedirectToAction("Index");
        }


        public ActionResult CreateKategori()
        {
            var p = new Price();
            ViewBag.List = p.GetKategories();
            return View();
        }
        public ActionResult DeleteKategori(int id)
        {
            var p = new Price();
            p.DeleteKategori(id);
            return RedirectToAction("CreateKategori");
        }
        [HttpPost]
        public ActionResult CreateKategori(kategoriPriceModel project)
        {
            var p = new Price();
            if (ModelState.IsValid)
            {
                if (project.id != 0)
                {
                    project.nameEn = project.name.ToTranslit();
                    p.EdditKategori(project);
                }
                else
                {
                    project.nameEn = project.name.ToTranslit();
                    p.SaveKategori(project);
                }
                
                return RedirectToAction("CreateKategori", "Price");
            }
            ViewBag.List = p.GetKategories();
            return View(project);
        }

        public ActionResult CreateUnits()
        {
            var p = new Units();
            ViewBag.List = p.Get();
            return View();
        }
        public ActionResult DeleteUnits(int id)
        {
            var p = new Units();
            p.Delete(id);
            return RedirectToAction("CreateUnits");
        }
        [HttpPost]
        public ActionResult CreateUnits(unitsModel project)
        {
            var p = new Units();
            if (ModelState.IsValid)
            {
                if (project.id != 0)
                {
                    p.Eddit(project);
                }
                else
                {
                    p.Save(project);
                }
                
                return RedirectToAction("CreateUnits", "Price");
            }
            ViewBag.List = p.Get();
            return View(project);
        }
    }
}
