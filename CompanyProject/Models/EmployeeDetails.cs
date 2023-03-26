using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class EmployeeDetails
    {
        public List<TaskDetails> taskDetails { set; get; }
        public List<EmployeeUse> UsedBy { set; get; }
        
    }
}
