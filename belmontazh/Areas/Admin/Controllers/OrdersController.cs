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
    public class OrdersController : Controller
    {        
        public ActionResult Index()
        {
            Orders o = new Orders();
            return View(o.GetContract(1, 50));
        }

        public ActionResult detalis(int id)
        {
            Orders o = new Orders();
            return View(o.GetContract(id));
        }
        [HttpPost]
        public ActionResult detalis(ContractModel project)
        {
            if (ModelState.IsValid)
            {
                Orders o = new Orders();
                o.Eddit(project);
                return RedirectToAction("index", "orders");
            }

            return View(project);
        }

        public ActionResult Done(int id)
        {
            Orders o = new Orders();
            o.Done(id);
            return RedirectToAction("index", "orders");
        }
    }
}