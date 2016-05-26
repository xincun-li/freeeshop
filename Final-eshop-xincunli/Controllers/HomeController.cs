using Final_eshop_xincunli.Models;
using System;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace Final_eshop_xincunli.Controllers
{
    public class HomeController : Controller
    {

        [AllowAnonymous]
        public ActionResult List()
        {
            return View(ProductManage.GetAllProducts());
        }

        public ActionResult Index()
        {
            ViewBag.Title = "My Product Store";

            return View(ProductManage.GetAllProducts());
            //return View();
        }
    }
}
