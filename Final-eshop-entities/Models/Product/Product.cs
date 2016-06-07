using System;
using System.ComponentModel.DataAnnotations;

namespace Final_eshop_entities.Models
{
    /// <summary>
    /// Model of Product
    /// </summary>
    public class Product
    {
        public Product()
        {
            this.CreateDate = DateTime.Now;
            this.Status = "OnSale"; //OnSale; OffSale
        }

        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }
        /// <summary>
        /// 
        /// </summary>

        [Required(ErrorMessage = "Please Enter Product Category")]
        [Display(Name = "Product Category")]
        public string Category { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Please Enter Product Name")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Please Enter Product SEO Name")]
        [Display(Name = "Product SEO Name")]
        public string ProductSEOName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Please Enter Product Price")]
        [Display(Name = "Product Price")]
        public double ProductPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Please Enter Product Dicount OFF rates")]
        [Display(Name = "Dicount OFF rates")]
        public int Discount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Please Enter Product Tax rates")]
        [Display(Name = "Product Tax rates")]
        public double Tax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Please Enter Product Shipping Fee")]
        [Display(Name = "Product Shipping Fee")]
        public double Shipping { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Please Enter Product Count")]
        [Display(Name = "Product Count")]
        public int ProductCount { get; set; }


        public string ImagePath { get; set; }

        public int SellerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }
    }
}