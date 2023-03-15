using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class Department
    {
        private CompanyContext context;
        public int depID { get; set; }
        public string location { get; set; }
        public string depName { get; set; }
        public int mgrID { get; set; }
        public int projID { get; set; }
        public int mgrSSN { get; set; }
        

    }
}
