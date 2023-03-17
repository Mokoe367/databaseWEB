using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class Asset
    {
        public int assetID { get; set; }
        public string type { get; set; }
        public int cost { get; set; }
        public int supID { get; set; }
        public int deleted_flag { get; set; }
    }
}
