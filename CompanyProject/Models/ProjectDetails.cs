﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class ProjectDetails
    {
        public string name { get; set; }        
        public string roleName { get; set; }        
        public string taskName { get; set; }
        public decimal hours { get; set; }
        public decimal status { get; set; }
        public string documentation { get; set; }
    }
}
