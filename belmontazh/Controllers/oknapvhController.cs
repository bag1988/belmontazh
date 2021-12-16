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
    public class oknapvhController : Controller
    {
        // GET: oknapvh
        public ActionResult Index()
        {
            return View();
        }
          
        public ActionResult dveriPVH()
        {
            return View();
        }          
       
        public ActionResult Calculator()
        {
            var p = new Okno();
            ViewBag.list = p.Get();
            ViewBag.imgUrl = p.GetTypes(1).urlImage;
            return View();
        }

        [HttpPost]
        public JsonResult Calculator(oknaForm project)
        {
            var p = new Okno();
            if (ModelState.IsValid)
            {
                double width = 0.0, height = 0.0, count = 0, col = 0;
                double cteklo = 0.0, prof = 0.0, hardware = 0.0, types = 0.0;
                double otliv = 0.0, otkos = 0.0, podokonik = 0.0, moskit = 0.0, montazh = 0.0, itog = 0.0;
                width = (float)project.width / 1000;
                height = (float)project.height / 1000;

                col = p.GetTypes(project.oknaTypeModel).col;
                count = p.GetTypes(project.oknaTypeModel).count;
                types = p.GetTypes(project.oknaTypeModel).cost;
                cteklo = p.GetCteklo(project.oknaCtekloModel).cost;
                prof = p.GetProf(project.oknaProfModel).cost;
                hardware = p.GetHardware(project.oknaHardwareModel).cost;
                double cost = Math.Ceiling((width * 2 + height * (col + 1)) * prof + (width / col * 2 + height * 2) * count * prof + width * height * cteklo + hardware * count) * types;

                if (project.otliv) otliv = width * 0.15 * p.GetMontazh().otliv;
                if (project.podokonik) podokonik = width * 0.25 * p.GetMontazh().podokonik;
                if (project.otkos) otkos = (width + height * 2) * p.GetMontazh().otkos;
                if (project.moskit) moskit = p.GetMontazh().moskit;
                if (project.montazh) { montazh = width * height * p.GetMontazh().montazh; if (montazh < 30) montazh = 30; }

                

                itog = cost + otliv + podokonik + otkos + moskit + montazh;
                
                
                Dictionary<string, string> costD = new Dictionary<string, string>();
                costD.Add("cost", cost.ToString("F"));
                costD.Add("otliv", otliv.ToString("F"));
                costD.Add("podokonik", podokonik.ToString("F"));
                costD.Add("otkos", otkos.ToString("F"));
                costD.Add("moskit", moskit.ToString("F"));
                costD.Add("montazh", montazh.ToString("F"));
                costD.Add("itog", itog.ToString("F"));
                return Json(new JavaScriptSerializer().Serialize(costD));
                    
            }
            else
            {
                List<string> errorStr = new List<string>();
                errorStr = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new JavaScriptSerializer().Serialize(errorStr));
            }
            
        }

        [ChildActionOnly]
        public ActionResult accessories()
        {
            return PartialView("accessories");
        }

        [ChildActionOnly]
        public ActionResult glazing()
        {
            return PartialView("glazing");
        }
        [ChildActionOnly]
        public ActionResult oknosteps()
        {
            return PartialView("oknosteps");
        }
        
        [ChildActionOnly]
        public ActionResult sales()
        {
            return PartialView("sales");
        }
        [ChildActionOnly]
        public ActionResult info_client()
        {
            return PartialView("info_client");
        }
        [ChildActionOnly]
        public ActionResult info_edge()
        {
            return PartialView("info_edge");
        }
    }
}