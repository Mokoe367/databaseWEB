﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class AdminViewModel
    {
        public Employee emp { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Department> Departments { get; set; }

    }
}