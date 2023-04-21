using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class Tasks
    {
        [DisplayName("Task ID")]
        public int taskID { get; set; }
        [Required (ErrorMessage = "Task Name Required")]
        public string taskName { get; set; }
        [Required(ErrorMessage = "Budget Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Range should be above 0 or more")]
        public int cost { get; set; }
        [Required(ErrorMessage = "Due Date Required")]
        [RegularExpression("([12]\\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\\d|3[01]))", ErrorMessage = "Invalid Date Format")]
        public string taskDueDate { get; set; }
        public int projID { get; set; }
        public string projName { get; set; }
        public int deleted_flag { get; set; }
        [Range(0, 100.0, ErrorMessage = "Range should be above 0 or more and less than 100")]
        public decimal status { get; set; }
        public List<SelectListItem> projects { get; set; }
    }
}
