using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Final_eshop_xincunli.Models
{
    public interface IProduct
    {
        int Id { get; set; }
        Product Product { get; set; }

        [DisplayName("Amount")]
        int Amount { get; set; }

        [DisplayName("Price")]
        [DataType(DataType.Currency)]
        double Price { get; set; }
    }
}