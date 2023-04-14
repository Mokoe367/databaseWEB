using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class distributed_to_audit
    {
        public int id { get; set; }
        public int supID { get; set; }
        public int assetID { get; set; }
        public int depID { get; set; }
        public string field { get; set; }
        public string deleted_at { get; set; }
        public string depName { get; set; }
        public string supName { get; set; }
        public string assetName { get; set; }
    }
}
