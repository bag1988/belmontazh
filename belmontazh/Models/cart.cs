using belmontazh.Models.Repositor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace belmontazh.Models
{
    public class OrdersCart
    {
        private List<OrdersModel> OrdersCollection = new List<OrdersModel>();

        public void AddItemDveri(int idDveri, double count)
        {
            DveriKomnat d = new DveriKomnat();
            OrdersModel line = OrdersCollection
                .Where(p => p.dveriId == idDveri)
                .FirstOrDefault();

            if (line == null)
            {
                OrdersCollection.Add(new OrdersModel { dveriId = idDveri, countDveri = count, cost = d.Get(idDveri).cost * d.GetIncrement().costDveri, name= d.Get(idDveri).name });
            }
            else
            {
                line.countDveri = count;
            }
        }
        public void AddItemKomplekt(int idKomplekt, double count)
        {
            DveriKomnat d = new DveriKomnat();
            OrdersModel line = OrdersCollection
               .Where(p => p.komplektId == idKomplekt)
               .FirstOrDefault();

            if (line == null)
            {
                OrdersCollection.Add(new OrdersModel { komplektId = idKomplekt, countKomplekt = count, cost = d.GetKomplekt(idKomplekt).cost * d.GetIncrement().costKomplekt, name = d.GetKomplekt(idKomplekt).name });
            }
            else
            {
                line.countKomplekt = count;
            }

        }

        public void RemoveDveri(int idDveri)
        {
            OrdersCollection.RemoveAll(l => l.dveriId == idDveri);
        }
        public void RemoveKomplekt(int idKomplekt)
        {
            OrdersCollection.RemoveAll(l => l.komplektId == idKomplekt);
        }

        public void Clear()
        {
            OrdersCollection.Clear();
        }

        public IEnumerable<OrdersModel> Lines
        {
            get { return OrdersCollection; }
        }
    }
    public enum SessionKey
    {
        CART
    }

    public static class SessionHelper
    {

        public static void Set(HttpSessionStateBase session, SessionKey key, object value)
        {
            session[Enum.GetName(typeof(SessionKey), key)] = value;
        }

        public static T Get<T>(HttpSessionStateBase session, SessionKey key)
        {
            object dataValue = session[Enum.GetName(typeof(SessionKey), key)];
            if (dataValue != null && dataValue is T)
            {
                return (T)dataValue;
            }
            else
            {
                return default(T);
            }
        }

        public static OrdersCart GetCart(HttpSessionStateBase session)
        {
            OrdersCart myCart = Get<OrdersCart>(session, SessionKey.CART);
            if (myCart == null)
            {
                myCart = new OrdersCart();
                Set(session, SessionKey.CART, myCart);
            }
            return myCart;
        }
    }

    

    public class KomplektForm
    {
        public int id { get; set; }
        public double count { get; set; }
    }

    public class dveriForm
    {
        [Required]
        public int id { get; set; }
        [Required]
        public double count { get; set; }
        public List<KomplektForm> komplekt { get; set; }
    }

    public class viewOrdersTotal
    {
        [DisplayFormat(DataFormatString = "{0:F}")]
        public double count { get; set; }
        [DisplayFormat(DataFormatString = "{0:F}")]
        public double cost { get; set; }
    }
}