#pragma checksum "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\TaskDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ccbae12c0c0b7765f8140dd400e5cf795bd027cc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Manager_TaskDetails), @"mvc.1.0.view", @"/Views/Manager/TaskDetails.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Manager/TaskDetails.cshtml", typeof(AspNetCore.Views_Manager_TaskDetails))]
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
#line 1 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\_ViewImports.cshtml"
using CompanyProject;

#line default
#line hidden
#line 2 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\_ViewImports.cshtml"
using CompanyProject.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ccbae12c0c0b7765f8140dd400e5cf795bd027cc", @"/Views/Manager/TaskDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc45b8a03ea425198c728c84e613c6da74b97dff", @"/Views/_ViewImports.cshtml")]
    public class Views_Manager_TaskDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CompanyProject.Models.TaskDetails>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Enlist", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Manager", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(55, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\TaskDetails.cshtml"
  
    ViewData["Title"] = "TaskDetails";
    Layout = "~/Views/Shared/_ManagerLayout.cshtml";

#line default
#line hidden
            BeginContext(158, 71, true);
            WriteLiteral("\r\n<style>\r\n    #table-div {\r\n        margin: 25px;\r\n    }\r\n</style>\r\n\r\n");
            EndContext();
#line 14 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\TaskDetails.cshtml"
 if (ViewData["TaskInfo"] != null)
{

#line default
#line hidden
            BeginContext(268, 29, true);
            WriteLiteral("    <h2 style=\"margin: 25px\">");
            EndContext();
            BeginContext(298, 31, false);
#line 16 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\TaskDetails.cshtml"
                        Write(ViewData["TaskInfo"].ToString());

#line default
#line hidden
            EndContext();
            BeginContext(329, 7, true);
            WriteLiteral("</h2>\r\n");
            EndContext();
#line 17 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\TaskDetails.cshtml"
}

#line default
#line hidden
            BeginContext(339, 31, true);
            WriteLiteral("\r\n<p style=\"margin:25px\">\r\n    ");
            EndContext();
            BeginContext(370, 149, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "dafcd7a7e26b481a9c0cc247aaad0b02", async() => {
                BeginContext(479, 36, true);
                WriteLiteral("\r\n        Enlist To A New Task\r\n    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 20 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\TaskDetails.cshtml"
                                                                              WriteLiteral(ViewData["employee"]);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(519, 710, true);
            WriteLiteral(@"
</p>

<div id=""table-div"">
    <table class=""table"">
        <thead>
            <tr>
                <th>
                    Employee ID
                </th>
                <th>
                    Project ID
                </th>
                <th>
                    Task ID
                </th>
                <th>
                    Task Name
                </th>
                <th>
                    Hours
                </th>
                <th>
                    Task Budget
                </th>
                <th>
                    Task Due Date
                </th>
                <th></th>
            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 54 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\TaskDetails.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
            BeginContext(1286, 72, true);
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1359, 40, false);
#line 58 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\TaskDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.empID));

#line default
#line hidden
            EndContext();
            BeginContext(1399, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1479, 41, false);
#line 61 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\TaskDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.projID));

#line default
#line hidden
            EndContext();
            BeginContext(1520, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1600, 41, false);
#line 64 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\TaskDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.taskID));

#line default
#line hidden
            EndContext();
            BeginContext(1641, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1721, 43, false);
#line 67 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\TaskDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.taskName));

#line default
#line hidden
            EndContext();
            BeginContext(1764, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1844, 40, false);
#line 70 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\TaskDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.hours));

#line default
#line hidden
            EndContext();
            BeginContext(1884, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1964, 41, false);
#line 73 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\TaskDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.budget));

#line default
#line hidden
            EndContext();
            BeginContext(2005, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2085, 42, false);
#line 76 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\TaskDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.dueDate));

#line default
#line hidden
            EndContext();
            BeginContext(2127, 120, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>                                         \r\n                        ");
            EndContext();
            BeginContext(2248, 85, false);
#line 79 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\TaskDetails.cshtml"
                   Write(Html.ActionLink("Unlist", "Unlist", new { empid = item.empID, taskid = item.taskID }));

#line default
#line hidden
            EndContext();
            BeginContext(2333, 52, true);
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 82 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\TaskDetails.cshtml"
            }

#line default
#line hidden
            BeginContext(2400, 138, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n<script>\r\n    $(document).ready(function () {\r\n        $(\'table\').DataTable();\r\n    });\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CompanyProject.Models.TaskDetails>> Html { get; private set; }
    }
}
#pragma warning restore 1591
