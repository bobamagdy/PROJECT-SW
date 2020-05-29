using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PROJ_SW.Model
{
    [MetadataType(typeof(CreditMetaDate))]
    public partial class Credit
    {
        //add method or add new properties
    }

    public class CreditMetaDate
    {

        [Required]
        [Display(Name = "Credit ID")]
        public int Credit_id { get; set; }
        [Required]
        [Display(Name = "Credit Holder Name")]

        public string Credit_Holder_Name { get; set; }
        [Required]
        [Display(Name = " Time Credit Expire")]
        public Nullable<System.DateTime> Credit_Expire { get; set; }
        [Required]
        [Display(Name = " Postal Code")]
        public Nullable<int> Postal_Code { get; set; }
        [Required]
        [Display(Name = " CVV Number")]
        public Nullable<int> CVV_Number { get; set; }
        [Required]
        [Display(Name = " User ID")]
        public Nullable<int> User_Id { get; set; }
    }
}