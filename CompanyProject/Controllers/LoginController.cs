using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompanyProject.Models;
using MySql.Data.MySqlClient;

namespace CompanyProject.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Verify(Login acc)
        {
            MySqlConnection conn = new MySqlConnection("server = localhost; port=3306;database=target;user=root;password=MonkeysInc7!");
            
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from login where username='"+acc.username+"' and user_password='"+acc.password+"'", conn);
            var reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                string privilege = reader["user_privilege"].ToString();

                if (privilege == "Admin")
                {
                    conn.Close();
                    return RedirectToAction("Index", "Employee");
                }
                else if (privilege == "Employee")
                {
                    conn.Close();
                    ViewData["LoginFlag"] = "test";
                    return View("Login");
                }

                ViewData["LoginFlag"] = "Something went Wrong";
                return View("Login");
            }
            else
            {
                conn.Close();
                ViewData["LoginFlag"] = "Invalid username or Password";
                return View("Login");
            }
           
           

            
        }
    }
}