using Final_eshop_entities.Models;
using System.Web.Mvc;

namespace Final_eshop_xincunli.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(CacheProfile = "StaticProduct")]
        public ActionResult Index()
        {
            ViewBag.Title = "Welcome to free eshop platform.";

            return View(ProductManage.GetAllProducts());
            //return View();
        }
    }
}
