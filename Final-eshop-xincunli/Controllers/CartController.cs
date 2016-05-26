using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Threading;
using Final_eshop_xincunli.Models;

namespace Final_eshop_xincunli.Controllers
{
    public class CartController : BaseController
    {


        public ActionResult Index()
        {
            return View(CartItems);
        }

        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            CartItem inCartItem=CartItems.FirstOrDefault(item => item.product.ProductId == id);
            if (inCartItem != null)
            {
                inCartItem.Amount = ++inCartItem.Amount;
            }
            else
            {
                var item = new CartItem
                {
                    Id=CartItems.Count()>0?CartItems.Last().Id+1:1,
                    product = db.Products.Find(id),
                    Amount = 1
                };
                CartItems.Add(item);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateAmount(List<CartItem> updateCartItems)
        {
            Thread.Sleep(1000);
            if (ModelState.IsValid)
            {

                foreach (var item in CartItems)
                {
                    var updateCartItem = updateCartItems.FirstOrDefault(newItem => newItem.Id == item.Id);
                    if (updateCartItem != null)
                    {
                        if (updateCartItem.Amount > item.product.ProductCount)
                            item.Amount=item.product.ProductCount;
                        else
                            item.Amount = updateCartItem.Amount;
                    }
                }
                return Json(
                    new { 
                            NewCartItems =CartItems.Select(item => 
                                new { 
                                        Id = item.Id, Amount = item.Amount, 
                                        Price = item.Price.ToString("C") 
                                     }),
                            Total=CartItems.Sum(i=>i.Price).ToString("C")
                         });
            }
            else
            {
                
                return Json(new { Error="Update Failed"});
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Thread.Sleep(1000);
            CartItem inCartItem = CartItems.FirstOrDefault(item => item.Id == id);
            if (inCartItem != null)
            {
                CartItems.Remove(inCartItem);
                return new HttpStatusCodeResult(200, "The product has been removed");
            }
            else
            {
                return HttpNotFound();
            }

        }


        public ActionResult NotItems()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            ViewData["CartCount"] = CartItems.Count;

            return PartialView("CartSummary");
        }
    }
}
