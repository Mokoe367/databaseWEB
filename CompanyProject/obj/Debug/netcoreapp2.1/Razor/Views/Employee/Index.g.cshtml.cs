#pragma checksum "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8b2cb01f53f70a74fd41359017fa3cad303181d5"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8b2cb01f53f70a74fd41359017fa3cad303181d5", @"/Views/Employee/Index.cshtml")]
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
            BeginContext(95, 39, true);
            WriteLiteral("\r\n<h2>Company Members</h2>\r\n\r\n<p>\r\n    ");
            EndContext();
            BeginContext(134, 37, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "51e32a3b977b4da59a370e8a7c50d1f0", async() => {
                BeginContext(157, 10, true);
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
            BeginContext(171, 92, true);
            WriteLiteral("\r\n</p>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(264, 38, false);
#line 16 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.ID));

#line default
#line hidden
            EndContext();
            BeginContext(302, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(358, 41, false);
#line 19 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Fname));

#line default
#line hidden
            EndContext();
            BeginContext(399, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(455, 41, false);
#line 22 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Lname));

#line default
#line hidden
            EndContext();
            BeginContext(496, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(552, 41, false);
#line 25 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Mname));

#line default
#line hidden
            EndContext();
            BeginContext(593, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(649, 43, false);
#line 28 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Address));

#line default
#line hidden
            EndContext();
            BeginContext(692, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(748, 39, false);
#line 31 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Sex));

#line default
#line hidden
            EndContext();
            BeginContext(787, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(843, 45, false);
#line 34 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.BirthDate));

#line default
#line hidden
            EndContext();
            BeginContext(888, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(944, 48, false);
#line 37 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Deleted_flag));

#line default
#line hidden
            EndContext();
            BeginContext(992, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1048, 39, false);
#line 40 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Ssn));

#line default
#line hidden
            EndContext();
            BeginContext(1087, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1143, 42, false);
#line 43 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Salary));

#line default
#line hidden
            EndContext();
            BeginContext(1185, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1241, 42, false);
#line 46 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.RoleID));

#line default
#line hidden
            EndContext();
            BeginContext(1283, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1339, 41, false);
#line 49 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.DepID));

#line default
#line hidden
            EndContext();
            BeginContext(1380, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1436, 43, false);
#line 52 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.SuperID));

#line default
#line hidden
            EndContext();
            BeginContext(1479, 86, true);
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
            EndContext();
#line 58 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
            BeginContext(1597, 48, true);
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(1646, 37, false);
#line 61 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.ID));

#line default
#line hidden
            EndContext();
            BeginContext(1683, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(1739, 40, false);
#line 64 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Fname));

#line default
#line hidden
            EndContext();
            BeginContext(1779, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(1835, 40, false);
#line 67 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Lname));

#line default
#line hidden
            EndContext();
            BeginContext(1875, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(1931, 40, false);
#line 70 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Mname));

#line default
#line hidden
            EndContext();
            BeginContext(1971, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2027, 42, false);
#line 73 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Address));

#line default
#line hidden
            EndContext();
            BeginContext(2069, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2125, 38, false);
#line 76 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Sex));

#line default
#line hidden
            EndContext();
            BeginContext(2163, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2219, 44, false);
#line 79 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.BirthDate));

#line default
#line hidden
            EndContext();
            BeginContext(2263, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2319, 47, false);
#line 82 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Deleted_flag));

#line default
#line hidden
            EndContext();
            BeginContext(2366, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2422, 38, false);
#line 85 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Ssn));

#line default
#line hidden
            EndContext();
            BeginContext(2460, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2516, 41, false);
#line 88 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Salary));

#line default
#line hidden
            EndContext();
            BeginContext(2557, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2613, 41, false);
#line 91 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.RoleID));

#line default
#line hidden
            EndContext();
            BeginContext(2654, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2710, 40, false);
#line 94 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.DepID));

#line default
#line hidden
            EndContext();
            BeginContext(2750, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2806, 42, false);
#line 97 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.SuperID));

#line default
#line hidden
            EndContext();
            BeginContext(2848, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2904, 65, false);
#line 100 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
            EndContext();
            BeginContext(2969, 20, true);
            WriteLiteral(" |\r\n                ");
            EndContext();
            BeginContext(2990, 71, false);
#line 101 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
            EndContext();
            BeginContext(3061, 20, true);
            WriteLiteral(" |\r\n                ");
            EndContext();
            BeginContext(3082, 69, false);
#line 102 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
           Write(Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
            EndContext();
            BeginContext(3151, 36, true);
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
            EndContext();
#line 105 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Employee\Index.cshtml"
}

#line default
#line hidden
            BeginContext(3190, 24, true);
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
