using Final_eshop_xincunli.Filters;
using Final_eshop_xincunli.Models;
using Final_eshop_xincunli.Models.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using System.Web.Mvc;

namespace Final_eshop_xincunli.API
{
    [Administrator]
    public class ProcessController : ApiController
    {
        /// <summary>
        /// DELETE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(OrderDTO order)
        {
            OrderManage.UpdateOrder(order);
            return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Has deleted." });
        }
    }
}
