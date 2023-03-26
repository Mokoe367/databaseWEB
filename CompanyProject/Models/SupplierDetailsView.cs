using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class SupplierDetailsView
    {
        public List<SupplierDetails> supplierDetails { set; get; }
        public List<Asset> assets { set; get; }
    }
}
