#pragma checksum "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "228a8eb9d5461f26e0247eab1bf20b43b631adcb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Employee_Index), @"mvc.1.0.view", @"/Views/Employee/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Employee/Index.cshtml", typeof(AspNetCore.Views_Employee_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"228a8eb9d5461f26e0247eab1bf20b43b631adcb", @"/Views/Employee/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc45b8a03ea425198c728c84e613c6da74b97dff", @"/Views/_ViewImports.cshtml")]
    public class Views_Employee_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CompanyProject.Models.Employee>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(52, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
            BeginContext(95, 29, true);
            WriteLiteral("\r\n<h2>Index</h2>\r\n\r\n<p>\r\n    ");
            EndContext();
            BeginContext(124, 37, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d8c2ecedd4504da992ae1cfcf884b53a", async() => {
                BeginContext(147, 10, true);
                WriteLiteral("Create New");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(161, 92, true);
            WriteLiteral("\r\n</p>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(254, 38, false);
#line 16 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.ID));

#line default
#line hidden
            EndContext();
            BeginContext(292, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(348, 41, false);
#line 19 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Fname));

#line default
#line hidden
            EndContext();
            BeginContext(389, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(445, 41, false);
#line 22 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Lname));

#line default
#line hidden
            EndContext();
            BeginContext(486, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(542, 41, false);
#line 25 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Mname));

#line default
#line hidden
            EndContext();
            BeginContext(583, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(639, 43, false);
#line 28 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Address));

#line default
#line hidden
            EndContext();
            BeginContext(682, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(738, 39, false);
#line 31 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Sex));

#line default
#line hidden
            EndContext();
            BeginContext(777, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(833, 45, false);
#line 34 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.BirthDate));

#line default
#line hidden
            EndContext();
            BeginContext(878, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(934, 48, false);
#line 37 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Deleted_flag));

#line default
#line hidden
            EndContext();
            BeginContext(982, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1038, 39, false);
#line 40 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Ssn));

#line default
#line hidden
            EndContext();
            BeginContext(1077, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1133, 42, false);
#line 43 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Salary));

#line default
#line hidden
            EndContext();
            BeginContext(1175, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1231, 42, false);
#line 46 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.RoleID));

#line default
#line hidden
            EndContext();
            BeginContext(1273, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1329, 41, false);
#line 49 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.DepID));

#line default
#line hidden
            EndContext();
            BeginContext(1370, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1426, 43, false);
#line 52 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.SuperID));

#line default
#line hidden
            EndContext();
            BeginContext(1469, 86, true);
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
            EndContext();
#line 58 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
            BeginContext(1587, 48, true);
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(1636, 37, false);
#line 61 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.ID));

#line default
#line hidden
            EndContext();
            BeginContext(1673, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(1729, 40, false);
#line 64 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Fname));

#line default
#line hidden
            EndContext();
            BeginContext(1769, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(1825, 40, false);
#line 67 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Lname));

#line default
#line hidden
            EndContext();
            BeginContext(1865, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(1921, 40, false);
#line 70 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Mname));

#line default
#line hidden
            EndContext();
            BeginContext(1961, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2017, 42, false);
#line 73 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Address));

#line default
#line hidden
            EndContext();
            BeginContext(2059, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2115, 38, false);
#line 76 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Sex));

#line default
#line hidden
            EndContext();
            BeginContext(2153, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2209, 44, false);
#line 79 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.BirthDate));

#line default
#line hidden
            EndContext();
            BeginContext(2253, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2309, 47, false);
#line 82 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Deleted_flag));

#line default
#line hidden
            EndContext();
            BeginContext(2356, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2412, 38, false);
#line 85 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Ssn));

#line default
#line hidden
            EndContext();
            BeginContext(2450, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2506, 41, false);
#line 88 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Salary));

#line default
#line hidden
            EndContext();
            BeginContext(2547, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2603, 41, false);
#line 91 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.RoleID));

#line default
#line hidden
            EndContext();
            BeginContext(2644, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2700, 40, false);
#line 94 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.DepID));

#line default
#line hidden
            EndContext();
            BeginContext(2740, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2796, 42, false);
#line 97 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.SuperID));

#line default
#line hidden
            EndContext();
            BeginContext(2838, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2894, 65, false);
#line 100 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
            EndContext();
            BeginContext(2959, 20, true);
            WriteLiteral(" |\r\n                ");
            EndContext();
            BeginContext(2980, 71, false);
#line 101 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
            EndContext();
            BeginContext(3051, 20, true);
            WriteLiteral(" |\r\n                ");
            EndContext();
            BeginContext(3072, 69, false);
#line 102 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
            EndContext();
            BeginContext(3141, 36, true);
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
            EndContext();
#line 105 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
}

#line default
#line hidden
            BeginContext(3180, 24, true);
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CompanyProject.Models.Employee>> Html { get; private set; }
    }
}
#pragma warning restore 1591
