using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PROJ_SW.Model
{
    [MetadataType(typeof(CategoryMetaDate))]
    public partial class Category
    {
        //add method or add new properties
    }
    public class CategoryMetaDate
    {
        [Display(Name = "ID")]
        public int cate_id { get; set; }
        
        [Display(Name = "Category Name")]
        public string cate_name { get; set; }
      
        [Display(Name = "Category Image")]
        public string Cate_Image { get; set; }

    }
}
