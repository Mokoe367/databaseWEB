#pragma checksum "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\Manager\ProjectDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5f41e694b28c50659a47459ffda8a54d709df19b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Manager_ProjectDetails), @"mvc.1.0.view", @"/Views/Manager/ProjectDetails.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Manager/ProjectDetails.cshtml", typeof(AspNetCore.Views_Manager_ProjectDetails))]
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
#line 1 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\_ViewImports.cshtml"
using CompanyProject;

#line default
#line hidden
#line 2 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\_ViewImports.cshtml"
using CompanyProject.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5f41e694b28c50659a47459ffda8a54d709df19b", @"/Views/Manager/ProjectDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc45b8a03ea425198c728c84e613c6da74b97dff", @"/Views/_ViewImports.cshtml")]
    public class Views_Manager_ProjectDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CompanyProject.Models.ProjectInfo>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(42, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\Manager\ProjectDetails.cshtml"
  
    ViewData["Title"] = "ProjectDetails";
    Layout = "~/Views/Shared/_ManagerLayout.cshtml";

#line default
#line hidden
            BeginContext(148, 71, true);
            WriteLiteral("\r\n<style>\r\n    #table-div {\r\n        margin: 25px;\r\n    }\r\n</style>\r\n\r\n");
            EndContext();
#line 14 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\Manager\ProjectDetails.cshtml"
 if (ViewData["ProjectInfo"] != null)
{

#line default
#line hidden
            BeginContext(261, 29, true);
            WriteLiteral("    <h2 style=\"margin: 25px\">");
            EndContext();
            BeginContext(291, 34, false);
#line 16 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\Manager\ProjectDetails.cshtml"
                        Write(ViewData["ProjectInfo"].ToString());

#line default
#line hidden
            EndContext();
            BeginContext(325, 7, true);
            WriteLiteral("</h2>\r\n");
            EndContext();
#line 17 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\Manager\ProjectDetails.cshtml"
}

#line default
#line hidden
            BeginContext(335, 623, true);
            WriteLiteral(@"<div id=""table-div"">
    <table class=""table table-hover col-md-3 table-striped table-bordered caption-top"">
        <caption>Employee Progress</caption>
        <thead>
            <tr>
                <th>
                    Name
                </th>
                <th>
                    Role Name
                </th>
                <th>
                    Task Name
                </th>
                <th>
                    Hours
                </th>
                <th>
                    Employee Status
                </th>
            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 41 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\Manager\ProjectDetails.cshtml"
             foreach (var item in Model.details)
            {

#line default
#line hidden
            BeginContext(1023, 60, true);
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1084, 39, false);
#line 45 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\Manager\ProjectDetails.cshtml"
               Write(Html.DisplayFor(modelItem => item.name));

#line default
#line hidden
            EndContext();
            BeginContext(1123, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1191, 43, false);
#line 48 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\Manager\ProjectDetails.cshtml"
               Write(Html.DisplayFor(modelItem => item.roleName));

#line default
#line hidden
            EndContext();
            BeginContext(1234, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1302, 43, false);
#line 51 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\Manager\ProjectDetails.cshtml"
               Write(Html.DisplayFor(modelItem => item.taskName));

#line default
#line hidden
            EndContext();
            BeginContext(1345, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1413, 40, false);
#line 54 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\Manager\ProjectDetails.cshtml"
               Write(Html.DisplayFor(modelItem => item.hours));

#line default
#line hidden
            EndContext();
            BeginContext(1453, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1521, 41, false);
#line 57 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\Manager\ProjectDetails.cshtml"
               Write(Html.DisplayFor(modelItem => item.status));

#line default
#line hidden
            EndContext();
            BeginContext(1562, 45, true);
            WriteLiteral("%\r\n                </td>\r\n            </tr>\r\n");
            EndContext();
#line 60 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\Manager\ProjectDetails.cshtml"
            }

#line default
#line hidden
            BeginContext(1622, 662, true);
            WriteLiteral(@"        </tbody>
    </table>
</div>

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
                <th>
                    Task Due Date
                </th>
                <th>
                    Task Status
                </th>
            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 88 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\Manager\ProjectDetails.cshtml"
             foreach (var item in Model.tasks)
            {

#line default
#line hidden
            BeginContext(2347, 60, true);
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(2408, 41, false);
#line 92 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\Manager\ProjectDetails.cshtml"
               Write(Html.DisplayFor(modelItem => item.taskID));

#line default
#line hidden
            EndContext();
            BeginContext(2449, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(2517, 43, false);
#line 95 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\Manager\ProjectDetails.cshtml"
               Write(Html.DisplayFor(modelItem => item.taskName));

#line default
#line hidden
            EndContext();
            BeginContext(2560, 68, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    $");
            EndContext();
            BeginContext(2629, 39, false);
#line 98 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\Manager\ProjectDetails.cshtml"
                Write(Html.DisplayFor(modelItem => item.cost));

#line default
#line hidden
            EndContext();
            BeginContext(2668, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(2736, 46, false);
#line 101 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\Manager\ProjectDetails.cshtml"
               Write(Html.DisplayFor(modelItem => item.taskDueDate));

#line default
#line hidden
            EndContext();
            BeginContext(2782, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(2850, 41, false);
#line 104 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\Manager\ProjectDetails.cshtml"
               Write(Html.DisplayFor(modelItem => item.status));

#line default
#line hidden
            EndContext();
            BeginContext(2891, 45, true);
            WriteLiteral("%\r\n                </td>\r\n            </tr>\r\n");
            EndContext();
#line 107 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\Manager\ProjectDetails.cshtml"
            }

#line default
#line hidden
            BeginContext(2951, 140, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n\r\n<script>\r\n    $(document).ready(function () {\r\n        $(\'table\').DataTable();\r\n    });\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CompanyProject.Models.ProjectInfo> Html { get; private set; }
    }
}
#pragma warning restore 1591
