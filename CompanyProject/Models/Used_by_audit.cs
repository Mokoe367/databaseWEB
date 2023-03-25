using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class Used_by_audit
    {
        public int id { get; set; }
        public string field { get; set; }
        public int empID { get; set; }
        public int supID { get; set; }
        public int assetID { get; set; }
        public string deleted_at { get; set; }
    }
}
