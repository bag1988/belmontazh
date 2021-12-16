using belmontazh.Models;
using belmontazh.Models.Repositor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace belmontazh.Controllers
{
    public class KrovlyaController : Controller
    {
        // GET: Krovlia
        public ActionResult Index()
        {
            var p = new Krovlia();
            ViewBag.Kategories = p.GetKategories();
            ViewBag.Property = p.GetTypes();
            ViewBag.Units = new Units().Get();
            return View(p.GetByKategories().ToList());
        }
        
        public ActionResult Dvuskatnaya()
        {
            var p = new Krovlia();
            ViewBag.List = p.GetMaterial();
            return View();
        }

        public ActionResult Odnoskatnaya()
        {
            var p = new Krovlia();
            ViewBag.List = p.GetMaterial();
            return View();
        }

        public ActionResult Valmovaya()
        {
            var p = new Krovlia();
            ViewBag.List = p.GetMaterial();
            return View();
        }

        [HttpPost]
        public ActionResult resultCalculate(krovliaCalculate project)
        {
            
            var ras = new racshetKrovlia();
            var p = new Krovlia();            
            if (ModelState.IsValid)
            {
                int typ = 1, lenghtC = 2;
                double a = 0, lenghtStrop = 0, lenghtStropBok = 0;
                a = project.widthA;
                if (project.widthA == 0)
                {
                    lenghtC = 1;
                    typ = 2;
                    a = project.width / 2;
                }

                lenghtStrop = Math.Sqrt(a * a + project.height * project.height) + project.lengthC * lenghtC;
                ras.countStrop = project.length / 0.6 * typ + 1;
                ras.obemStrop = 0.05 * 0.15 * lenghtStrop * ras.countStrop;
                ras.ploshad = lenghtStrop * (project.length + project.lengthC * 2) * typ;
                ras.lenghtStrop = lenghtStrop;
                ras.vesStrop = ras.obemStrop * 600;
                ras.ugol =Math.Atan2(project.height , a)*180/Math.PI;
                ras.radObr = lenghtStrop / 0.35 * typ;
                ras.countObr = (project.length + 1) / 6 * ras.radObr;
                ras.obemObr = ras.countObr * 0.03 * 0.10 * 6;
                ras.vesObr = ras.obemObr * 600;
                if (project.widthTwo > 0)
                {
                    lenghtStropBok = Math.Sqrt(project.widthTwo * project.widthTwo + project.height * project.height) + project.lengthC;
                    ras.ploshad = 0.5 * ((project.length + project.lengthC * 2) + (project.length + project.lengthC * 2 - project.widthTwo * 2)) * lenghtStrop * 2 + 0.5 * (project.width + project.lengthC * 2) * lenghtStropBok * 2;

                    ras.lenghtStropBok = lenghtStropBok;
                    ras.countStropBok = project.width / 0.6 * typ + 1;
                    ras.obemStropBok = 0.05 * 0.15 * lenghtStropBok * ras.countStropBok;
                    ras.vesStrop = ras.vesStrop + ras.obemStropBok * 600;
                    ras.countObr = ras.countObr + (project.width) / 6 * ras.radObr;
                    ras.obemObr = ras.countObr * 0.03 * 0.10 * 6;
                    ras.vesObr = ras.obemObr * 600;
                    ras.ugolBok = Math.Atan2(project.height, project.widthTwo) * 180 / Math.PI;
                    ras.lenghtStropD = Math.Sqrt((project.width + project.lengthC * 2) / 2 * (project.width + project.lengthC * 2) / 2 + lenghtStropBok * lenghtStropBok);
                }
                if (project.widthA > 0)
                    ras.cost = p.GetCost(project.krovMat, 1) * ras.ploshad;
                else
                {
                    if (project.width > 0 && project.widthTwo == 0)
                        ras.cost = p.GetCost(project.krovMat, 2) * ras.ploshad;
                    else
                        ras.cost = p.GetCost(project.krovMat, 3) * ras.ploshad;
                }
                return PartialView(ras);
            }
            else
            {
                Response.Write(string.Join(",", ModelState.Values.SelectMany(m => m.Errors).Select(e => "<br/><b style='color:red;'>" + e.ErrorMessage+"<b>")));
                return null;
            }
        }

    }
}