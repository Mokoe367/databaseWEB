using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class SupplierDetails
    {
        public int assetID { get; set; }
        public string type { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Range should be more than 0")]
        public int cost { get; set; }
        public string field { get; set; }
        public int amount { get; set; }
    }
}
