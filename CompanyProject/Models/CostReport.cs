using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class CostReport
    {
        public int projectID { get; set; }
        public int projectBudget { get; set; }
        public decimal employeeSalary { get; set; }
        public int departmentCost { get; set; }
        public int employeeCost { get; set; }
        public int taskCost { get; set; }
        public List<Tasks> tasks { get; set; }
        public List<TaskReport> taskReports { get; set; }
        public List<EmployeeAssetReport> employeeAssets { get; set; }
        public List<DepartmentAssetsReport> departmentAssets { get; set; }

    }
}
