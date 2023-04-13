using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            return new MySqlConnection("server = databaseproject.czelvhdtgas7.us-east-2.rds.amazonaws.com; port=3306;database=target;user=root;password=group2database");
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

        public IActionResult DeletedLogs()
        {
            return View(getLogs());
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
                MySqlCommand insert = new MySqlCommand(); 
                string query = "insert into employee(Fname, Lname, Mname, salary, ssn, address, birthDate, sex) Values(@Fname, @Lname, @Mname, @salary, @ssn, @address, @birthDate, @sex);";
                insert.CommandText = query;
                insert.Parameters.AddWithValue("@Fname", obj.Fname);
                insert.Parameters.AddWithValue("@Mname", obj.Mname);
                insert.Parameters.AddWithValue("@Lname", obj.Lname);
                insert.Parameters.AddWithValue("@sex", obj.Sex);
                insert.Parameters.AddWithValue("@birthdate", obj.BirthDate);
                insert.Parameters.AddWithValue("@salary", obj.Salary);
                insert.Parameters.AddWithValue("@ssn", obj.Ssn);
                insert.Parameters.AddWithValue("@address", obj.Address);
                insert.Connection = conn;
                insert.ExecuteNonQuery();
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
            MySqlCommand cmd = new MySqlCommand("select e.employeeID, e.Fname, e.Lname, e.Mname, e.Address, e.sex, e.birthDate, e.deleted_flag, e.roleID, e.depID, e.ssn, e.salary, e.superID, r.roleName, " +
                "s.Fname as superFname, s.Lname as superLname, s.Mname as superMname, d.depName" +
                " from employee as e left outer join employee as s on e.superID = s.employeeID left outer join roles as r on r.roleID = e.roleID left outer join department as d on d.depID = e.depID where e.employeeID = " + id+" ;", conn);

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
            emp.superFname = superFname;
            emp.superMname = superMname;
            emp.superLname = superLname;
            conn.Close();
            return View(emp);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditEmployee(Employee employee, bool check)
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
            cmd2 = new MySqlCommand("select depID from department where (depID = @depName or depName = @depName);", conn);
            cmd2.Parameters.AddWithValue("@depName", employee.DepName);
            reader = cmd2.ExecuteReader();
            if(!reader.HasRows && !string.IsNullOrEmpty(employee.DepName))
            {
                ModelState.AddModelError("depName","Department doesn't exist");
            }
            else if(reader.HasRows)
            {
                reader.Read();
                employee.DepID = getIntValue(reader["depID"]);
            }
            reader.Close();

            cmd2 = new MySqlCommand("select roleId from roles where (roleId = @roleName or roleName = @roleName);", conn);
            cmd2.Parameters.AddWithValue("@roleName", employee.RoleName);
            reader = cmd2.ExecuteReader();
            if (!reader.HasRows && !string.IsNullOrEmpty(employee.RoleName))
            {
                ModelState.AddModelError("roleName", "Role doesn't exist");               
            }
            else if (reader.HasRows)
            {
                reader.Read();
                employee.RoleID = getIntValue(reader["roleId"]);
            }
            reader.Close();
            
            if(check)
            {
                if (string.IsNullOrEmpty(employee.superFname) && string.IsNullOrEmpty(employee.superMname) && string.IsNullOrEmpty(employee.superLname))
                {
                    employee.SuperID = 0;
                }
                else
                {
                    cmd2 = new MySqlCommand("select employeeID from employee where " +
                        "(employee.Fname = @superFname and employee.Mname = @superMname and employee.Lname = @superLname);", conn);
                    cmd2.Parameters.AddWithValue("@superFname", employee.superFname);
                    cmd2.Parameters.AddWithValue("@superMname", employee.superMname);
                    cmd2.Parameters.AddWithValue("@superLname", employee.superLname);

                    reader = cmd2.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        ModelState.AddModelError("superLname", "superviser doesn't exist");
                    }
                    if (reader.Read())
                    {
                        int id = getIntValue(reader["employeeID"]);
                        if (id == employee.ID)
                        {
                            ModelState.AddModelError("superID", "supervisor can't be same as employeeID");
                        }
                        else
                        {
                            employee.SuperID = id;
                        }

                    }
                    reader.Close();                    
                }               
            }
            else
            {
                cmd2 = new MySqlCommand("select employeeID from employee where employeeID = " + employee.SuperID + ";", conn);

                reader = cmd2.ExecuteReader();
                if (!reader.HasRows && employee.SuperID != 0)
                {
                    ModelState.AddModelError("superID", "superviser doesn't exist");
                }
                if (reader.Read())
                {
                    int id = getIntValue(reader["employeeID"]);
                    if (id == employee.ID)
                    {
                        ModelState.AddModelError("superID", "supervisor can't be same as employeeID");
                    }
                    else
                    {
                        employee.SuperID = id;
                    }

                }
                reader.Close();                
            }            

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
                if (string.IsNullOrEmpty(employee.DepName))
                {
                    cmd.Parameters.AddWithValue("@depId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@depId", employee.DepID);
                }

                if (string.IsNullOrEmpty(employee.RoleName))
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

        public IActionResult LoginDetails(int id)
        {
            var login = new Login();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select l.username, l.user_password, l.user_privilege, l.employeeID from employee as e, login as l where l.employeeID = e.employeeID and e.employeeID ='" + id + "'", conn);
            var reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                login.ID = getIntValue(reader["employeeID"]);
                login.username = getStringValue(reader["username"]);
                login.password = getStringValue(reader["user_password"]);
                login.privilege = getStringValue(reader["user_privilege"]);
                conn.Close();
                return View(login);
            }
            else
            {
                conn.Close();
                return RedirectToAction("AddLogin", new { loginId = id});
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
            string query = "select username from login where username = @username and employeeID != " + login.ID + " ;";
            cmd.Parameters.AddWithValue("@username", login.username);
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
                return View(login);
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
            MySqlCommand cmd = new MySqlCommand();
            conn.Open();
            if(login.privilege != "Admin" && login.privilege != "Employee" && login.privilege != "Manager")
            {
                ModelState.AddModelError("privilege", "Privilege must be either Admin, Employee, or Manager");
            }
            string query = "select username from login where username = @username;";
            cmd.Parameters.AddWithValue("@username", login.username);
            cmd.CommandText = query;
            cmd.Connection = conn;
            var reader = cmd.ExecuteReader();
            if(reader.HasRows)
            {
                ModelState.AddModelError("username", "Username already exists");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
           
                var salt = DateTime.Now.ToString();
                var hash = HashPassword($"{login.password}{salt}");

                MySqlCommand insert = new MySqlCommand();
                query = "insert into login(employeeID, username, user_password, user_privilege, hash, salt) " +
                    "Values(@employeeID, @username, @user_password, @user_privilege, @hash, @salt);";
                insert.CommandText = query;
                insert.Parameters.AddWithValue("@employeeID", login.ID);
                insert.Parameters.AddWithValue("@username", login.username);
                insert.Parameters.AddWithValue("@user_password", login.password);
                insert.Parameters.AddWithValue("@user_privilege", login.privilege);
                insert.Parameters.AddWithValue("@hash", hash);
                insert.Parameters.AddWithValue("@salt", salt);
                insert.Connection = conn;
                insert.ExecuteNonQuery();
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
        public IActionResult AddDepartment(Department obj, bool check)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            if (check)
            {
                if (string.IsNullOrEmpty(obj.mgrFname) && string.IsNullOrEmpty(obj.mgrMname) && string.IsNullOrEmpty(obj.mgrLname))
                {
                    obj.mgrID = 0;
                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand("select employeeID from employee where employee.Fname = @mgrFname and employee.Mname = @mgrMname and employee.Lname = @mgrLname", conn);
                    cmd.Parameters.AddWithValue("@mgrFname", obj.mgrFname);
                    cmd.Parameters.AddWithValue("@mgrMname", obj.mgrMname);
                    cmd.Parameters.AddWithValue("@mgrLname", obj.mgrLname);

                    var reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        ModelState.AddModelError("mgrLname", "Employee doesn't exist");
                    }
                    else if (reader.HasRows)
                    {
                        reader.Read();
                        obj.mgrID = getIntValue(reader["employeeID"]);
                    }
                    reader.Close();
                }
            }
            else
            {
                MySqlCommand cmd = new MySqlCommand("select employeeID from employee where employeeID = " + obj.mgrID + "; ", conn);

                var reader = cmd.ExecuteReader();

                if (!reader.HasRows && obj.mgrID != 0)
                {
                    ModelState.AddModelError("mgrID", "Employee doesn't exist");
                }
                reader.Close();
            }
            if (ModelState.IsValid)
            {

                string query;                           
                query = "insert into department(location, depName, mgrID) Values(@location, @depName, @mgrID);";
                MySqlCommand cmd2 = new MySqlCommand();
                cmd2.CommandText = query;
                cmd2.Parameters.AddWithValue("@depName", obj.depName);
                cmd2.Parameters.AddWithValue("@location", obj.location);
                if (obj.mgrID == 0)
                {
                    cmd2.Parameters.AddWithValue("@mgrID", DBNull.Value);
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@mgrID", obj.mgrID);
                }
                cmd2.Connection = conn;
                cmd2.ExecuteNonQuery();
                  
                conn.Close();
                TempData["success"] = "Department succesfully added";
                return RedirectToAction("Index");
            }
            else
            {
                conn.Close();
                return View(obj);
            }


            
        }

        public IActionResult EditDepartment(int id)
        {
            Department dep = new Department();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select d.depID, d.location, d.depName, d.mgrID, d.deleted_flag, s.Fname, s.Lname, s.Mname " +
                "from department as d left outer join employee as s on d.mgrID = s.employeeID where d.depID = " + id + "; ", conn);

            var reader = cmd.ExecuteReader();
            reader.Read();
            string Fname = getStringValue(reader["Fname"]);
            string Mname = getStringValue(reader["Mname"]);
            string Lname = getStringValue(reader["Lname"]);
            string mgrName = Fname + " " + Mname + " " + Lname;
            dep.depID = getIntValue(reader["depID"]);
            dep.location = getStringValue(reader["location"]);
            dep.depName = getStringValue(reader["depName"]);
            dep.mgrID = getIntValue(reader["mgrID"]);
            dep.mgrName = mgrName;
            dep.mgrFname = Fname;
            dep.mgrMname = Mname;
            dep.mgrLname = Lname;
            conn.Close();
            return View(dep);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDepartment(Department dep, bool check)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            if(check)
            {
                if (string.IsNullOrEmpty(dep.mgrFname) && string.IsNullOrEmpty(dep.mgrMname) && string.IsNullOrEmpty(dep.mgrLname))
                {
                    dep.mgrID = 0;
                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand("select employeeID from employee where employee.Fname = @mgrFname and employee.Mname = @mgrMname and employee.Lname = @mgrLname", conn);
                    cmd.Parameters.AddWithValue("@mgrFname", dep.mgrFname);
                    cmd.Parameters.AddWithValue("@mgrMname", dep.mgrMname);
                    cmd.Parameters.AddWithValue("@mgrLname", dep.mgrLname);

                    var reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        ModelState.AddModelError("mgrLname", "Employee doesn't exist");
                    }
                    else if(reader.HasRows)
                    {
                        reader.Read();
                        dep.mgrID = getIntValue(reader["employeeID"]);
                    }
                    reader.Close();
                }               
            }
            else
            {
                MySqlCommand cmd = new MySqlCommand("select employeeID from employee where employeeID = " + dep.mgrID + "; ", conn);

                var reader = cmd.ExecuteReader();

                if (!reader.HasRows && dep.mgrID != 0)
                {
                    ModelState.AddModelError("mgrID", "EmployeeID doesn't exist");
                }
                reader.Close();
            }           
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
                conn.Close();
                TempData["success"] = "Department edited successfully";
                return RedirectToAction("Index");
            }
            else
            {
                conn.Close();
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
            MySqlCommand cmd = new MySqlCommand("select roleID from roles where roleID = @roleName or roleName = @roleName", conn);
            cmd.Parameters.AddWithValue("@roleName", obj.roleName);
            var reader = cmd.ExecuteReader();

            if (!reader.HasRows && !string.IsNullOrEmpty(obj.roleName))
            {
                ModelState.AddModelError("roleName", "Role doesn't exist");
            }
            else if (reader.HasRows)
            {
                reader.Read();
                obj.roleID = getIntValue(reader["roleID"]);
            }
            reader.Close();

            if (ModelState.IsValid)
            {

                string query;               
                query = "insert into suppliers(product, name, roleID) Values(@product, @name, @roleID);";

                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@product", obj.product);
                cmd.Parameters.AddWithValue("@name", obj.name);
                if (string.IsNullOrEmpty(obj.roleName))
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
                conn.Close();
                return View(obj);
            }
          
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

            return View(sup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSupplier(Supplier sup)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select roleID from roles where roleID = @roleName or roleName = @roleName", conn);
            cmd.Parameters.AddWithValue("@roleName", sup.roleName);
            var reader = cmd.ExecuteReader();

            if (!reader.HasRows && !string.IsNullOrEmpty(sup.roleName))
            {
                ModelState.AddModelError("roleName", "Role doesn't exist");
            }
            else if (reader.HasRows)
            {
                reader.Read();
                sup.roleID = getIntValue(reader["roleID"]);
            }
            reader.Close();

            if (ModelState.IsValid)
            {
                string query = "UPDATE suppliers SET product=@product, name=@name, roleID=@roleID where supID = " + sup.supID + ";";

                MySqlCommand cmd2 = new MySqlCommand();

                cmd2.CommandText = query;
                cmd2.Parameters.AddWithValue("@product", sup.product);
                cmd2.Parameters.AddWithValue("@name", sup.name);
                if (string.IsNullOrEmpty(sup.roleName))
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
            MySqlCommand cmd = new MySqlCommand("select depID from department where depID = @depName or depName = @depName", conn);
            cmd.Parameters.AddWithValue("@depName", obj.depName);
            var reader = cmd.ExecuteReader();

            if (!reader.HasRows && !string.IsNullOrEmpty(obj.depName))
            {
                ModelState.AddModelError("depName", "Department doesn't exist");
            }
            else if(reader.HasRows)
            {
                reader.Read();
                obj.depID = getIntValue(reader["depID"]);
            }
            reader.Close();

            string query = "select projName from project where projName = @projName;";
            cmd.Parameters.AddWithValue("@projName", obj.projName);
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
                             
               query = "insert into project (dueDate, projName, location, cost, field, projStatus, depID) " +
                        "VALUES (@dueDate, @projName, @location, @cost, @field, @projStatus, @depID);";

                MySqlCommand cmd2 = new MySqlCommand();
                cmd2.CommandText = query;
                cmd2.Parameters.AddWithValue("@projName", obj.projName);
                cmd2.Parameters.AddWithValue("@dueDate", obj.dueDate);
                if (obj.depID == 0)
                {
                    cmd2.Parameters.AddWithValue("@depID", DBNull.Value);
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@depID", obj.depID);
                }
                cmd2.Parameters.AddWithValue("@location", obj.location);
                cmd2.Parameters.AddWithValue("@cost", obj.cost);
                cmd2.Parameters.AddWithValue("@field", obj.field);
                cmd2.Parameters.AddWithValue("@projStatus", obj.projStatus);

                cmd2.Connection = conn;
                cmd2.ExecuteNonQuery();
                conn.Close();
                TempData["success"] = "Project succesfully added";
                return RedirectToAction("Index");
            }
            else
            {
                conn.Close();
                return View(obj);
            }

        }

        public IActionResult EditProject(int id)
        {
            Project proj = new Project();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select p.projID, p.projName, p.dueDate, p.projID, p.location, p.cost, p.field, p.projStatus, p.deleted_flag, p.depID, d.depName" +
                " from project as p left outer join department as d on d.depID = p.depID where p.projID = "+id+";", conn);
   
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
            proj.depName = getStringValue(reader["depName"]);
            conn.Close();
            return View(proj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProject(Project proj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select depID from department where depID = @depName or depName = @depName", conn);
            cmd.Parameters.AddWithValue("@depName", proj.depName);

            var reader = cmd.ExecuteReader();

            if (!reader.HasRows && !string.IsNullOrEmpty(proj.depName))
            {
                ModelState.AddModelError("depName", "Department doesn't exist");
            }
            else if (reader.HasRows)
            {
                reader.Read();
                proj.depID = getIntValue(reader["depID"]);
            }
            reader.Close();

            string query = "select projName from project where projName = @projName and projID != " + proj.projID + ";";
            cmd.Parameters.AddWithValue("@projName", proj.projName);
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
                if (string.IsNullOrEmpty(proj.depName))
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
            MySqlCommand cmd = new MySqlCommand("select projID from project where projID = @projName or projName = @projName", conn);
            cmd.Parameters.AddWithValue("@projName", obj.projName);
            var reader = cmd.ExecuteReader();

            if (!reader.HasRows && !string.IsNullOrEmpty(obj.projName))
            {
                ModelState.AddModelError("projName", "Project doesn't exist");
            }
            else if(reader.HasRows)
            {
                reader.Read();
                obj.projID = getIntValue(reader["projID"]);
            }
            reader.Close();           
            if (ModelState.IsValid)
            {
                try
                {
                    string query;

                    query = "insert into task (taskName, cost, taskDueDate, projID) VALUES (@taskName, @cost, @date, @projID);";

                    MySqlCommand cmd2 = new MySqlCommand();

                    cmd2.CommandText = query;
                    cmd2.Parameters.AddWithValue("@taskName", obj.taskName);
                    cmd2.Parameters.AddWithValue("@cost", obj.cost);
                    cmd2.Parameters.AddWithValue("@date", obj.taskDueDate);
                    if (string.IsNullOrEmpty(obj.projName))
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
                catch (MySqlException e)
                {
                    conn.Close();
                    ModelState.AddModelError("projName", e.Message);
                    return View(obj);
                }
            }
            else
            {
                conn.Close();
                return View(obj);
            }
        }

        public IActionResult EditTask(int id)
        {
            Tasks task = new Tasks();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select t.taskID, t.taskName, t.taskDueDate, t.cost, t.projID, p.projName, t.deleted_flag " +
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
            task.deleted_flag = getIntValue(reader["deleted_flag"]);
            task.projName = getStringValue(reader["projName"]);
            conn.Close();
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTask(Tasks task)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select projID from project where projID = @projName or projName = @projName", conn);
            cmd.Parameters.AddWithValue("@projName", task.projName);
            var reader = cmd.ExecuteReader();

            if (!reader.HasRows && !string.IsNullOrEmpty(task.projName))
            {
                ModelState.AddModelError("projName", "Project doesn't exist");
            }
            else if (reader.HasRows)
            {
                reader.Read();
                task.projID = getIntValue(reader["projID"]);
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                try
                {
                    string query = "UPDATE task SET taskName=@taskName, cost=@cost, taskDueDate=@date, projID=@projID where taskID = " + task.taskID + ";";
                    MySqlCommand cmd2 = new MySqlCommand();

                    cmd2.CommandText = query;
                    cmd2.Parameters.AddWithValue("@taskName", task.taskName);
                    cmd2.Parameters.AddWithValue("@cost", task.cost);
                    cmd2.Parameters.AddWithValue("@date", task.taskDueDate);
                    if (string.IsNullOrEmpty(task.projName))
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
                catch (MySqlException e)
                {
                    conn.Close();
                    ModelState.AddModelError("projName", e.Message);
                    return View(task);
                }
            }
            else
            {
                conn.Close();
                return View(task);
            }
        }

        public IActionResult AddAsset()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAsset(Asset obj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select supID from suppliers where supID = @supName or name = @supName", conn);
            cmd.Parameters.AddWithValue("@supName", obj.supName);
            var reader = cmd.ExecuteReader();

            if (!reader.HasRows && !string.IsNullOrEmpty(obj.supName))
            {
                ModelState.AddModelError("supName", "Supplier doesn't exist");
            }
            else if (reader.HasRows)
            {
                reader.Read();
                obj.supID = getIntValue(reader["supID"]);
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                string query;
                
                query = "insert into assets (type, cost, supID) VALUES (@type, @cost, @supID);";
                MySqlCommand cmd2 = new MySqlCommand();
                cmd2.CommandText = query;
                cmd2.Parameters.AddWithValue("@type", obj.type);
                cmd2.Parameters.AddWithValue("@cost", obj.cost);
                if (string.IsNullOrEmpty(obj.supName))
                {
                    cmd2.Parameters.AddWithValue("@supID", DBNull.Value);
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@supID", obj.supID);
                }
                cmd2.Connection = conn;
                cmd2.ExecuteNonQuery();
                conn.Close();
                TempData["success"] = "Asset succesfully added";
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
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

            conn.Close();
            return View(asset);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAsset(Asset asset)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select supID from suppliers where supID = @supName or name = @supName", conn);
            cmd.Parameters.AddWithValue("@supName", asset.supName);
            var reader = cmd.ExecuteReader();

            if (!reader.HasRows && !string.IsNullOrEmpty(asset.supName))
            {
                ModelState.AddModelError("supName", "Supplier doesn't exist");
            }
            else if (reader.HasRows)
            {
                reader.Read();
                asset.supID = getIntValue(reader["supID"]);
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                string query = "UPDATE assets SET type=@type, cost=@cost, supID=@supID where assetID = " + asset.assetID + ";";
                MySqlCommand cmd2 = new MySqlCommand();

                cmd2.CommandText = query;
                cmd2.Parameters.AddWithValue("@type", asset.type);
                cmd2.Parameters.AddWithValue("@cost", asset.cost);               
                if (string.IsNullOrEmpty(asset.supName))
                {
                    cmd2.Parameters.AddWithValue("@supID", DBNull.Value);
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@supID", asset.supID);
                }
                cmd2.Connection = conn;
                cmd2.ExecuteNonQuery();

                conn.Close();
                TempData["success"] = "Asset edited added";
                return RedirectToAction("Index");
            }
            else
            {
                conn.Close();
                return View(asset);
            }
        }

        public IActionResult AddLocations()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddLocations(Dep_locations obj)
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
            string query = "select loc_name from dep_locations where loc_name = @name and depID = " + obj.depID + ";";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@name", obj.loc_name);           
            reader = cmd.ExecuteReader();
            if(reader.HasRows)
            {
                ModelState.AddModelError("loc_name", "Department ID and Location name already exist");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                query = "insert into dep_locations (loc_name, depID) VALUES (@loc_name, '" + obj.depID + "');"; ;
               
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
                return View(obj);
            }
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
                
                string query = "UPDATE dep_locations SET loc_name = @loc_name, depID = @depID  WHERE loc_name = @pastName " +
                    "and depID = " + location.pastDepID + ";";
                MySqlCommand cmd2 = new MySqlCommand();

                cmd2.CommandText = query;
                cmd2.Parameters.AddWithValue("@depID", location.depID);
                cmd2.Parameters.AddWithValue("@loc_name", location.loc_name);
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

        public IActionResult AddDistribution()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDistribution(Distributed_to obj)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select depID from department where depID = @depName or depName = @depName", conn);
            cmd.Parameters.AddWithValue("@depName", obj.depName);
            var reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                ModelState.AddModelError("depName", "Department ID doesn't exist");
            }
            else if (reader.HasRows)
            {
                reader.Read();
                obj.depID = getIntValue(reader["depID"]);
            }
            reader.Close();
            string test = "select supID from suppliers where supID = @supName or name = @supName;";
            cmd.CommandText = test;
            cmd.Parameters.AddWithValue("@supName", obj.supName);
            reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ModelState.AddModelError("supName", "Supplier ID doesn't exist");
            }
            else if (reader.HasRows)
            {
                reader.Read();
                obj.supID = getIntValue(reader["supID"]);
            }
            reader.Close();
            test = "select assetID from assets where assetID = @assetName or type = @assetName;";
            cmd.CommandText = test;
            cmd.Parameters.AddWithValue("@assetName", obj.assetName);
            reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ModelState.AddModelError("assetName", "Asset ID doesn't exist");
            }
            else if (reader.HasRows)
            {
                reader.Read();
                obj.assetID = getIntValue(reader["assetID"]);
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
            if (!reader.HasRows)
            {
                ModelState.AddModelError("assetName", "assetID not associated with supplier");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                string query = "insert into distributed_to (depID, supID, assetID, field) VALUES ('" + obj.depID + 
                    "', '" + obj.supID + "', '" + obj.assetID + "', @field);"; 
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@field", obj.field);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["success"] = "Distribution succesfully added";
                return RedirectToAction("Index");
            }
            else
            {
                conn.Close();
                return View(obj);
            }
        }       

        public IActionResult EditDistribution(int depId, int supId, int assetId)
        {
            Distributed_to distribution = new Distributed_to();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select d.depName, s.name, a.type, t.field, t.assetID, t.depID, t.supID " +
                "from distributed_to as t left outer join suppliers as s on s.supID = t.supID left outer join assets as a on a.assetID = t.assetID" +
                " left outer join department as d on d.depID = t.depID where t.depID = " + depId + " and " +
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
            conn.Close();
            return View(distribution);
        }

        public IActionResult AddUse()
        {           
            return View();
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
                return RedirectToAction("Index");
            }
            else
            {
                conn.Close();
                return View(obj);
            }
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
            conn.Close();
            return View(use);
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
                return RedirectToAction("Index");
            }
            else
            {
                conn.Close();
                return View(use);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDistribution(Distributed_to distribution)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select depID from department where depID = @depName or depName = @depName", conn);
            cmd.Parameters.AddWithValue("@depName", distribution.depName);
            var reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                ModelState.AddModelError("depName", "Department ID doesn't exist");
            }
            else if(reader.HasRows)
            {
                reader.Read();
                distribution.depID = getIntValue(reader["depID"]);
            }
            reader.Close();
            string test = "select supID from suppliers where supID = @supName or name = @supName;";            
            cmd.CommandText = test;
            cmd.Parameters.AddWithValue("@supName", distribution.supName);
            reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ModelState.AddModelError("supName", "Supplier ID doesn't exist");
            }
            else if (reader.HasRows)
            {
                reader.Read();
                distribution.supID = getIntValue(reader["supID"]);
            }
            reader.Close();
            test = "select assetID from assets where assetID = @assetName or type = @assetName;";          
            cmd.CommandText = test;
            cmd.Parameters.AddWithValue("@assetName", distribution.assetName);
            reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ModelState.AddModelError("assetName", "Asset ID doesn't exist");
            }
            else if (reader.HasRows)
            {
                reader.Read();
                distribution.assetID = getIntValue(reader["assetID"]);
            }
            reader.Close();
            test = "select depID, supID, assetID from distributed_to where depID = '" + distribution.depID + "' " +
                    "and supID = " + distribution.supID + " and assetID = " + distribution.assetID +";";
            cmd.CommandText = test;
            reader = cmd.ExecuteReader();
            if (reader.HasRows && (distribution.tempAssetID != distribution.assetID || distribution.tempDepID != distribution.depID || distribution.tempSupID != distribution.supID))
            {              
               ModelState.AddModelError("field", "Department ID, Supplier ID, and Asset ID already exist");                         
            }
            reader.Close();
            test = "select assetID from assets where supID = " + distribution.supID + " and assetID = " + distribution.assetID + ";";
            cmd.CommandText = test;
            reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ModelState.AddModelError("assetID", "assetID not associated with supplier");
            }
            reader.Close();
            if (ModelState.IsValid)
            {
                string query = "UPDATE distributed_to SET depID=@dep, supID=@sup, assetID=@asset, field=@field WHERE depID = '" + distribution.tempDepID + "' " +
                    "and supID = " + distribution.tempSupID + " and assetID = " + distribution.tempAssetID + ";";
                MySqlCommand cmd2 = new MySqlCommand();

                cmd2.CommandText = query;
                cmd2.Parameters.AddWithValue("@dep", distribution.depID);
                cmd2.Parameters.AddWithValue("@sup", distribution.supID);
                cmd2.Parameters.AddWithValue("@asset", distribution.assetID);
                cmd2.Parameters.AddWithValue("@field", distribution.field);
                cmd2.Connection = conn;
                cmd2.ExecuteNonQuery();

                conn.Close();
                TempData["success"] = "Distribution successfully edited";
                return RedirectToAction("Index");
            }
            else
            {
                conn.Close();
                return View(distribution);
            }
        }

        public IActionResult AddWork()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddWork(Works_on obj)
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
                    return RedirectToAction("Index");
                }
                catch(MySqlException e)
                {
                    ModelState.AddModelError("taskID", e.Message);
                    return View(obj);
                }
            }
            else
            {
                conn.Close();
                return View(obj);
            }
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
                try
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
                    return RedirectToAction("Index");
                }
                catch(MySqlException e)
                {
                    ModelState.AddModelError("taskID", e.Message);
                    return View(work);
                }
            }
            else
            {
                conn.Close();
                return View(work);
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
            conn.Close();
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
            conn.Close();
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
            conn.Close();
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
            conn.Close();
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
            conn.Close();
            return View(role);
        }

        public IActionResult DeleteAsset(int id)
        {
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select assets.deleted_flag from assets where assets.assetID = " + id + ";", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            int flag = getIntValue(reader["deleted_flag"]);
            reader.Close();

            string query = "UPDATE assets SET deleted_flag=@deleted_flag where assetID = " + id + ";";
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
            conn.Close();

            return RedirectToAction("Index");
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
                cmd2.Parameters.AddWithValue("@name", location.loc_name);
                cmd2.CommandText = query;
                cmd2.Connection = conn;
                cmd2.ExecuteNonQuery();

                conn.Close();
                TempData["success"] = "Location successfully deleted";
                return RedirectToAction("Index");
            }
            else
            {
                return View(location);
            }
        }

        public IActionResult DeleteDistribution(int depId, int supId, int assetId)
        {
            Distributed_to distribution = new Distributed_to();

            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select d.depName, s.name, a.type, t.field, t.assetID, t.depID, t.supID " +
                "from distributed_to as t left outer join suppliers as s on s.supID = t.supID left outer join assets as a on a.assetID = t.assetID" +
                " left outer join department as d on d.depID = t.depID where t.depID = " + depId + " and " +
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
            conn.Close();

            return View(distribution);
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
            return RedirectToAction("Index");
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
            conn.Close();

            return View(use);

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
            return RedirectToAction("Index");
        }

        public IActionResult DeleteWork(int empid, int taskid)
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

            conn.Close();

            return View(work);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteWork(Works_on work)
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

        public IEnumerable<Logs> getLogs()
        {
            List<Logs> logs = new List<Logs>();
            Logs model = new Logs();

            model.distributed_To_Audits = getDeletedDistribution();
            model.used_By_Audits = getDeletedUsedBy();
            model.works_On_Audits = getDeletedWorksOn();

            logs.Add(model);

            return logs;

        }

        public List<Employee> getEmployeeData()
        {
            MySqlConnection conn = GetConnection();
            List<Employee> employeeData = new List<Employee>();
            
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select e.employeeID, e.Fname, e.Lname, e.Mname, e.Address, e.sex, e.birthDate, e.deleted_flag, e.roleID, e.depID, e.ssn, e.salary, e.superID, r.roleName, " +
                "s.Fname as superFname, s.Lname as superLname, s.Mname as superMname, d.depName" +
                " from employee as e left outer join employee as s on e.superID = s.employeeID left outer join roles as r on r.roleID = e.roleID left outer join department as d on d.depID = e.depID;", conn);

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
                    string superFname = getStringValue(reader["superFname"]);
                    string superMname = getStringValue(reader["superMname"]);
                    string superLname = getStringValue(reader["superLname"]);
                    string supervisorName = superFname + " " + superMname + " " + superLname;
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
                        SuperID = getIntValue(reader["superID"]),
                        SupervisorName = supervisorName,
                        DepName = getStringValue(reader["depName"]),
                        RoleName = getStringValue(reader["roleName"]),
                        superFname = superFname,
                        superMname = superMname,
                        superLname = superLname
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

            MySqlCommand cmd = new MySqlCommand("select d.depID, d.location, d.depName, d.mgrID, d.deleted_flag, s.Fname, s.Lname, s.Mname " +
                "from department as d left outer join employee as s on d.mgrID = s.employeeID;", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    string Fname = getStringValue(reader["Fname"]);
                    string Mname = getStringValue(reader["Mname"]);
                    string Lname = getStringValue(reader["Lname"]);
                    string mgrName = Fname + " " + Mname + " " + Lname;
                    departmentData.Add(new Department()
                    {
                        depID = getIntValue(reader["depID"]),
                        location = getStringValue(reader["location"]),
                        depName = getStringValue(reader["depName"]),
                        mgrID = getIntValue(reader["mgrID"]),
                        deleted_flag = getIntValue(reader["deleted_flag"]),
                        mgrName = mgrName                     
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

            MySqlCommand cmd = new MySqlCommand("select p.projID, p.projName, p.dueDate, p.projID, p.location, p.cost, p.field, p.projStatus, p.deleted_flag, p.depID, d.depName" +
                " from project as p left outer join department as d on d.depID = p.depID;", conn);

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
                        deleted_flag = getIntValue(reader["deleted_flag"]),
                        depName = getStringValue(reader["depName"])
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

            MySqlCommand cmd = new MySqlCommand("select s.supID, s.product, s.name, s.deleted_flag, s.roleID, r.roleName " +
                "from suppliers as s left outer join roles as r on r.roleID = s.roleID", conn);

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
                        deleted_flag = getIntValue(reader["deleted_flag"]),
                        roleName = getStringValue(reader["roleName"])
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

            MySqlCommand cmd = new MySqlCommand("select t.taskID, t.taskName, t.taskDueDate, t.cost, t.projID, p.projName, t.deleted_flag " +
                "from task as t left outer join project as p on p.projID = t.projID;", conn);

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
                        projName = getStringValue(reader["projName"])

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

            MySqlCommand cmd = new MySqlCommand("select a.assetID, a.type, a.cost, a.deleted_flag, a.supID, s.name " +
                "from assets as a left outer join suppliers as s on s.supID = a.supID", conn);

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
                        deleted_flag = getIntValue(reader["deleted_flag"]),
                        supName = getStringValue(reader["name"])

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

            MySqlCommand cmd = new MySqlCommand("select d.depName, s.name, a.type, t.field, t.assetID, t.depID, t.supID " +
                "from distributed_to as t left outer join suppliers as s on s.supID = t.supID left outer join assets as a on a.assetID = t.assetID" +
                " left outer join department as d on d.depID = t.depID", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    DistributionsData.Add(new Distributed_to()
                    {
                        depID = getIntValue(reader["depID"]),
                        supID = getIntValue(reader["supID"]),
                        assetID = getIntValue(reader["assetID"]),
                        field = getStringValue(reader["field"]),
                        depName = getStringValue(reader["depName"]),
                        supName = getStringValue(reader["name"]),
                        assetName = getStringValue(reader["type"])
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
                        field = getStringValue(reader["field"])
                       
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

        public List<distributed_to_audit> getDeletedDistribution()
        {
            MySqlConnection conn = GetConnection();
            List<distributed_to_audit> WorksOnData = new List<distributed_to_audit>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from distributed_to_audits", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    WorksOnData.Add(new distributed_to_audit()
                    {
                        id = getIntValue(reader["id"]),
                        assetID = getIntValue(reader["assetID"]),
                        depID = getIntValue(reader["depID"]),
                        supID = getIntValue(reader["supID"]),
                        field = getStringValue(reader["field"]),
                        deleted_at = getStringValue(reader["deleted_at"])

                    });
                }
            }
            conn.Close();

            return WorksOnData;
        }

        public List<Used_by_audit> getDeletedUsedBy()
        {
            MySqlConnection conn = GetConnection();
            List<Used_by_audit> UseOnData = new List<Used_by_audit>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from used_by_audits", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    UseOnData.Add(new Used_by_audit()
                    {
                        id = getIntValue(reader["id"]),
                        assetID = getIntValue(reader["assetID"]),
                        empID = getIntValue(reader["employeeID"]),
                        supID = getIntValue(reader["supID"]),
                        field = getStringValue(reader["field"]),
                        deleted_at = getStringValue(reader["deleted_at"])

                    });
                }
            }
            conn.Close();

            return UseOnData;
        }

        public List<works_on_audit> getDeletedWorksOn()
        {
            MySqlConnection conn = GetConnection();
            List<works_on_audit> WorkOnData = new List<works_on_audit>();

            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select * from works_on_audits", conn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    WorkOnData.Add(new works_on_audit()
                    {
                        id = getIntValue(reader["id"]),
                        taskID = getIntValue(reader["taskID"]),
                        empID = getIntValue(reader["employeeID"]),                        
                        hours = Convert.ToDecimal(reader["hours"]),
                        deleted_at = getStringValue(reader["deleted_at"])

                    });
                }
            }
            conn.Close();

            return WorkOnData;
        }

        public string HashPassword(string password)
        {
            SHA256 hash = SHA256.Create();
            var passwordBytes = Encoding.Default.GetBytes(password);
            var hashedpassword = hash.ComputeHash(passwordBytes);

            return BitConverter.ToString(hashedpassword).Replace("-","");
        }
    }
}