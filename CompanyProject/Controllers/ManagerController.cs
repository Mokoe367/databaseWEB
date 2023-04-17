using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CompanyProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
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
            //var consentFeature = HttpContext.Features.Get<ITrackingConsentFeature>();
            return new MySqlConnection("server = 127.0.0.1; port=3306;database=company_project;user=root;password=Ram1500trx@mopar");
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
                    user.DepName = getDepartmentName(getIntValue(reader["depID"]));
                }
                conn.Close();
                HttpContext.Session.SetString("depID", user.DepID.ToString());
                userDepID = user.DepID;
                userEmpID = Convert.ToInt32(empID);
                string msg = "Signed in as " + user.Fname + " " + user.Lname + " showing Department " + user.DepName + " (Department ID: " + user.DepID + ")";
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

        public IActionResult AddProject()
        {
            string depID = "Adding Project into Department number " + HttpContext.Session.GetString("depID");
            ViewData["AddInfo"] = depID;
            return View();
        }

        public IActionResult AddTask()
        {
            string depID = "Adding Task for Department number " + HttpContext.Session.GetString("depID");
            ViewData["AddInfo"] = depID;
            return View();
        }

        public IActionResult AddRole()
        {
            return View();
        }

        public IActionResult AddSupplier()
        {
            return View();
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
            conn.Close();
            return View(proj);
        }

        public IActionResult EditTask(int id)
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
            conn.Close();
            return View(task);
        }

        public IActionResult EditSupplier(int id)
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
            MySqlCommand cmd = new MySqlCommand("select * from assets where assetID = " + id + "; ", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            asset.assetID = getIntValue(reader["assetID"]);
            asset.type = getStringValue(reader["type"]);
            asset.cost = getIntValue(reader["cost"]);
            asset.supID = getIntValue(reader["supID"]);

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
            MySqlCommand cmd = new MySqlCommand("select * from distributed_to where depID = " + department + " and " +
                "supID = " + supId + " and assetID = " + assetId + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            distribution.depID = getIntValue(reader["depID"]);
            distribution.supID = getIntValue(reader["supID"]);
            distribution.assetID = getIntValue(reader["assetID"]);
            distribution.tempDepID = getIntValue(reader["depID"]);
            distribution.tempSupID = getIntValue(reader["supID"]);
            distribution.tempAssetID = getIntValue(reader["assetID"]);
            distribution.field = getStringValue(reader["field"]);

            ViewData["supplier"] = supId;
            conn.Close();
            return View(distribution);
        }

        public IActionResult EditUse(int empId, int supId, int assetId)
        {
            Used_by use = new Used_by();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from used_by where employeeID = " + empId + " and " +
                "supID = " + supId + " and assetID = " + assetId + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            use.employeeID = getIntValue(reader["employeeID"]);
            use.supID = getIntValue(reader["supID"]);
            use.assetID = getIntValue(reader["assetID"]);
            use.tempemployeeID = getIntValue(reader["employeeID"]);
            use.tempsupID = getIntValue(reader["supID"]);
            use.tempassetID = getIntValue(reader["assetID"]);
            use.field = getStringValue(reader["field"]);

            ViewData["use"] = empId;
            conn.Close();
            return View(use);
        }

        public IActionResult EditLocation(int id, string name)
        {
            Dep_locations location = new Dep_locations();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from dep_locations where depID = " + id + " and loc_name = @name;", conn);
            cmd.Parameters.AddWithValue("@name", name);
            var reader = cmd.ExecuteReader();
            reader.Read();
            location.depID = getIntValue(reader["depID"]);
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
            MySqlCommand cmd = new MySqlCommand("select * from distributed_to where depID = " + department + " and " +
                "supID = " + supId + " and assetID = " + assetId + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();

            distribution.depID = getIntValue(reader["depID"]);
            distribution.supID = getIntValue(reader["supID"]);
            distribution.assetID = getIntValue(reader["assetID"]);
            distribution.field = getStringValue(reader["field"]);

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
            IEnumerable<ProjectDetails> list = GetProjectDetails(id);

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
            MySqlCommand cmd = new MySqlCommand("select * from works_on where employeeID = " + empid + " and taskID = '" + taskid + "';", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            work.employeeID = getIntValue(reader["employeeID"]);
            work.TaskID = getIntValue(reader["TaskID"]);
            work.hours = getIntValue(reader["hours"]);

            ViewData["employee"] = empid;

            return View(work);
        }

        public IActionResult Enlist(int id)
        {
            Works_on work = new Works_on();
            work.employeeID = id;
            ViewData["employee"] = id;
            return View(work);
        }       

        public IActionResult RequestAssets(int id)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select name from suppliers where supID = " + id + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            string msg = "Requesting Assets from " + getStringValue(reader["name"]);
            conn.Close();

            ViewData["request"] = msg;
            Distributed_to asset = new Distributed_to();
            string depID = HttpContext.Session.GetString("depID");
            int department = Convert.ToInt32(depID);
            asset.depID = department;
            asset.supID = id;
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
            MySqlCommand cmd = new MySqlCommand("select * from assets where assetID = " + id + "; ", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            asset.assetID = getIntValue(reader["assetID"]);
            asset.type = getStringValue(reader["type"]);
            asset.cost = getIntValue(reader["cost"]);
            asset.supID = getIntValue(reader["supID"]);

            ViewData["asset"] = asset.supID;
            conn.Close();
            return View(asset);
        }

        public IActionResult DeleteUse(int empId, int supId, int assetId)
        {
            Used_by use = new Used_by();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from used_by where employeeID = " + empId + " and " +
                "supID = " + supId + " and assetID = " + assetId + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            use.employeeID = getIntValue(reader["employeeID"]);
            use.supID = getIntValue(reader["supID"]);
            use.assetID = getIntValue(reader["assetID"]);
            use.field = getStringValue(reader["field"]);

            ViewData["use"] = empId;
            conn.Close();
            return View(use);

        }

        public IActionResult DeleteLocation(int id, string name)
        {
            Dep_locations location = new Dep_locations();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from dep_locations where depID = " + id + " and loc_name = @name;", conn);
            cmd.Parameters.AddWithValue("@name", name);
            var reader = cmd.ExecuteReader();
            reader.Read();
            location.depID = getIntValue(reader["depID"]);
            location.loc_name = getStringValue(reader["loc_name"]);
            location.pastDepID = getIntValue(reader["depID"]);
            location.pastLoc_name = getStringValue(reader["loc_name"]);
            conn.Close();
            return View(location);
        }

        public IActionResult CostReport()
        {
            return View();
        }

        public IActionResult LoginDetails()
        {
            var login = new Login();
            string empID = HttpContext.Session.GetString("id");
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProgressReport(int projectID)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select projID, depID from project where projID = " + projectID + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            string depID = HttpContext.Session.GetString("depID");
            int department = Convert.ToInt32(depID);
            bool valid = true;
            if (!reader.HasRows)
            {
                ViewData["inputError"] = "ProjectID doesn't exist";
                valid = false;
            }
            else if (getIntValue(reader["depID"]) != department)
            {
                ViewData["inputError"] = "projectID not in this department";
                valid = false;
            }
            reader.Close();
            if (valid)
            {
                IEnumerable<ProgressReport> list = GetProgressReports(projectID);
                string query = "select projName, projStatus, dueDate, cost from project where projID = " + projectID + ";";
                cmd.CommandText = query;
                reader = cmd.ExecuteReader();
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
                ViewData["status"] = status;
                ViewData["dueDate"] = sqlDate;
                ViewData["cost"] = cost;

                conn.Close();

                return View("ProgressReportList", list);
            }
            else
            {
                conn.Close();
                return View();
            }

        }
    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CostReport(CostReport obj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select depID from project where projID = " + obj.projectID + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            string depID = HttpContext.Session.GetString("depID");
            int department = Convert.ToInt32(depID);
            if (!reader.HasRows)
            {
                ModelState.AddModelError("projectID", "projectID doesn't exist");               
            }
            else if(getIntValue(reader["depID"]) != department)
            {
                ModelState.AddModelError("projectID", "projectID not in this department");
            }
            reader.Close();
            if(ModelState.IsValid)
            {
                IEnumerable<CostReport> list = GetCostReport(obj.projectID);
                string query = "select projName, cost from project where projID = " + obj.projectID + ";";
                cmd.CommandText = query;
                reader = cmd.ExecuteReader();
                reader.Read();               
                string msg = "Project Details for " + getStringValue(reader["projName"]);
                list.FirstOrDefault().projectBudget = getIntValue(reader["cost"]);

                conn.Close();

                ViewData["ProjectInfo"] = msg;

                ViewData["project"] = obj.projectID;
                decimal total = list.FirstOrDefault().projectBudget - list.FirstOrDefault().taskCost;
                ViewData["remaining"] = total.ToString();

                return View("CostReportList", list);
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
                conn.Close();
                string depID = "Adding Employee into Department number " + HttpContext.Session.GetString("depID");
                ViewData["AddInfo"] = depID;
                return View(obj);
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditEmployee(Employee employee)
        {

            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd2 = new MySqlCommand("select depID from department where depID = " + employee.DepID + ";", conn);
            var reader = cmd2.ExecuteReader();
            if (!reader.Read() && employee.DepID != 0)
            {
                ModelState.AddModelError("DepId", "Department number doesn't exist");
            }
            reader.Close();

            cmd2 = new MySqlCommand("select roleId from roles where roleId = " + employee.RoleID + ";", conn);
            reader = cmd2.ExecuteReader();
            if (!reader.Read() && employee.RoleID != 0)
            {
                ModelState.AddModelError("RoleId", "Role number doesn't exist");
            }
            reader.Close();

            cmd2 = new MySqlCommand("select employeeID from employee where employeeID = " + employee.SuperID + ";", conn);
            reader = cmd2.ExecuteReader();
            if (!reader.HasRows && employee.SuperID != 0)
            {
                ModelState.AddModelError("superID", "superviser Id number doesn't exist");
            }
            if (reader.Read() && employee.SuperID != 0)
            {
                int id = getIntValue(reader["employeeID"]);
                if (id == employee.ID)
                {
                    ModelState.AddModelError("superID", "supervisor can't be same as employeeID");
                }

            }
            reader.Close();

            if (employee.Sex != "M" && employee.Sex != "F")
            {
                ModelState.AddModelError("Sex", "Gender Must be either M or F");
            }

            if (ModelState.IsValid)
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

            conn.Close();
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProject(Project obj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
         
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
                int department = Convert.ToInt32(depID);
                query = "insert into project (dueDate, projName, location, cost, field, projStatus, depID) VALUES (@date, @projName , @location " +
                       ", @cost, @field, @projStatus, @department);";
                
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@date", obj.dueDate);
                cmd.Parameters.AddWithValue("@projName", obj.projName);
                cmd.Parameters.AddWithValue("@cost", obj.cost);
                cmd.Parameters.AddWithValue("@location", obj.location);
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

                string depID = HttpContext.Session.GetString("depID");
                int department = Convert.ToInt32(depID);

                cmd2.CommandText = query;
                cmd2.Parameters.AddWithValue("@projName", proj.projName);
                cmd2.Parameters.AddWithValue("@dueDate", proj.dueDate);
                cmd2.Parameters.AddWithValue("@depID", department);
                cmd2.Parameters.AddWithValue("@location", proj.location);
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
                return View(proj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProject(Project proj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select project.deleted_flag from project where project.projID = " + proj.projID + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            int flag = getIntValue(reader["deleted_flag"]);
            reader.Close();

            string query = "UPDATE project SET deleted_flag=@deleted_flag where projID = " + proj.projID + ";";
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTask(Tasks obj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select projID from project where projID = " + obj.projID + "; ", conn);
            string depID = HttpContext.Session.GetString("depID");
            int department = Convert.ToInt32(depID);
            var reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                ModelState.AddModelError("projID", "Project ID doesn't exist");
            }
            reader.Close();
            string query = "select depID from project where projID = " + obj.projID + ";";
            cmd.CommandText = query;
            cmd.Connection = conn;
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                if(getIntValue(reader["depID"]) != department)
                {
                    ModelState.AddModelError("projID", "Project not in this department");
                }
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                try
                {
                    query = "insert into task (taskName, cost, taskDueDate, projID) " +
                        "VALUES (@taskName, @cost, @date, @projID);";

                    MySqlCommand cmd2 = new MySqlCommand();

                    cmd2.CommandText = query;
                    cmd2.Parameters.AddWithValue("@taskName", obj.taskName);
                    cmd2.Parameters.AddWithValue("@cost", obj.cost);
                    cmd2.Parameters.AddWithValue("@date", obj.taskDueDate);
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
                    return View(obj);
                }
               
            }
            else
            {
                conn.Close();
                string info = "Adding Task for Department number " + department;
                ViewData["AddInfo"] = info;
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

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                ModelState.AddModelError("projID", "Project ID doesn't exist");
            }
            reader.Close();
            string query = "select depID from project where projID = " + task.projID + ";";
            cmd.CommandText = query;
            cmd.Connection = conn;
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                if (getIntValue(reader["depID"]) != department)
                {
                    ModelState.AddModelError("projID", "Project not in this department");
                }
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                try
                {
                    query = "UPDATE task SET taskName=@taskName, cost=@cost, taskDueDate=@date, projID=@projID where taskID = " + task.taskID + ";";
                    MySqlCommand cmd2 = new MySqlCommand();

                    cmd2.CommandText = query;
                    cmd2.Parameters.AddWithValue("@taskName", task.taskName);
                    cmd2.Parameters.AddWithValue("@cost", task.cost);
                    cmd2.Parameters.AddWithValue("@date", task.taskDueDate);
                    cmd2.Parameters.AddWithValue("@projID", task.projID);
                    cmd2.Connection = conn;
                    cmd2.ExecuteNonQuery();

                    conn.Close();
                    TempData["success"] = "Task edited added";
                    return RedirectToAction("Index");
                }
                catch(MySqlException e)
                {
                    conn.Close();
                    ModelState.AddModelError("projID", e.Message);
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
        public IActionResult DeleteTask(Tasks task)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select task.deleted_flag from task where task.taskID = " + task.taskID + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            int flag = getIntValue(reader["deleted_flag"]);
            reader.Close();

            string query = "UPDATE task SET deleted_flag=@deleted_flag where taskID = " + task.taskID + ";";
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSupplier(Supplier obj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select roleID from roles where roleID = " + obj.roleID + "; ", conn);

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows && obj.roleID != 0)
            {
                ModelState.AddModelError("roleID", "RoleID doesn't exist");
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
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["success"] = "Supplier succesfully added";
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSupplier(Supplier sup)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select roleID from roles where roleID = " + sup.roleID + "; ", conn);

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows && sup.roleID != 0)
            {
                ModelState.AddModelError("roleID", "RoleID doesn't exist");
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
                return View(sup);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSupplier(Supplier sup)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select suppliers.deleted_flag from suppliers where suppliers.supID = " + sup.supID + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            int flag = getIntValue(reader["deleted_flag"]);
            reader.Close();

            string query = "UPDATE suppliers SET deleted_flag=@deleted_flag where supID = " + sup.supID + ";";
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
            MySqlCommand cmd = new MySqlCommand("select supID from suppliers where supID = " + obj.supID + "; ", conn);

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows && obj.supID != 0)
            {
                ModelState.AddModelError("supID", "Supplier ID doesn't exist");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
               
                string query = "insert into assets (type, cost, supID) VALUES (@type, @cost, @supID);";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@type", obj.type);
                cmd.Parameters.AddWithValue("@cost", obj.cost);          
                cmd.Parameters.AddWithValue("@supID", obj.supID);
                
                cmd.CommandText = query;
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
            MySqlCommand cmd = new MySqlCommand("select supID from suppliers where supID = " + asset.supID + "; ", conn);

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                ModelState.AddModelError("supID", "Supplier ID doesn't exist");
            }
            reader.Close();
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
                string query = "UPDATE distributed_to SET field=@field WHERE depID = '" + distribution.tempDepID + "' " +
                    "and supID = " + distribution.tempSupID + " and assetID = " + distribution.tempAssetID + ";";
                MySqlCommand cmd2 = new MySqlCommand();

                cmd2.CommandText = query;             
                cmd2.Parameters.AddWithValue("@field", distribution.field);
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
            MySqlCommand cmd = new MySqlCommand("select depID from department where depID = " + obj.depID + "; ", conn);

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                ModelState.AddModelError("depID", "Department ID doesn't exist");
            }
            reader.Close();
            string test = "select supID from suppliers where supID = " + obj.supID + ";";
            cmd.CommandText = test;
            reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ModelState.AddModelError("supID", "Supplier ID doesn't exist");
            }
            reader.Close();
            test = "select assetID from assets where assetID = " + obj.assetID + ";";
            cmd.CommandText = test;
            reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ModelState.AddModelError("assetID", "Asset ID doesn't exist");
            }
            reader.Close();
            test = "select * from distributed_to where depID = " + obj.depID + " and supID = " + obj.supID + " and assetID = " + obj.assetID + ";";
            cmd.CommandText = test;
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ModelState.AddModelError("field", "Asset ID, Department ID, and Supplier ID already exist");
            }
            reader.Close();
            test = "select assetID from assets where supID = " + obj.supID + " and assetID = " + obj.assetID + ";";
            cmd.CommandText = test;
            reader = cmd.ExecuteReader();
            if(!reader.HasRows)
            {
                ModelState.AddModelError("assetID", "assetID not associated with supplier");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                string query = "insert into distributed_to (depID, supID, assetID, field) VALUES ('" + obj.depID +
                    "', '" + obj.supID + "', '" + obj.assetID + "', @field);"; ;
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@field", obj.field);
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
                return View(obj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUse(Used_by obj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select employeeID from employee where employeeID = " + obj.employeeID + "; ", conn);

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                ModelState.AddModelError("employeeID", "Employee ID doesn't exist");
            }
            reader.Close();
            string test = "select supID from suppliers where supID = " + obj.supID + ";";
            cmd.CommandText = test;
            reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ModelState.AddModelError("supID", "Supplier ID doesn't exist");
            }
            reader.Close();
            test = "select assetID from assets where assetID = " + obj.assetID + ";";
            cmd.CommandText = test;
            reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ModelState.AddModelError("assetID", "Asset ID doesn't exist");
            }
            reader.Close();
            test = "select * from used_by where employeeID = " + obj.employeeID + " and supID = " + obj.supID + " and assetID = " + obj.assetID + ";";
            cmd.CommandText = test;
            reader = cmd.ExecuteReader();
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
                ModelState.AddModelError("assetID", "assetID not associated with supplier");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                string query = "insert into used_by (employeeID, supID, assetID, field) VALUES ('" + obj.employeeID +
                    "', '" + obj.supID + "', '" + obj.assetID + "', @field);"; ;
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@field", obj.field);
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
                return View(obj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUse(Used_by use)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select employeeID from employee where employeeID = " + use.employeeID + "; ", conn);

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                ModelState.AddModelError("employeeID", "Employee ID doesn't exist");
            }
            reader.Close();
            string test = "select supID from suppliers where supID = " + use.supID + ";";
            cmd.CommandText = test;
            reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ModelState.AddModelError("supID", "Supplier ID doesn't exist");
            }
            reader.Close();
            test = "select assetID from assets where assetID = " + use.assetID + ";";
            cmd.CommandText = test;
            reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ModelState.AddModelError("assetID", "Asset ID doesn't exist");
            }
            reader.Close();
            test = "select employeeID, supID, assetID from used_by where employeeID = '" + use.employeeID + "' " +
                    "and supID = " + use.supID + " and assetID = " + use.assetID + ";";
            cmd.CommandText = test;
            reader = cmd.ExecuteReader();
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
                ModelState.AddModelError("assetID", "assetID not associated with supplier");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                string query = "UPDATE used_by SET employeeID=@emp, supID=@sup, assetID=@asset, field=@field WHERE employeeID = '" + use.tempemployeeID + "' " +
                    "and supID = " + use.tempsupID + " and assetID = " + use.tempassetID + ";";
                MySqlCommand cmd2 = new MySqlCommand();

                cmd2.CommandText = query;
                cmd2.Parameters.AddWithValue("@emp", use.employeeID);
                cmd2.Parameters.AddWithValue("@sup", use.supID);
                cmd2.Parameters.AddWithValue("@asset", use.assetID);
                cmd2.Parameters.AddWithValue("@field", use.field);
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
            MySqlCommand cmd = new MySqlCommand("select depID from department where depID = " + location.depID + "; ", conn);

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                ModelState.AddModelError("depID", "Department ID doesn't exist");
            }
            reader.Close();
            string test = "select depID, loc_name from dep_locations where loc_name = @lname " +
                    "and depID = " + location.depID + ";";
            cmd.CommandText = test;
            cmd.Parameters.AddWithValue("@lname", location.loc_name);
            reader = cmd.ExecuteReader();
            if (reader.HasRows && (location.loc_name != location.pastLoc_name && location.depID != location.pastDepID))
            {
                ModelState.AddModelError("loc_name", "Department ID and Location name already exist");
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


            if (ModelState.IsValid)
            {
                string query = "delete from dep_locations where depID = " + location.depID + " and loc_name = @name;";

                MySqlCommand cmd2 = new MySqlCommand();                           
                cmd2.CommandText = query;
                cmd2.Parameters.AddWithValue("@name", location.loc_name);
                cmd2.Connection = conn;
                cmd2.ExecuteNonQuery();

                conn.Close();
                TempData["success"] = "Location successfully deleted";
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
        public IActionResult FireEmployee(Employee employee)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select employee.deleted_flag from employee where employee.employeeID = " + employee.ID + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            int flag = getIntValue(reader["deleted_flag"]);
            reader.Close();

            string query = "UPDATE employee SET deleted_flag=@deleted_flag where employeeID = " + employee.ID + ";";
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
            MySqlCommand cmd = new MySqlCommand("select employeeID from employee where employeeID = " + obj.employeeID + "; ", conn);

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                ModelState.AddModelError("employeeID", "Employee ID doesn't exist");
            }
            reader.Close();
            string test = "select taskID from task where taskID = " + obj.TaskID + ";";
            cmd.CommandText = test;
            reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ModelState.AddModelError("TaskID", "Task ID doesn't exist");
            }
            reader.Close();
            test = "select * from works_on where employeeID = " + obj.employeeID + " and taskID = " + obj.TaskID + ";";
            cmd.CommandText = test;
            reader = cmd.ExecuteReader();
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
                    return View(obj);
                }
            }
            else
            {
                conn.Close();
                ViewData["employee"] = obj.employeeID;
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

        public IEnumerable<CostReport> GetCostReport(int id)
        {
            List<CostReport> data = new List<CostReport>();
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
            model.departmentAssets = GetDepartmentAssets(department);
            model.departmentCost = model.departmentAssets.Sum(item => item.cost);
            model.employeeAssets = GetEmployeeAssets(department);
            model.employeeCost = model.employeeAssets.Sum(item => item.cost);
            data.Add(model);

            return data;
        }

        public IEnumerable<ProgressReport> GetProgressReports(int id)
        {
            List<ProgressReport> data = new List<ProgressReport>();
            ProgressReport model = new ProgressReport();
            model.progress = GetEmployeeProgs(id);
            model.tasks = getTasks(id);
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

            MySqlCommand cmd = new MySqlCommand("select t.taskID, t.taskName, t.cost, t.taskDueDate, t.projID " +
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
                        projName = getTaskProjName(getIntValue(reader["projID"]))

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
            MySqlCommand cmd = new MySqlCommand("select w.employeeID, w.hours, w.taskID, t.taskName, t.cost, t.taskDueDate, t.projID " +
                "from works_on as w right outer join task as t on t.taskID = w.taskID where w.employeeID = " + id + ";", conn);

            using(var reader = cmd.ExecuteReader())
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
                        taskID = getIntValue(reader["taskID"]),
                        projID = getIntValue(reader["projID"]),
                        dueDate = sqlDate,
                        hours = getIntValue(reader["hours"]),
                        taskName = getStringValue(reader["taskName"]),
                        budget = getIntValue(reader["cost"])
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
            MySqlCommand cmd = new MySqlCommand("select distinct e.Fname, e.Lname, r.roleName, t.taskID, t.taskName, t.taskDueDate, w.hours, t.cost " +
                "from employee as e, project as p, task as t, works_on as w, roles as r where " +
                "t.projID = " + id + " and t.taskID = w.taskID and w.employeeID = e.employeeID and r.roleID = e.roleID order by t.taskID;", conn);

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

                    ProjectData.Add(new ProjectDetails()
                    {
                        Fname = getStringValue(reader["Fname"]),
                        Lname = getStringValue(reader["Lname"]),
                        roleName = getStringValue(reader["roleName"]),
                        taskName = getStringValue(reader["taskName"]),
                        taskID = getIntValue(reader["taskID"]),
                        taskDueDate = sqlDate,
                        hours = Convert.ToDecimal(reader["hours"]),                       
                        cost = getIntValue(reader["cost"])
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
            MySqlCommand cmd = new MySqlCommand("select e.Fname, e.Lname, w.hours, r.roleName " +
                "from employee as e, works_on as w, roles as r where " +
                "w.taskID = " + id + " and e.employeeID = w.employeeID and e.roleID = r.roleID;", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {                
                    TaskInfo.Add(new TaskInformation()
                    {
                        Fname = getStringValue(reader["Fname"]),
                        Lname = getStringValue(reader["Lname"]),
                        roleName = getStringValue(reader["roleName"]),                      
                        hours = Convert.ToDecimal(reader["hours"])
                       
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
            MySqlCommand cmd = new MySqlCommand("select a.assetID, a.type, a.cost, d.field " +
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
                        field = getStringValue(reader["field"])

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

            MySqlCommand cmd = new MySqlCommand("select u.employeeID, u.supID, u.assetID, a.type, u.field, a.cost" +
                " from used_by as u, assets as a where a.assetID = u.assetID and u.employeeID = " + id + ";", conn);

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
                        cost = getIntValue(reader["cost"])

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

            MySqlCommand cmd = new MySqlCommand("select t.taskID, t.taskName, t.cost " +
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
         
            MySqlCommand cmd = new MySqlCommand("select s.name, a.type, a.cost " +
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
                        cost = getIntValue(reader["cost"])                      

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

            MySqlCommand cmd = new MySqlCommand("select e.Fname, e.Lname, a.type, a.cost " +
                "from assets as a, used_by as u, employee as e " +
                "where e.depID = " + id + " and a.assetID = u.assetID and u.employeeID = e.employeeID and a.deleted_flag = 1;", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Report.Add(new EmployeeAssetReport()
                    {
                        Fname = getStringValue(reader["Fname"]),
                        Lname = getStringValue(reader["Lname"]),
                        type = getStringValue(reader["type"]),
                        cost = getIntValue(reader["cost"])

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

            MySqlCommand cmd = new MySqlCommand("select e.Fname, e.Lname, r.roleName, w.hours, t.taskName " +
                "from works_on as w inner join task as t on t.taskID = w.taskID " +
                "inner join employee as e on e.employeeID = w.employeeID inner join roles as r on r.roleID = e.roleID " +
                "where t.projID = " + id + " ;", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Report.Add(new employeeProg()
                    {
                        Fname = getStringValue(reader["Fname"]),
                        Lname = getStringValue(reader["Lname"]),
                        roleName = getStringValue(reader["roleName"]),
                        hours = Convert.ToDecimal(reader["hours"]),
                        taskName = getStringValue(reader["taskName"])

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

            MySqlCommand cmd = new MySqlCommand("select t.taskID, t.taskName, t.cost, t.taskDueDate " +
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