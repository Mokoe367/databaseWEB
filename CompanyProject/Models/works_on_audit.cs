using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class works_on_audit
    {
        public int id { get; set; }
        public int empID { get; set; }
        public int taskID { get; set; }
        public decimal hours { get; set; }
        public string deleted_at { get; set; }
        public string fullName { get; set; }
        public string taskName { get; set; }
        public decimal status { get; set; }
    }
}
