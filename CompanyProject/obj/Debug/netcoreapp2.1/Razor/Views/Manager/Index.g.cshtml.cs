#pragma checksum "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e2ba8b1b9ea069d55df7fbeee2500a17e569d6bc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Manager_Index), @"mvc.1.0.view", @"/Views/Manager/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Manager/Index.cshtml", typeof(AspNetCore.Views_Manager_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e2ba8b1b9ea069d55df7fbeee2500a17e569d6bc", @"/Views/Manager/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc45b8a03ea425198c728c84e613c6da74b97dff", @"/Views/_ViewImports.cshtml")]
    public class Views_Manager_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\Index.cshtml"
  
    ViewData["Title"] = "Index";
    ViewBag.Title = "Manager View";
    Layout = "~/Views/Shared/_ManagerLayout.cshtml";

#line default
#line hidden
            BeginContext(134, 128, true);
            WriteLiteral("\r\n<style>\r\n    #table-div {\r\n        margin: 25px;\r\n    }\r\n</style>\r\n\r\n\r\n<div style=\"margin: 25px\">\r\n    <h2>Manager View</h2>\r\n");
            EndContext();
#line 17 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\Index.cshtml"
     if (ViewData["userInfo"] != null)
    {
        

#line default
#line hidden
            BeginContext(318, 20, false);
#line 19 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\Index.cshtml"
   Write(ViewData["userInfo"]);

#line default
#line hidden
            EndContext();
#line 19 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\Index.cshtml"
                             ;

    }

#line default
#line hidden
            BeginContext(350, 14, true);
            WriteLiteral("</div>\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591