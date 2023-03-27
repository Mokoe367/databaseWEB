﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class ManagerViewModel
    {
        public List<Employee> Employees { get; set; }
        public List<Project> Projects { get; set; }
        public List<Tasks> tasks { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public List<Role> Roles { get; set; }
        public List<Dep_locations> locations { get; set; }
        
    }
}
