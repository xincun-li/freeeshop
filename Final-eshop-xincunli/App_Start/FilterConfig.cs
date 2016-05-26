using System.Web;
using System.Web.Mvc;

namespace Final_eshop_xincunli
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
