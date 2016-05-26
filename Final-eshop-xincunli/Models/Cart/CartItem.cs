using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Final_eshop_xincunli.Models
{
    public class CartItem :IProduct
    {

        public int Id { get; set; }

        [DisplayName("Product")]
        public Product product { get; set;}

        [DisplayName("Amount")]
        [Range(1,999,ErrorMessage= "Amount must be range in 1-999")]
        public int Amount { get; set; }

        [DataType(DataType.Currency)]
        [DisplayName("Price")]
        public double Price
        {
            get
            {
                if (product != null)
                {
                    return product.ProductPrice * Amount;
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