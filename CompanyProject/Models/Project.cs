using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class Project
    {
        private CompanyContext context;
        public int projID { get; set; }
        public string dueDate { get; set; }
        public int depID { get; set; }
        public string projName { get; set; }
        public string location { get; set; }
        public int cost { get; set; }
        public decimal projStatus { get; set; }
        public string field { get; set; }
        public int deleted_flag { get; set; }

    }
}
