using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace CompanyProject.Controllers
{
    public class ManagerController : Controller
    {
        public int userDepID { set; get; }
        public int userEmpID { set; get; }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection("server = localhost; port=3306;database=target;user=root;password=MonkeysInc7!");
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
       

        public IActionResult Index()
        {
            string empID = HttpContext.Session.GetString("id");
            if (empID == null)
            {
                return RedirectToAction("Login", "Login", new { error = "Session Timed out" });
            }
            else
            {               
                MySqlConnection conn = GetConnection();
                conn.Open();
                Employee user = new Employee();
                MySqlCommand cmd = new MySqlCommand("select e.Fname, e.Lname, e.depID from employee as e where e.employeeID = '" + empID + "'", conn);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user.Fname = getStringValue(reader["Fname"]);
                    user.Lname = getStringValue(reader["Lname"]);
                    user.DepID = getIntValue(reader["depID"]);
                }
                conn.Close();
                HttpContext.Session.SetString("depID", user.DepID.ToString());               
                userDepID = user.DepID;
                userEmpID = Convert.ToInt32(empID);
                string msg = "Signed in as " + user.Fname + " " + user.Lname + " showing Department " + user.DepID;
                ViewData["userInfo"] = msg;               
                return View(getViewData());
            }
                         
        }

        public IActionResult AddEmployee()
        {
            string depID = "Adding Employee into Department number " + HttpContext.Session.GetString("depID");
            ViewData["AddInfo"] = depID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmployee(Employee obj)
        {
            
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select employee.ssn from employee where employee.ssn = " + obj.Ssn + "; ", conn);
            string empID = HttpContext.Session.GetString("id");
            obj.ID = Convert.ToInt32(empID);
            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                ModelState.AddModelError("Ssn", "No Duplicate SSN");
            }
            reader.Close();
            string query = "select roleId from roles where roleId = " + obj.RoleID + ";";
            cmd.CommandText = query;
            cmd.Connection = conn;
            reader = cmd.ExecuteReader();
            if (!reader.HasRows && obj.RoleID != 0)
            {
                ModelState.AddModelError("RoleId", "Role number doesn't exist");
            }
            reader.Close();
            query = "select employeeID from employee where employeeID = " + obj.SuperID + ";";
            cmd.CommandText = query;
            reader = cmd.ExecuteReader();
            if (!reader.HasRows && obj.SuperID != 0)
            {
                ModelState.AddModelError("superID", "superviser Id number doesn't exist");
            }
            if (reader.HasRows && obj.SuperID != 0)
            {
                reader.Read();
                int id = getIntValue(reader["employeeID"]);
                if (id == obj.ID)
                {
                    ModelState.AddModelError("superID", "supervisor can't be same as employeeID");
                }

            }
            reader.Close();
            if (obj.Sex != "M" && obj.Sex != "F")
            {
                ModelState.AddModelError("Sex", "Gender Must be either M or F");
            }
            if (ModelState.IsValid)
            {
                string depID = HttpContext.Session.GetString("depID");
                int department = Convert.ToInt32(depID);
                MySqlCommand insert = new MySqlCommand();
                query = "insert into employee(Fname, Mname, Lname, salary, ssn, address, birthDate, sex, roleID, superID, depID) " +
                    "Values( @Fname, @Mname, @Lname, @salary , @ssn , @address, @birthdate, @sex " +
                    ", @roleId, @superID, @depId);";
                insert.CommandText = query;
                insert.Parameters.AddWithValue("@Fname", obj.Fname);
                insert.Parameters.AddWithValue("@Mname", obj.Mname);
                insert.Parameters.AddWithValue("@Lname", obj.Lname);
                insert.Parameters.AddWithValue("@sex", obj.Sex);
                insert.Parameters.AddWithValue("@birthdate", obj.BirthDate);
                insert.Parameters.AddWithValue("@salary", obj.Salary);
                insert.Parameters.AddWithValue("@ssn", obj.Ssn);
                insert.Parameters.AddWithValue("@address", obj.Address);
                insert.Parameters.AddWithValue("@depId", department);
                if (obj.RoleID == 0)
                {
                    insert.Parameters.AddWithValue("@roleId", DBNull.Value);
                }
                else
                {
                    insert.Parameters.AddWithValue("@roleId", obj.RoleID);
                }

                if (obj.SuperID == 0)
                {
                    insert.Parameters.AddWithValue("@superID", DBNull.Value);
                }
                else
                {
                    insert.Parameters.AddWithValue("@superID", obj.SuperID);
                }
                insert.Connection = conn;
                insert.ExecuteNonQuery();
                conn.Close();
                TempData["success"] = "Employee added successfully";
                return RedirectToAction("Index");


            }
            else
            {
                string depID = "Adding Employee into Department number " + HttpContext.Session.GetString("depID");
                ViewData["AddInfo"] = depID;
                return View(obj);
            }

            
        }

        public IEnumerable<ManagerViewModel> getViewData()
        {
            List<ManagerViewModel> data = new List<ManagerViewModel>();
            ManagerViewModel model = new ManagerViewModel();

            model.Employees = getEmployeeData();            
            model.Projects = getProjectData();           

            data.Add(model);

            return data;

        }

        public List<Employee> getEmployeeData()
        {
            MySqlConnection conn = GetConnection();
            List<Employee> employeeData = new List<Employee>();

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from employee where employee.depID = " + userDepID + " and employee.deleted_flag != 0;", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    DateTime date = Convert.ToDateTime(getStringValue(reader["birthdate"]));
                    string dateNoTime = date.ToShortDateString();
                    string[] dateTemp = dateNoTime.Split('/');
                    int month = Int32.Parse(dateTemp[0]);
                    int day = Int32.Parse(dateTemp[1]);
                    if (month < 10)
                    {
                        dateTemp[0] = "0" + dateTemp[0];
                    }
                    if (day < 10)
                    {
                        dateTemp[1] = "0" + dateTemp[1];
                    }
                    string sqlDate = dateTemp[2] + "-" + dateTemp[0] + "-" + dateTemp[1];
                    employeeData.Add(new Employee()
                    {
                        ID = getIntValue(reader["employeeID"]),
                        Fname = getStringValue(reader["Fname"]),
                        Lname = getStringValue(reader["Lname"]),
                        Mname = getStringValue(reader["Mname"]),
                        Address = getStringValue(reader["address"]),
                        Sex = getStringValue(reader["sex"]),
                        BirthDate = sqlDate,
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

        public List<Project> getProjectData()
        {
            MySqlConnection conn = GetConnection();
            List<Project> projectData = new List<Project>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from project where project.depID = " + userDepID + " and project.deleted_flag != 0;", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    DateTime date = Convert.ToDateTime(getStringValue(reader["dueDate"]));
                    string dateNoTime = date.ToShortDateString();
                    string[] dateTemp = dateNoTime.Split('/');
                    int month = Int32.Parse(dateTemp[0]);
                    int day = Int32.Parse(dateTemp[1]);
                    if (month < 10)
                    {
                        dateTemp[0] = "0" + dateTemp[0];
                    }
                    if (day < 10)
                    {
                        dateTemp[1] = "0" + dateTemp[1];
                    }
                    string sqlDate = dateTemp[2] + "-" + dateTemp[0] + "-" + dateTemp[1];
                    projectData.Add(new Project()
                    {
                        projID = getIntValue(reader["projID"]),
                        dueDate = sqlDate,
                        depID = getIntValue(reader["depID"]),
                        projName = getStringValue(reader["projName"]),
                        location = getStringValue(reader["location"]),
                        cost = getIntValue(reader["cost"]),
                        projStatus = Convert.ToDecimal(reader["projStatus"]),
                        field = getStringValue(reader["field"]),
                        deleted_flag = getIntValue(reader["deleted_flag"])
                    });
                }
            }
            conn.Close();

            return projectData;
        }
    }
}