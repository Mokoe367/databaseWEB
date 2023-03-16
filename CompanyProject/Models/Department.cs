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
        public string mgrID { get; set; }
        public string projID { get; set; }
      

    }
}
