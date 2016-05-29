using System.Linq;
using System.Web.Mvc;
using System.Threading;
using Final_eshop_entities.Models;

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
            CartItem inCartItem = CartItems.FirstOrDefault(item => item.Product.ProductId == id);
            if (inCartItem != null)
            {
                inCartItem.Amount = ++inCartItem.Amount;
            }
            else
            {
                var item = new CartItem
                {
                    Id = CartItems.Count() > 0 ? CartItems.Last().Id + 1 : 1,
                    Product = db.Products.Find(id),
                    Amount = 1
                };
                CartItems.Add(item);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Thread.Sleep(1000);
            CartItem inCartItem = CartItems.FirstOrDefault(item => item.Id == id);
            if (inCartItem != null)
            {
                CartItems.Remove(inCartItem);

                return RedirectToAction("Index");
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
            ViewData["CartCount"] = CartItems.Sum(o => o.Amount);
            return PartialView("CartSummary");
        }
    }
}
