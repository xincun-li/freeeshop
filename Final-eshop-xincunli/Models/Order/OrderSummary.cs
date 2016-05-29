using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_eshop_xincunli.Models
{
    /// <summary>
    /// Order summary
    /// </summary>
    public class OrderSummary
    {
        [Key]
        [DisplayName("OrderId")]
        public int Id { get; set; }

        [Required]
        [DisplayName("OrderDate")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Please input shipping name")]
        [DisplayName("Shipping Name")]
        [MaxLength(40, ErrorMessage = "Shipping Name must be shorter than 40")]
        public string ContactName { get; set; }

        [Required(ErrorMessage="Please input zip code")]
        [DisplayName("Zip Code")]
        [RegularExpression("^\\d{5}$",ErrorMessage="Please input zip code")]
        public int Zipcode { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage="Please input city.")]
        public string City { get; set; }

        [Required]
        [Display(Name = "State")]
        [RegularExpression("[A-Z]{2}")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please input shipping address")]
        [DisplayName("Shipping Address")]
        public string ContactAddress { get; set; }

        [NotMapped]
        [DisplayName("Full address")]
        public string Address 
        {
            get
            {
                return ContactAddress + ", " +  City + State + Zipcode;
            }
        }

        [Required(ErrorMessage = "Please input cell phone")]
        [DisplayName("Cell phone")]
        [MaxLength(11, ErrorMessage = "Shipping Name must be shorter than 11")]
        [DataType(DataType.PhoneNumber)]
        public string ContactPhone { get; set; }
        

        [NotMapped]
        [DisplayName("Total Amount")]
        public int TotalAmount
        {
            get
            {
                if (OrderDetails != null)
                    return OrderDetails.Sum(od => od.Amount);
                else
                    return 0;
            }
        }

        [Required]
        [DisplayName("Total Price")]
        [DataType(DataType.Currency)]
        public double TotalPrice { get; set; }

        [Required]
        [DisplayName("Total Tax")]
        [DataType(DataType.Currency)]
        public double TotalTax { get; set; }

        [Required]
        [DisplayName("Shipping Fee")]
        [DataType(DataType.Currency)]
        public double Shipping { get; set; }

        public virtual Member Member { get; set; }

        [DisplayName("Order Status")]
        public virtual OrderStatus OrderStatus { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}