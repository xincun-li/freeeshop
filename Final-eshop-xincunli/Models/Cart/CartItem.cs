using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Final_eshop_xincunli.Models
{
    public class CartItem : IProduct
    {

        public int Id { get; set; }

        [DisplayName("Product")]
        public Product Product { get; set; }

        [DisplayName("Amount")]
        [Range(1, 999, ErrorMessage = "Amount must be range in 1-999")]
        public int Amount { get; set; }

        [DataType(DataType.Currency)]
        [DisplayName("Price")]
        public double Price
        {
            get
            {
                if (Product != null)
                {
                    return Product.ProductPrice * Amount;
                }
                else
                {
                    return 0;
                }

            }
            set
            {

            }
        }

        [DataType(DataType.Currency)]
        [DisplayName("Tax Price")]
        public double TaxPrice
        {
            get
            {
                if (Product != null)
                {
                    return Math.Round(Product.ProductPrice * Product.Tax / 100, 2);
                }
                else
                {
                    return 0;
                }

            }
            set
            {

            }
        }

        [DataType(DataType.Currency)]
        [DisplayName("Shipping Fee")]
        public double Shipping
        {
            get
            {
                if (Product != null)
                {
                    return Product.Shipping;
                }
                else
                {
                    return 0;
                }

            }
            set
            {

            }
        }
    }
}