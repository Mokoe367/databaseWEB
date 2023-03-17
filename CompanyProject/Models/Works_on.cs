using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class Works_on
    {
        public int employeeID { get; set; }
        public int TaskID { get; set; }
        public decimal hours { get; set; }
    }
}
