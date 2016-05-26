using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Final_eshop_xincunli.Models
{
    public class MemberLoginView
    {
        [DisplayName("EMail")]
        [DataType(DataType.EmailAddress,ErrorMessage="Please input email address")]
        [Required(ErrorMessage= "Please input email address")]
        public string email { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please input password")]
        public string password { get; set; }
    }
}