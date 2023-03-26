using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class ProjectDetails
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string roleName { get; set; }
        public int taskID { get; set; }
        public string taskName { get; set; }
        public decimal hours { get; set; }
        public string taskDueDate { get; set; }
        public int cost { get; set; }
    }
}
