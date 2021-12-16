using belmontazh.Models;
using belmontazh.Models.Repositor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace belmontazh.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            if (SessionHelper.GetCart(Session).Lines.ToList().Count != 0)
                return View(SessionHelper.GetCart(Session).Lines.ToList());
            else
                return RedirectToAction("mejkomnatnie", "Dveri");
        }

        public ActionResult createOrder()
        {
            if (SessionHelper.GetCart(Session).Lines.ToList().Count != 0)
            {
                ViewBag.Order = SessionHelper.GetCart(Session).Lines.ToList();
                ViewBag.Cost = SessionHelper.GetCart(Session).Lines.ToList().Sum(x => x.cost * x.countDveri + x.cost * x.countKomplekt);
                return View();
            }
            else
                return RedirectToAction("mejkomnatnie", "Dveri");
        }
        [HttpPost]
        public ActionResult createOrder(ContractModel project)
        {
            if (ModelState.IsValid)
            {
                if (SessionHelper.GetCart(Session).Lines.ToList().Count == 0)
                {
                    return RedirectToAction("mejkomnatnie", "Dveri");
                }
                    List<OrdersModel> order = SessionHelper.GetCart(Session).Lines.ToList();
                    Orders o = new Orders();
                    project.Orders = order;
                    o.Save(project);
                    ViewBag.Succses = "Ваш заказ успешно записан!";
                    SessionHelper.GetCart(Session).Clear();
                Email e = new Email();
                string textEmail = "<p>Новый заказ</p><p>Заказчик: <b>"+project.name+"</b></p><p>Телефон: <b>"+project.phone+"</b></p> ";
                e.send_email(textEmail);
                return View();
            }
            ViewBag.Order = SessionHelper.GetCart(Session).Lines.ToList();
            ViewBag.Cost = SessionHelper.GetCart(Session).Lines.ToList().Sum(x => x.cost * x.countDveri + x.cost * x.countKomplekt);
            return View(project);
        }

        public ActionResult viewCart()
        {
            viewOrdersTotal total = new viewOrdersTotal();
            if (SessionHelper.GetCart(Session).Lines == null)
                return PartialView("viewCart", null);
            List<OrdersModel> orderList = SessionHelper.GetCart(Session).Lines.ToList();
            total.count = orderList.Count();
            total.cost = orderList.Sum(x => (x.cost * x.countDveri) + (x.cost * x.countKomplekt));

            return PartialView("viewCart", total);
        }

        public ActionResult deletecart(int idDveri = 0, int idKomplekt = 0)
        {
            if (idDveri != 0)
                SessionHelper.GetCart(Session).RemoveDveri(idDveri);
            if (idKomplekt != 0)
                SessionHelper.GetCart(Session).RemoveKomplekt(idKomplekt);
            return RedirectToAction("index", "cart"); ;
        }
        [HttpPost]
        public void editCartDveri(int idDveri, double count)
        {
            SessionHelper.GetCart(Session).AddItemDveri(idDveri, count);
        }
        [HttpPost]
        public ActionResult editCartKomplekt(int idKomplekt, double count)
        {
            SessionHelper.GetCart(Session).AddItemKomplekt(idKomplekt, count);
            return null;
        }

        [HttpPost]
        public JsonResult addCart(dveriForm project)
        {
            if (ModelState.IsValid)
            {
                DveriKomnat d = new DveriKomnat();

                if (d.Get(project.id) != null)
                {
                    SessionHelper.GetCart(Session).AddItemDveri(project.id, project.count);
                    if (project.komplekt != null)
                    {
                        foreach (KomplektForm k in project.komplekt)
                        {
                            if (d.GetKomplekt(k.id) != null)
                            {
                                SessionHelper.GetCart(Session).AddItemKomplekt(k.id, k.count);
                            }
                        }
                    }
                }
            }
            return null;
        }


    }
}