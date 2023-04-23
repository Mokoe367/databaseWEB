using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class TeamTasks
    {
        public string Fname { get; set; }

        public string Lname { get; set; }
        public int taskID { get; set; }

        public string taskName { get; set; }

        public string documentation { get; set; }

        public string projName { get; set; }

        public int projID { get; set; }


        public int employeeID { get; set; }
    }
}
