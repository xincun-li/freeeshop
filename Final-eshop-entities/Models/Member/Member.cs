using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_eshop_entities.Models
{
    public enum Role
    {
        Basic,
        Premium,
        Seller
    }
    [DisplayName("Memebership information")]
    [DisplayColumn("Name")]
    public class Member
    {
        public Member()
        {
            this.RegisterDate = DateTime.Now;
            //this.Role = Role.Basic;
        }
        [Key]
        public int Id { get; set; }

        [DisplayName("EMail")]
        [Required(ErrorMessage = "Please input email address")]
        [Description("We can using email address to login")]
        [MaxLength(250, ErrorMessage = "Email address must be shorter than 250 chars.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Please input password")]
        [MaxLength(40, ErrorMessage = "Password must be shorter than 40 chars.")]
        [Description("Password will be encrypt")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "Please input password again.")]
        [MaxLength(40, ErrorMessage = "Password must be shorter than 40 chars.")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Please input same password.")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Please input name")]
        [MaxLength(250, ErrorMessage = "Name must be shorter than 250 chars.")]
        public string Name { get; set; }

        [DisplayName("Register Date")]
        public DateTime RegisterDate { get; set; }
        

        [DisplayName("Member Level")]
        public Role Role { get; set; }
        
        public virtual ICollection<OrderSummary> Orders { get; set; }
    }
}