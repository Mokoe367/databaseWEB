﻿using System;
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
    public class ProjectHoursController : Controller
    {
        private MySqlConnection GetConnection()
        {
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

        public List<ProjectHoursReport> GetProjectHoursReport(int id)
        {
            List<ProjectHoursReport> ProjectHoursData = new List<ProjectHoursReport>();
            MySqlConnection conn = GetConnection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("select p.projID, p.projName, w.employeeID, w.hours, t.taskID " +
                "from task as t, works_on as w, project as p " + "where t.projID = p.projID and w.taskID = t.taskID " + "and p.projID = " + id 
                 + " order by projID;", conn);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    ProjectHoursData.Add(new ProjectHoursReport()
                    {
                        projID = getIntValue(reader["projID"]),
                        projName = getStringValue(reader["projName"]),
                        employeeID = getIntValue(reader["employeeID"]),
                        hours = Convert.ToDecimal(reader["hours"]),
                        taskID = getIntValue(reader["taskID"])

                    });
                }
            }

            conn.Close();
            return ProjectHoursData;
        }




        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Index(ProjectHoursReport obj)
        {
            IEnumerable<ProjectHoursReport> list = GetProjectHoursReport(obj.projID);
           

            
            return View("ProjectHoursList",list);

        }


    }
}

    
