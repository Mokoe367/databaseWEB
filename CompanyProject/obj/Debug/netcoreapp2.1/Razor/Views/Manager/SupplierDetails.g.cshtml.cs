#pragma checksum "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "661495337cbc60afae310db807b653699415c3ba"
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
#line 1 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\_ViewImports.cshtml"
using CompanyProject;

#line default
#line hidden
#line 2 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\_ViewImports.cshtml"
using CompanyProject.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"661495337cbc60afae310db807b653699415c3ba", @"/Views/Manager/SupplierDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc45b8a03ea425198c728c84e613c6da74b97dff", @"/Views/_ViewImports.cshtml")]
    public class Views_Manager_SupplierDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CompanyProject.Models.SupplierDetailsView>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "RequestAssets", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Manager", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddAsset", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(63, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
  
    ViewData["Title"] = "SupplierDetails";
    Layout = "~/Views/Shared/_ManagerLayout.cshtml";

#line default
#line hidden
            BeginContext(170, 71, true);
            WriteLiteral("\r\n<style>\r\n    #table-div {\r\n        margin: 25px;\r\n    }\r\n</style>\r\n\r\n");
            EndContext();
#line 14 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
 if (ViewData["SupplierInfo"] != null)
{

#line default
#line hidden
            BeginContext(284, 29, true);
            WriteLiteral("    <h2 style=\"margin: 25px\">");
            EndContext();
            BeginContext(314, 35, false);
#line 16 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                        Write(ViewData["SupplierInfo"].ToString());

#line default
#line hidden
            EndContext();
            BeginContext(349, 7, true);
            WriteLiteral("</h2>\r\n");
            EndContext();
#line 17 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
}

#line default
#line hidden
            BeginContext(359, 97, true);
            WriteLiteral("\r\n\r\n<h5 style=\"margin: 25px\">Assets Distributed by this supplier</h5>\r\n<div id=\"table-div\">\r\n    ");
            EndContext();
            BeginContext(456, 150, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5243192354ec4970a864790f40bb695e", async() => {
                BeginContext(572, 30, true);
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
#line 22 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
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
            BeginContext(606, 515, true);
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
                    Cost
                </th>
                <th>
                    Field
                </th>
                <th>

                </th>
            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 46 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
             foreach (var item in Model.FirstOrDefault().supplierDetails)
            {

#line default
#line hidden
            BeginContext(1211, 72, true);
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1284, 42, false);
#line 50 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.assetID));

#line default
#line hidden
            EndContext();
            BeginContext(1326, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1406, 39, false);
#line 53 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.type));

#line default
#line hidden
            EndContext();
            BeginContext(1445, 80, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        $");
            EndContext();
            BeginContext(1526, 39, false);
#line 56 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                    Write(Html.DisplayFor(modelItem => item.cost));

#line default
#line hidden
            EndContext();
            BeginContext(1565, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1645, 40, false);
#line 59 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.field));

#line default
#line hidden
            EndContext();
            BeginContext(1685, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1765, 105, false);
#line 62 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                   Write(Html.ActionLink("Edit", "EditDistribution", new { supId = ViewData["supplier"], assetId = item.assetID }));

#line default
#line hidden
            EndContext();
            BeginContext(1870, 28, true);
            WriteLiteral(" |\r\n                        ");
            EndContext();
            BeginContext(1899, 109, false);
#line 63 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                   Write(Html.ActionLink("Delete", "DeleteDistribution", new { supId = ViewData["supplier"], assetId = item.assetID }));

#line default
#line hidden
            EndContext();
            BeginContext(2008, 52, true);
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 66 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
            }

#line default
#line hidden
            BeginContext(2075, 68, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n\r\n<div id=\"table-div\">\r\n    ");
            EndContext();
            BeginContext(2143, 124, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e7a2d221e04b400bb914361443bf30d0", async() => {
                BeginContext(2254, 9, true);
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
#line 72 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
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
            BeginContext(2267, 559, true);
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
                <th>
                    Supplier ID
                </th>                
                <th></th>
            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 93 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
             foreach (var item in Model.FirstOrDefault().assets)
            {

#line default
#line hidden
            BeginContext(2907, 72, true);
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2980, 42, false);
#line 97 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.assetID));

#line default
#line hidden
            EndContext();
            BeginContext(3022, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(3102, 39, false);
#line 100 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.type));

#line default
#line hidden
            EndContext();
            BeginContext(3141, 80, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        $");
            EndContext();
            BeginContext(3222, 39, false);
#line 103 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                    Write(Html.DisplayFor(modelItem => item.cost));

#line default
#line hidden
            EndContext();
            BeginContext(3261, 55, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n");
            EndContext();
#line 106 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                         if (item.supID != 0)
                        {
                            

#line default
#line hidden
            BeginContext(3419, 40, false);
#line 108 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                       Write(Html.DisplayFor(modelItem => item.supID));

#line default
#line hidden
            EndContext();
#line 108 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                                                                     
                        }

#line default
#line hidden
            BeginContext(3488, 96, true);
            WriteLiteral("                    </td>                   \r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(3585, 63, false);
#line 112 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                   Write(Html.ActionLink("Edit", "EditAsset", new { id = item.assetID }));

#line default
#line hidden
            EndContext();
            BeginContext(3648, 28, true);
            WriteLiteral(" |\r\n                        ");
            EndContext();
            BeginContext(3677, 67, false);
#line 113 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
                   Write(Html.ActionLink("Delete", "DeleteAsset", new { id = item.assetID }));

#line default
#line hidden
            EndContext();
            BeginContext(3744, 52, true);
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 116 "C:\Users\maste\OneDrive\Documents\GitHub\databaseWEB\CompanyProject\Views\Manager\SupplierDetails.cshtml"
            }

#line default
#line hidden
            BeginContext(3811, 38, true);
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
