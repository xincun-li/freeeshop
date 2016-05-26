using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Final_eshop_xincunli.Models
{
    public class OrderDetail:IProduct
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [DisplayName("Price")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Required]
        [DisplayName("Amount")]
        public int Amount { get; set; }

        [Required]
        public virtual Product product { get; set; }

        public virtual OrderSummary orderSummary { get; set; }
        
    }
}