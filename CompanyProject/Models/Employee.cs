using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class Employee
    {
       
        public int ID { get; set; }
        [Required(ErrorMessage = "First Name Required")]
        [DisplayName("First Name")]
        public string Fname { get; set; }

        [Required(ErrorMessage = "Last Name Required")]
        [DisplayName("Last Name")]
        public string Lname { get; set; }
        [DisplayName("Middle Name")]
        public string Mname { get; set; }

        [Required(ErrorMessage = "Address Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Gender Required")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Birth Day Required")]
        [RegularExpression("([12]\\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\\d|3[01]))", ErrorMessage = "Invalid Date Format")]
        public string BirthDate { get; set; }

        public int Deleted_flag { get; set; }
        [DisplayName("Role Name")]
        public string RoleName { get; set; }
        public int RoleID { get; set; }
        [DisplayName("Department Name")]
        public string DepName { get; set; }

        public int DepID { get; set; }
       
      //  [RegularExpression("[0-9]{9}", ErrorMessage = "Invalid SSN Format")]
        public int Ssn { get; set; }

        [Required(ErrorMessage = "Salary Required")]
        [Range(0, 100000, ErrorMessage = "Range should be between 0 and 100k")]
        public int Salary { get; set; }
        public int SuperID { get; set; }
        [DisplayName("Supervisor Name")]
        public string SupervisorName { get; set; }
        public string superFname { get; set; }
        public string superLname { get; set; }
        public string superMname { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public List<SelectListItem> supervisors { get; set; }
        public List<SelectListItem> departments { get; set; }
        public List<SelectListItem> genders { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "M", Text = "M" },
            new SelectListItem { Value = "F", Text = "F" }      
        };
    }
}
