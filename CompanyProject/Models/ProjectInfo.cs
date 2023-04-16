using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class ProjectInfo
    {
        public List<ProjectDetails> details { get; set; }
        public List<Tasks> tasks { get; set; }
    }
}
