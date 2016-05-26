using System.Linq;
using System.Web.Mvc;
using Final_eshop_xincunli.Models;

namespace Final_eshop_xincunli.Filters
{
    public class AdministratorAttribute:AuthorizeAttribute
    {
        ShopContext db = new ShopContext();
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated ||
                !isAdiministrator(filterContext.HttpContext.User.Identity.Name))
            {
                UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                filterContext.Result = new RedirectResult(urlHelper.Action("NotAuthorization", "Error"));
            }
        }
        private bool isAdiministrator(string email)
        {
            return db.MemberShips.First(member => member.Email == email).Role==Role.Admin;
        }
    }
}