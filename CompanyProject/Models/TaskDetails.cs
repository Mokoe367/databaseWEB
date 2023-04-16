using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class TaskDetails
    {
        public int empID { get; set; }
        public int projID { get; set; }
        public string projName { get; set; }
        public int taskID { get; set; }
        [Range(0.0, double.MaxValue, ErrorMessage = "Can't be less than 0")]
        public decimal hours { get; set; }
        public string taskName { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Range should be more than 0")]
        public int budget { get; set; }
        public string dueDate { get; set; }

    }
}
