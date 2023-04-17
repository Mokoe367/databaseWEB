using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class Project
    {        
        public int projID { get; set; }

        [Required(ErrorMessage = "Due Date Required")]
        [RegularExpression("([12]\\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\\d|3[01]))", ErrorMessage = "Invalid Date Format")]
        public string dueDate { get; set; }

        public int depID { get; set; }
        [DisplayName("Department Name")]
        public string depName { get; set; }

        
        [Required(ErrorMessage = "Project Name Required")]
        public string projName { get; set; }

        public string location { get; set; }

        [Required(ErrorMessage = "Budget Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Range should be above 0 or more")]
        public int cost { get; set; }

        [Required(ErrorMessage = "Status Required")]
        [Range(0.0, 100.0, ErrorMessage = "0.0 and 100.0")]
        public decimal projStatus { get; set; }

        public string field { get; set; }

        public int deleted_flag { get; set; }

        public List<SelectListItem> departments { get; set; }
        public List<SelectListItem> locations { get; set; }
    }
}
