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
                if (Session["CartItems-" + User.Identity.Name] == null)
                {
                    Session["CartItems-" + User.Identity.Name] = new List<CartItem>();
                }
                return Session["CartItems-" + User.Identity.Name] as List<CartItem>;
            }
            set
            {
                CartItems = value;
            }
        }

        public int TotalCount
        {
            get
            {
                if (Session["TotalCount-" + User.Identity.Name] == null)
                {
                    Session["TotalCount-" + User.Identity.Name] = 0;
                }
                return int.Parse(Session["TotalCount-" + User.Identity.Name].ToString());
            }
        }

        protected bool isPicture(string fileName)
        {
            string extensionName = fileName.Substring(fileName.LastIndexOf('.') + 1);
            var reg = new Regex("^(gif|png|jpg|bmp)$", RegexOptions.IgnoreCase);
            return reg.IsMatch(extensionName);
        }

    }
}
