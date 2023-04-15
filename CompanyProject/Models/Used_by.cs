using Microsoft.AspNetCore.Mvc.Rendering;
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
        public string supName { get; set; }       
        public string assetName { get; set; }
        public string fullName { get; set; }
        public List<SelectListItem> employees { get; set; }
        public List<SelectListItem> suppliers { get; set; }
        public List<SelectListItem> assets { get; set; }
    }
}
