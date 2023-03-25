using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProject.Models
{
    public class Logs
    {
        public List<distributed_to_audit> distributed_To_Audits { set; get; }
        public List<Used_by_audit> used_By_Audits { set; get; }
        public List<works_on_audit> works_On_Audits { set; get; }
    }
}
