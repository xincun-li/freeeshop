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
    public class ProductController : ApiController
    {
        private string keyAllProduct = "AllProduct";
        private string keyPrefixProductId = "Product-";

        public List<ProductDTO> GetAllProducts()
        {
            if (HttpContext.Current.Cache[keyAllProduct] != null)
            {
                return (List<ProductDTO>)HttpContext.Current.Cache[keyAllProduct];
            }
            List<ProductDTO> list =
            ProductManage.GetAllProducts().Select(u => new ProductDTO
            {
                ProductId = u.ProductId,
                ProductName = u.ProductName,
                ProductSEOName = u.ProductSEOName,
                Category = u.Category,
                ProductPrice = u.ProductPrice,
                ProductCount = u.ProductCount,
                Discount = u.Discount,
                Tax = u.Tax,
                Shipping = u.Shipping,
                Status = u.Status,
                ImagePath = u.ImagePath
            }).ToList();

            HttpContext.Current.Cache.Insert(keyAllProduct, list, null,
                DateTime.Now.ToUniversalTime().AddMinutes(10),
                Cache.NoSlidingExpiration);
            return list;
        }

        public ProductDTO GetProduct(int id)
        {
            if (HttpContext.Current.Cache[keyPrefixProductId + id] != null)
            {
                return (ProductDTO)HttpContext.Current.Cache[keyPrefixProductId + id];
            }

            Product p = ProductManage.Get(id);
            if (p == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            ProductDTO mpd = new ProductDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductSEOName = p.ProductSEOName,
                Category = p.Category,
                Discount = p.Discount,
                Tax = p.Tax,
                Shipping = p.Shipping,
                ProductPrice = p.ProductPrice,
                ProductCount = p.ProductCount,
                Status = p.Status,
                ImagePath = p.ImagePath
            };
            //HttpContext.Current.Cache[keyPrefixProductId + id] = mpd;

            HttpContext.Current.Cache.Insert(keyPrefixProductId + id, mpd, null,
                DateTime.Now.ToUniversalTime().AddMinutes(5),
                Cache.NoSlidingExpiration);
            return mpd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]ProductDTO p)
        {
            ProductManage.Save(p);
            HttpContext.Current.Cache.Remove(keyAllProduct);
            HttpContext.Current.Cache.Remove(keyPrefixProductId + p.ProductId);
            HttpResponse.RemoveOutputCacheItem("/Home/GetProducts");
            return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Has added." });
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Product"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(int id, ProductDTO Product)
        {
            Product.ProductId = id;
            if (!ProductManage.Update(Product))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            HttpContext.Current.Cache.Remove(keyPrefixProductId + id);
            HttpContext.Current.Cache.Remove(keyAllProduct);
            HttpResponse.RemoveOutputCacheItem("/Home/GetProducts");
            return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Has updated." });
        }

        /// <summary>
        /// DELETE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id)
        {
            ProductManage.Remove(id);
            HttpContext.Current.Cache.Remove(keyPrefixProductId + id);
            HttpContext.Current.Cache.Remove(keyAllProduct);
            HttpResponse.RemoveOutputCacheItem("/Home/GetProducts");
            return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Has deleted." });
        }
    }
}
