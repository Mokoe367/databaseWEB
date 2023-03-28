using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class TaskInformation
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        [Range(0.0, double.MaxValue, ErrorMessage = "Can't be less than 0")]
        public decimal hours { get; set; }
        public string roleName { get; set; }
    }
}
