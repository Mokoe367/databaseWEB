using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class Used_by
    {
        public int employeeID { get; set; }
        public int supID { get; set; }
        public int assetID { get; set; }
        public decimal status { get; set; }
    }
}
