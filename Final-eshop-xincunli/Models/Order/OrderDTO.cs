using System;

namespace Final_eshop_xincunli.Models.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public string ContactName { get; set; }
        public string ContactAddress { get; set; }
        public string ContactPhone { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
    }
}