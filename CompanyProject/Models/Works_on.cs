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
        [Required(ErrorMessage = "Task Required")]
        public string taskName { get; set; }
        [Range(0.0, double.MaxValue, ErrorMessage = "Can't be less than 0")]
        public decimal hours { get; set; }     
        public int tempemployeeID { get; set; }
        public int tempTaskID { get; set; }
       
    }
}
