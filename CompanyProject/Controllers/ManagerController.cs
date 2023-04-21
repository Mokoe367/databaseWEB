using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CompanyProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;

namespace CompanyProject.Controllers
{
    public class ManagerController : Controller
    {
        public int userDepID { set; get; }
        public int userEmpID { set; get; }

        private MySqlConnection GetConnection()
        {
            //var consentFeature = HttpContext.Features.Get<ITrackingConsentFeature>();
            return new MySqlConnection("server = databaseproject.czelvhdtgas7.us-east-2.rds.amazonaws.com; port=3306;database=target;user=root;password=group2database");
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

        public string getDepartmentName(int depID)
        {
            string depName = "";
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd1 = new MySqlCommand("select depName from department where depID=" + depID, conn);
            var reader = cmd1.ExecuteReader();

            if (reader.Read())
            {
                depName = getStringValue(reader["depName"]);
            }
            conn.Close();
            return depName;
        }

        // GET ROLE NAME
        public string getRoleName(int roleID)
        {
            string roleName = "";
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd1 = new MySqlCommand("select roleName from roles where roleID=" + roleID, conn);
            var reader = cmd1.ExecuteReader();

            if (reader.Read())
            {
                roleName = getStringValue(reader["roleName"]);
            }
            conn.Close();
            return roleName;
        }

        // GET SUPERVISOR NAME
        public string getSuperName(int superID)
        {
            if (superID == 0)
            {
                return "";
            }

            string superFname = "";
            string superMname = "";
            string superLname = "";
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd1 = new MySqlCommand("select S.Fname, S.Mname, S.Lname from employee as S " +
                "where  S.employeeID = " + superID, conn);
            var reader = cmd1.ExecuteReader();

            if (reader.Read())
            {
                superFname = getStringValue(reader["Fname"]);
                superMname = getStringValue(reader["Mname"]);
                superLname = getStringValue(reader["Lname"]);
            }
            conn.Close();
            return superFname + " " + superMname + " " + superLname;
        }

        public IActionResult Index()
        {
            string empID = HttpContext.Session.GetString("ManagerID");

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
                    user.DepName = getDepartmentName(getIntValue(reader["depID"]));
                }
                reader.Close();                                
                HttpContext.Session.SetString("depID", user.DepID.ToString());
                userDepID = user.DepID;
                userEmpID = Convert.ToInt32(empID);
                string msg = "Signed in as " + user.Fname + " " + user.Lname + " showing Department " + user.DepName + " (Department ID: " + user.DepID + ")";
                ViewData["userInfo"] = msg;

                string query = "select e.Fname, e.Lname, e.Mname from department as d left outer join employee as e on d.mgrID = e.employeeID where d.depID = @dep;";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@dep", user.DepID);
                reader = cmd.ExecuteReader();
                string mgrName = "";
                if(reader.Read())
                {
                    mgrName = getStringValue(reader["Fname"]) + " " + getStringValue(reader["Mname"]) + " " + getStringValue(reader["Lname"]);
                }
                else
                {
                    mgrName = "none";
                }
                reader.Close();
                msg = "Department manager: " + mgrName;
                ViewData["mgrInfo"] = msg;
                conn.Close();
                return View(getViewData());
            }

        }

