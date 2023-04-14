using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class Used_by
    {        
        public int employeeID { get; set; }        
        public int supID { get; set; }    
        public int assetID { get; set; }      
        public string field { get; set; }
        public int tempemployeeID { get; set; }
        public int tempsupID { get; set; }
        public int tempassetID { get; set; }
        public string Fname { get; set; }
        public string Mname { get; set; }
        public string Lname { get; set; }
        [Required(ErrorMessage = "Supplier Required")]
        public string supName { get; set; }
        [Required(ErrorMessage = "Asset Required")]
        public string assetName { get; set; }
        public string fullName { get; set; }
    }
}
