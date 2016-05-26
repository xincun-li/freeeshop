using System.Collections.Generic;
using System.Linq;
namespace Final_eshop_xincunli.Models
{
    public enum OrderSort
    {
        ByOrderIdHightToLow,
        ByOrderIdLowToHight,
        ByDateHightToLow,
        ByDateLowToHight
    }

    public enum ProductSort
    {
        ByNameHightToLow,
        ByNameLowToHight,
        ByDateHightToLow,
        ByDateLowToHight,
        ByPriceHightToLow,
        ByPriceLowToHight
    }

    public static class CollectionExtension
    {
        public static IOrderedEnumerable<Product> Sort(this IEnumerable<Product> products, ProductSort sort)
        {
            switch (sort)
            {
                case ProductSort.ByNameHightToLow:
                    return products.OrderByDescending(p => p.ProductName);
                case ProductSort.ByNameLowToHight:
                    return products.OrderBy(p => p.ProductName);
                case ProductSort.ByDateHightToLow:
                    return products.OrderByDescending(p => p.CreateDate);
                case ProductSort.ByDateLowToHight:
                    return products.OrderBy(p => p.CreateDate);
                case ProductSort.ByPriceHightToLow:
                    return products.OrderByDescending(p => p.ProductPrice);
                case ProductSort.ByPriceLowToHight:
                    return products.OrderBy(p => p.ProductPrice);
                default:
                    return products.OrderBy(p => p.ProductName);
            }
        }

        public static IOrderedEnumerable<OrderSummary> Sort(this IEnumerable<OrderSummary> orders, OrderSort sort)
        {
            switch (sort)
            {
                case OrderSort.ByDateHightToLow:
                    return orders.OrderByDescending(order => order.OrderDate);
                case OrderSort.ByDateLowToHight:
                    return orders.OrderBy(order => order.OrderDate);
                case OrderSort.ByOrderIdHightToLow:
                    return orders.OrderByDescending(order => order.Id);
                case OrderSort.ByOrderIdLowToHight:
                    return orders.OrderBy(order => order.Id);
                default:
                    return orders.OrderByDescending(order => order.OrderDate);
            }
        }

        public static int AtWhere<T>(this IEnumerable<T> items, T findItem) where T : class
        {
            int i = 0;
            foreach (var item in items)
            {
                if (item == findItem)
                    return i;
                i++;
            }
            return -1;
        }


    }
}