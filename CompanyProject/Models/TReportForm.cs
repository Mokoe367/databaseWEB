using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class TReportForm
    {
        public int projID { get; set; }
        public List<SelectListItem> projects { get; set; }
    }
}
