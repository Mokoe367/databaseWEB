using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace CompanyProject.Controllers
{
    public class AdminViewController : Controller
    {
        public IActionResult Index()
        {
            
            return View("AdminIndex", getViewData());
        }

        public static string getStringValue(object value)
        {
            if (value == DBNull.Value) return string.Empty;
            return value.ToString();
        }

        public int getIntValue(object value)
        {
            if (value == DBNull.Value) return 0;
            return Convert.ToInt32(value);
        }

        public IEnumerable<AdminViewModel> getViewData()
        {
            List<AdminViewModel> data = new List<AdminViewModel>();
            AdminViewModel model = new AdminViewModel();

            model.Employees = getEmployeeData();
            model.Departments = getDepartmentData();

            data.Add(model);

            return data;

        }

        public List<Employee> getEmployeeData()
        {
            MySqlConnection conn = new MySqlConnection("server = localhost; port=3306;database=target;user=root;password=MonkeysInc7!");
            List<Employee> employeeData = new List<Employee>();
            
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from employee;", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    DateTime date = Convert.ToDateTime(getStringValue(reader["birthdate"]));
                    string dateNoTime = date.ToShortDateString();
                    employeeData.Add(new Employee()
                    {
                        ID = getIntValue(reader["employeeID"]),
                        Fname = getStringValue(reader["Fname"]),
                        Lname = getStringValue(reader["Lname"]),
                        Mname = getStringValue(reader["Mname"]),
                        Address = getStringValue(reader["address"]),
                        Sex = getStringValue(reader["sex"]),
                        BirthDate = dateNoTime,
                        Deleted_flag = getIntValue(reader["deleted_flag"]),
                        RoleID = getIntValue(reader["roleId"]),
                        DepID = getIntValue(reader["depId"]),
                        Ssn = getIntValue(reader["ssn"]),
                        Salary = getIntValue(reader["salary"]),
                        SuperID = getIntValue(reader["superID"])

                     });
                }
            }
            conn.Close();
            return employeeData;
        }

        public List<Department> getDepartmentData()
        {
            MySqlConnection conn = new MySqlConnection("server = localhost; port=3306;database=target;user=root;password=MonkeysInc7!");
            List<Department> departmentData = new List<Department>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from department;", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                
                    departmentData.Add(new Department()
                    {
                        depID = getIntValue(reader["depID"]),
                        location = getStringValue(reader["location"]),
                        depName = getStringValue(reader["depName"]),
                        mgrID = getStringValue(reader["mgrID"]),
                        projID = getStringValue(reader["projID"]),
                        mgrSSN = getStringValue(reader["mgrSSN"])
  
                    });
                }
            }
            conn.Close();

            return departmentData;
      
   
        }
    }
}