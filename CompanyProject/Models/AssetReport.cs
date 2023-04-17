using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class AssetReport
    {
        public List<EmployeeAssetReport> employeeAssets { get; set; }
        public List<DepartmentAssetsReport> departmentAssets { get; set; }
        public int employeeTotal { get; set; }
        public int departmentTotal { get; set; }
        public int total { get; set; }
    }
}
