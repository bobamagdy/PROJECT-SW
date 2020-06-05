using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PROJ_SW.Model
{
    [MetadataType(typeof(UsertMetaDate))]
    public partial class User
    {
    }
    public class UsertMetaDate
    {



        
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Display(Name = "Last Name")]
        public string LName { get; set; }

        [Display(Name = "Email ")]
        public string Email { get; set; }

        //[StringLength(8, MinimumLength = 6, ErrorMessage = "password should bettwen 6 and 8")]
      
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Phone")]
        public Nullable<int> Phone { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        //public Nullable<int> Login_Id { get; set; }

        [Display(Name = "Credit Holder Name ")]
        public string Credit_Holder_Name { get; set; }

        [Display(Name = "Time Credit Name ")]
        public Nullable<System.DateTime> time_credit_name { get; set; }

        [Display(Name = "Postel Code ")]
        public Nullable<int> Postel_code { get; set; }

        [Display(Name = "CVV Number")]
        public Nullable<int> CVV_Number { get; set; }
    }
}