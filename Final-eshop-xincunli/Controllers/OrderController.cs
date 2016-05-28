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
        public ActionResult CheckOut()
        {
            if (CartItems.Count <= 0)
                return RedirectToAction("Index", "Cart");
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
            var member = db.MemberShips.First(m => m.Email == User.Identity.Name);
            order.OrderDate = DateTime.Now;
            if (TryUpdateModel(order))
            {
                order.OrderDetails = GetOrderDetails(member);
                order.TotalPrice = Math.Round(CartItems.Sum(item => item.Price), 2);
                order.TotalTax = CartItems.Sum(item => item.TaxPrice);
                order.Member = member;
                order.OrderStatus = db.OrderStatuses.First(os => os.Id == 1);
                StockSellOut(order);
                db.Orders.Add(order);
                db.SaveChanges();
                CartItems.Clear();
                Session["TotalCount-" + User.Identity.Name] = 0;
                TempData["OrderId"] = order.Id;
                SendOrderMail(order);

                CheckAndUpgradeMember(order.Member);
                return RedirectToAction("Finish");
            }
            return View("CheckOut");

        }

        /// <summary>
        /// Upgrade member to premium, if they have consumed over than $1000.00
        /// </summary>
        /// <param name="member"></param>
        private void CheckAndUpgradeMember(Member member)
        {
            if (member.Role == Role.Basic)
            {
                double? total = (from order in db.Orders
                                 where order.Member.Id == member.Id
                                 select order.TotalPrice).Sum();
                if (total >= 1000.0)
                {
                    MemberManage.UpgradePremium(member);
                }
            }
        }

        private void StockSellOut(OrderSummary order)
        {
            foreach (var od in order.OrderDetails)
            {
                od.Product.ProductCount -= od.Amount;
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
                smtpSever.Credentials = new System.Net.NetworkCredential("freeeshop.e583@gmail.com", "123abcabc");
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

        private List<OrderDetail> GetOrderDetails(Member member)
        {
            return CartItems.ConvertAll(item =>
            {
                //Only Premium member have discount, otherwise no discount.
                int discount = (member.Role == Role.Premium ? item.Product.Discount : 100);
                double price = Math.Round(item.Price * discount / 100, 2);
                return new OrderDetail
                {
                    Product = db.Products.Find(item.Product.ProductId),
                    Price = price,
                    Discount = discount,
                    TaxPrice = Math.Round(item.Product.Tax * price, 2),
                    Amount = item.Amount
                };
            });
        }

    }
}
