using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using static System.Net.WebRequestMethods;


namespace CompanyProject.Controllers
{
    public class ProjectDocController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private MySqlConnection GetConnection()
        {
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

    }
}

