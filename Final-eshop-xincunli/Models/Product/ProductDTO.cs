namespace Final_eshop_xincunli.Models.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        public string ProductSEOName { get; set; }
        public double Tax { get; set; }
        public int Discount { get; set; }
        public double ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public string ImagePath { get; set; }
    }
}