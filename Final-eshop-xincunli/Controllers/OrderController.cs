using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Net.Mail;
using Final_eshop_xincunli.Models;
using Final_eshop_xincunli.Filters;
using Final_eshop_xincunli.Models.ViewModel;

namespace Final_eshop_xincunli.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {

        [HttpGet]
        public ActionResult Detail(int id)
        {
            OrderSummary order=db.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) RedirectToAction("OrderHistory", "Member");

            return View(order);
        }


        public ActionResult OrderStatusFilter(int status=0)
        {
            var selectList = new SelectList(db.OrderStatuses, "Id", "Name",status);
            ViewData["Query"] = "status"; 
            return PartialView("Filter",selectList);
        }

        public ActionResult OrderSortSelect(OrderSort sort = OrderSort.ByDateHightToLow)
        {
            var selectList = new SelectList(GetSortSelectList(), "Value", "Text", sort);
            return PartialView("SortSelect",selectList);
        }

        private IEnumerable<SelectListItem> GetSortSelectList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem(){Text="ByDateHightToLow",Value=OrderSort.ByDateHightToLow.ToString()},
                new SelectListItem(){Text="ByDateLowToHight",Value=OrderSort.ByDateLowToHight.ToString()},
                new SelectListItem(){Text="ByOrderIdHightToLow",Value=OrderSort.ByOrderIdHightToLow.ToString()},
                new SelectListItem(){Text="ByOrderIdLowToHight",Value=OrderSort.ByOrderIdLowToHight.ToString()}
            };
        }
        
        [Administrator]
        public ActionResult OrderStatusSelect(int id)
        {
            System.Threading.Thread.Sleep(1000);
            OrderSummary order = db.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return HttpNotFound();
            var seletList = new SelectList(db.OrderStatuses, "Id", "Name", order.OrderStatus.Id);
            return PartialView(seletList);
        }

        [HttpPost]
        [Administrator]
        public ActionResult UpdateOrderStauts(int id,int status)
        {
            System.Threading.Thread.Sleep(1000);
            OrderSummary order = db.Orders.FirstOrDefault(o => o.Id == id);
            OrderStatus orderStatus = db.OrderStatuses.FirstOrDefault(os => os.Id == status);
            if (order == null || orderStatus==null) return HttpNotFound();
            order.OrderStatus = orderStatus;
            db.SaveChanges();
            return Content(order.OrderStatus.Name);
        }

        [HttpPost]
        [Administrator]
        public ActionResult Delete(int id)
        {
            System.Threading.Thread.Sleep(1000);
            try
            {
                OrderSummary order = db.Orders.FirstOrDefault(o => o.Id == id);
                if (order == null) return HttpNotFound();
                db.Orders.Remove(order);
                db.SaveChanges();
                TempData["deletedName"] = order.Id;
                return new HttpStatusCodeResult(200, "Delete successfully");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, "Delete failed");
            }
        }

        [HttpGet]
        public ActionResult CheckOut()
        {
            if (CartItems.Count <= 0)
                return RedirectToAction("Index","Cart");
            return View();
        }


        [HttpPost]
        public ActionResult CheckOut(FormCollection form)
        {
            var order = new OrderSummary();
            order.OrderDate = DateTime.Now;
            if (TryUpdateModel(order))
            {
                var orderViewModel = new OrderConfirmViewModel
                {
                    Order = order,
                    CartItems = this.CartItems
                };
                return View("Confirm", orderViewModel);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Confirm(FormCollection form)
        {
            if (CartItems.Count <= 0)
                return RedirectToAction("Index", "Cart");
            var order = new OrderSummary();
            order.OrderDate = DateTime.Now;

            if (TryUpdateModel(order))
            {
                var orderViewModel = new OrderConfirmViewModel
                {
                    Order = order,
                    CartItems = this.CartItems
                };
                return View(orderViewModel);
            }
            return View("CheckOut");

        }

        [HttpPost]
        public ActionResult Finish(FormCollection form)
        {
            if (CartItems.Count <= 0)
                return RedirectToAction("Index", "Cart");
            var order = new OrderSummary();
            order.OrderDate = DateTime.Now;
            if (TryUpdateModel(order))
            {
                order.OrderDetails = GetOrderDetails();
                order.TotalPrice = CartItems.Sum(item => item.Price);
                order.Member = db.MemberShips.First(m => m.Email == User.Identity.Name);
                order.OrderStatus = db.OrderStatuses.First(os => os.Id == 1);
                StockSellOut(order);
                db.Orders.Add(order);
                db.SaveChanges();
                CartItems.Clear();
                TempData["OrderId"] = order.Id;
                //SendOrderMail(order);
                return RedirectToAction("Finish");
            }
            return View("CheckOut");

        }

        private void StockSellOut(OrderSummary order)
        {
            foreach (var od in order.OrderDetails)
            {
                od.product.ProductCount -= od.Amount;
            }
        }

        [HttpGet]
        public ActionResult Finish()
        {
            if (TempData["OrderId"] != null)
            {
                ViewData["OrderId"] = TempData["OrderId"];
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        private bool SendOrderMail(OrderSummary order)
        {
            try
            {
                string mailBody = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/FinishedOrderMail.html"));
                mailBody = mailBody.Replace("{{OrderId}}", order.Id.ToString());
                mailBody = mailBody.Replace("{{ContactName}}", order.ContactName);
                mailBody = mailBody.Replace("{{ContactAddress}}", order.Zipcode +
                    order.City + order.State + order.ContactAddress);
                mailBody = mailBody.Replace("{{ContactPhone}}", order.ContactPhone);
                mailBody = mailBody.Replace("{{OrderDate}}", order.OrderDate.ToShortDateString());
                mailBody = mailBody.Replace("{{Name}}", db.MemberShips.First(m => m.Email == User.Identity.Name).Name);

                var smtpSever = new SmtpClient("smtp.gmail.com");
                smtpSever.Port = 587;
                smtpSever.Credentials = new System.Net.NetworkCredential("exile1030@gmail.com", "exile0204");
                smtpSever.EnableSsl = true;
                var mailMsg = new MailMessage
                {
                    From = new MailAddress("exile1030@gmail.com"),
                    Subject = "(FreeShop)Thanks for order our product.",
                    Body = mailBody,
                    IsBodyHtml = true
                };
                mailMsg.To.Add(new MailAddress(User.Identity.Name));

                smtpSever.Send(mailMsg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

          
        }

        private List<OrderDetail> GetOrderDetails()
        {
            return CartItems.ConvertAll(item =>
            {
                return new OrderDetail
                {
                    product=db.Products.Find(item.product.ProductId),
                    Price=item.Price,
                    Amount=item.Amount
                };
            });
        }

    }
}
