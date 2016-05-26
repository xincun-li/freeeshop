using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using Final_eshop_xincunli.Models;
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


        private IEnumerable<SelectListItem> GetSortSelectList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem(){Text="By product name from low to high",Value=ProductSort.ByNameLowToHight.ToString()},
                new SelectListItem(){Text="By product name from high to low",Value=ProductSort.ByNameHightToLow.ToString()},
                new SelectListItem(){Text="By arrival date from low to high",Value=ProductSort.ByDateLowToHight.ToString()},
                new SelectListItem(){Text="By arrival date from high to low",Value=ProductSort.ByDateHightToLow.ToString()},
                new SelectListItem(){Text="By product price from high to low",Value=ProductSort.ByPriceHightToLow.ToString()},
                new SelectListItem(){Text="By product price from low to high",Value=ProductSort.ByPriceLowToHight.ToString()}
            };
        }


        [HttpGet]
        //[OutputCache(CacheProfile = "StaticProduct")]
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

            //int total;
            //var records = new object();
            //if (System.Web.HttpContext.Current.Cache["StaticProductList"] != null)
            //{
            //    total = (int)System.Web.HttpContext.Current.Cache["total"];
            //    records = System.Web.HttpContext.Current.Cache["StaticProductList"];
            //}
            //else
            //{
            //    records = ProductManage.GetAllProducts(page, limit, sortBy, direction, searchString, out total);


            //    System.Web.HttpContext.Current.Cache.Insert("StaticProductList", records, null,
            //        DateTime.Now.ToUniversalTime().AddMinutes(5),
            //        Cache.NoSlidingExpiration);
            //    System.Web.HttpContext.Current.Cache.Insert("total", total, null,
            //        DateTime.Now.ToUniversalTime().AddMinutes(5),
            //        Cache.NoSlidingExpiration);
            //}
            //return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }
    }
}
