﻿using System;
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
            if(reader.Read() && reader["user_privilege"].ToString() == "Admin")
            {
                conn.Close();
                return RedirectToAction("Index", "Employee");
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