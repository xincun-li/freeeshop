
using Final_eshop_xincunli.Models.ViewModel;
using System.Linq;
using System.Web.Mvc;

namespace Final_eshop_xincunli.Controllers.Shared
{
    public class SharedController :BaseController
    {
        //
        // GET: /Shared/

        public ActionResult Navigater()
        {
            var navigaterViewModel = new NavigaterViewModel();
            navigaterViewModel.Products = db.Products.ToList();
            if (User.Identity.IsAuthenticated)
            {
                navigaterViewModel.Member = db.MemberShips.FirstOrDefault(m => m.Email == User.Identity.Name);
                return PartialView(navigaterViewModel);
            }
            else
            {
                return PartialView(navigaterViewModel);
            }
        }

    }
}
