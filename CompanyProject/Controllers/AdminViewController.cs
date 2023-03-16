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
 
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection("server = localhost; port=3306;database=target;user=root;password=MonkeysInc7!");
        }

        public IActionResult Index()
        {
            int empID = (int)TempData["id"];

            MySqlConnection conn = GetConnection();
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
            string msg = "Signed in as " + user.Fname + " " + user.Lname;
            ViewData["userInfo"] = msg;


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
            model.Projects = getProjectData();
            model.Suppliers = getSupplierData();
            model.Roles = getRoleData();

            data.Add(model);

            return data;

        }

        public List<Employee> getEmployeeData()
        {
            MySqlConnection conn = GetConnection();
            List<Employee> employeeData = new List<Employee>();
            
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from employee", conn);

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
            MySqlConnection conn = GetConnection();
            List<Department> departmentData = new List<Department>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from department", conn);

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

        public List<Project> getProjectData()
        {
            MySqlConnection conn = GetConnection();
            List<Project> projectData = new List<Project>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from project", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    DateTime date = Convert.ToDateTime(getStringValue(reader["dueDate"]));
                    string dateNoTime = date.ToShortDateString();
                    projectData.Add(new Project()
                    {
                        projID = getIntValue(reader["projID"]),
                        dueDate = getStringValue(reader["dueDate"]),
                        depID = getIntValue(reader["depID"]),
                        projName = getStringValue(reader["projName"]),
                        location = getStringValue(reader["location"]),
                        cost = getIntValue(reader["cost"]),
                        projStatus = Convert.ToDecimal(reader["projID"]),
                        field = getStringValue(reader["field"]),
                        deleted_flag = getIntValue(reader["deleted_flag"])
                    });
                }
            }
            conn.Close();

            return projectData;
        }

        public List<Supplier> getSupplierData()
        {
            MySqlConnection conn = GetConnection();
            List<Supplier> SupplierData = new List<Supplier>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from suppliers", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                  
                    SupplierData.Add(new Supplier()
                    {
                        supID = getIntValue(reader["supID"]),
                        product = getStringValue(reader["product"]),
                        name = getStringValue(reader["name"]),
                        roleID = getIntValue(reader["roleID"]),
                        deleted_flag = getIntValue(reader["deleted_flag"])
                    });
                }
            }
            conn.Close();

            return SupplierData;
        }

        public List<Role> getRoleData()
        {
            MySqlConnection conn = GetConnection();
            List<Role> RoleData = new List<Role>();

            conn.Open();
            
            MySqlCommand cmd = new MySqlCommand("select * from roles", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                 
                    RoleData.Add(new Role()
                    {
                        roleID = getIntValue(reader["roleID"]), 
                        roleName = getStringValue(reader["rolename"])
                        
                    });
                }
            }
            conn.Close();

            return RoleData;
        }
    }
}