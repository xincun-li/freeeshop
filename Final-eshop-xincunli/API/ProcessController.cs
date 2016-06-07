using Final_eshop_xincunli.Filters;
using Final_eshop_entities.Models;
using Final_eshop_entities.Models.DTO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Final_eshop_xincunli.API
{
    [Administrator]
    public class ProcessController : ApiController
    {
        /// <summary>
        /// Process Order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(OrderDTO order)
        {
            OrderManage.UpdateOrder(order);
            return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Has updated." });
        }
    }
}
