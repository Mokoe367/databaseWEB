using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class Tasks
    {
        public int taskID { get; set; }
        public string taskName { get; set; }
        public int cost { get; set; }
        public string taskDueDate { get; set; }
        public int projID { get; set; }
        public int employeeID { get; set; }
        public int deleted_flag { get; set; }


    }
}
