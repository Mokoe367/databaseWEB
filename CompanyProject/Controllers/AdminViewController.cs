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
            int empID = (int)TempData["id"];

            MySqlConnection conn = new MySqlConnection("server = localhost; port=3306;database=target;user=root;password=MonkeysInc7!");
            conn.Open();
            Employee user = new Employee();
            MySqlCommand cmd = new MySqlCommand("select e.Fname, e.Lname, e.depID from employee as e where e.employeeID = '"+empID+"'", conn);
            
            var reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                user.Fname = getStringValue(reader["Fname"]);
                user.Lname = getStringValue(reader["Lname"]);
                user.DepID = getIntValue(reader["depID"]);
            }
            conn.Close();
            string msg = "Signed in as " + user.Fname + " " + user.Lname + " showing department number: " + user.DepID;
            ViewData["userInfo"] = msg;


            return View("AdminIndex", getViewData(empID));
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

        public IEnumerable<AdminViewModel> getViewData(int id)
        {
            List<AdminViewModel> data = new List<AdminViewModel>();
            AdminViewModel model = new AdminViewModel();

            model.Employees = getEmployeeData(id);
            model.Departments = getDepartmentData(id);

            data.Add(model);

            return data;

        }

        public List<Employee> getEmployeeData(int id)
        {
            MySqlConnection conn = new MySqlConnection("server = localhost; port=3306;database=target;user=root;password=MonkeysInc7!");
            List<Employee> employeeData = new List<Employee>();
            
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select e.employeeID, e.Fname, e.Lname, e.salary, e.roleID, e.depID, e.SuperID from employee as e, department as d where d.mgrID = '"+id+"' and e.depID = d.depID; ", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    employeeData.Add(new Employee()
                    {
                        ID = getIntValue(reader["employeeID"]),
                        Fname = getStringValue(reader["Fname"]),
                        Lname = getStringValue(reader["Lname"]),
                        RoleID = getIntValue(reader["roleId"]),
                        DepID = getIntValue(reader["depID"]),
                        Salary = getIntValue(reader["salary"]),
                        SuperID = getIntValue(reader["superID"])

                     });
                }
            }
            conn.Close();
            return employeeData;
        }

        public List<Department> getDepartmentData(int id)
        {
            MySqlConnection conn = new MySqlConnection("server = localhost; port=3306;database=target;user=root;password=MonkeysInc7!");
            List<Department> departmentData = new List<Department>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from department where department.mgrID = '"+id+"';", conn);

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
                     
                    });
                }
            }
            conn.Close();

            return departmentData;
      
   
        }
    }
}