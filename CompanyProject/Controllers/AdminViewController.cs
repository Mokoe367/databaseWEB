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
    public class AdminViewController : Controller
    {
 
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection("server = localhost; port=3306;database=target;user=root;password=MonkeysInc7!");
        }

        public IActionResult Index()
        {
            string empID = HttpContext.Session.GetString("id");

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
            string msg = "Signed in as " + user.Fname + " " + user.Lname;
            ViewData["userInfo"] = msg;


            return View("AdminIndex", getViewData());
        }

        //GET
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmployee(Employee obj)
        {
          
           MySqlConnection conn = GetConnection();
           conn.Open();
           MySqlCommand cmd = new MySqlCommand("select employee.ssn from employee where employee.ssn = " + obj.Ssn + "; ", conn);

           var reader = cmd.ExecuteReader();

           if (reader.Read())
           {
                ModelState.AddModelError("Ssn", "No Duplicate SSN");

           }
           reader.Close();
           if(obj.Sex != "M" && obj.Sex != "F")
           {
                ModelState.AddModelError("Sex", "Gender Must be either M or F");
           }
           if (ModelState.IsValid)
           {
                MySqlCommand insert = new MySqlCommand("insert into employee(Fname, Lname, Mname, salary, ssn, address, birthDate, sex) Values('"+obj.Fname+"', '"+obj.Lname+"', '"+obj.Mname+"', "+obj.Salary+", "+obj.Ssn+", '"+obj.Address+"', '"+obj.BirthDate+"', '"+obj.Sex+"');", conn);

                var reader2 = insert.ExecuteReader();
                conn.Close();
                TempData["success"] = "Employee added successfully";
                return RedirectToAction("Index");

             
           }
            
                  
            return View(obj);
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

            
            return View(emp);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditEmployee(Employee employee)
        {
            
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd2 = new MySqlCommand("select employee.ssn from employee where employee.employeeID != "+employee.ID+" and employee.ssn = " + employee.Ssn + "; ", conn);
            var reader = cmd2.ExecuteReader();

            if (reader.Read())
            {
                ModelState.AddModelError("Ssn", "No Duplicate SSN");
            }

            reader.Close();
            cmd2 = new MySqlCommand("select depID from department where depID = "+employee.DepID+";", conn);
            reader = cmd2.ExecuteReader();
            if(!reader.Read() && employee.DepID != 0)
            {
                ModelState.AddModelError("DepId","Department number doesn't exist");
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
            if(reader.Read())
            {
                int id = getIntValue(reader["employeeID"]);
                if(id == employee.ID)
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
                cmd.Parameters.AddWithValue("@ssn", employee.Ssn);
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
                TempData["success"] = "Employee edited successfully";
                return RedirectToAction("Index");

            }
           
            
            return View(employee);
        }

        public IActionResult LoginDetails(int id)
        {
            var login = new Login();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select l.username, l.user_password, l.user_privilege from employee as e, login as l where l.employeeID = e.employeeID and e.employeeID ='" + id + "'", conn);
            var reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                login.username = getStringValue(reader["username"]);
                login.password = getStringValue(reader["user_password"]);
                login.privilege = getStringValue(reader["user_privilege"]);
                return View(login);
            }
            else
            {
                return RedirectToAction("AddLogin", new { loginId = id});
            }
            
        }

        public IActionResult AddLogin(int loginId)
        {
            TempData["loginID"] = loginId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddLogin(Login login)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            if(login.privilege != "Admin" && login.privilege != "Employee" && login.privilege != "Manager")
            {
                ModelState.AddModelError("privilege", "Privilege must be either Admin, Employee, or Manager");
            }
            if (ModelState.IsValid)
            {
                MySqlCommand insert = new MySqlCommand("insert into login(employeeID, username, user_password, user_privilege) Values("+ login.ID + ", '" + login.username + "', '" + login.password + "', '" + login.privilege + "');", conn);

                var reader2 = insert.ExecuteReader();
                conn.Close();
                TempData["success"] = "Login added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(login);
            }
                  
        }

        public IActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDepartment(Department obj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select mgrID from department where mgrID = " + obj.mgrID + "; ", conn);

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows && obj.mgrID != 0)
            {
                ModelState.AddModelError("mgrID", "EmployeeID doesn't exist");
            }
            reader.Close();
            
            if (ModelState.IsValid)
            {

                string query;
                if (obj.mgrID == 0)
                {
                    query = "insert into department(location, depName) Values('" + obj.location + "', '" + obj.depName + "');";
                }
                else
                {
                    query = "insert into department(location, depName, mgrID) Values('" + obj.location + "', '" + obj.depName + "', '" + obj.mgrID + "');";
                }
               
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();               
                conn.Close();
                TempData["success"] = "Department succesfully added";
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }


            
        }

        public IActionResult EditDepartment(int id)
        {
            Department dep = new Department();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from department where depID = " + id + "; ", conn);

            var reader = cmd.ExecuteReader();
            reader.Read();

            dep.depID = getIntValue(reader["depID"]);
            dep.location = getStringValue(reader["location"]);
            dep.depName = getStringValue(reader["depName"]);
            dep.mgrID = getIntValue(reader["mgrID"]);
       
            return View(dep);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDepartment(Department dep)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select mgrID from department where mgrID = " + dep.mgrID + "; ", conn);

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows && dep.mgrID != 0)
            {
                ModelState.AddModelError("mgrID", "EmployeeID doesn't exist");
            }
            reader.Close();

            if (ModelState.IsValid)
            {
                string query = "UPDATE department SET depName=@depName, location=@location, mgrID=@mgrID where depID = " + dep.depID + ";";

                MySqlCommand cmd2 = new MySqlCommand();

                cmd2.CommandText = query;
                cmd2.Parameters.AddWithValue("@depName", dep.depName);
                cmd2.Parameters.AddWithValue("@location", dep.location);
                if(dep.mgrID == 0)
                {
                    cmd2.Parameters.AddWithValue("@mgrID", DBNull.Value);
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@mgrID", dep.mgrID);
                }
                cmd2.Connection = conn;
                cmd2.ExecuteNonQuery();
                
                TempData["success"] = "Department edited successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(dep);
            }           
        }

        public IActionResult AddSupplier()
        {

            return View();
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
                if (obj.roleID == 0)
                {
                    query = "insert into suppliers(product, name) Values('" + obj.product + "', '" + obj.name + "');";
                }
                else
                {
                    query = "insert into suppliers(product, name, roleID) Values('" + obj.product + "', '" + obj.name + "', '" + obj.roleID + "');";
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

            return View(sup);
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

                TempData["success"] = "Supplier edited successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(sup);
            }
        }

        public IActionResult AddProject()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProject(Project obj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select depID from department where depID = " + obj.depID + "; ", conn);

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows && obj.depID != 0)
            {
                ModelState.AddModelError("depID", "DepartmentID doesn't exist");
            }
            reader.Close();

            string query = "select projName from project where projName = '" + obj.projName + "';";
            cmd.CommandText = query;
            cmd.Connection = conn;
            reader = cmd.ExecuteReader();
            if(reader.HasRows)
            {
                ModelState.AddModelError("projName", "Duplicate project name not allowed");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
               
                if (obj.depID == 0)
                {
                    query = "insert into project (dueDate, projName, location, cost, field, projStatus) VALUES ('" + obj.dueDate + "', '" + obj.projName + "', '" + obj.location +
                        "', '" + obj.cost + "', '" + obj.field + "', '" + obj.projStatus + "');";
                }
                else
                {
                    query = "insert into project (dueDate, projName, location, cost, field, projStatus, depID) VALUES ('" + obj.dueDate + "', '" + obj.projName + "', '" + obj.location +
                        "', '" + obj.cost + "', '" + obj.field + "', '" + obj.projStatus + "', '" + obj.depID + "');";
                }

                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["success"] = "Project succesfully added";
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
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
           
            return View(proj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProject(Project proj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select depID from department where depID = " + proj.depID + "; ", conn);

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows && proj.depID != 0)
            {
                ModelState.AddModelError("depID", "DepartmentID doesn't exist");
            }
            reader.Close();

            string query = "select projName from project where projName = '" + proj.projName + "' and projID != " + proj.projID + ";";
            cmd.CommandText = query;
            cmd.Connection = conn;
            reader = cmd.ExecuteReader();
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
                if (proj.depID == 0)
                {
                    cmd2.Parameters.AddWithValue("@depID", DBNull.Value);
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@depID", proj.depID);
                }
                cmd2.Parameters.AddWithValue("@location", proj.location);
                cmd2.Parameters.AddWithValue("@cost", proj.cost);
                cmd2.Parameters.AddWithValue("@field", proj.field);
                cmd2.Parameters.AddWithValue("@projStatus", proj.projStatus);
            
                cmd2.Connection = conn;
                cmd2.ExecuteNonQuery();

                TempData["success"] = "Project edited successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(proj);
            }
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRole(Role obj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
           
            string query = "select roleName from roles where roleName = '" + obj.roleName + "';";
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
                query = "insert into roles (roleName) VALUES ('" + obj.roleName + "');";
                
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["success"] = "Role succesfully added";
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
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
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRole(Role role)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();

            string query = "select roleName from roles where roleName = '" + role.roleName + "' and roleID != " + role.roleID + ";";
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
                return View(role);
            }
        }

        public IActionResult AddTask()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTask(Tasks obj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();          
            MySqlCommand cmd = new MySqlCommand("select projID from task where projID = " + obj.projID + "; ", conn);

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows && obj.projID != 0)
            {
                ModelState.AddModelError("projID", "Project ID doesn't exist");
            }
            reader.Close();           
            if (ModelState.IsValid)
            {
                string query;
                if (obj.projID == 0)
                {
                    query = "insert into task (taskName, cost, taskDueDate) VALUES ('" + obj.taskName + "', '" + obj.cost + "', '" + obj.taskDueDate +"');";
                }
                else
                {
                    query = "insert into task (taskName, cost, taskDueDate, projID) VALUES ('" + obj.taskName + "', '" + obj.cost + "', '"
                        + obj.taskDueDate + "', '" + obj.projID + "');";
                }

                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["success"] = "Task succesfully added";
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
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
            task.deleted_flag = getIntValue(reader["deleted_flag"]);

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTask(Tasks task)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select projID from task where projID = " + task.projID + "; ", conn);

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows && task.projID != 0)
            {
                ModelState.AddModelError("projID", "Project ID doesn't exist");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                string query = "UPDATE task SET taskName=@taskName, cost=@cost, taskDueDate=@date, projID=@projID where taskID = " + task.taskID + ";";
                MySqlCommand cmd2 = new MySqlCommand();

                cmd2.CommandText = query;
                cmd2.Parameters.AddWithValue("@taskName", task.taskName);
                cmd2.Parameters.AddWithValue("@cost", task.cost);
                cmd2.Parameters.AddWithValue("@date", task.taskDueDate);
                if (task.projID == 0)
                {
                    cmd2.Parameters.AddWithValue("@projID", DBNull.Value);
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@projID", task.projID);
                }
                cmd2.Connection = conn;
                cmd2.ExecuteNonQuery();

                conn.Close();
                TempData["success"] = "Task edited added";
                return RedirectToAction("Index");
            }
            else
            {
                return View(task);
            }
        }

        public IActionResult DeleteEmployee(int id)
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
            if(flag == 1)
            {
                cmd.Parameters.AddWithValue("@deleted_flag", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@deleted_flag", 1);
            }
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteDepartment(int id)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select department.deleted_flag from department where department.depID = " + id + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            int flag = getIntValue(reader["deleted_flag"]);
            reader.Close();

            string query = "UPDATE department SET deleted_flag=@deleted_flag where depID = " + id + ";";
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

            return RedirectToAction("Index");       
        }

        public IActionResult DeleteSupplier(int id)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select suppliers.deleted_flag from suppliers where suppliers.supID = " + id + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            int flag = getIntValue(reader["deleted_flag"]);
            reader.Close();

            string query = "UPDATE suppliers SET deleted_flag=@deleted_flag where supID = " + id + ";";
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

            return RedirectToAction("Index");
        }

        public IActionResult DeleteProject(int id)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select project.deleted_flag from project where project.projID = " + id + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            int flag = getIntValue(reader["deleted_flag"]);
            reader.Close();

            string query = "UPDATE project SET deleted_flag=@deleted_flag where projID = " + id + ";";
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

            return RedirectToAction("Index");
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
            return View(role);
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
                return View(role);
            }           
        }

        public IActionResult DeleteTask(int id)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select task.deleted_flag from task where task.taskID = " + id + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            int flag = getIntValue(reader["deleted_flag"]);
            reader.Close();

            string query = "UPDATE task SET deleted_flag=@deleted_flag where taskID = " + id + ";";
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

            return RedirectToAction("Index");
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
            model.Tasks = getTaskData();
            model.Assets = getAssetsData();
            model.Locations = getLocationsData();
            model.Distributions = getDistributionsData();
            model.UsedBy = getUsedByData();
            model.Works_Ons = getWorksOnData();

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
                        mgrID = getIntValue(reader["mgrID"]),
                        deleted_flag = getIntValue(reader["deleted_flag"])                    
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

        public List<Tasks> getTaskData()
        {
            MySqlConnection conn = GetConnection();
            List<Tasks> TaskData = new List<Tasks>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from task", conn);

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
                        deleted_flag = getIntValue(reader["deleted_flag"]),

                    });
                }
            }
            conn.Close();

            return TaskData;
        }

        public List<Asset> getAssetsData()
        {
            MySqlConnection conn = GetConnection();
            List<Asset> AssetsData = new List<Asset>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from assets", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    AssetsData.Add(new Asset()
                    {
                        assetID = getIntValue(reader["assetID"]),
                        type = getStringValue(reader["type"]),
                        cost = getIntValue(reader["cost"]),
                        supID = getIntValue(reader["supID"]),
                        deleted_flag = getIntValue(reader["deleted_flag"])

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

            MySqlCommand cmd = new MySqlCommand("select * from dep_locations", conn);

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

        public List<Distributed_to> getDistributionsData()
        {
            MySqlConnection conn = GetConnection();
            List<Distributed_to> DistributionsData = new List<Distributed_to>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from distributed_to", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    DistributionsData.Add(new Distributed_to()
                    {
                        depID = getIntValue(reader["depID"]),
                        supID = getIntValue(reader["supID"]),
                        assetID = getIntValue(reader["assetID"]),
                        status = Convert.ToDecimal(reader["status"])
                    });
                }
            }
            conn.Close();

            return DistributionsData;
        }

        public List<Used_by> getUsedByData()
        {
            MySqlConnection conn = GetConnection();
            List<Used_by> UsedByData = new List<Used_by>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from used_by", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    UsedByData.Add(new Used_by()
                    {
                        employeeID = getIntValue(reader["employeeID"]),
                        supID = getIntValue(reader["supID"]),
                        assetID = getIntValue(reader["assetID"]),
                        status = Convert.ToDecimal(reader["status"])
                    });
                }
            }
            conn.Close();

            return UsedByData;
        }

        public List<Works_on> getWorksOnData()
        {
            MySqlConnection conn = GetConnection();
            List<Works_on> WorksOnData = new List<Works_on>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from works_on", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    WorksOnData.Add(new Works_on()
                    {
                        employeeID = getIntValue(reader["employeeID"]),
                        TaskID = getIntValue(reader["taskID"]),
                        hours = Convert.ToDecimal(reader["hours"])
                       
                    });
                }
            }
            conn.Close();

            return WorksOnData;
        }
    }
}