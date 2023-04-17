using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class EmployeeTaskReportView
    {
        public List<Project> projects { get; set; }
        public List<TaskDetails> taskDetails { get; set; }


    }
}
