using System.Collections.Generic;

namespace Final_eshop_xincunli.Models.ViewModel
{
    public class OrderConfirmViewModel
    {
        public OrderSummary Order { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}