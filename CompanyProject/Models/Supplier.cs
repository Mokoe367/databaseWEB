using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class Supplier
    {
        private CompanyContext context;
        public int supID { get; set; }
        public string product { get; set; }
        public string supName { get; set; }
        public int roleID { get; set; }


    }
}
