using Assignment5_xincunli.Extension;
using Final_eshop_xincunli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
namespace Final_eshop_xincunli.Controllers
{
    public class MemberController : BaseController
    {
        private string salt = "aslkjfoisdiofuhsdnfkjsdigajknakls";
        public ActionResult Index(int id, int p = 1)
        {
            var member = db.MemberShips.Find(id);

            return View(member);
        }


        [Authorize]
        public ActionResult MyAccount()
        {
            return View("Edit", db.MemberShips.First(m => m.Email == User.Identity.Name));
        }

        [Authorize]
        public ActionResult OrderHistory(int p = 1, int status = 0)
        {
            return View(db.MemberShips.First(m => m.Email == User.Identity.Name)
                                  .Orders.Where(o => status == 0 || o.OrderStatus.Id == status)
                                  .OrderByDescending(o => o.OrderDate)
                                  );
        }

        private SelectList GetOrderStatusFilter(string filter)
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem { Value = "All", Text = "All Order" });
            foreach (var item in db.OrderStatuses)
            {
                items.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
            }
            return new SelectList(items, "Value", "Text", filter);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(MemberDTO member)
        {
            if (CheckEmailHasBeUsed(member.Email))
            {
                ModelState.AddModelError(string.Empty, "You input email has registed by other people.");
            }
            if (member.Password.Length < 8)
            {
                ModelState.AddModelError(string.Empty, "Password must be longer than 8 chars.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Encrypt Password
                    member.Password = Utils.Md5Hash(salt + member.Password);
                    member.ConfirmPassword = member.Password;
                    MemberManage.Create(member);
                    //return View("RegisterResult", member);
                    return RedirectToAction("Login", "Member");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex);
                }
            }

            return View();
        }


        private bool CheckEmailHasBeUsed(string Email)
        {
            Member member = db.MemberShips.Where(m => m.Email == Email).FirstOrDefault();
            if (member == null)
                return false;
            else
                return true;
        }




        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string email, string password, string returnUrl)
        {
            if (IsValid(email, password))
            {
                FormsAuthentication.SetAuthCookie(email, false);

                if (String.IsNullOrEmpty(returnUrl))
                    return RedirectToAction("Index", "Home");
                else
                    return Redirect(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Login failed.");
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        private bool IsValid(string email, string password)
        {
            bool IsValid = false;
            Member m = MemberManage.Get(email);
            if (m != null && Utils.Md5Hash(salt + password) == m.Password)
            {
                IsValid = true;
            }
            return IsValid;
        }
    }
}