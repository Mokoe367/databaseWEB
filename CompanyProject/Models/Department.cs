using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class Department
    {
        
        public int depID { get; set; }
        [Required]
        public string location { get; set; }
        [Required]
        public string depName { get; set; }
        public int mgrID { get; set; }
        public string mgrName { get; set; }
        public int deleted_flag { get; set; }
        public bool check { get; set; }
        public string mgrFname { get; set; }
        public string mgrMname { get; set; }
        public string mgrLname { get; set; }
    }
}
