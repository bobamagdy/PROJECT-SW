//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.ComponentModel.DataAnnotations;

//namespace PROJ_SW.Model
//{
//    [MetadataType(typeof(ProductMetaDate))]
//    public partial class Product
    
//    {
//        //add method or add new properties
//    }
//    public class ProductMetaDate {
//        //edit properties
//        [Display(Name = "ID")]
//        public int prod_id { get; set; }
//        [Display(Name = "Name")]
//        public string prod_name { get; set; }
//        public Nullable<double> Price { get; set; }
//        [Display(Name = "Image")]
//        public string Prod_Image { get; set; }
//        public string Description { get; set; }
//        [Display(Name = "Date")]
//        public Nullable<System.DateTime> MGF_Date { get; set; }
//        [Display(Name = "Expire Date")]
//        public Nullable<System.DateTime> Expiry_Date { get; set; }
//        [Display(Name = "Batch Number")]
//        public Nullable<int> Batch_No { get; set; }
//        [Display(Name = "Category Name")]
//        public Nullable<int> Cate_Id { get; set; }
//        [Display(Name = "Inventory Name")]
//        public Nullable<int> inventory_id { get; set; }
//    }

//}