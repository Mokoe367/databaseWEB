#pragma checksum "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3b172bda07e9b7506a5d503c9cda633317983bb7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Manager_SupplierDetails), @"mvc.1.0.view", @"/Views/Manager/SupplierDetails.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Manager/SupplierDetails.cshtml", typeof(AspNetCore.Views_Manager_SupplierDetails))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3b172bda07e9b7506a5d503c9cda633317983bb7", @"/Views/Manager/SupplierDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc45b8a03ea425198c728c84e613c6da74b97dff", @"/Views/_ViewImports.cshtml")]
    public class Views_Manager_SupplierDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CompanyProject.Models.SupplierDetailsView>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "RequestAssets", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Manager", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddAsset", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
            BeginContext(63, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
  
    ViewData["Title"] = "SupplierDetails";
    Layout = "~/Views/Shared/_ManagerLayout.cshtml";

#line default
#line hidden
            BeginContext(170, 71, true);
            WriteLiteral("\r\n<style>\r\n    #table-div {\r\n        margin: 25px;\r\n    }\r\n</style>\r\n\r\n");
            EndContext();
#line 14 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
 if (ViewData["SupplierInfo"] != null)
{

#line default
#line hidden
            BeginContext(284, 29, true);
            WriteLiteral("    <h2 style=\"margin: 25px\">");
            EndContext();
            BeginContext(314, 35, false);
#line 16 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                        Write(ViewData["SupplierInfo"].ToString());

#line default
#line hidden
            EndContext();
            BeginContext(349, 7, true);
            WriteLiteral("</h2>\r\n");
            EndContext();
#line 17 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
}

#line default
#line hidden
            BeginContext(359, 28, true);
            WriteLiteral("<div style=\"margin: 25px\">\r\n");
            EndContext();
#line 19 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
     if (TempData["success"] != null)
    {
        

#line default
#line hidden
            BeginContext(442, 19, false);
#line 21 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
   Write(TempData["success"]);

#line default
#line hidden
            EndContext();
#line 21 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                            ;

    }

#line default
#line hidden
            BeginContext(473, 105, true);
            WriteLiteral("</div>\r\n\r\n\r\n<h5 style=\"margin: 25px\">Assets Distributed by this supplier</h5>\r\n<div id=\"table-div\">\r\n    ");
            EndContext();
            BeginContext(578, 150, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3b172bda07e9b7506a5d503c9cda633317983bb77070", async() => {
                BeginContext(694, 30, true);
                WriteLiteral("\r\n        Request Assets\r\n    ");
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
#line 29 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                                                                                     WriteLiteral(ViewData["supplier"]);

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
            BeginContext(728, 594, true);
            WriteLiteral(@"
    <table class=""table table-hover col-md-3 table-striped table-bordered caption-top"">
        <thead>
            <tr>
                <th>
                    AssetID
                </th>
                <th>
                    Type
                </th>
                <th>
                    Cost per 1
                </th>
                <th>
                    Field
                </th>
                <th>
                    Amount
                </th>
                <th>

                </th>
            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 56 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
             foreach (var item in Model.FirstOrDefault().supplierDetails)
            {

#line default
#line hidden
            BeginContext(1412, 60, true);
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1473, 42, false);
#line 60 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
               Write(Html.DisplayFor(modelItem => item.assetID));

#line default
#line hidden
            EndContext();
            BeginContext(1515, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1583, 39, false);
#line 63 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
               Write(Html.DisplayFor(modelItem => item.type));

#line default
#line hidden
            EndContext();
            BeginContext(1622, 68, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    $");
            EndContext();
            BeginContext(1691, 39, false);
#line 66 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                Write(Html.DisplayFor(modelItem => item.cost));

#line default
#line hidden
            EndContext();
            BeginContext(1730, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1798, 40, false);
#line 69 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
               Write(Html.DisplayFor(modelItem => item.field));

#line default
#line hidden
            EndContext();
            BeginContext(1838, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1906, 41, false);
#line 72 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
               Write(Html.DisplayFor(modelItem => item.amount));

#line default
#line hidden
            EndContext();
            BeginContext(1947, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(2015, 105, false);
#line 75 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
               Write(Html.ActionLink("Edit", "EditDistribution", new { supId = ViewData["supplier"], assetId = item.assetID }));

#line default
#line hidden
            EndContext();
            BeginContext(2120, 24, true);
            WriteLiteral(" |\r\n                    ");
            EndContext();
            BeginContext(2145, 109, false);
#line 76 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
               Write(Html.ActionLink("Delete", "DeleteDistribution", new { supId = ViewData["supplier"], assetId = item.assetID }));

#line default
#line hidden
            EndContext();
            BeginContext(2254, 44, true);
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
            EndContext();
#line 79 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
            }

#line default
#line hidden
            BeginContext(2313, 68, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n\r\n<div id=\"table-div\">\r\n    ");
            EndContext();
            BeginContext(2381, 124, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3b172bda07e9b7506a5d503c9cda633317983bb714486", async() => {
                BeginContext(2492, 9, true);
                WriteLiteral("Add Asset");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 85 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                                                                                WriteLiteral(ViewData["supplier"]);

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
            BeginContext(2505, 494, true);
            WriteLiteral(@"
    <table class=""table table-hover col-md-3 table-striped table-bordered caption-top"">
        <caption>Assets</caption>
        <thead>
            <tr>
                <th>
                    Asset ID
                </th>
                <th>
                    Asset Type
                </th>
                <th>
                    Cost
                </th>                             
                <th></th>
            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 103 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
             foreach (var item in Model.FirstOrDefault().assets)
            {

#line default
#line hidden
            BeginContext(3080, 72, true);
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(3153, 42, false);
#line 107 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.assetID));

#line default
#line hidden
            EndContext();
            BeginContext(3195, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(3275, 39, false);
#line 110 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.type));

#line default
#line hidden
            EndContext();
            BeginContext(3314, 80, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        $");
            EndContext();
            BeginContext(3395, 39, false);
#line 113 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                    Write(Html.DisplayFor(modelItem => item.cost));

#line default
#line hidden
            EndContext();
            BeginContext(3434, 116, true);
            WriteLiteral("\r\n                    </td>                                     \r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(3551, 63, false);
#line 116 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                   Write(Html.ActionLink("Edit", "EditAsset", new { id = item.assetID }));

#line default
#line hidden
            EndContext();
            BeginContext(3614, 28, true);
            WriteLiteral(" |\r\n                        ");
            EndContext();
            BeginContext(3643, 67, false);
#line 117 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                   Write(Html.ActionLink("Delete", "DeleteAsset", new { id = item.assetID }));

#line default
#line hidden
            EndContext();
            BeginContext(3710, 52, true);
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 120 "C:\Users\imher\OneDrive\Desktop\CompanyProject4\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
            }

#line default
#line hidden
            BeginContext(3777, 38, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CompanyProject.Models.SupplierDetailsView>> Html { get; private set; }
    }
}
#pragma warning restore 1591
