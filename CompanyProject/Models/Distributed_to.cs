using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class Distributed_to
    {   
        [Required (ErrorMessage = "Supplier ID required")]
        public int supID { get; set; }
        [Required(ErrorMessage = "Asset ID required")]
        public int assetID { get; set; }
        [Required(ErrorMessage = "Department ID required")]
        public int depID { get; set; }     
        public int tempSupID { get; set; }
        public int tempAssetID { get; set; }
        public int tempDepID { get; set; }
        public string field { get; set; }        
        public string supName { get; set; }      
        public string depName { get; set; }      
        public string assetName { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Range should be 0 or more")]
        public int amount { get; set; }
        public List<SelectListItem> departments { get; set; }
        public List<SelectListItem> suppliers { get; set; }
        public List<SelectListItem> assets { get; set; }

    }
}
