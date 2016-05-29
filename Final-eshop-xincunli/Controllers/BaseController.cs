using System.Collections.Generic;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Final_eshop_xincunli.Models;

namespace Final_eshop_xincunli.Controllers
{
    public class BaseController : Controller
    {
        protected ShopContext db = new ShopContext();
        public List<CartItem> CartItems
        {
            get
            {
                if (Session["CartItems"] == null)
                {
                    Session["CartItems"] = new List<CartItem>();
                }
                return Session["CartItems"] as List<CartItem>;
            }
            set
            {
                CartItems = value;
            }
        }
    }
}
