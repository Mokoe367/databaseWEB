#pragma checksum "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\ProjectHours\ProjectHoursList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8727f51d360b1ee94ad4b06159b11d69b806d688"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ProjectHours_ProjectHoursList), @"mvc.1.0.view", @"/Views/ProjectHours/ProjectHoursList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/ProjectHours/ProjectHoursList.cshtml", typeof(AspNetCore.Views_ProjectHours_ProjectHoursList))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8727f51d360b1ee94ad4b06159b11d69b806d688", @"/Views/ProjectHours/ProjectHoursList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc45b8a03ea425198c728c84e613c6da74b97dff", @"/Views/_ViewImports.cshtml")]
    public class Views_ProjectHours_ProjectHoursList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CompanyProject.Models.ProjectHoursReport>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(62, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\ProjectHours\ProjectHoursList.cshtml"
  
    ViewData["Title"] = "ProjectHoursReportList";
    Layout = "~/Views/Shared/_ManagerLayout.cshtml";

#line default
#line hidden
            BeginContext(176, 101, true);
            WriteLiteral("\r\n<style>\r\n    #table-div {\r\n        margin: 25px;\r\n    }\r\n</style>\r\n\r\n<div style=\"margin: 25px\">\r\n\r\n");
            EndContext();
#line 16 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\ProjectHours\ProjectHoursList.cshtml"
     if (ViewData["ProjectInfo"] != null)
    {

#line default
#line hidden
            BeginContext(327, 33, true);
            WriteLiteral("        <h2>Cost Report List For ");
            EndContext();
            BeginContext(361, 23, false);
#line 18 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\ProjectHours\ProjectHoursList.cshtml"
                            Write(ViewData["ProjectInfo"]);

#line default
#line hidden
            EndContext();
            BeginContext(384, 7, true);
            WriteLiteral("</h2>\r\n");
            EndContext();
#line 19 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\ProjectHours\ProjectHoursList.cshtml"

    }

#line default
#line hidden
            BeginContext(400, 632, true);
            WriteLiteral(@"</div>

<div id=""table-div"">
    <table class=""table table-hover col-md-3 table-striped table-bordered caption-top"">
        <caption>Project Hours</caption>
        <thead>
            <tr>
                <th>
                    Project ID
                </th>
                <th>
                    Project Name
                </th>
                <th>
                    Employee ID
                </th>
                <th>
                    Hours
                </th>
                <th>
                    Task ID
                </th>
            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 46 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\ProjectHours\ProjectHoursList.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
            BeginContext(1089, 72, true);
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1162, 41, false);
#line 50 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\ProjectHours\ProjectHoursList.cshtml"
                   Write(Html.DisplayFor(modelItem => item.projID));

#line default
#line hidden
            EndContext();
            BeginContext(1203, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1283, 43, false);
#line 53 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\ProjectHours\ProjectHoursList.cshtml"
                   Write(Html.DisplayFor(modelItem => item.projName));

#line default
#line hidden
            EndContext();
            BeginContext(1326, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1406, 45, false);
#line 56 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\ProjectHours\ProjectHoursList.cshtml"
                   Write(Html.DisplayFor(modelItem => item.employeeID));

#line default
#line hidden
            EndContext();
            BeginContext(1451, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1531, 40, false);
#line 59 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\ProjectHours\ProjectHoursList.cshtml"
                   Write(Html.DisplayFor(modelItem => item.hours));

#line default
#line hidden
            EndContext();
            BeginContext(1571, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1651, 41, false);
#line 62 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\ProjectHours\ProjectHoursList.cshtml"
                   Write(Html.DisplayFor(modelItem => item.taskID));

#line default
#line hidden
            EndContext();
            BeginContext(1692, 52, true);
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 65 "C:\Users\imher\source\repos\databaseWEB\CompanyProject2\CompanyProject\Views\ProjectHours\ProjectHoursList.cshtml"
            }

#line default
#line hidden
            BeginContext(1759, 42, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CompanyProject.Models.ProjectHoursReport>> Html { get; private set; }
    }
}
#pragma warning restore 1591
