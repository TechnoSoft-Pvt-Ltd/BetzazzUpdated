using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace betzazz1._1.Models
{
    public class Account
    {
        [Required(ErrorMessage = "Please Enter UserId!")]
        public string UserID
        {get;set;}

        [Required(ErrorMessage = "Please Enter Password!")]
        public string Password
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please Enter New Password!")]
        public string NPassword
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please Enter Confirm Password!")]
        public string CPassword
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please Enter User Name!")]
        public string Username
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please select Currency!")]
        public string currency
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Please Enter Email Id!")]
        [EmailAddress(ErrorMessage = "Please Enter Valid Email Id!")]
        public string emailid
        {
            get;
            set;
        }

        public  string Stake { get; set; }
        public  string Totalreturn { get; set; }
    }
}