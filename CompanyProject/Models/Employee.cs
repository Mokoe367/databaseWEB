using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class Employee
    {
        private CompanyContext context;
        public int ID { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Mname { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public string BirthDate{ get; set; }
        public int Deleted_flag { get; set; }
        public int RoleID { get; set; }
        public int DepID { get; set; }
        public int Ssn { get; set; }
        public int Salary { get; set; }
        public int SuperID { get; set; }


    }
}
