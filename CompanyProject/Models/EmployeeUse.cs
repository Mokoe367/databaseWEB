using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class EmployeeUse
    {
        [Required(ErrorMessage = "EmployeeID Required")]
        public int employeeID { get; set; }
        [Required(ErrorMessage = "SupplierID Required")]
        public int supID { get; set; }
        [Required(ErrorMessage = "AssetID Required")]
        public int assetID { get; set; }
        public string type { get; set; }
        public string field { get; set; }
        public int cost { get; set; }
        public int tempemployeeID { get; set; }
        public int tempsupID { get; set; }
        public int tempassetID { get; set; }
        public string supName { get; set; }
        public int amount { get; set; }
    }
}
