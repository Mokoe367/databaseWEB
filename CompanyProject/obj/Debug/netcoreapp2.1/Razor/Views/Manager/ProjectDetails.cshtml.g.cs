#pragma checksum "C:\Users\khoin\Courses\Spring 2023\Database system\dataweb\databaseWEB\CompanyProject\Views\Manager\ProjectDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "df9c1d6f7870d2b6ea5ffbbca11f78cd55ae4e88"
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
#line 1 "C:\Users\khoin\Courses\Spring 2023\Database system\dataweb\databaseWEB\CompanyProject\Views\_ViewImports.cshtml"
using CompanyProject;

#line default
#line hidden
#line 2 "C:\Users\khoin\Courses\Spring 2023\Database system\dataweb\databaseWEB\CompanyProject\Views\_ViewImports.cshtml"
using CompanyProject.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"df9c1d6f7870d2b6ea5ffbbca11f78cd55ae4e88", @"/Views/Manager/ProjectDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc45b8a03ea425198c728c84e613c6da74b97dff", @"/Views/_ViewImports.cshtml")]
    public class Views_Manager_ProjectDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CompanyProject.Models.ProjectInfo>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(42, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\khoin\Courses\Spring 2023\Database system\dataweb\databaseWEB\CompanyProject\Views\Manager\ProjectDetails.cshtml"
  
    ViewData["Title"] = "ProjectDetails";
    Layout = "~/Views/Shared/_ManagerLayout.cshtml";

#line default
#line hidden
            BeginContext(148, 71, true);
            WriteLiteral("\r\n<style>\r\n    #table-div {\r\n        margin: 25px;\r\n    }\r\n</style>\r\n\r\n");
            EndContext();
#line 14 "C:\Users\khoin\Courses\Spring 2023\Database system\dataweb\databaseWEB\CompanyProject\Views\Manager\ProjectDetails.cshtml"
 if (ViewData["ProjectInfo"] != null)
{

#line default
#line hidden
            BeginContext(261, 29, true);
            WriteLiteral("    <h2 style=\"margin: 25px\">");
            EndContext();
            BeginContext(291, 34, false);
#line 16 "C:\Users\khoin\Courses\Spring 2023\Database system\dataweb\databaseWEB\CompanyProject\Views\Manager\ProjectDetails.cshtml"
                        Write(ViewData["ProjectInfo"].ToString());

#line default
#line hidden
            EndContext();
            BeginContext(325, 7, true);
            WriteLiteral("</h2>\r\n");
            EndContext();
#line 17 "C:\Users\khoin\Courses\Spring 2023\Database system\dataweb\databaseWEB\CompanyProject\Views\Manager\ProjectDetails.cshtml"
}

#line default
#line hidden
            BeginContext(335, 495, true);
            WriteLiteral(@"<div id=""table-div"">
    <table class=""table table-hover col-md-3 table-striped table-bordered caption-top"">
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
            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 37 "C:\Users\khoin\Courses\Spring 2023\Database system\dataweb\databaseWEB\CompanyProject\Views\Manager\ProjectDetails.cshtml"
             foreach (var item in Model.details)
            {

#line default
#line hidden
            BeginContext(895, 72, true);
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(968, 39, false);
#line 41 "C:\Users\khoin\Courses\Spring 2023\Database system\dataweb\databaseWEB\CompanyProject\Views\Manager\ProjectDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.name));

#line default
#line hidden
            EndContext();
            BeginContext(1007, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1087, 43, false);
#line 44 "C:\Users\khoin\Courses\Spring 2023\Database system\dataweb\databaseWEB\CompanyProject\Views\Manager\ProjectDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.roleName));

#line default
#line hidden
            EndContext();
            BeginContext(1130, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1210, 43, false);
#line 47 "C:\Users\khoin\Courses\Spring 2023\Database system\dataweb\databaseWEB\CompanyProject\Views\Manager\ProjectDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.taskName));

#line default
#line hidden
            EndContext();
            BeginContext(1253, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1333, 40, false);
#line 50 "C:\Users\khoin\Courses\Spring 2023\Database system\dataweb\databaseWEB\CompanyProject\Views\Manager\ProjectDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.hours));

#line default
#line hidden
            EndContext();
            BeginContext(1373, 52, true);
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 53 "C:\Users\khoin\Courses\Spring 2023\Database system\dataweb\databaseWEB\CompanyProject\Views\Manager\ProjectDetails.cshtml"
            }

#line default
#line hidden
            BeginContext(1440, 584, true);
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
            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 78 "C:\Users\khoin\Courses\Spring 2023\Database system\dataweb\databaseWEB\CompanyProject\Views\Manager\ProjectDetails.cshtml"
             foreach (var item in Model.tasks)
            {

#line default
#line hidden
            BeginContext(2087, 72, true);
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2160, 41, false);
#line 82 "C:\Users\khoin\Courses\Spring 2023\Database system\dataweb\databaseWEB\CompanyProject\Views\Manager\ProjectDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.taskID));

#line default
#line hidden
            EndContext();
            BeginContext(2201, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2281, 43, false);
#line 85 "C:\Users\khoin\Courses\Spring 2023\Database system\dataweb\databaseWEB\CompanyProject\Views\Manager\ProjectDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.taskName));

#line default
#line hidden
            EndContext();
            BeginContext(2324, 80, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        $");
            EndContext();
            BeginContext(2405, 39, false);
#line 88 "C:\Users\khoin\Courses\Spring 2023\Database system\dataweb\databaseWEB\CompanyProject\Views\Manager\ProjectDetails.cshtml"
                    Write(Html.DisplayFor(modelItem => item.cost));

#line default
#line hidden
            EndContext();
            BeginContext(2444, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2524, 46, false);
#line 91 "C:\Users\khoin\Courses\Spring 2023\Database system\dataweb\databaseWEB\CompanyProject\Views\Manager\ProjectDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.taskDueDate));

#line default
#line hidden
            EndContext();
            BeginContext(2570, 52, true);
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 94 "C:\Users\khoin\Courses\Spring 2023\Database system\dataweb\databaseWEB\CompanyProject\Views\Manager\ProjectDetails.cshtml"
            }

#line default
#line hidden
            BeginContext(2637, 140, true);
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