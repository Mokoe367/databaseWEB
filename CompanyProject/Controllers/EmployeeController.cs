using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CompanyProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace CompanyProject.Controllers
{
    public class EmployeeController : Controller
    {
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection("server = 127.0.0.1; port=3306;database=company_project;user=root;password=Ram1500trx@mopar");
        }


        public IActionResult Index()
        {
            /*CompanyContext context = HttpContext.RequestServices.GetService(typeof(CompanyProject.Models.CompanyContext)) as CompanyContext;
            return View(context.GetAllEmployees());*/

            /*List<Employee> employee = new List<Employee>();
            return View("Index", employee);*/

            string empID = HttpContext.Session.GetString("id");
            ViewData["id"] = Convert.ToInt32(empID);
            //List<Employee> list = new List<Employee>();
            // add this employee to the new list
            MySqlConnection conn = GetConnection();
            conn.Open();
            Employee user = new Employee();
            MySqlCommand cmd = new MySqlCommand("select e.Fname, e.Lname, e.depID from employee as e where e.employeeID = '" + empID + "'", conn);

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                user.Fname = getStringValue(reader["Fname"]);
                user.Lname = getStringValue(reader["Lname"]);
                user.DepName = getDepartmentName(getIntValue(reader["depID"]));
            }
            conn.Close();
            string msg = "Signed in as " + user.Fname + " " + user.Lname + " from Department " + user.DepName;
            ViewData["userInfo"] = msg;


            return View("Index", getEmployeeData(empID)); // you can pass models into view
        }

        public string getDepartment(string empID)
        {
            string ID = "0";
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd1 = new MySqlCommand("select depID from employee where employeeID=" + empID, conn);
            var reader = cmd1.ExecuteReader();

            if (reader.Read())
            {
                ID = getStringValue(reader["depID"]);
            }
            conn.Close();
            return ID;
        }

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

        public List<Employee> getEmployeeData(string empID)
        {
            
            MySqlConnection conn = GetConnection();
            List<Employee> employeeData = new List<Employee>();

            conn.Open();
            
            string depID = getDepartment(empID);
            
            MySqlCommand cmd = new MySqlCommand("select * from employee where depID=" + depID, conn);

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

        public IActionResult Edit(int id)
        {

            var emp = new Employee(); // create an instance of the Employee model

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
            //emp.Fname = getStringValue(reader["Fname"]);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd2 = new MySqlCommand("select employee.ssn from employee where employee.employeeID != " + employee.ID + " and employee.ssn = " + employee.Ssn + "; ", conn);
            var reader = cmd2.ExecuteReader();

            if (reader.Read())
            {
                ModelState.AddModelError("Ssn", "Invalid SSN");
            }
            if(employee.Ssn != 0)
            {
                Regex regex = new Regex("[0-9]{9}$");
                Match match = regex.Match(employee.Ssn.ToString());
                if(!match.Success)
                {
                    ModelState.AddModelError("Ssn", "Invalid SSN");
                }
            }

            reader.Close();
            cmd2 = new MySqlCommand("select depID from department where depID = " + employee.DepID + ";", conn);
            reader = cmd2.ExecuteReader();
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
            if (reader.Read())
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

                string query = "UPDATE employee SET Fname=@Fname, Mname=@Mname, Lname=@Lname, sex=@sex , birthdate=@birthdate , salary=@salary, ssn=@ssn, address=@address " +
                    ", depId=@depId, roleId=@roleId, superID=@superID where employeeID = " + employee.ID + ";";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@Fname", employee.Fname);
                cmd.Parameters.AddWithValue("@Mname", employee.Mname);
                cmd.Parameters.AddWithValue("@Lname", employee.Lname);
                cmd.Parameters.AddWithValue("@sex", employee.Sex);
                cmd.Parameters.AddWithValue("@birthdate", employee.BirthDate);
                cmd.Parameters.AddWithValue("@salary", employee.Salary);                
                if (employee.Ssn == 0)
                {
                    cmd.Parameters.AddWithValue("@ssn", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ssn", employee.Ssn);
                }
                cmd.Parameters.AddWithValue("@address", employee.Address);
                if (employee.DepID == 0)
                {
                    cmd.Parameters.AddWithValue("@depId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@depId", employee.DepID);
                }

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
                conn.Close();
                TempData["success"] = "Employee edited successfully";
                return RedirectToAction("Index");

            }

            conn.Close();
            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select employee.deleted_flag from employee where employee.employeeID = " + id + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            int flag = getIntValue(reader["deleted_flag"]);
            reader.Close();

            string query = "UPDATE employee SET deleted_flag=@deleted_flag where employeeID = " + id + ";";
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

        public IActionResult Detail(int id)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from employee where employeeID=" + id + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();

            var emp = new Employee(); // an instance of the employee model

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
            /*emp.Fname = getStringValue(reader["Fname"]);*/
            emp.Lname = getStringValue(reader["Lname"]);
            emp.Mname = getStringValue(reader["Mname"]);
            emp.Address = getStringValue(reader["address"]);
            emp.Sex = getStringValue(reader["sex"]);
            emp.BirthDate = sqlDate;
            emp.RoleID = getIntValue(reader["roleId"]);
            emp.RoleName = getRoleName(getIntValue(reader["roleId"]));
            emp.DepID = getIntValue(reader["depId"]);
            emp.DepName = getDepartmentName(getIntValue(reader["depId"]));
            emp.Ssn = getIntValue(reader["ssn"]);
            emp.Salary = getIntValue(reader["salary"]);
            emp.SuperID = getIntValue(reader["superID"]);
            emp.SupervisorName = getSuperName(getIntValue(reader["superID"]));

            conn.Close();
            reader.Close();
            return View(emp);


        }

        public IActionResult TaskReport()
        {
            string empID = HttpContext.Session.GetString("id");
            int id = Convert.ToInt32(empID);
            return View(GetEmployeeTaskReportViews(id));
        }

        public IActionResult EditWork(int empid, int taskid)
        {
            Works_on work = new Works_on();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from works_on where employeeID = " + empid + " and " +
                "taskID = " + taskid + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            work.employeeID = getIntValue(reader["employeeID"]);
            work.TaskID = getIntValue(reader["taskID"]);
            work.tempemployeeID = getIntValue(reader["employeeID"]);
            work.tempTaskID = getIntValue(reader["taskID"]);
            work.hours = getIntValue(reader["hours"]);
            conn.Close();
            return View(work);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditWork(Works_on work)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select employeeID from employee where employeeID = " + work.employeeID + "; ", conn);

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                ModelState.AddModelError("employeeID", "Employee ID doesn't exist");
            }
            reader.Close();
            string test = "select taskID from task where taskID = " + work.TaskID + ";";
            cmd.CommandText = test;
            reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ModelState.AddModelError("TaskID", "Task ID doesn't exist");
            }
            reader.Close();
            test = "select employeeID, taskID from works_on where employeeID = '" + work.employeeID + "' " +
                    "and taskID = " + work.TaskID + " and employeeID != '" + work.tempemployeeID + "' " +
                    "and taskID = " + work.tempTaskID + ";";
            cmd.CommandText = test;
            reader = cmd.ExecuteReader();
            if (reader.HasRows && (work.employeeID != work.tempemployeeID || work.TaskID != work.tempTaskID))
            {
                ModelState.AddModelError("hours", "Employee ID and Task ID already exist");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                string query = "UPDATE works_on SET employeeID=@emp, taskID=@task, hours=@hours WHERE employeeID = '" + work.tempemployeeID + "' " +
                    "and taskID = " + work.tempTaskID + ";";
                MySqlCommand cmd2 = new MySqlCommand();

                cmd2.CommandText = query;
                cmd2.Parameters.AddWithValue("@emp", work.employeeID);
                cmd2.Parameters.AddWithValue("@task", work.TaskID);
                cmd2.Parameters.AddWithValue("@hours", work.hours);

                cmd2.Connection = conn;
                cmd2.ExecuteNonQuery();

                conn.Close();
                TempData["success"] = "Works on successfully edited";
                return RedirectToAction("TaskReport");
            }
            else
            {
                conn.Close();
                return View(work);
            }
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

        public IEnumerable<EmployeeTaskReportView> GetEmployeeTaskReportViews(int id)
        {
            string empID = HttpContext.Session.GetString("id");
            MySqlConnection conn = GetConnection();
            conn.Open();
            int depID;
            MySqlCommand cmd = new MySqlCommand("select e.depID from employee as e where e.employeeID = " + id + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();                      
            depID = getIntValue(reader["depID"]);
            string depName = getDepartmentName(depID);
            ViewData["DepName"] = depName;
            conn.Close();

            List<EmployeeTaskReportView> data = new List<EmployeeTaskReportView>();
            EmployeeTaskReportView model = new EmployeeTaskReportView();

            model.taskDetails = getTaskDetails(id);
            model.projects = getProjectData(depID);
            

            data.Add(model);
            return data;
            
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

        public List<TaskDetails> getTaskDetails(int id)
        {
            List<TaskDetails> TaskData = new List<TaskDetails>();
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select w.employeeID, w.hours, t.taskName, t.cost, t.taskDueDate, p.projName, w.taskID " +
                "from project as p left outer join task as t on t.projID = p.projID " +
                "left outer join works_on as w on w.taskID = t.taskID where w.employeeID = " + id + ";", conn);

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

                    TaskData.Add(new TaskDetails()
                    {
                        empID = getIntValue(reader["employeeID"]),
                        projName = getStringValue(reader["projName"]),
                        dueDate = sqlDate,
                        hours = Convert.ToDecimal(reader["hours"]),
                        taskName = getStringValue(reader["taskName"]),
                        budget = getIntValue(reader["cost"]),
                        taskID = getIntValue(reader["taskID"])
                    });
                    

                }
            }

            projNum = countUniqueProj(TaskData);
            ViewData["projNum"] = projNum;
            conn.Close();

            return TaskData;
        }

        public int countUniqueProj(List<TaskDetails> TaskData)
        {
            int count = 0; // num of unique projects
            List<int> projects = new List<int>();
            foreach(var item in TaskData)
            {
                if (projects.Contains(item.projID))
                {
                    continue;
                }
                else
                {
                    projects.Add(item.projID);
                    count++;
                }
            }
            return count;
        }
        public string getProjectName(int projectID)
        {
            if (projectID == 0) { return ""; }
            string projName = "";
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd1 = new MySqlCommand("select projName from project where projID=" + projectID + ";", conn);
            var reader = cmd1.ExecuteReader();

            if (reader.Read())
            {
                projName = getStringValue(reader["projName"]);
            }
            
            return projName;
        }

        public string getTaskDaysLeft(int taskID)
        {
            if (taskID == 0) { return ""; }
            string daysLeft = "";
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd1 = new MySqlCommand("select datediff(taskDueDate,(select curdate())) as days from task where taskID =" + taskID + ";", conn);
            var reader = cmd1.ExecuteReader();

            if (reader.Read())
            {
                daysLeft = getStringValue(reader["days"]);
            }
            int temp = getIntValue(daysLeft);
            if (temp < 0) { return "Overdue!"; }
            return daysLeft;
        }

        public List<Project> getProjectData(int id)
        {
            MySqlConnection conn = GetConnection();
            List<Project> projectData = new List<Project>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from project where project.depID = " + id + " and project.deleted_flag != 0;", conn);

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
                        depName = getDepartmentName(getIntValue(reader["depID"])),
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

        public string HashPassword(string password)
        {
            SHA256 hash = SHA256.Create();
            var passwordBytes = Encoding.Default.GetBytes(password);
            var hashedpassword = hash.ComputeHash(passwordBytes);

            return BitConverter.ToString(hashedpassword).Replace("-", "");
        }
    }
}
