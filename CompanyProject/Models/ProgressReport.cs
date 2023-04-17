using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class ProgressReport
    {
        public List<Tasks> tasks { get; set; }
        public List<employeeProg> progress { get; set; }
        public List<EmployeeHours> EmployeeHours { get; set; }
        public decimal status { get; set; }
        public string dueDate { get; set; }
        public int budget { get; set; }
        public decimal totalHours { get; set; }
    }
}
