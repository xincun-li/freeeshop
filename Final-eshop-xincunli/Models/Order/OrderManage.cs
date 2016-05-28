using Final_eshop_xincunli.Models;
using Final_eshop_xincunli.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_eshop_xincunli.Models
{
    public class OrderManage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<OrderDTO> OrderHistory(int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {
            using (var db = new ShopContext())
            {
                int mid = MemberManage.Get(HttpContext.Current.User.Identity.Name).Id;
                //total = db.Orders.Count(o => o.Member.Id == mid);
                var records = (from u in db.Orders
                               where u.Member.Id == mid
                               select new OrderDTO
                               {
                                   OrderId = u.Id,
                                   ContactName = u.ContactName,
                                   ContactAddress = u.ContactAddress,
                                   ContactPhone = u.ContactPhone,
                                   Status = u.OrderStatus.Name,
                                   OrderDate = u.OrderDate
                               }).AsQueryable();


                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    records = records.Where(p => p.ContactName.Contains(searchString) ||
                    p.ContactAddress.Contains(searchString) ||
                    p.ContactPhone.Contains(searchString)
                    );
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
                    records = SortHelper.OrderByDescending(records, "OrderId");
                }

                if (page.HasValue && limit.HasValue)
                {
                    int start = (page.Value - 1) * limit.Value;
                    records = records.Skip(start).Take(limit.Value);
                }
                total = records.Count();
                return records.Select(u => new OrderDTO
                {
                    OrderId = u.OrderId,
                    ContactName = u.ContactName,
                    ContactAddress = u.ContactAddress,
                    ContactPhone = u.ContactPhone,
                    Status = u.Status,
                    OrderDate = u.OrderDate
                }).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<OrderDTO> OrderSeller(int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {
            using (var db = new ShopContext())
            {
                int mid = MemberManage.Get(HttpContext.Current.User.Identity.Name).Id;
                

                var records = (from item in db.Orders
                              from subItem in item.OrderDetails
                              where subItem.Product.SellerId == mid
                              select new OrderDTO
                              {
                                  OrderId = item.Id,
                                  ContactName = item.ContactName,
                                  ContactAddress = item.ContactAddress,
                                  ContactPhone = item.ContactPhone,
                                  Status = item.OrderStatus.Name,
                                  OrderDate = item.OrderDate
                              }).Distinct().AsQueryable();

                //var records = (from u in db.OrderDetails
                //               where u.product.SellerId == mid
                //               select new OrderDTO
                //               {
                //                   OrderId = u.orderSummary.Id,
                //                   ContactName = u.orderSummary.ContactName,
                //                   ContactAddress = u.orderSummary.ContactAddress,
                //                   ContactPhone = u.orderSummary.ContactPhone,
                //                   Status = u.orderSummary.OrderStatus.Name,
                //                   OrderDate = u.orderSummary.OrderDate
                //               }).AsQueryable();
                

                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    records = records.Where(p => p.ContactName.Contains(searchString) ||
                    p.ContactAddress.Contains(searchString) ||
                    p.ContactPhone.Contains(searchString)
                    );
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
                    records = SortHelper.OrderByDescending(records, "OrderId");
                }

                if (page.HasValue && limit.HasValue)
                {
                    int start = (page.Value - 1) * limit.Value;
                    records = records.Skip(start).Take(limit.Value);
                }
                total = records.Count();
                return records.Select(u => new OrderDTO
                {
                    OrderId = u.OrderId,
                    ContactName = u.ContactName,
                    ContactAddress = u.ContactAddress,
                    ContactPhone = u.ContactPhone,
                    Status = u.Status,
                    OrderDate = u.OrderDate
                }).ToList();
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public static bool UpdateOrder(OrderDTO o)
        {
            if (o == null)
            {
                throw new ArgumentNullException("Can't match a product.");
            }
            using (var db = new ShopContext())
            {

                var order = (from u in db.Orders
                               where u.Id == o.OrderId
                               select u).FirstOrDefault();
                if (order == null)
                {
                    return false;
                }
                db.Orders.Attach(order);
                var entry = db.Entry(order);
                entry.Entity.OrderStatus = db.OrderStatuses.First(os => os.Name == o.Status);
                db.SaveChanges();

                return true;
            }
        }
    }
}