using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class Supplier
    {
        public int supID { get; set; }
        public string product { get; set; }
        public string name { get; set; }
        public int roleID { get; set; }
        public int deleted_flag { get; set; }

    }
}