        public List<SelectListItem> getRoles()
        {
            MySqlConnection conn = GetConnection();
            List<SelectListItem> Roles = new List<SelectListItem>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from roles", conn);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    Roles.Add(new SelectListItem()
                    {
                        Value = getStringValue(reader["roleID"]),
                        Text = getStringValue(reader["rolename"])
                    });
                }
            }
            conn.Close();

            return Roles;
        }

        public List<SelectListItem> getSupervisors(int depID, int empID)
        {
            if (depID == 0)
            {
                List<SelectListItem> empty = new List<SelectListItem>();
                return empty;
            }
            MySqlConnection conn = GetConnection();
            List<SelectListItem> supervisors = new List<SelectListItem>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select Fname, Lname, Mname, employeeID from employee where depID = " + depID + " and deleted_flag = 1;", conn);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string id = getStringValue(reader["employeeID"]);
                    if (id != empID.ToString())
                    {
                        string Fname = getStringValue(reader["Fname"]);
                        string Mname = getStringValue(reader["Mname"]);
                        string Lname = getStringValue(reader["Lname"]);
                        string Name = id + " " + Fname + " " + Mname + " " + Lname;
                        supervisors.Add(new SelectListItem()
                        {
                            Value = id,
                            Text = Name
                        });
                    }
                }
            }
            conn.Close();

            return supervisors;
        }

        public List<SelectListItem> getEmployees()
        {
            MySqlConnection conn = GetConnection();
            List<SelectListItem> employees = new List<SelectListItem>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select Fname, Lname, Mname, employeeID from employee where deleted_flag = 1 and depID = @department;", conn);
            cmd.Parameters.AddWithValue("@department", HttpContext.Session.GetString("depID"));
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string id = getStringValue(reader["employeeID"]);
                    string Fname = getStringValue(reader["Fname"]);
                    string Mname = getStringValue(reader["Mname"]);
                    string Lname = getStringValue(reader["Lname"]);
                    string Name = id + " " + Fname + " " + Mname + " " + Lname;
                    employees.Add(new SelectListItem()
                    {
                        Value = id,
                        Text = Name
                    });

                }
            }
            conn.Close();

            return employees;
        }
       
        public List<SelectListItem> getLocations(int depID)
        {
            MySqlConnection conn = GetConnection();
            List<SelectListItem> locations = new List<SelectListItem>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select loc_name from dep_locations where depID = " + depID + ";", conn);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    locations.Add(new SelectListItem()
                    {
                        Value = getStringValue(reader["loc_name"]),
                        Text = getStringValue(reader["loc_name"])
                    });
                }
            }
            conn.Close();

            return locations;
        }

        public List<SelectListItem> getProjects()
        {
            MySqlConnection conn = GetConnection();
            List<SelectListItem> departments = new List<SelectListItem>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select projName, projID from project where deleted_flag = 1 and depID = @department;", conn);
            cmd.Parameters.AddWithValue("@department", HttpContext.Session.GetString("depID"));
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    departments.Add(new SelectListItem()
                    {
                        Value = getStringValue(reader["projID"]),
                        Text = getStringValue(reader["projName"])
                    });
                }
            }
            conn.Close();

            return departments;
        }

        public List<SelectListItem> getSuppliers()
        {
            MySqlConnection conn = GetConnection();
            List<SelectListItem> suppliers = new List<SelectListItem>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select name, supID from suppliers where deleted_flag = 1;", conn);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    suppliers.Add(new SelectListItem()
                    {
                        Value = getStringValue(reader["supID"]),
                        Text = getStringValue(reader["name"])
                    });
                }
            }
            conn.Close();

            return suppliers;
        }

        public List<SelectListItem> getAssets()
        {
            MySqlConnection conn = GetConnection();
            List<SelectListItem> assets = new List<SelectListItem>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select a.type, a.assetID, s.name from assets as a " +
                "left outer join suppliers as s on s.supID = a.supID where a.deleted_flag = 1;", conn);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string assetName = getStringValue(reader["type"]);
                    string supName = getStringValue(reader["name"]);
                    string text = supName + " - " + assetName;
                    assets.Add(new SelectListItem()
                    {
                        Value = getStringValue(reader["assetID"]),
                        Text = text
                    });
                }
            }
            conn.Close();

            return assets;
        }

        public List<SelectListItem> getSpecificAssets(int supID)
        {
            MySqlConnection conn = GetConnection();
            List<SelectListItem> assets = new List<SelectListItem>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select a.type, a.assetID, s.name from assets as a " +
                "left outer join suppliers as s on s.supID = a.supID where a.deleted_flag = 1 and a.supID = @supplier;", conn);
            cmd.Parameters.AddWithValue("@supplier", supID);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string assetName = getStringValue(reader["type"]);
                    string supName = getStringValue(reader["name"]);
                    string text = supName + " - " + assetName;
                    assets.Add(new SelectListItem()
                    {
                        Value = getStringValue(reader["assetID"]),
                        Text = text
                    });
                }
            }
            conn.Close();

            return assets;
        }

        public List<SelectListItem> getTasks()
        {
            MySqlConnection conn = GetConnection();
            List<SelectListItem> tasks = new List<SelectListItem>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select t.taskID, t.taskName, p.projName " +
                "from task as t, project as p where t.projID = p.projID and p.depID = @department and t.deleted_flag = 1;", conn);
            cmd.Parameters.AddWithValue("@department", HttpContext.Session.GetString("depID"));
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string projName = getStringValue(reader["projName"]);
                    string taskName = getStringValue(reader["taskName"]);
                    tasks.Add(new SelectListItem()
                    {
                        Value = getStringValue(reader["taskID"]),
                        Text = projName + " - " + taskName
                    });
                }
            }
            conn.Close();

            return tasks;
        }

        public IActionResult AddEmployee()
        {
            string depID = "Adding Employee into Department number " + HttpContext.Session.GetString("depID");
            ViewData["AddInfo"] = depID;
            int department = int.Parse(HttpContext.Session.GetString("depID"));
            Employee employee = new Employee();
            employee.Roles = getRoles();
            employee.supervisors = getSupervisors(department, -1);            
            return View(employee);
        }

        public IActionResult AddProject()
        {
            string depID = "Adding Project into Department number " + HttpContext.Session.GetString("depID");
            ViewData["AddInfo"] = depID;
            int department = Convert.ToInt32(HttpContext.Session.GetString("depID"));
            Project proj = new Project();
            proj.locations = getLocations(department);
            return View(proj);
        }

        public IActionResult AddTask()
        {
            string depID = "Adding Task for Department number " + HttpContext.Session.GetString("depID");
            ViewData["AddInfo"] = depID;
            Tasks task = new Tasks();
            task.projects = getProjects();
            return View(task);
        }

        public IActionResult AddRole()
        {
            return View();
        }

        public IActionResult AddSupplier()
        {
            Supplier supplier = new Supplier();
            supplier.Roles = getRoles();
            return View(supplier);
        }

        public IActionResult AddAsset(int id)
        {
            Asset asset = new Asset();
            asset.supID = id;            
            ViewData["asset"] = id;
            return View(asset);           
        }

        public IActionResult AddUse(int id)
        {
            Used_by use = new Used_by();
            use.employeeID = id;
            use.assets = getAssets();
            use.suppliers = getSuppliers();
            ViewData["use"] = id;
            return View(use);
        }

        public IActionResult AddLocations()
        {
            string depID = "Adding Location into Department number " + HttpContext.Session.GetString("depID");
            ViewData["AddInfo"] = depID;
            return View();
        }

        public IActionResult EditEmployee(int id)
        {
            var emp = new Employee();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select e.employeeID, e.Fname, e.Lname, e.Mname, e.Address, e.sex, e.birthDate, e.deleted_flag, e.roleID, e.depID, e.ssn, e.salary, e.superID, r.roleName, " +
                "s.Fname as superFname, s.Lname as superLname, s.Mname as superMname, d.depName" +
                " from employee as e left outer join employee as s on e.superID = s.employeeID left outer join roles as r on r.roleID = e.roleID left outer join department as d on d.depID = e.depID where e.employeeID = " + id + " ;", conn);

            var reader = cmd.ExecuteReader();
            reader.Read();
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
            string superFname = getStringValue(reader["superFname"]);
            string superMname = getStringValue(reader["superMname"]);
            string superLname = getStringValue(reader["superLname"]);
            string supervisorName = superFname + " " + superMname + " " + superLname;
            emp.Fname = getStringValue(reader["Fname"]);
            emp.ID = getIntValue(reader["employeeID"]);
            emp.Fname = getStringValue(reader["Fname"]);
            emp.Lname = getStringValue(reader["Lname"]);
            emp.Mname = getStringValue(reader["Mname"]);
            emp.Address = getStringValue(reader["address"]);
            emp.Sex = getStringValue(reader["sex"]);
            emp.BirthDate = sqlDate;
            emp.RoleID = getIntValue(reader["roleId"]);
            emp.DepID = getIntValue(reader["depId"]);
            emp.Ssn = getIntValue(reader["ssn"]);
            emp.Salary = getIntValue(reader["salary"]);
            emp.SuperID = getIntValue(reader["superID"]);
            emp.SupervisorName = supervisorName;
            emp.DepName = getStringValue(reader["depName"]);
            emp.RoleName = getStringValue(reader["roleName"]);           
            reader.Close();
            conn.Close();
            emp.Roles = getRoles();
            emp.supervisors = getSupervisors(emp.DepID, emp.ID);
            string empID = HttpContext.Session.GetString("ManagerID");
            if (emp.ID.ToString() == empID)
            {
                return View("selfEdit", emp);
            }
            else
            {
                return View(emp);
            }

        }
    
        public IActionResult EditProject(int id)
        {
            Project proj = new Project();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from project where projID = " + id + "; ", conn);

            var reader = cmd.ExecuteReader();
            reader.Read();
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

            proj.projID = getIntValue(reader["projID"]);
            proj.dueDate = sqlDate;
            proj.depID = getIntValue(reader["depID"]);
            proj.projName = getStringValue(reader["projName"]);
            proj.location = getStringValue(reader["location"]);
            proj.cost = getIntValue(reader["cost"]);
            proj.projStatus = Convert.ToDecimal(reader["projStatus"]);
            proj.field = getStringValue(reader["field"]);
            int department = Convert.ToInt32(HttpContext.Session.GetString("depID"));           
            proj.locations = getLocations(department);
            conn.Close();
            return View(proj);
        }

        public IActionResult EditTask(int id)
        {
            Tasks task = new Tasks();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select t.taskID, t.taskName, t.taskDueDate, t.cost, t.projID, p.projName, t.deleted_flag, t.status " +
               "from task as t left outer join project as p on p.projID = t.projID where taskID = " + id + "; ", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            DateTime date = Convert.ToDateTime(getStringValue(reader["taskDueDate"]));
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
            task.taskID = getIntValue(reader["taskID"]);
            task.taskName = getStringValue(reader["taskName"]);
            task.cost = getIntValue(reader["cost"]);
            task.taskDueDate = sqlDate;
            task.projID = getIntValue(reader["projID"]);
            task.projName = getStringValue(reader["projName"]);
            task.status = Convert.ToDecimal(reader["status"]);
            task.projects = getProjects();
            conn.Close();
            return View(task);
        }

        public IActionResult EditSupplier(int id)
        {
            Supplier sup = new Supplier();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select s.supID, s.product, s.name, s.deleted_flag, s.roleID, r.roleName " +
               "from suppliers as s left outer join roles as r on r.roleID = s.roleID where supID = " + id + "; ", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            sup.supID = getIntValue(reader["supID"]);
            sup.product = getStringValue(reader["product"]);
            sup.name = getStringValue(reader["name"]);
            sup.roleID = getIntValue(reader["roleID"]);
            sup.roleName = getStringValue(reader["roleName"]);
            sup.Roles = getRoles();
            conn.Close();
            return View(sup);
        }

        public IActionResult EditRole(int id)
        {
            Role role = new Role();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from roles where roleID = " + id + "; ", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            role.roleID = getIntValue(reader["roleID"]);
            role.roleName = getStringValue(reader["rolename"]);
            reader.Close();
            conn.Close();
            return View(role);
        }

        public IActionResult EditAsset(int id)
        {
            Asset asset = new Asset();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select a.assetID, a.type, a.cost, a.deleted_flag, a.supID, s.name " +
                "from assets as a left outer join suppliers as s on s.supID = a.supID where assetID = " + id + "; ", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            asset.assetID = getIntValue(reader["assetID"]);
            asset.type = getStringValue(reader["type"]);
            asset.cost = getIntValue(reader["cost"]);
            asset.supID = getIntValue(reader["supID"]);
            asset.supName = getStringValue(reader["name"]);
          
            ViewData["asset"] = asset.supID;
            conn.Close();
            return View(asset);
        }

        public IActionResult EditDistribution(int supId, int assetId)
        {
            Distributed_to distribution = new Distributed_to();
            string depID = HttpContext.Session.GetString("depID");
            int department = Convert.ToInt32(depID);

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select d.depName, s.name, a.type, t.field, t.assetID, t.depID, t.supID, t.amount " +
               "from distributed_to as t left outer join suppliers as s on s.supID = t.supID left outer join assets as a on a.assetID = t.assetID" +
               " left outer join department as d on d.depID = t.depID where t.depID = " + department + " and " +
               "t.supID = " + supId + " and t.assetID = " + assetId + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            distribution.depID = getIntValue(reader["depID"]);
            distribution.supID = getIntValue(reader["supID"]);
            distribution.assetID = getIntValue(reader["assetID"]);
            distribution.tempDepID = getIntValue(reader["depID"]);
            distribution.tempSupID = getIntValue(reader["supID"]);
            distribution.tempAssetID = getIntValue(reader["assetID"]);
            distribution.field = getStringValue(reader["field"]);
            distribution.depName = getStringValue(reader["depName"]);
            distribution.supName = getStringValue(reader["name"]);
            distribution.assetName = getStringValue(reader["type"]);
            distribution.amount = getIntValue(reader["amount"]);
            distribution.assets = getAssets();
            distribution.suppliers = getSuppliers();

            ViewData["supplier"] = supId;
            conn.Close();
            return View(distribution);
        }

        public IActionResult EditUse(int empId, int supId, int assetId)
        {
            Used_by use = new Used_by();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select u.employeeID, u.assetID, u.supID, u.field, u.amount, e.Fname, e.Mname, e.Lname, a.type, s.name " +
                "from used_by as u left outer join employee as e on e.employeeID = u.employeeID left outer join assets as a on a.assetID = u.assetID " +
                "left outer join suppliers as s on s.supID = u.supID where u.employeeID = " + empId + " and " +
                "u.supID = " + supId + " and u.assetID = " + assetId + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            use.employeeID = getIntValue(reader["employeeID"]);
            use.supID = getIntValue(reader["supID"]);
            use.assetID = getIntValue(reader["assetID"]);
            use.tempemployeeID = getIntValue(reader["employeeID"]);
            use.tempsupID = getIntValue(reader["supID"]);
            use.tempassetID = getIntValue(reader["assetID"]);
            use.field = getStringValue(reader["field"]);
            use.Fname = getStringValue(reader["Fname"]);
            use.Mname = getStringValue(reader["Mname"]);
            use.Lname = getStringValue(reader["Lname"]);
            use.supName = getStringValue(reader["name"]);
            use.assetName = getStringValue(reader["type"]);
            use.amount = getIntValue(reader["amount"]);
            use.suppliers = getSuppliers();
            use.assets = getAssets();
            use.employees = getEmployees();

            ViewData["use"] = empId;
            conn.Close();
            return View(use);
        }

        public IActionResult EditLocation(int id, string name)
        {
            Dep_locations location = new Dep_locations();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select d.depName, l.loc_name, l.depID from dep_locations as l left outer join department as d on d.depID = l.depID" +
                " where l.depID = " + id + " and l.loc_name = @name;", conn);
            cmd.Parameters.AddWithValue("@name", name);
            var reader = cmd.ExecuteReader();
            reader.Read();
            location.depID = getIntValue(reader["depID"]);
            location.depName = getStringValue(reader["depName"]);
            location.loc_name = getStringValue(reader["loc_name"]);
            location.pastDepID = getIntValue(reader["depID"]);
            location.pastLoc_name = getStringValue(reader["loc_name"]);
            conn.Close();
            return View(location);
        }

        public IActionResult DeleteDistribution(int supId, int assetId)
        {
            Distributed_to distribution = new Distributed_to();
            string depID = HttpContext.Session.GetString("depID");
            int department = Convert.ToInt32(depID);
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select d.depName, s.name, a.type, t.field, t.assetID, t.depID, t.supID, t.amount " +
               "from distributed_to as t left outer join suppliers as s on s.supID = t.supID left outer join assets as a on a.assetID = t.assetID" +
               " left outer join department as d on d.depID = t.depID where t.depID = " + department + " and " +
               "t.supID = " + supId + " and t.assetID = " + assetId + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();

            distribution.depID = getIntValue(reader["depID"]);
            distribution.supID = getIntValue(reader["supID"]);
            distribution.assetID = getIntValue(reader["assetID"]);
            distribution.field = getStringValue(reader["field"]);
            distribution.depName = getStringValue(reader["depName"]);
            distribution.supName = getStringValue(reader["name"]);
            distribution.assetName = getStringValue(reader["type"]);
            distribution.amount = getIntValue(reader["amount"]);
            ViewData["supplier"] = supId;
            conn.Close();
            return View(distribution);
        }

        public IActionResult FireEmployee(int id)
        {
            var emp = new Employee();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from employee where employeeID = " + id + "; ", conn);

            var reader = cmd.ExecuteReader();
            reader.Read();
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

            emp.Fname = getStringValue(reader["Fname"]);
            emp.ID = getIntValue(reader["employeeID"]);
            emp.Fname = getStringValue(reader["Fname"]);
            emp.Lname = getStringValue(reader["Lname"]);
            emp.Mname = getStringValue(reader["Mname"]);
            emp.Address = getStringValue(reader["address"]);
            emp.Sex = getStringValue(reader["sex"]);
            emp.BirthDate = sqlDate;
            emp.RoleID = getIntValue(reader["roleId"]);
            emp.DepID = getIntValue(reader["depId"]);
            emp.Ssn = getIntValue(reader["ssn"]);
            emp.Salary = getIntValue(reader["salary"]);
            emp.SuperID = getIntValue(reader["superID"]);

            conn.Close();
            return View(emp);
        }

        public IActionResult DeleteProject(int id)
        {
            Project proj = new Project();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from project where projID = " + id + "; ", conn);

            var reader = cmd.ExecuteReader();
            reader.Read();
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

            proj.projID = getIntValue(reader["projID"]);
            proj.dueDate = sqlDate;
            proj.depID = getIntValue(reader["depID"]);
            proj.projName = getStringValue(reader["projName"]);
            proj.location = getStringValue(reader["location"]);
            proj.cost = getIntValue(reader["cost"]);
            proj.projStatus = Convert.ToDecimal(reader["projStatus"]);
            proj.field = getStringValue(reader["field"]);
            conn.Close();
            return View(proj);
        }

        public IActionResult EmployeeDetails(int id)
        {
            
            MySqlConnection conn = GetConnection();
            conn.Open();
            Employee user = new Employee();
            MySqlCommand cmd = new MySqlCommand("select e.Fname, e.Lname from employee as e where e.employeeID = '" + id + "'", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            user.Fname = getStringValue(reader["Fname"]);
            user.Lname = getStringValue(reader["Lname"]);

            conn.Close();

            string msg = "Employee Data for " + user.Fname + " " + user.Lname;
            ViewData["TaskInfo"] = msg;

            ViewData["employee"] = id;
           
            return View(getEmployeeDetails(id));

        }

        public IActionResult ProjectDetails(int id)
        {
            ProjectInfo list = new ProjectInfo();
            list.details = GetProjectDetails(id);
            list.tasks = getTasks(id);
            MySqlConnection conn = GetConnection();
            conn.Open();           
            MySqlCommand cmd = new MySqlCommand("select projName from project where projID = " + id + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            string msg = "Project Details for " + getStringValue(reader["projName"]);
            conn.Close();
           
            ViewData["ProjectInfo"] = msg;

            ViewData["project"] = id;
           
            return View(list);
        }

        public IActionResult SupplierDetails(int id)
        {
           
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select name from suppliers where supID = " + id + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            string msg = "Supplier Information for " + getStringValue(reader["name"]);
            conn.Close();

            ViewData["SupplierInfo"] = msg;

            ViewData["supplier"] = id;
            
            return View(GetSupplierDetailsViews(id));
        }

        public IActionResult TaskInformation(int id)
        {
            IEnumerable<TaskInformation> list = GetTaskInformation(id);

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select taskName from task where taskID = " + id + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            string msg = "Task Details for " + getStringValue(reader["taskName"]);
            conn.Close();

            ViewData["TaskInfo"] = msg;

            ViewData["task"] = id;
            
            return View(list);
        }

        public IActionResult Unlist(int empid, int taskid)
        {
            Works_on work = new Works_on();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select w.employeeID, w.taskID, e.Fname, e.Mname, e.Lname, t.taskName, w.hours, w.taskStatus " +
                "from works_on as w left outer join employee as e on e.employeeID = w.employeeID left outer join task as t on t.taskID = w.taskID where w.employeeID = " + empid + " and " +
                "w.taskID = " + taskid + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            work.employeeID = getIntValue(reader["employeeID"]);
            work.TaskID = getIntValue(reader["taskID"]);
            work.hours = Convert.ToDecimal(reader["hours"]);
            string Fname = getStringValue(reader["Fname"]);
            string Mname = getStringValue(reader["Mname"]);
            string Lname = getStringValue(reader["Lname"]);
            work.Fname = Fname + " " + Mname + " " + Lname;
            work.taskName = getStringValue(reader["taskName"]);
            work.status = Convert.ToDecimal(reader["taskStatus"]);
            ViewData["employee"] = empid;
            conn.Close();
            return View(work);
        }

        public IActionResult Enlist(int id)
        {
            Works_on work = new Works_on();
            work.employeeID = id;
            work.employees = getEmployees();
            work.tasks = getTasks();
            ViewData["employee"] = id;
            return View(work);
        }       

        public IActionResult RequestAssets(int id)
        {          
            Distributed_to asset = new Distributed_to();
            string depID = HttpContext.Session.GetString("depID");
            int department = Convert.ToInt32(depID);
            asset.depID = department;
            asset.supID = id;            
            asset.assets = getSpecificAssets(id); 
            ViewData["asset"] = id;
            return View(asset);
        }

        public IActionResult DeleteTask(int id)
        {
            Tasks task = new Tasks();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from task where taskID = " + id + "; ", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            DateTime date = Convert.ToDateTime(getStringValue(reader["taskDueDate"]));
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
            task.taskID = getIntValue(reader["taskID"]);
            task.taskName = getStringValue(reader["taskName"]);
            task.cost = getIntValue(reader["cost"]);
            task.taskDueDate = sqlDate;
            task.projID = getIntValue(reader["projID"]);
            task.status = Convert.ToDecimal(reader["status"]);
            conn.Close();
            return View(task);
        }

        public IActionResult DeleteSupplier(int id)
        {
            Supplier sup = new Supplier();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from suppliers where supID = " + id + "; ", conn);

            var reader = cmd.ExecuteReader();
            reader.Read();

            sup.supID = getIntValue(reader["supID"]);
            sup.product = getStringValue(reader["product"]);
            sup.name = getStringValue(reader["name"]);
            sup.roleID = getIntValue(reader["roleID"]);
            conn.Close();
            return View(sup);
        }

        public IActionResult DeleteRole(int id)
        {
            Role role = new Role();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from roles where roleID = " + id + "; ", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            role.roleID = getIntValue(reader["roleID"]);
            role.roleName = getStringValue(reader["rolename"]);
            reader.Close();
            conn.Close();
            return View(role);
        }

        public IActionResult DeleteAsset(int id)
        {
            Asset asset = new Asset();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select a.assetID, a.type, a.cost, a.deleted_flag, a.supID, s.name " +
                "from assets as a left outer join suppliers as s on s.supID = a.supID where assetID = " + id + "; ", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            asset.assetID = getIntValue(reader["assetID"]);
            asset.type = getStringValue(reader["type"]);
            asset.cost = getIntValue(reader["cost"]);
            asset.supID = getIntValue(reader["supID"]);
            asset.supName = getStringValue(reader["name"]);

            ViewData["asset"] = asset.supID;
            conn.Close();
            return View(asset);
        }

        public IActionResult DeleteUse(int empId, int supId, int assetId)
        {
            Used_by use = new Used_by();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select u.employeeID, u.assetID, u.supID, u.field, u.amount, e.Fname, e.Mname, e.Lname, a.type, s.name " +
                "from used_by as u left outer join employee as e on e.employeeID = u.employeeID left outer join assets as a on a.assetID = u.assetID " +
                "left outer join suppliers as s on s.supID = u.supID where u.employeeID = " + empId + " and " +
                "u.supID = " + supId + " and u.assetID = " + assetId + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            use.employeeID = getIntValue(reader["employeeID"]);
            use.supID = getIntValue(reader["supID"]);
            use.assetID = getIntValue(reader["assetID"]);
            use.field = getStringValue(reader["field"]);
            string Fname = getStringValue(reader["Fname"]);
            string Mname = getStringValue(reader["Mname"]);
            string Lname = getStringValue(reader["Lname"]);
            use.Fname = Fname;
            use.Mname = Mname;
            use.Lname = Lname;
            use.fullName = Fname + " " + Mname + " " + Lname;
            use.supName = getStringValue(reader["name"]);
            use.assetName = getStringValue(reader["type"]);
            use.amount = getIntValue(reader["amount"]);
            ViewData["use"] = empId;
            conn.Close();
            return View(use);

        }

        public IActionResult DeleteLocation(int id, string name)
        {
            Dep_locations location = new Dep_locations();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select d.depName, l.loc_name, l.depID from dep_locations as l left outer join department as d on d.depID = l.depID" +
               " where l.depID = " + id + " and l.loc_name = @name;", conn);
            cmd.Parameters.AddWithValue("@name", name);
            var reader = cmd.ExecuteReader();
            reader.Read();
            location.depID = getIntValue(reader["depID"]);
            location.loc_name = getStringValue(reader["loc_name"]);
            location.depName = getStringValue(reader["depName"]);
            location.pastDepID = getIntValue(reader["depID"]);
            location.pastLoc_name = getStringValue(reader["loc_name"]);
            conn.Close();
            return View(location);
        }

        public IActionResult CostReport()
        {
            ReportForm reportForm = new ReportForm();
            reportForm.projects = getProjects();
            return View(reportForm);
        }

        public IActionResult AssetReport()
        {
            string depID = HttpContext.Session.GetString("depID");
            int department = Convert.ToInt32(depID);
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select depName from department where depID = " + department + ";", conn);
            var reader = cmd.ExecuteReader();
            string name = "";
            if(reader.Read())
            {
                name = getStringValue(reader["depName"]);
            }
            reader.Close();
            conn.Close();
            AssetReport model = new AssetReport();
            model.employeeAssets = GetEmployeeAssets(department);
            model.departmentAssets = GetDepartmentAssets(department);
            model.employeeTotal = 0;
            model.departmentTotal = 0;
            foreach (var item in model.employeeAssets)
            {
                model.employeeTotal += item.total;
            }
            foreach (var item in model.departmentAssets)
            {
                model.departmentTotal += item.total;
            }
            string msg = "Asset Info for department " + depID + ": " + name;
            model.total = model.departmentTotal + model.employeeTotal;
            ViewData["AssetInfo"] = msg;
            return View(model);
        }

        public IActionResult LoginDetails()
        {
            var login = new Login();
            string empID = HttpContext.Session.GetString("ManagerID");
            int id = Convert.ToInt32(empID);
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select l.username, l.user_password, l.user_privilege, l.employeeID from employee as e, login as l where l.employeeID = e.employeeID and e.employeeID ='" + id + "'", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();

            login.ID = getIntValue(reader["employeeID"]);
            login.username = getStringValue(reader["username"]);
            login.password = getStringValue(reader["user_password"]);
            login.privilege = getStringValue(reader["user_privilege"]);
            conn.Close();
            return View(login);

        }

        public IActionResult ProgressReport()
        {
            ReportForm reportForm = new ReportForm();
            reportForm.projects = getProjects();
            return View(reportForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProgressReport(ReportForm obj)
        {
            
            string depID = HttpContext.Session.GetString("depID");
            int department = Convert.ToInt32(depID);
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            if (ModelState.IsValid)
            {                
                ProgressReport report = GetProgressReport(obj.projID);
                string query = "select projName, projStatus, dueDate, cost from project where projID = " + obj.projID + ";";
                cmd.CommandText = query;
                cmd.Connection = conn;
                var reader = cmd.ExecuteReader();
                reader.Read();
                string msg = getStringValue(reader["projName"]);
                decimal status = Convert.ToDecimal(reader["projStatus"]);
                int cost = getIntValue(reader["cost"]);
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

                ViewData["ProjectInfo"] = msg;
                report.status = status;
                report.dueDate = sqlDate;
                report.budget = cost;

                conn.Close();

                return View("ProgressReportList", report);
            }
            else
            {
                conn.Close();
                return View();
            }

        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CostReport(ReportForm obj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            
            if(ModelState.IsValid)
            {
                CostReport report = GetCostReport(obj.projID);
                string query = "select projName, cost from project where projID = " + obj.projID + ";";
                cmd.CommandText = query;
                cmd.Connection = conn;
                var reader = cmd.ExecuteReader();
                reader.Read();               
                string msg = "Project Details for " + getStringValue(reader["projName"]);
                report.projectBudget = getIntValue(reader["cost"]);

                conn.Close();

                ViewData["ProjectInfo"] = msg;

                ViewData["project"] = obj.projID;
                decimal total = report.projectBudget - report.taskCost;
                report.remaining = total;
                report.totalRemaining = total - report.employeeSalary;
                return View("CostReportList", report);
            }
            else
            {
                conn.Close();
                return View(obj);
            }
                                        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmployee(Employee obj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            
            if (ModelState.IsValid)
            {
                try
                {
                    string depID = HttpContext.Session.GetString("depID");
                    int department = Convert.ToInt32(depID);
                    MySqlCommand insert = new MySqlCommand();
                    string query = "insert into employee(Fname, Mname, Lname, salary, address, birthDate, sex, roleID, superID, depID) " +
                        "Values( @Fname, @Mname, @Lname, @salary , @address, @birthdate, @sex " +
                        ", @roleId, @superID, @depId);";
                    insert.CommandText = query;
                    insert.Parameters.AddWithValue("@Fname", obj.Fname);
                    if (string.IsNullOrEmpty(obj.Mname))
                    {
                        insert.Parameters.AddWithValue("@Mname", DBNull.Value);
                    }
                    else
                    {
                        insert.Parameters.AddWithValue("@Mname", obj.Mname);
                    }
                    insert.Parameters.AddWithValue("@Lname", obj.Lname);
                    insert.Parameters.AddWithValue("@sex", obj.Sex);
                    insert.Parameters.AddWithValue("@birthdate", obj.BirthDate);
                    insert.Parameters.AddWithValue("@salary", obj.Salary);
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
                catch(MySqlException e)
                {
                    conn.Close();
                    string depID = "Adding Employee into Department number " + HttpContext.Session.GetString("depID");
                    ViewData["AddInfo"] = depID;
                    ModelState.AddModelError("superID", e.Message);
                    obj.Roles = getRoles();                    
                    obj.supervisors = getSupervisors(Convert.ToInt32(depID), -1);
                    return View(obj);
                }
            }
            else
            {
                conn.Close();
                string depID = "Adding Employee into Department number " + HttpContext.Session.GetString("depID");
                ViewData["AddInfo"] = depID;
                obj.Roles = getRoles();               
                obj.supervisors = getSupervisors(Convert.ToInt32(depID), -1);
                return View(obj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditEmployee(Employee employee)
        {

            MySqlConnection conn = GetConnection();
            conn.Open();
            
            if (ModelState.IsValid)
            {
                try
                {
                    string query = "UPDATE employee SET salary=@salary," +
                    " roleId=@roleId, superID=@superID where employeeID = " + employee.ID + ";";

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@salary", employee.Salary);
                    if (employee.RoleID == 0)
                    {
                        cmd.Parameters.AddWithValue("@roleId", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@roleId", employee.RoleID);
                    }

                    if (employee.SuperID == 0)
                    {
                        cmd.Parameters.AddWithValue("@superID", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@superID", employee.SuperID);
                    }

                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    TempData["success"] = "Employee edited successfully";
                    conn.Close();
                    return RedirectToAction("Index");
                }
                catch(MySqlException e)
                {
                    ModelState.AddModelError("SuperID", e.Message);
                    conn.Close();
                    employee.Roles = getRoles();                    
                    employee.supervisors = getSupervisors(employee.DepID, employee.ID);
                    return View(employee);
                }

            }
            else
            {
                conn.Close();
                employee.Roles = getRoles();                
                employee.supervisors = getSupervisors(employee.DepID, employee.ID);
                return View(employee);
            }            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult selfEdit(Employee employee)
        {

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd2 = new MySqlCommand("select employee.ssn from employee where employee.employeeID != " + employee.ID + " and employee.ssn = " + employee.Ssn + "; ", conn);
            var reader = cmd2.ExecuteReader();

            if (reader.Read())
            {
                ModelState.AddModelError("Ssn", "Invalidss SSN");
            }
            if (employee.Ssn != 0)
            {
                Regex regex = new Regex("[0-9]{9}$");
                Match match = regex.Match(employee.Ssn.ToString());
                if (!match.Success)
                {
                    ModelState.AddModelError("Ssn", "Invalid SSN");
                }
            }
            reader.Close();            
            if (ModelState.IsValid)
            {
                try
                {
                    string query = "UPDATE employee SET Fname=@Fname, Mname=@Mname, Lname=@Lname, sex=@sex , birthdate=@birthdate , salary=@salary, ssn=@ssn, address=@address " +
                    ", roleId=@roleId, superID=@superID where employeeID = " + employee.ID + ";";

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@Fname", employee.Fname);
                    cmd.Parameters.AddWithValue("@Mname", employee.Mname);
                    cmd.Parameters.AddWithValue("@Lname", employee.Lname);
                    cmd.Parameters.AddWithValue("@sex", employee.Sex);
                    if (employee.Ssn == 0)
                    {
                        cmd.Parameters.AddWithValue("@ssn", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ssn", employee.Ssn);
                    }
                    cmd.Parameters.AddWithValue("@birthdate", employee.BirthDate);
                    cmd.Parameters.AddWithValue("@salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@address", employee.Address);                  
                    if (employee.RoleID == 0)
                    {
                        cmd.Parameters.AddWithValue("@roleId", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@roleId", employee.RoleID);
                    }

                    if (employee.SuperID == 0)
                    {
                        cmd.Parameters.AddWithValue("@superID", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@superID", employee.SuperID);
                    }
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    TempData["success"] = "Employee edited successfully";
                    conn.Close();
                    return RedirectToAction("Index");
                }
                catch (MySqlException e)
                {
                    ModelState.AddModelError("SuperID", e.Message);
                    conn.Close();
                    employee.Roles = getRoles();
                    employee.supervisors = getSupervisors(employee.DepID, employee.ID);
                    return View(employee);
                }

            }

            conn.Close();
            employee.Roles = getRoles();
            employee.supervisors = getSupervisors(employee.DepID, employee.ID);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProject(Project obj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            int department = Convert.ToInt32(HttpContext.Session.GetString("depID"));
            string query = "select projName from project where projName = @name;";
            cmd.Parameters.AddWithValue("@name", obj.projName);
            cmd.CommandText = query;
            cmd.Connection = conn;
            var reader = cmd.ExecuteReader();          
            if (reader.HasRows)
            {
                ModelState.AddModelError("projName", "Duplicate project name not allowed");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                string depID = HttpContext.Session.GetString("depID");                
                query = "insert into project (dueDate, projName, location, cost, field, projStatus, depID) VALUES (@date, @projName , @location " +
                       ", @cost, @field, @projStatus, @department);";
                
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@date", obj.dueDate);
                cmd.Parameters.AddWithValue("@projName", obj.projName);
                cmd.Parameters.AddWithValue("@cost", obj.cost);
                if (obj.location == "0")
                {
                    cmd.Parameters.AddWithValue("@location", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@location", obj.location);
                }
                cmd.Parameters.AddWithValue("@field", obj.field);
                cmd.Parameters.AddWithValue("@projStatus", obj.projStatus);
                cmd.Parameters.AddWithValue("@department", department);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["success"] = "Project succesfully added";
                return RedirectToAction("Index");
            }
            else
            {
                conn.Close();
                string depID = "Adding Project into Department number " + HttpContext.Session.GetString("depID");
                ViewData["AddInfo"] = depID;
                obj.locations = getLocations(department);
                return View(obj);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProject(Project proj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            string depID = HttpContext.Session.GetString("depID");
            int department = Convert.ToInt32(depID);
            string query = "select projName from project where projName = @name and projID != " + proj.projID + ";";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@name", proj.projName);
            cmd.Connection = conn;
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ModelState.AddModelError("projName", "Duplicate project name not allowed");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                query = "UPDATE project SET dueDate=@dueDate, projName=@projName, location=@location, cost=@cost, field=@field, " +
                    "projStatus=@projStatus, depID=@depID where projID = " + proj.projID + ";";

                MySqlCommand cmd2 = new MySqlCommand();              
                cmd2.CommandText = query;
                cmd2.Parameters.AddWithValue("@projName", proj.projName);
                cmd2.Parameters.AddWithValue("@dueDate", proj.dueDate);
                cmd2.Parameters.AddWithValue("@depID", department);
                if (proj.location == "0")
                {
                    cmd2.Parameters.AddWithValue("@location", DBNull.Value);
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@location", proj.location);
                }
                cmd2.Parameters.AddWithValue("@cost", proj.cost);
                cmd2.Parameters.AddWithValue("@field", proj.field);
                cmd2.Parameters.AddWithValue("@projStatus", proj.projStatus);

                cmd2.Connection = conn;
                cmd2.ExecuteNonQuery();
                conn.Close();
                TempData["success"] = "Project edited successfully";
                return RedirectToAction("Index");
            }
            else
            {                
                conn.Close();
                proj.locations = getLocations(department);
                return View(proj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProject(Project proj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            string query = "select projID from task where projID = @id;";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", proj.projID);
            cmd.Connection = conn;
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ModelState.AddModelError("projID", "project still involed with tasks");
            }
            reader.Close();
            if(ModelState.IsValid)
            {
                try
                {
                    cmd = new MySqlCommand("select project.deleted_flag from project where project.projID = " + proj.projID + ";", conn);
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    int flag = getIntValue(reader["deleted_flag"]);
                    reader.Close();

                    query = "UPDATE project SET deleted_flag=@deleted_flag where projID = " + proj.projID + ";";
                    cmd.CommandText = query;
                    if (flag == 1)
                    {
                        cmd.Parameters.AddWithValue("@deleted_flag", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@deleted_flag", 1);
                    }
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return RedirectToAction("Index");
                }
                catch(MySqlException e)
                {
                    ModelState.AddModelError("projID", e.Message);
                    conn.Close();
                    return View(proj);
                }
            }
            else
            {
                conn.Close();
                return View(proj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTask(Tasks obj)
        {
            int department = Convert.ToInt32(HttpContext.Session.GetString("depID"));
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select projID from project where projID = @projName or projName = @projName", conn);
            cmd.Parameters.AddWithValue("@projName", obj.projName);
            var reader = cmd.ExecuteReader();

            if (!reader.HasRows && !string.IsNullOrEmpty(obj.projName))
            {
                ModelState.AddModelError("projName", "Project doesn't exist");
            }
            else if (reader.HasRows)
            {
                reader.Read();
                obj.projID = getIntValue(reader["projID"]);
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                try
                {
                    string query = "insert into task (taskName, cost, taskDueDate, projID, status) " +
                        "VALUES (@taskName, @cost, @date, @projID, @status);";

                    MySqlCommand cmd2 = new MySqlCommand();

                    cmd2.CommandText = query;
                    cmd2.Parameters.AddWithValue("@taskName", obj.taskName);
                    cmd2.Parameters.AddWithValue("@cost", obj.cost);
                    cmd2.Parameters.AddWithValue("@date", obj.taskDueDate);
                    cmd2.Parameters.AddWithValue("@status", obj.status);
                    if (obj.projID == 0)
                    {
                        cmd2.Parameters.AddWithValue("@projID", DBNull.Value);
                    }
                    else
                    {
                        cmd2.Parameters.AddWithValue("@projID", obj.projID);
                    }
                    cmd2.Connection = conn;
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    TempData["success"] = "Task succesfully added";
                    return RedirectToAction("Index");
                }
                catch(MySqlException e)
                {
                    ModelState.AddModelError("projID", e.Message);
                    string info = "Adding Task for Department number " + department;
                    ViewData["AddInfo"] = info;
                    conn.Close();
                    obj.projects = getProjects();
                    return View(obj);
                }
               
            }
            else
            {
                conn.Close();
                string info = "Adding Task for Department number " + department;
                ViewData["AddInfo"] = info;
                obj.projects = getProjects();
                return View(obj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTask(Tasks task)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select projID from project where projID = " + task.projID + "; ", conn);
            string depID = HttpContext.Session.GetString("depID");
            int department = Convert.ToInt32(depID);
           
            if (ModelState.IsValid)
            {
                try
                {
                    string query = "UPDATE task SET taskName=@taskName, cost=@cost, taskDueDate=@date, projID=@projID, status=@status where taskID = " + task.taskID + ";";
                    MySqlCommand cmd2 = new MySqlCommand();

                    cmd2.CommandText = query;
                    cmd2.Parameters.AddWithValue("@taskName", task.taskName);
                    cmd2.Parameters.AddWithValue("@cost", task.cost);
                    cmd2.Parameters.AddWithValue("@date", task.taskDueDate);
                    cmd2.Parameters.AddWithValue("@projID", task.projID);
                    cmd2.Parameters.AddWithValue("@status", task.status);
                    cmd2.Connection = conn;
                    cmd2.ExecuteNonQuery();

                    conn.Close();
                    TempData["success"] = "Task edited";
                    return RedirectToAction("Index");
                }
                catch(MySqlException e)
                {
                    conn.Close();
                    ModelState.AddModelError("projID", e.Message);
                    task.projects = getProjects();
                    return View(task);
                }                
            }
            else
            {
                conn.Close();
                task.projects = getProjects();
                return View(task);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTask(Tasks task)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            string query = "select taskID from works_on where taskID = @id;";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", task.taskID);
            cmd.Connection = conn;
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ModelState.AddModelError("taskID", "project still involed with works_on");
            }
            reader.Close();
            if(ModelState.IsValid)
            {
                try
                {
                    cmd = new MySqlCommand("select task.deleted_flag from task where task.taskID = " + task.taskID + ";", conn);
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    int flag = getIntValue(reader["deleted_flag"]);
                    reader.Close();
                    query = "UPDATE task SET deleted_flag=@deleted_flag where taskID = " + task.taskID + ";";
                    cmd.CommandText = query;
                    if (flag == 1)
                    {
                        cmd.Parameters.AddWithValue("@deleted_flag", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@deleted_flag", 1);
                    }
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return RedirectToAction("Index");
                }
                catch(MySqlException e)
                {
                    ModelState.AddModelError("taskID", e.Message);
                    conn.Close();
                    return View(task);
                }
            }
            else
            {
                conn.Close();
                return View(task);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSupplier(Supplier obj)
        {
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand();
            conn.Open();
            string test = "select name from suppliers where name = @supName";
            cmd.CommandText = test;
            cmd.Parameters.AddWithValue("@supName", obj.name);
            cmd.Connection = conn;
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ModelState.AddModelError("name", "supplier name already exists");
            }
            reader.Close();

            if (ModelState.IsValid)
            {

                string query;
                query = "insert into suppliers(product, name, roleID) Values(@product, @name, @roleID);";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@product", obj.product);
                cmd.Parameters.AddWithValue("@name", obj.name);
                if (obj.roleID == 0)
                {
                    cmd.Parameters.AddWithValue("@roleID", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@roleID", obj.roleID);
                }                
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["success"] = "Supplier succesfully added";
                return RedirectToAction("Index");
            }
            else
            {
                conn.Close();
                obj.Roles = getRoles();
                return View(obj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSupplier(Supplier sup)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            string test = "select name from suppliers where name = @supName and supID != " + sup.supID + ";";
            cmd.CommandText = test;
            cmd.Parameters.AddWithValue("@supName", sup.name);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ModelState.AddModelError("name", "supplier name already exists");
            }
            reader.Close();

            if (ModelState.IsValid)
            {
                string query = "UPDATE suppliers SET product=@product, name=@name, roleID=@roleID where supID = " + sup.supID + ";";

                MySqlCommand cmd2 = new MySqlCommand();

                cmd2.CommandText = query;
                cmd2.Parameters.AddWithValue("@product", sup.product);
                cmd2.Parameters.AddWithValue("@name", sup.name);
                if (sup.roleID == 0)
                {
                    cmd2.Parameters.AddWithValue("@roleID", DBNull.Value);
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@roleID", sup.roleID);
                }
                cmd2.Connection = conn;
                cmd2.ExecuteNonQuery();
                conn.Close();
                TempData["success"] = "Supplier edited successfully";
                return RedirectToAction("Index");
            }
            else
            {
                conn.Close();
                sup.Roles = getRoles();
                return View(sup);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSupplier(Supplier sup)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            string query = "select supID from assets where supID = @id union select supID from used_by where supID = @id" +
                " union select supID from distributed_to where supID = @id;";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", sup.supID);
            cmd.Connection = conn;
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ModelState.AddModelError("supID", "supplier still involed with task and/or distribution");
            }
            reader.Close();
            if(ModelState.IsValid)
            {
                cmd = new MySqlCommand("select suppliers.deleted_flag from suppliers where suppliers.supID = " + sup.supID + ";", conn);
                reader = cmd.ExecuteReader();
                reader.Read();
                int flag = getIntValue(reader["deleted_flag"]);
                reader.Close();
                query = "UPDATE suppliers SET deleted_flag=@deleted_flag where supID = " + sup.supID + ";";
                cmd.CommandText = query;
                if (flag == 1)
                {
                    cmd.Parameters.AddWithValue("@deleted_flag", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@deleted_flag", 1);
                }
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                return RedirectToAction("Index");
            }
            else
            {
                conn.Close();
                return View(sup);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRole(Role obj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();

            string query = "select roleName from roles where roleName = @roleName ;";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@roleName", obj.roleName);
            cmd.Connection = conn;

            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ModelState.AddModelError("roleName", "Duplicate role name not allowed");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                query = "insert into roles(roleName) VALUES (@Name);";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@Name", obj.roleName);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["success"] = "Role succesfully added";
                return RedirectToAction("Index");
            }
            else
            {
                conn.Close();
                return View(obj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRole(Role role)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();

            string query = "select roleName from roles where roleName = @roleName and roleID != " + role.roleID + ";";
            cmd.Parameters.AddWithValue("@roleName", role.roleName);
            cmd.CommandText = query;
            cmd.Connection = conn;

            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ModelState.AddModelError("roleName", "Duplicate role name not allowed");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                query = "UPDATE roles SET roleName=@roleName where roleID = " + role.roleID + ";";
                MySqlCommand cmd2 = new MySqlCommand();

                cmd2.CommandText = query;
                cmd2.Parameters.AddWithValue("@roleName", role.roleName);
                cmd2.Connection = conn;
                cmd2.ExecuteNonQuery();

                conn.Close();
                TempData["success"] = "Role edited added";
                return RedirectToAction("Index");
            }
            else
            {
                conn.Close();
                return View(role);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRole(Role role)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();

            string query = "select e.roleID from employee as e where e.roleID = " + role.roleID +
                " Union select s.roleID from suppliers as s where s.roleID = " + role.roleID + ";";
            cmd.CommandText = query;
            cmd.Connection = conn;
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ModelState.AddModelError("roleID", "Employees or Suppliers still assigned this role");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                query = "delete from roles where roleID = " + role.roleID + ";";
                MySqlCommand cmd2 = new MySqlCommand();

                cmd2.CommandText = query;
                cmd2.Connection = conn;
                cmd2.ExecuteNonQuery();

                conn.Close();
                TempData["success"] = "Role successfully deleted";
                return RedirectToAction("Index");
            }
            else
            {
                conn.Close();
                return View(role);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAsset(Asset obj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();

            if (ModelState.IsValid)
            {               
                string query = "insert into assets (type, cost, supID) VALUES (@type, @cost, @supID);";
                cmd.CommandText = query;               
                cmd.Parameters.AddWithValue("@type", obj.type);
                cmd.Parameters.AddWithValue("@cost", obj.cost);          
                cmd.Parameters.AddWithValue("@supID", obj.supID);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["success"] = "Asset succesfully added";
                return RedirectToAction("SupplierDetails", new { id = obj.supID });
            }
            else
            {
                conn.Close();
                ViewData["asset"] = obj.supID;               
                return View(obj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAsset(Asset asset)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
           
            if (ModelState.IsValid)
            {
                string query = "UPDATE assets SET type=@type, cost=@cost, supID=@supID where assetID = " + asset.assetID + ";";
                MySqlCommand cmd2 = new MySqlCommand();

                cmd2.CommandText = query;
                cmd2.Parameters.AddWithValue("@type", asset.type);
                cmd2.Parameters.AddWithValue("@cost", asset.cost);                
                cmd2.Parameters.AddWithValue("@supID", asset.supID);                
                cmd2.Connection = conn;
                cmd2.ExecuteNonQuery();

                conn.Close();
                TempData["success"] = "Asset edited added";
                return RedirectToAction("SupplierDetails", new { id = asset.supID });
            }
            else
            {
                conn.Close();                
                return View(asset);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAsset(Asset asset)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select assets.deleted_flag from assets where assets.assetID = " + asset.assetID + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            int flag = getIntValue(reader["deleted_flag"]);
            reader.Close();

            string query = "UPDATE assets SET deleted_flag=@deleted_flag where assetID = " + asset.assetID + ";";
            cmd.CommandText = query;
            if (flag == 1)
            {
                cmd.Parameters.AddWithValue("@deleted_flag", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@deleted_flag", 1);
            }
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("SupplierDetails", new { id = asset.supID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDistribution(Distributed_to distribution)
        {
            MySqlConnection conn = GetConnection();
            if (ModelState.IsValid)
            {
                conn.Open();
                string query = "UPDATE distributed_to SET field=@field, amount=@amount WHERE depID = '" + distribution.tempDepID + "' " +
                    "and supID = " + distribution.tempSupID + " and assetID = " + distribution.tempAssetID + ";";
                MySqlCommand cmd2 = new MySqlCommand();

                cmd2.CommandText = query;             
                cmd2.Parameters.AddWithValue("@field", distribution.field);
                cmd2.Parameters.AddWithValue("@amount", distribution.amount);
                cmd2.Connection = conn;
                cmd2.ExecuteNonQuery();

                conn.Close();
                TempData["success"] = "Distribution successfully edited";
                return RedirectToAction("SupplierDetails", new { id=distribution.supID });
            }
            else
            {
                conn.Close();
                ViewData["supplier"] = distribution.supID;
                return View(distribution);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteDistribution(Distributed_to distribution)
        {

            MySqlConnection conn = GetConnection();
            conn.Open();
            string query = "delete from distributed_to where depID = " + distribution.depID + " and " +
                "supID = " + distribution.supID + " and assetID = " + distribution.assetID + ";";
            MySqlCommand cmd2 = new MySqlCommand();

            cmd2.CommandText = query;
            cmd2.Connection = conn;
            cmd2.ExecuteNonQuery();

            conn.Close();
            TempData["success"] = "Distribution successfully deleted";
            return RedirectToAction("SupplierDetails", new { id = distribution.supID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RequestAssets(Distributed_to obj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            string test = "select * from distributed_to where depID = " + obj.depID + " and supID = " + obj.supID + " and assetID = " + obj.assetID + ";";
            cmd.CommandText = test;
            cmd.Connection = conn;
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ModelState.AddModelError("field", "Asset ID, Department ID, and Supplier ID already exist");
            }
            reader.Close();
            test = "select assetID from assets where supID = " + obj.supID + " and assetID = " + obj.assetID + ";";
            cmd.CommandText = test;
            reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ModelState.AddModelError("assetID", "assetID not associated with supplier");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                string query = "insert into distributed_to (depID, supID, assetID, field, amount) VALUES ('" + obj.depID +
                    "', '" + obj.supID + "', '" + obj.assetID + "', @field, @amount);"; ;
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@field", obj.field);
                cmd.Parameters.AddWithValue("@amount", obj.amount);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["success"] = "Distribution succesfully added";
                return RedirectToAction("SupplierDetails", new { id = obj.supID });
            }
            else
            {
                conn.Close();
                ViewData["asset"] = obj.supID;
                obj.assets = getSpecificAssets(obj.supID);               
                return View(obj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUse(Used_by obj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            string test = "select * from used_by where employeeID = " + obj.employeeID + " and supID = " + obj.supID + " and assetID = " + obj.assetID + ";";
            cmd.CommandText = test;
            cmd.Connection = conn;
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ModelState.AddModelError("field", "Asset ID, Employee ID, and Supplier ID already exist");
            }
            reader.Close();
            test = "select assetID from assets where supID = " + obj.supID + " and assetID = " + obj.assetID + ";";
            cmd.CommandText = test;
            reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ModelState.AddModelError("assetID", "Asset not associated with supplier");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                string query = "insert into used_by (employeeID, supID, assetID, field, amount) VALUES ('" + obj.employeeID +
                    "', '" + obj.supID + "', '" + obj.assetID + "', @field, @amount);"; ;
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@field", obj.field);
                cmd.Parameters.AddWithValue("@amount", obj.amount);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["success"] = "Used By succesfully added";
                return RedirectToAction("EmployeeDetails", new { id = obj.employeeID });
            }
            else
            {
                conn.Close();
                ViewData["use"] = obj.employeeID;
                obj.suppliers = getSuppliers();
                obj.assets = getAssets();
                return View(obj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUse(Used_by use)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            string test = "select employeeID, supID, assetID from used_by where employeeID = '" + use.employeeID + "' " +
                    "and supID = " + use.supID + " and assetID = " + use.assetID + ";";
            cmd.CommandText = test;
            cmd.Connection = conn;
            var reader = cmd.ExecuteReader();
            if (reader.HasRows && (use.assetID != use.tempassetID || use.employeeID != use.tempemployeeID || use.supID != use.tempsupID))
            {
                ModelState.AddModelError("field", "Employee ID, Supplier ID, and Asset ID already exist");
            }
            reader.Close();
            test = "select assetID from assets where supID = " + use.supID + " and assetID = " + use.assetID + ";";
            cmd.CommandText = test;
            reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ModelState.AddModelError("assetID", "Asset not associated with supplier");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                string query = "UPDATE used_by SET employeeID=@emp, supID=@sup, assetID=@asset, field=@field, amount=@amount WHERE employeeID = '" + use.tempemployeeID + "' " +
                    "and supID = " + use.tempsupID + " and assetID = " + use.tempassetID + ";";
                MySqlCommand cmd2 = new MySqlCommand();

                cmd2.CommandText = query;
                cmd2.Parameters.AddWithValue("@emp", use.employeeID);
                cmd2.Parameters.AddWithValue("@sup", use.supID);
                cmd2.Parameters.AddWithValue("@asset", use.assetID);
                cmd2.Parameters.AddWithValue("@field", use.field);
                cmd2.Parameters.AddWithValue("@amount", use.amount);
                cmd2.Connection = conn;
                cmd2.ExecuteNonQuery();

                conn.Close();
                TempData["success"] = "Used By successfully edited";
                return RedirectToAction("EmployeeDetails", new { id = use.employeeID });
            }
            else
            {
                conn.Close();
                ViewData["use"] = use.employeeID;
                use.suppliers = getSuppliers();
                use.assets = getAssets();
                return View(use);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteUse(Used_by use)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();

            string query = "delete from used_by where employeeID = " + use.employeeID + " and supID = " + use.supID + " and assetID = " + use.assetID + ";";
            MySqlCommand cmd2 = new MySqlCommand();

            cmd2.CommandText = query;
            cmd2.Connection = conn;
            cmd2.ExecuteNonQuery();

            conn.Close();
            TempData["success"] = "Used_by successfully deleted";
            return RedirectToAction("EmployeeDetails", new { id = use.employeeID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddLocations(Dep_locations obj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();

            string depID = HttpContext.Session.GetString("depID");
            int department = Convert.ToInt32(depID);

            string query = "select loc_name from dep_locations where loc_name = @name and depID = " + department + ";";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@name", obj.loc_name);
            cmd.Connection = conn;
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ModelState.AddModelError("loc_name", "Department ID and Location name already exist");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                query = "insert into dep_locations (loc_name, depID) VALUES (@loc_name, '" + department + "');"; ;

                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@loc_name", obj.loc_name);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["success"] = "Location succesfully added";
                return RedirectToAction("Index");
            }
            else
            {
                conn.Close();
                string info = "Adding Location into Department number " + department;
                ViewData["AddInfo"] = info;
                return View(obj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditLocation(Dep_locations location)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            string test = "select depID, loc_name from dep_locations where loc_name = @lname " +
                    "and depID = " + location.depID + ";";
            cmd.CommandText = test;
            cmd.Parameters.AddWithValue("@lname", location.loc_name);
            cmd.Connection = conn;
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                string name = getStringValue(reader["loc_name"]);
                int id = getIntValue(reader["depID"]);
                if (name != location.pastLoc_name || id != location.pastDepID)
                {
                    ModelState.AddModelError("loc_name", "Department and Location name already exist");
                }
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                string query = "UPDATE dep_locations SET loc_name = @name, depID = @depID  WHERE loc_name = @pastName " +
                    "and depID = " + location.pastDepID + ";";
                MySqlCommand cmd2 = new MySqlCommand();

                cmd2.CommandText = query;
                cmd2.Parameters.AddWithValue("@depID", location.depID);
                cmd2.Parameters.AddWithValue("@name", location.loc_name);
                cmd2.Parameters.AddWithValue("@pastName", location.pastLoc_name);
                cmd2.Connection = conn;
                cmd2.ExecuteNonQuery();

                conn.Close();
                TempData["success"] = "Location successfully edited";
                return RedirectToAction("Index");
            }
            else
            {
                conn.Close();
                return View(location);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteLocation(Dep_locations location)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            string query = "select location from department where location = @location " +
                "Union select location from project where location = @location";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@location", location.loc_name);
            cmd.Connection = conn;
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ModelState.AddModelError("depName", "Department or Project still assigned this location");
            }
            reader.Close();

            if (ModelState.IsValid)
            {
                try
                {
                    query = "delete from dep_locations where depID = " + location.depID + " and loc_name = @name;";

                    MySqlCommand cmd2 = new MySqlCommand();
                    cmd2.Parameters.AddWithValue("@name", location.loc_name);
                    cmd2.CommandText = query;
                    cmd2.Connection = conn;
                    cmd2.ExecuteNonQuery();

                    conn.Close();
                    TempData["success"] = "Location successfully deleted";
                    return RedirectToAction("Index");
                }
                catch (MySqlException e)
                {
                    ModelState.AddModelError("depName", e.Message);
                    return View(location);
                }
            }
            else
            {
                return View(location);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FireEmployee(Employee employee)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            string query = "select superID from employee where superID = @id union select mgrID from department where mgrID = @id " +
                "union select employeeID from works_on where employeeID = @id union select employeeID from used_by where employeeID = @id;";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", employee.ID);
            cmd.Connection = conn;
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ModelState.AddModelError("ID", "Employee still involed with supervisor, manager, works_on, and/or used_by");
            }
            reader.Close();

            if(ModelState.IsValid)
            {
                try
                {
                    cmd = new MySqlCommand("select employee.deleted_flag from employee where employee.employeeID = " + employee.ID + ";", conn);
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    int flag = getIntValue(reader["deleted_flag"]);
                    reader.Close();

                    query = "UPDATE employee SET deleted_flag=@deleted_flag where employeeID = " + employee.ID + ";";
                    cmd.CommandText = query;
                    if (flag == 1)
                    {
                        cmd.Parameters.AddWithValue("@deleted_flag", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@deleted_flag", 1);
                    }
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return RedirectToAction("Index");
                }
                catch(MySqlException e)
                {
                    ModelState.AddModelError("employeeID", e.Message);
                    conn.Close();
                    return View(employee);
                }
            }
            else
            {
                conn.Close();
                return View(employee);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Unlist(Works_on work)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();

            string query = "delete from works_on where employeeID = " + work.employeeID + " and taskID = '" + work.TaskID + "';";
            MySqlCommand cmd2 = new MySqlCommand();

            cmd2.CommandText = query;
            cmd2.Connection = conn;
            cmd2.ExecuteNonQuery();

            conn.Close();
            TempData["success"] = "Works_on successfully deleted";
            return RedirectToAction("EmployeeDetails", new { id = work.employeeID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Enlist(Works_on obj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            string test = "select * from works_on where employeeID = " + obj.employeeID + " and taskID = " + obj.TaskID + ";";
            cmd.CommandText = test;
            cmd.Connection = conn;
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ModelState.AddModelError("hours", "Task ID and Employee ID already exist");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                try
                {
                    string query = "insert into works_on (employeeID, taskID, hours) VALUES ('" + obj.employeeID +
                    "', '" + obj.TaskID + "', '" + obj.hours + "');";
                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    TempData["success"] = "Works On succesfully added";
                    return RedirectToAction("EmployeeDetails", new { id = obj.employeeID });
                }
                catch(MySqlException e)
                {
                    conn.Close();
                    ModelState.AddModelError("taskID", e.Message);
                    ViewData["employee"] = obj.employeeID;
                    obj.tasks = getTasks();
                    return View(obj);
                }
            }
            else
            {
                conn.Close();
                ViewData["employee"] = obj.employeeID;
                obj.tasks = getTasks();
                return View(obj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginDetails(Login login)
        {
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand();
            conn.Open();
            if (login.privilege != "Admin" && login.privilege != "Employee" && login.privilege != "Manager")
            {
                ModelState.AddModelError("privilege", "Privilege must be either Admin, Employee, or Manager");
            }
            string query = "select username from login where username = '" + login.username + "' and employeeID != " + login.ID + " ;";
            cmd.CommandText = query;
            cmd.Connection = conn;
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ModelState.AddModelError("username", "Username already exists");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                var salt = DateTime.Now.ToString();
                var hash = HashPassword($"{login.password}{salt}");

                query = "UPDATE login SET username=@username, user_password=@password, user_privilege=@priv, hash=@hash, salt=@salt where employeeID = " + login.ID + ";";
                MySqlCommand update = new MySqlCommand();
                update.CommandText = query;
                update.Parameters.AddWithValue("@username", login.username);
                update.Parameters.AddWithValue("@password", login.password);
                update.Parameters.AddWithValue("@priv", login.privilege);
                update.Parameters.AddWithValue("@hash", hash);
                update.Parameters.AddWithValue("@salt", salt);

                update.Connection = conn;
                update.ExecuteNonQuery();
                conn.Close();
                TempData["success"] = "Login edited successfully";
                return RedirectToAction("Index");
            }
            else
            {
                conn.Close();
                return View(login);
            }
        }

        public IEnumerable<ManagerViewModel> getViewData()
        {
            List<ManagerViewModel> data = new List<ManagerViewModel>();
            ManagerViewModel model = new ManagerViewModel();

            model.Employees = getEmployeeData();            
            model.Projects = getProjectData();
            model.tasks = getTaskData();
            model.Suppliers = getSupplierData();
            model.Roles = getRoleData();
            model.locations = getLocationsData();

            data.Add(model);

            return data;

        }

        public IEnumerable<EmployeeDetails> getEmployeeDetails(int id)
        {
            List<EmployeeDetails> data = new List<EmployeeDetails>();
            EmployeeDetails model = new EmployeeDetails();

            model.taskDetails = getTaskDetails(id);
            model.UsedBy = getUsedByData(id);
            data.Add(model);
            return data;
        }

        public IEnumerable<SupplierDetailsView> GetSupplierDetailsViews(int id)
        {
            List<SupplierDetailsView> data = new List<SupplierDetailsView>();
            SupplierDetailsView model = new SupplierDetailsView();

            model.supplierDetails = GetSupplierDetails(id);
            model.assets = getAssetsData(id);

            data.Add(model);
            return data;
        }

        public CostReport GetCostReport(int id)
        {           
            CostReport model = new CostReport();

            string depID = HttpContext.Session.GetString("depID");
            int department = Convert.ToInt32(depID);
            decimal total = 0;
            model.projectID = id;
            model.tasks = getTaskInfo(id);
            model.taskCost = model.tasks.Sum(item => item.cost);
            model.taskReports = GetTaskReports(id);
            foreach(var item in model.taskReports)
            {
                total += item.pay;
            }
            model.employeeSalary = total;            
                                   
            return model;
        }

        public ProgressReport GetProgressReport(int id)
        {           
            ProgressReport model = new ProgressReport();
            model.progress = GetEmployeeProgs(id);
            model.tasks = getTasks(id);
            model.EmployeeHours = GetEmployeeHours(id);
            model.totalHours = 0;
            foreach (var item in model.EmployeeHours)
            {
                model.totalHours += item.hours;
            }
            return model;
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
                        RoleName = getRoleName(getIntValue(reader["roleId"])),
                        DepID = getIntValue(reader["depId"]),
                        Ssn = getIntValue(reader["ssn"]),
                        Salary = getIntValue(reader["salary"]),
                        SuperID = getIntValue(reader["superID"]),
                        SupervisorName = getSuperName(getIntValue(reader["superID"]))

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

        public List<Tasks> getTaskData()
        {
            MySqlConnection conn = GetConnection();
            List<Tasks> TaskData = new List<Tasks>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select t.taskID, t.taskName, t.cost, t.taskDueDate, t.projID, t.status " +
                "from task as t, project as p where t.projID = p.projID and p.depID = " + userDepID + " and t.deleted_flag = 1;", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    DateTime date = Convert.ToDateTime(getStringValue(reader["taskDueDate"]));
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
                    TaskData.Add(new Tasks()
                    {
                        taskID = getIntValue(reader["taskID"]),
                        taskName = getStringValue(reader["taskName"]),
                        cost = getIntValue(reader["cost"]),
                        taskDueDate = sqlDate,
                        projID = getIntValue(reader["projID"]),
                        projName = getTaskProjName(getIntValue(reader["projID"])),
                        status = Convert.ToDecimal(reader["status"])

                    });
                }
            }
            conn.Close();

            return TaskData;
        }

        public string getTaskProjName(int projID)
        {
            if (projID == 0)
            {
                return "";
            }

            string taskProjName = "";
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd1 = new MySqlCommand("select projName from project " +
                "where  projID = " + projID, conn);
            var reader = cmd1.ExecuteReader();

            if (reader.Read())
            {
                taskProjName = getStringValue(reader["projName"]);
                
            }
            conn.Close();
            return taskProjName;
        }

        public List<Supplier> getSupplierData()
        {
            MySqlConnection conn = GetConnection();
            List<Supplier> SupplierData = new List<Supplier>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from suppliers where deleted_flag = 1", conn);

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
                        roleName = getRoleName(getIntValue(reader["roleID"]))
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

        public List<Asset> getAssetsData(int id)
        {
            MySqlConnection conn = GetConnection();
            List<Asset> AssetsData = new List<Asset>();
            
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from assets where deleted_flag = 1 and supID = " + id + ";", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    AssetsData.Add(new Asset()
                    {
                        assetID = getIntValue(reader["assetID"]),
                        type = getStringValue(reader["type"]),
                        cost = getIntValue(reader["cost"]),
                        supID = getIntValue(reader["supID"])                     

                    });
                }
            }
            conn.Close();

            return AssetsData;
        }

        public List<Dep_locations> getLocationsData()
        {
            MySqlConnection conn = GetConnection();
            List<Dep_locations> LocationsData = new List<Dep_locations>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from dep_locations where depID = " + userDepID + ";", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    LocationsData.Add(new Dep_locations()
                    {
                        depID = getIntValue(reader["depID"]),
                        loc_name = getStringValue(reader["loc_name"])
                    });
                }
            }
            conn.Close();

            return LocationsData;
        }

        public List<TaskDetails> getTaskDetails(int id)
        {
            List<TaskDetails> TaskData = new List<TaskDetails>();
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select w.employeeID, w.hours, t.taskName, t.cost, t.taskDueDate, p.projName, w.taskID, w.taskStatus, t.status, p.projStatus " +
                "from project as p left outer join task as t on t.projID = p.projID " +
                "left outer join works_on as w on w.taskID = t.taskID where w.employeeID = " + id + ";", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while(reader.Read())
                {
                    DateTime date = Convert.ToDateTime(getStringValue(reader["taskDueDate"]));
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

                    TaskData.Add(new TaskDetails()
                    {
                        empID = getIntValue(reader["employeeID"]),
                        projName = getStringValue(reader["projName"]),
                        dueDate = sqlDate,
                        hours = Convert.ToDecimal(reader["hours"]),
                        taskName = getStringValue(reader["taskName"]),
                        budget = getIntValue(reader["cost"]),
                        taskID = getIntValue(reader["taskID"]),
                        employeeStatus = Convert.ToDecimal(reader["taskStatus"]),
                        taskStatus = Convert.ToDecimal(reader["status"]),
                        projStatus = Convert.ToDecimal(reader["projStatus"])
                    });

                }
            }
            conn.Close();

            return TaskData;
        }

        public List<ProjectDetails> GetProjectDetails(int id)
        {
            List<ProjectDetails> ProjectData = new List<ProjectDetails>();
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select distinct e.Fname, e.Mname ,e.Lname, r.roleName, t.taskName, w.hours, w.taskStatus " +
                "from works_on as w left outer join task as t on t.taskID = w.taskID left outer join employee as e on e.employeeID = w.employeeID " +
                "left outer join roles as r on r.roleID = e.roleID where t.projID = " + id + ";", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string Fname = getStringValue(reader["Fname"]);
                    string Lname = getStringValue(reader["Lname"]);
                    string Mname = getStringValue(reader["Mname"]);
                    string name = Fname + " " + Mname + " " + Lname;
                    ProjectData.Add(new ProjectDetails()
                    {
                        name = name,
                        roleName = getStringValue(reader["roleName"]),
                        taskName = getStringValue(reader["taskName"]),                       
                        hours = Convert.ToDecimal(reader["hours"]),
                        status = Convert.ToDecimal(reader["taskStatus"])
                    });

                }
            }
            conn.Close();

            return ProjectData;
        }

        public List<TaskInformation> GetTaskInformation(int id)
        {
            List<TaskInformation> TaskInfo = new List<TaskInformation>();
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select e.Fname, e.Mname, e.Lname, w.hours, r.roleName, w.taskStatus " +
                "from works_on as w left outer join employee as e on e.employeeID = w.employeeID " +
                "left outer join roles as r on r.roleID = e.roleID where w.taskID = " + id + ";", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string Fname = getStringValue(reader["Fname"]);
                    string Lname = getStringValue(reader["Lname"]);
                    string Mname = getStringValue(reader["Mname"]);
                    string name = Fname + " " + Mname + " " + Lname;
                    TaskInfo.Add(new TaskInformation()
                    {
                        name = name,
                        roleName = getStringValue(reader["roleName"]),                      
                        hours = Convert.ToDecimal(reader["hours"]),
                        status = Convert.ToDecimal(reader["taskStatus"])
                    });

                }
            }
            conn.Close();

            return TaskInfo;
        }

        public List<SupplierDetails> GetSupplierDetails(int id)
        {
            List<SupplierDetails> SupplierInfo = new List<SupplierDetails>();
            MySqlConnection conn = GetConnection();
            conn.Open();
            string depID = HttpContext.Session.GetString("depID");
            int department = Convert.ToInt32(depID);
            MySqlCommand cmd = new MySqlCommand("select a.assetID, a.type, a.cost, d.field, d.amount " +
                "from assets as a, distributed_to as d " +
                "where a.assetID = d.assetID and d.supID = "+id+" and d.depID = "+department+" and a.deleted_flag = 1", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    SupplierInfo.Add(new SupplierDetails()
                    {
                        assetID = getIntValue(reader["assetID"]),
                        type = getStringValue(reader["type"]),
                        cost = getIntValue(reader["cost"]),
                        field = getStringValue(reader["field"]),
                        amount = getIntValue(reader["amount"])

                    });

                }
            }
            conn.Close();

            return SupplierInfo;
        }

        public List<EmployeeUse> getUsedByData(int id)
        {
            MySqlConnection conn = GetConnection();
            List<EmployeeUse> UsedByData = new List<EmployeeUse>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select u.employeeID, u.supID, u.assetID, u.field, u.amount, s.name, a.type, a.cost " +
                "from used_by as u left outer join suppliers as s on s.supiD = u.supID left outer join assets as a on a.assetID = u.assetID " +
                "left outer join employee as e on e.employeeID = u.employeeID where e.employeeID = " + id + ";", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    UsedByData.Add(new EmployeeUse()
                    {
                        employeeID = getIntValue(reader["employeeID"]),
                        supID = getIntValue(reader["supID"]),
                        assetID = getIntValue(reader["assetID"]),
                        field = getStringValue(reader["field"]),
                        type = getStringValue(reader["type"]),
                        cost = getIntValue(reader["cost"]),
                        supName = getStringValue(reader["name"]),                       
                        amount = getIntValue(reader["amount"])
                    });
                }
            }
            conn.Close();

            return UsedByData;
        }

        public List<Tasks> getTaskInfo(int id)
        {
            MySqlConnection conn = GetConnection();
            List<Tasks> TaskData = new List<Tasks>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select t.taskID, t.taskName, t.cost, t.status " +
                "from task as t where t.projID = "+ id + " and t.deleted_flag = 1;", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {                    
                    TaskData.Add(new Tasks()
                    {
                        taskID = getIntValue(reader["taskID"]),
                        taskName = getStringValue(reader["taskName"]),
                        cost = getIntValue(reader["cost"]),                      
                        status = Convert.ToDecimal(reader["status"])
                    });
                }
            }
            conn.Close();

            return TaskData;
        }

        public List<TaskReport> GetTaskReports(int id)
        {
            MySqlConnection conn = GetConnection();
            List<TaskReport> TaskData = new List<TaskReport>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select t.taskName, e.Fname, e.Lname, w.hours as 'hours', e.salary, (round((w.hours / 8), 2) * e.salary) as pay " +
                "from employee as e, works_on as w, task as t " +
                "where t.projID = " + id + " and e.employeeID = w.employeeID and w.taskID = t.taskID and t.deleted_flag = 1;", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    TaskData.Add(new TaskReport()
                    {
                        Fname = getStringValue(reader["Fname"]),
                        Lname = getStringValue(reader["Lname"]),
                        taskName = getStringValue(reader["taskName"]),
                        salary = getIntValue(reader["salary"]),
                        hours = Convert.ToDecimal(reader["hours"]),
                        pay = Convert.ToDecimal(reader["pay"])
                       
                    });
                }
            }
            conn.Close();

            return TaskData;
        }

        public List<DepartmentAssetsReport> GetDepartmentAssets(int id)
        {
            MySqlConnection conn = GetConnection();
            List<DepartmentAssetsReport> Report = new List<DepartmentAssetsReport>();

            conn.Open();
         
            MySqlCommand cmd = new MySqlCommand("select s.name, a.type, a.cost, d.amount, (a.cost * d.amount) as total " +
                "from assets as a, distributed_to as d, suppliers as s " +
                "where d.depID = " + id + " and a.assetID = d.assetID  and s.supID = a.supID and a.deleted_flag = 1;", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Report.Add(new DepartmentAssetsReport()
                    {
                        name = getStringValue(reader["name"]),
                        type = getStringValue(reader["type"]),                       
                        cost = getIntValue(reader["cost"]),
                        amount = getIntValue(reader["amount"]),
                        total = getIntValue(reader["total"])

                    });
                }
            }
            conn.Close();

            return Report;
        }

        public List<EmployeeAssetReport> GetEmployeeAssets(int id)
        {
            MySqlConnection conn = GetConnection();
            List<EmployeeAssetReport> Report = new List<EmployeeAssetReport>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select e.Fname, e.Lname, e.Mname, a.type, a.cost, u.amount, (a.cost * u.amount) as total " +
                "from assets as a, used_by as u, employee as e " +
                "where e.depID = " + id + " and a.assetID = u.assetID and u.employeeID = e.employeeID and a.deleted_flag = 1;", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string Fname = getStringValue(reader["Fname"]);
                    string Lname = getStringValue(reader["Lname"]);
                    string Mname = getStringValue(reader["Mname"]);
                    string name = Fname + " " + Mname + " " + Lname;
                    Report.Add(new EmployeeAssetReport()
                    {
                        name = name,
                        type = getStringValue(reader["type"]),
                        cost = getIntValue(reader["cost"]),
                        amount = getIntValue(reader["amount"]),
                        total = getIntValue(reader["total"])
                    });
                }
            }
            conn.Close();

            return Report;
        }

        public List<employeeProg> GetEmployeeProgs(int id)
        {
            MySqlConnection conn = GetConnection();
            List<employeeProg> Report = new List<employeeProg>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select e.Fname, e.Lname, e.Mname, r.roleName, w.hours, t.taskName, w.taskStatus " +
                "from works_on as w left outer join task as t on t.taskID = w.taskID " +
                "left outer join employee as e on e.employeeID = w.employeeID left outer join roles as r on r.roleID = e.roleID " +
                "where t.projID = " + id + " ;", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string Fname = getStringValue(reader["Fname"]);
                    string Lname = getStringValue(reader["Lname"]);
                    string Mname = getStringValue(reader["Mname"]);
                    string name = Fname + " " + Mname + " " + Lname;
                    Report.Add(new employeeProg()
                    {
                        name = name,
                        roleName = getStringValue(reader["roleName"]),
                        hours = Convert.ToDecimal(reader["hours"]),
                        taskName = getStringValue(reader["taskName"]),
                        status = Convert.ToDecimal(reader["taskStatus"])
                    });
                }
            }
            conn.Close();

            return Report;
        }

        public List<EmployeeHours> GetEmployeeHours(int id)
        {
            MySqlConnection conn = GetConnection();
            List<EmployeeHours> Report = new List<EmployeeHours>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select w.employeeID, e.Fname, e.Mname, e.Lname, sum(hours) totalHours " +
                "from works_on as w left outer join task as t on t.taskID = w.taskID " +
                "left outer join employee as e on e.employeeID = w.employeeID where t.projID = " + id + " " +
                "group by employeeID order by employeeID desc;", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string Fname = getStringValue(reader["Fname"]);
                    string Lname = getStringValue(reader["Lname"]);
                    string Mname = getStringValue(reader["Mname"]);
                    string name = Fname + " " + Mname + " " + Lname;
                    Report.Add(new EmployeeHours()
                    {
                        name = name,                       
                        hours = Convert.ToDecimal(reader["totalHours"]),                     
                    });
                }
            }
            conn.Close();

            return Report;
        }

        public List<Tasks> getTasks(int id)
        {
            MySqlConnection conn = GetConnection();
            List<Tasks> TaskData = new List<Tasks>();
        
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select t.taskID, t.taskName, t.cost, t.taskDueDate, t.status " +
                "from task as t where t.projID = " + id + " and t.deleted_flag = 1;", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    DateTime date = Convert.ToDateTime(getStringValue(reader["taskDueDate"]));
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
                    TaskData.Add(new Tasks()
                    {
                        taskID = getIntValue(reader["taskID"]),
                        taskName = getStringValue(reader["taskName"]),
                        cost = getIntValue(reader["cost"]),
                        taskDueDate = sqlDate,
                        status = Convert.ToDecimal(reader["status"])
                       
                    });
                }
            }
            conn.Close();

            return TaskData;
        }

        public string HashPassword(string password)
        {
            SHA256 hash = SHA256.Create();
            var passwordBytes = Encoding.Default.GetBytes(password);
            var hashedpassword = hash.ComputeHash(passwordBytes);

            return BitConverter.ToString(hashedpassword).Replace("-", "");
        }

    }
}