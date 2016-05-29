using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
            this.Status = "OnSale"; //0:On-Sale; 1:Off-Sale
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
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Category { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Please Enter Product Name")]
        [Display(Name = "Product Name")]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string ProductName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Please Enter Product SEO Name")]
        [Display(Name = "Product SEO Name")]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
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
        //[Range(1, 100, ErrorMessage = "The {0} must be a number between {1} and {2}.")]
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