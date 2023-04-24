using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class Works_on
    {
       
        public int employeeID { get; set; }
        public string Fname { get; set; }
        public string Mname { get; set; }
        public string Lname { get; set; }
        public string fullName { get; set; }
        public int TaskID { get; set; }        
        public string taskName { get; set; }
        public string documentation { get; set; }
        [Range(0.0, double.MaxValue, ErrorMessage = "Can't be less than 0")]
        public decimal hours { get; set; }     
        public int tempemployeeID { get; set; }
        public int tempTaskID { get; set; }
        [Range(0.0, 100.0, ErrorMessage = "Can't be less than 0 and more than 100")]
        [Required(ErrorMessage = "Status Required")]
        public decimal status { get; set; }
        public List<SelectListItem> employees { get; set; }
        public List<SelectListItem> tasks { get; set; }

    }
}
