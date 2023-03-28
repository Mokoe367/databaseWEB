﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class ProjectHoursReport
    {
            public int projID { get; set; }

            public string projName { get; set; }

            public int employeeID { get; set; }

            [Range(0.0, double.MaxValue, ErrorMessage = "0.0 and 100.0")]
            public decimal hours { get; set; }
            
            public int taskID { get; set; }
        
    }

}

