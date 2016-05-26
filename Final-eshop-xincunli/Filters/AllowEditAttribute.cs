using System;
using System.Web.Mvc;
using Final_eshop_xincunli.Models;

namespace Final_eshop_xincunli.Filters
{
    public abstract class AllowEditAttribute:AuthorizeAttribute
    {
        private string compareEmail = "";
        protected ShopContext db = new ShopContext();

        protected abstract string GetCompareEmail(int id);

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            base.OnAuthorization(filterContext);

            if (filterContext.RouteData.Values["id"] != null && filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                int id = Int32.Parse(filterContext.RouteData.Values["id"].ToString());
                
                string compareEmail = GetCompareEmail(id);
                if (!String.IsNullOrEmpty(compareEmail))
                {
                    if (compareEmail != filterContext.HttpContext.User.Identity.Name)
                    {
                        UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                        filterContext.Result = new RedirectResult(urlHelper.Action("NotAuthorization", "Error"));
                    }
                }
            }
        }
    }
}