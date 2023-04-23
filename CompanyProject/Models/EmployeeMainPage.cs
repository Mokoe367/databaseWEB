using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class EmployeeMainPage
    {
        public List<Project> projects { get; set; }
        public List<TaskDetails> taskDetails { get; set; }
        public List<EmployeeUse> assets { get; set; }
        public List<Employee> employees { get; set; }

    }
}

