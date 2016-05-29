using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Final_eshop_entities.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }

        [DisplayName("Order Status")]
        [Required(ErrorMessage="Please select order status")]
        public string Name { get; set; }

        public virtual ICollection<OrderSummary> Orders { get; set; }
    }
}