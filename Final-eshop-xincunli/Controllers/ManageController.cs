using System;
using System.Web.Mvc;
using Final_eshop_entities.Models;
using Final_eshop_xincunli.Filters;

namespace Final_eshop_xincunli.Controllers
{

    [Administrator]
    public class ManageController : BaseController
    {
        public ActionResult Product()
        {
            ViewBag.Title = "Manage Product";

            return View();
        }      
        

        [HttpGet]
        [OutputCache(CacheProfile = "StaticProduct")]
        public JsonResult GetProducts(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total = 0;
            var records = new object();
            try
            {
                records = ProductManage.GetAllProducts(page, limit, sortBy, direction, searchString, out total);
            }
            catch (Exception e)
            {
                records = "error:" + e.Message;
            }
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
            
        }
    }
}
