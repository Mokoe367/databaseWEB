#pragma checksum "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1d58371d87d792620e8d2ccf14d52c2e515099a2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Manager_CostReportList), @"mvc.1.0.view", @"/Views/Manager/CostReportList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Manager/CostReportList.cshtml", typeof(AspNetCore.Views_Manager_CostReportList))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\_ViewImports.cshtml"
using CompanyProject;

#line default
#line hidden
#line 2 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\_ViewImports.cshtml"
using CompanyProject.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1d58371d87d792620e8d2ccf14d52c2e515099a2", @"/Views/Manager/CostReportList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc45b8a03ea425198c728c84e613c6da74b97dff", @"/Views/_ViewImports.cshtml")]
    public class Views_Manager_CostReportList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CompanyProject.Models.CostReport>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(41, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
  
    ViewData["Title"] = "CostReportList";
    Layout = "~/Views/Shared/_ManagerLayout.cshtml";

#line default
#line hidden
            BeginContext(147, 101, true);
            WriteLiteral("\r\n<style>\r\n    #table-div {\r\n        margin: 25px;\r\n    }\r\n</style>\r\n\r\n<div style=\"margin: 25px\">\r\n\r\n");
            EndContext();
#line 16 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
     if (ViewData["ProjectInfo"] != null)
    {

#line default
#line hidden
            BeginContext(298, 33, true);
            WriteLiteral("        <h2>Cost Report List For ");
            EndContext();
            BeginContext(332, 23, false);
#line 18 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
                            Write(ViewData["ProjectInfo"]);

#line default
#line hidden
            EndContext();
            BeginContext(355, 7, true);
            WriteLiteral("</h2>\r\n");
            EndContext();
#line 19 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"

    }

#line default
#line hidden
            BeginContext(371, 58, true);
            WriteLiteral("</div>\r\n\r\n<p style=\"margin: 25px\">\r\n    <strong>Budget = $");
            EndContext();
            BeginContext(430, 30, false);
#line 24 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
                 Write(Model.projectBudget.ToString());

#line default
#line hidden
            EndContext();
            BeginContext(460, 17, true);
            WriteLiteral(", Budget Used = $");
            EndContext();
            BeginContext(478, 25, false);
#line 24 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
                                                                 Write(Model.taskCost.ToString());

#line default
#line hidden
            EndContext();
            BeginContext(503, 22, true);
            WriteLiteral(", Budget Remaining = $");
            EndContext();
            BeginContext(526, 26, false);
#line 24 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
                                                                                                                 Write(Model.remaining.ToString());

#line default
#line hidden
            EndContext();
            BeginContext(552, 481, true);
            WriteLiteral(@"</strong>
</p>

<div id=""table-div"">
    <table class=""table table-hover col-md-3 table-striped table-bordered caption-top"">
        <caption>Tasks</caption>
        <thead>
            <tr>
                <th>
                    Task ID
                </th>
                <th>
                    Task Name
                </th>
                <th>
                    Task Budget
                </th>
            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 44 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
             foreach (var item in Model.tasks)
            {

#line default
#line hidden
            BeginContext(1096, 72, true);
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1169, 41, false);
#line 48 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
                   Write(Html.DisplayFor(modelItem => item.taskID));

#line default
#line hidden
            EndContext();
            BeginContext(1210, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1290, 43, false);
#line 51 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
                   Write(Html.DisplayFor(modelItem => item.taskName));

#line default
#line hidden
            EndContext();
            BeginContext(1333, 80, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        $");
            EndContext();
            BeginContext(1414, 39, false);
#line 54 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
                    Write(Html.DisplayFor(modelItem => item.cost));

#line default
#line hidden
            EndContext();
            BeginContext(1453, 52, true);
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 57 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
            }

#line default
#line hidden
            BeginContext(1520, 88, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n\r\n<p style=\"margin: 25px\"><strong>Task Total = $");
            EndContext();
            BeginContext(1609, 25, false);
#line 62 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
                                         Write(Model.taskCost.ToString());

#line default
#line hidden
            EndContext();
            BeginContext(1634, 741, true);
            WriteLiteral(@"</strong></p>

<div id=""table-div"">
    <table class=""table table-hover col-md-3 table-striped table-bordered caption-top"">
        <caption>Employee Pay</caption>
        <thead>
            <tr>
                <th>
                    Task Name
                </th>
                <th>
                    First Name
                </th>
                <th>
                    Last Name
                </th>
                <th>
                    Hours
                </th>
                <th>
                    Salary
                </th>
                <th>
                    Salary for every 8 hours worked on project
                </th>
            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 90 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
             foreach (var item in Model.taskReports)
            {

#line default
#line hidden
            BeginContext(2444, 72, true);
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2517, 43, false);
#line 94 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
                   Write(Html.DisplayFor(modelItem => item.taskName));

#line default
#line hidden
            EndContext();
            BeginContext(2560, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2640, 40, false);
#line 97 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Fname));

#line default
#line hidden
            EndContext();
            BeginContext(2680, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2760, 40, false);
#line 100 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Lname));

#line default
#line hidden
            EndContext();
            BeginContext(2800, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2880, 40, false);
#line 103 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
                   Write(Html.DisplayFor(modelItem => item.hours));

#line default
#line hidden
            EndContext();
            BeginContext(2920, 80, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        $");
            EndContext();
            BeginContext(3001, 41, false);
#line 106 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
                    Write(Html.DisplayFor(modelItem => item.salary));

#line default
#line hidden
            EndContext();
            BeginContext(3042, 80, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        $");
            EndContext();
            BeginContext(3123, 38, false);
#line 109 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
                    Write(Html.DisplayFor(modelItem => item.pay));

#line default
#line hidden
            EndContext();
            BeginContext(3161, 52, true);
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 112 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
            }

#line default
#line hidden
            BeginContext(3228, 90, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n\r\n<p style=\"margin: 25px\"><strong>Total Salary = $");
            EndContext();
            BeginContext(3319, 31, false);
#line 117 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
                                           Write(Model.employeeSalary.ToString());

#line default
#line hidden
            EndContext();
            BeginContext(3350, 78, true);
            WriteLiteral("</strong></p>\r\n\r\n<p style=\"margin: 25px;\"><strong>Total Budget with salary = $");
            EndContext();
            BeginContext(3429, 31, false);
#line 119 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\CostReportList.cshtml"
                                                        Write(Model.totalRemaining.ToString());

#line default
#line hidden
            EndContext();
            BeginContext(3460, 119, true);
            WriteLiteral("</strong></p>\r\n\r\n<script>\r\n    $(document).ready(function () {\r\n        $(\'table\').DataTable();\r\n    });\r\n</script>\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CompanyProject.Models.CostReport> Html { get; private set; }
    }
}
#pragma warning restore 1591
