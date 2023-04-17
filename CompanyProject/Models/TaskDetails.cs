using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class TaskDetails
    {
        public int empID { get; set; }
        public string projName { get; set; }          
        public decimal hours { get; set; }
        public string taskName { get; set; }       
        public int budget { get; set; }
        public string dueDate { get; set; }
        public int taskID { get; set; }
    }
}
