#pragma checksum "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\TaskInformation.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b6233c1b978bd62c949d2e94ab689b0f70b3b71f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Manager_TaskInformation), @"mvc.1.0.view", @"/Views/Manager/TaskInformation.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Manager/TaskInformation.cshtml", typeof(AspNetCore.Views_Manager_TaskInformation))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b6233c1b978bd62c949d2e94ab689b0f70b3b71f", @"/Views/Manager/TaskInformation.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc45b8a03ea425198c728c84e613c6da74b97dff", @"/Views/_ViewImports.cshtml")]
    public class Views_Manager_TaskInformation : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CompanyProject.Models.TaskInformation>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(59, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\TaskInformation.cshtml"
  
    ViewData["Title"] = "TaskInformation";
    Layout = "~/Views/Shared/_ManagerLayout.cshtml";

#line default
#line hidden
            BeginContext(166, 71, true);
            WriteLiteral("\r\n<style>\r\n    #table-div {\r\n        margin: 25px;\r\n    }\r\n</style>\r\n\r\n");
            EndContext();
#line 14 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\TaskInformation.cshtml"
 if (ViewData["TaskInfo"] != null)
{

#line default
#line hidden
            BeginContext(276, 29, true);
            WriteLiteral("    <h2 style=\"margin: 25px\">");
            EndContext();
            BeginContext(306, 31, false);
#line 16 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\TaskInformation.cshtml"
                        Write(ViewData["TaskInfo"].ToString());

#line default
#line hidden
            EndContext();
            BeginContext(337, 7, true);
            WriteLiteral("</h2>\r\n");
            EndContext();
#line 17 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\TaskInformation.cshtml"
}

#line default
#line hidden
            BeginContext(347, 626, true);
            WriteLiteral(@"    <div id=""table-div"">
        <table class=""table"">
            <thead>
                <tr>
                    <th>
                        Name
                    </th>                  
                    <th>
                        Hours
                    </th>
                    <th>
                       Role Name
                    </th>  
                    <th>
                        Employee Status
                    </th>
                    <th>
                        Documentation
                    </th>
                </tr>
            </thead>
            <tbody>
");
            EndContext();
#line 40 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\TaskInformation.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
            BeginContext(1038, 72, true);
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1111, 39, false);
#line 44 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\TaskInformation.cshtml"
                   Write(Html.DisplayFor(modelItem => item.name));

#line default
#line hidden
            EndContext();
            BeginContext(1150, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1230, 40, false);
#line 47 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\TaskInformation.cshtml"
                   Write(Html.DisplayFor(modelItem => item.hours));

#line default
#line hidden
            EndContext();
            BeginContext(1270, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1350, 43, false);
#line 50 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\TaskInformation.cshtml"
                   Write(Html.DisplayFor(modelItem => item.roleName));

#line default
#line hidden
            EndContext();
            BeginContext(1393, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1473, 41, false);
#line 53 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\TaskInformation.cshtml"
                   Write(Html.DisplayFor(modelItem => item.status));

#line default
#line hidden
            EndContext();
            BeginContext(1514, 80, true);
            WriteLiteral("%\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1595, 48, false);
#line 56 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\TaskInformation.cshtml"
                   Write(Html.DisplayFor(modelItem => item.documentation));

#line default
#line hidden
            EndContext();
            BeginContext(1643, 52, true);
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 59 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\TaskInformation.cshtml"
                }

#line default
#line hidden
            BeginContext(1714, 50, true);
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CompanyProject.Models.TaskInformation>> Html { get; private set; }
    }
}
#pragma warning restore 1591
