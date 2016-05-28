using Final_eshop_xincunli.Models.DTO;
using Final_eshop_xincunli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// 
/// </summary>
namespace Final_eshop_xincunli.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductManage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static void Save(ProductDTO p)
        {
            if (p.ProductId > 0)
            {
                Update(p);
            }
            else
            {
                using (var db = new ShopContext())
                {
                    Product product = db.Products.Create();
                    product.ProductName = p.ProductName;
                    product.Category = p.Category;
                    product.ProductPrice = p.ProductPrice;
                    product.ProductCount = p.ProductCount;
                    product.ProductSEOName = p.ProductSEOName;
                    product.Discount = p.Discount;
                    product.Tax = p.Tax;
                    product.SellerId = db.MemberShips.First(m => m.Email == HttpContext.Current.User.Identity.Name).Id;//MemberManage.Get(HttpContext.Current.User.Identity.Name).Id;
                    db.Products.Add(product);
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Product> GetAllProducts()
        {
            using (var db = new ShopContext())
            {
                var query = from u in db.Products
                            orderby u.ProductId ascending
                            select u;

                return new List<Product>(query);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<ProductDTO> GetAllProducts(int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {
            using (var db = new ShopContext())
            {
                //total = db.Products.Count();
                var records = (from u in db.Products
                               select new ProductDTO
                               {
                                   ProductId = u.ProductId,
                                   ProductName = u.ProductName,
                                   ProductSEOName = u.ProductSEOName,
                                   Category = u.Category,
                                   ProductPrice = u.ProductPrice,
                                   ProductCount = u.ProductCount,
                                   Discount = u.Discount,
                                   Tax = u.Tax
                               }).AsQueryable();
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    records = records.Where(p => p.Category.Contains(searchString));
                }

                if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
                {
                    if (direction.Trim().ToLower() == "asc")
                    {
                        records = SortHelper.OrderBy(records, sortBy);
                    }
                    else
                    {
                        records = SortHelper.OrderByDescending(records, sortBy);
                    }
                }
                else
                {
                    records = SortHelper.OrderByDescending(records, "ProductId");
                }

                if (page.HasValue && limit.HasValue)
                {
                    int start = (page.Value - 1) * limit.Value;
                    records = records.Skip(start).Take(limit.Value);
                }

                total = records.Count();

                return records.Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductSEOName = p.ProductSEOName,
                    Category = p.Category,
                    ProductPrice = p.ProductPrice,
                    ProductCount = p.ProductCount,
                    Discount = p.Discount,
                    Tax = p.Tax
                }).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static Product Get(int Id)
        {
            using (var db = new ShopContext())
            {
                var product = (from u in db.Products
                               where u.ProductId == Id
                               select u).FirstOrDefault();
                return product;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public static void Remove(int id)
        {
            using (var db = new ShopContext())
            {
                var p = db.Products.Find(id);
                db.Products.Remove(p);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool Update(ProductDTO p)
        {
            if (p == null)
            {
                throw new ArgumentNullException("Can't match a product.");
            }
            using (var db = new ShopContext())
            {

                var product = (from u in db.Products
                               where u.ProductId == p.ProductId
                               select u).FirstOrDefault();
                if (product == null)
                {
                    return false;
                }
                db.Products.Attach(product);
                var entry = db.Entry(product);
                entry.Entity.ProductName = p.ProductName;
                entry.Entity.Category = p.Category;
                entry.Entity.ProductPrice = p.ProductPrice;
                entry.Entity.ProductCount = p.ProductCount;
                db.SaveChanges();

                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool UpdateImagePath(int id, string imagePath)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("Can't match a product.");
            }
            using (var db = new ShopContext())
            {

                var product = (from u in db.Products
                               where u.ProductId == id
                               select u).FirstOrDefault();
                if (product == null)
                {
                    return false;
                }
                db.Products.Attach(product);
                var entry = db.Entry(product);
                entry.Entity.ImagePath = imagePath;
                db.SaveChanges();

                return true;
            }
        }
        /// <summary>
        /// Knuth hash function - From wikipedia
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string CalcHash(string originalString)
        {
            UInt64 hashedValue = 3074457345618258791ul;
            for (int i = 0; i < originalString.Length; i++)
            {
                hashedValue += originalString[i];
                hashedValue *= 3074457345618258799ul;
            }
            return hashedValue.ToString();
        }

        /// <summary>
        /// MD5 Hash
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string md5Hash(string originalString)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(originalString);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}