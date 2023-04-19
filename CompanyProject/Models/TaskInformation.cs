using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class TaskInformation
    {
        public string name { get; set; }               
        public decimal hours { get; set; }
        public string roleName { get; set; }
        public decimal status { get; set; }
    }
}
