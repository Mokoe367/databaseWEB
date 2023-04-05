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
    }
}
