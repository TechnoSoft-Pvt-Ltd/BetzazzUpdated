using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace betzazz1._1.Models
{
    public class Account
    {
        [Required]
        public string UserID
        {
            get;
            set;
        }

        [Required]
        public string Password
        {
            get;
            set;
        }

        [Required]
        public string NPassword
        {
            get;
            set;
        }

        [Required]
        public string CPassword
        {
            get;
            set;
        }

        [Required]
        public string Username
        {
            get;
            set;
        }

        [Required]
        public string currency
        {
            get;
            set;
        }

        [Required]
        public string emailid
        {
            get;
            set;
        }
    }
}