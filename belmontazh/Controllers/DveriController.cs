using belmontazh.Models;
using belmontazh.Models.Repositor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace belmontazh.Controllers
{
    public class DveriController : Controller
    {
        private static string sortDveri;
        public ActionResult mejkomnatnie(string material="", int page=1, string name="", string sort="")
        {                      
            int pageSize = 32;
            if (sort != "")
                sortDveri = sort;
            if (sortDveri == "")
                sortDveri = "popular";     
            var p = new DveriKomnat();
            var dveri = p.GetDveri(page, pageSize, material, 1, sortDveri, name);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = dveri.PageCount };

            ViewBag.PageInfo = pageInfo;
            double incrementKomplekt = p.GetIncrement().costKomplekt;
            dveri.dveri.ForEach(x => x.cost = x.cost * p.GetIncrement().costDveri + p.GetKomplektDveri(x.id).Sum(k => k.cost * k.defaultValue) * incrementKomplekt);
            ViewBag.Sort = sortDveri;
            return View(dveri.dveri.ToList());
        }

        public ActionResult vhodnye(string proiz="", int page=1, string name = "", string sort = "")
        {
            
            int pageSize = 32;
            if (sort != "")
                sortDveri = sort;
            if (sortDveri == "")
                sortDveri = "popular";
            var p = new DveriKomnat();
            var dveri = p.GetDveriV(page, pageSize, proiz, 2, sortDveri, name);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = dveri.PageCount };
            ViewBag.Sort = sortDveri;
            ViewBag.PageInfo = pageInfo;
            dveri.dveri.ForEach(x => x.cost = x.cost * p.GetIncrement().costDveri);
            return View(dveri.dveri.ToList());
        }

        [Route("Dveri/Detalis/{name=all}")]
        public ActionResult Detalis(string name)
        {
            if (name == "all")
                return RedirectToAction("mejkomnatnie", "Dveri");
            var p = new DveriKomnat();
            DveriKomnatModel d = new DveriKomnatModel();
            
            d = p.Get(name);
            p.SaveView(d.id);
            d.cost = d.cost * p.GetIncrement().costDveri;

            if (d.kategoriModelid==1)
            {
                List<dveriKomplektModel> komplekt = new List<dveriKomplektModel>();
                List<propertyDveriModel> prop = new List<propertyDveriModel>();
                using (var dbContext = new belmontazhBase())
                {
                    komplekt = dbContext.Mouldings.Where(x => x.DveriKomnatModelid == d.id).Select(x => x.dveriKomplektModel).ToList();
                    prop = dbContext.propertyDveri.ToList();
                }
                d.valueProperty.ToList().ForEach(x => x.propertyDveriModel = prop.First(t => t.id == x.propertyDveriModelid));

                komplekt.ForEach(x => x.cost = x.cost * p.GetIncrement().costKomplekt);
                ViewBag.Komplekt = komplekt;
            }
            return View(d);
        }

        [ChildActionOnly]
        public ActionResult leftmenu()
        {
            leftMenuDveri menu = new leftMenuDveri();
            using (var dbContext = new belmontazhBase())
            {
                menu.proiz = dbContext.DveriKomnat.Include("ProizvoditelModel").Where(x => x.kategoriModelid == 2).GroupBy(x => x.ProizvoditelModel).Select(x => x.Key).ToList();
                menu.material = dbContext.DveriKomnat.Include("materialDveriModel").Where(x => x.kategoriModelid == 1).GroupBy(x => x.materialDveriModel).Select(x => x.Key).ToList();
            }
            return PartialView("leftmenu", menu);
        }
        [ChildActionOnly]
        public ActionResult Sort()
        {
            ViewBag.Sort = sortDveri;
            return PartialView("Sort");
        }


    }
}