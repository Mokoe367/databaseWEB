using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class AdminViewModel
    {
        public List<Employee> Employees { get; set; }
        public List<Department> Departments { get; set; }
        public List<Project> Projects { get; set; }
        public List<Role> Roles { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public List<Task> Tasks { get; set; }

    }
}
