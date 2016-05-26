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
    public class UploadController : ApiController
    {
        private string keyAllProduct = "AllProduct";
        private string keyPrefixProductId = "Product-";
        public HttpResponseMessage Post(int id)
        {
            string saveFileName = "";
            //int id = int.Parse(HttpContext.Current.Request["id"]);
            var file = HttpContext.Current.Request.Files[0];
            //for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            //{
                //HttpPostedFile file = HttpContext.Current.Request.Files[i];
                var extension = new FileInfo(file.FileName).Extension;
                saveFileName = Guid.NewGuid().ToString() + extension;

                if (file != null && file.ContentLength > 0)
                {
                    string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Uploads"), saveFileName);

                    file.SaveAs(filePath);

                    ProductManage.UpdateImagePath(id, saveFileName);


                HttpContext.Current.Cache.Remove(keyPrefixProductId + id);
                HttpContext.Current.Cache.Remove(keyAllProduct);
                HttpResponse.RemoveOutputCacheItem("/Home/GetProducts");
            }
            //}

            return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = saveFileName});
        }
    }
}
