#pragma checksum "G:\01\04\Gather Requirement Project\Views\ScreenComponent\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "522ff02edfb997bdfbf8b2c11f67b953b30a3ffd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ScreenComponent_Details), @"mvc.1.0.view", @"/Views/ScreenComponent/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/ScreenComponent/Details.cshtml", typeof(AspNetCore.Views_ScreenComponent_Details))]
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
#line 1 "G:\01\04\Gather Requirement Project\Views\_ViewImports.cshtml"
using Gather_Requirement_Project;

#line default
#line hidden
#line 2 "G:\01\04\Gather Requirement Project\Views\_ViewImports.cshtml"
using Gather_Requirement_Project.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"522ff02edfb997bdfbf8b2c11f67b953b30a3ffd", @"/Views/ScreenComponent/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d1b7013e94b18a1b4839ca552d19bbd548f16f86", @"/Views/_ViewImports.cshtml")]
    public class Views_ScreenComponent_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Gather_Requirement_Project.Models.ScreenComponent>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(58, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "G:\01\04\Gather Requirement Project\Views\ScreenComponent\Details.cshtml"
  
    ViewData["Title"] = "Details";

    var ScreenComponentDepend = ViewBag.ScreenComponentDepend;

    //SelectList ValidationList = new SelectList(ViewBag.screencomValidations, "FieldValidationID", "ScreenComponentID");


#line default
#line hidden
            BeginContext(296, 346, true);
            WriteLiteral(@"<div class=""container-fluid mt-5"">
    <div class=""card mb-3  border"">

        <div class=""row card-header"">
            <div class=""col-md-10"">
                <h4>  <i class=""fa fa-address-card-o"" aria-hidden=""true""></i>&nbsp&nbsp Details of Screen Component </h4>
            </div>
            <div class=""col-md-2"">
                ");
            EndContext();
            BeginContext(642, 73, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "522ff02edfb997bdfbf8b2c11f67b953b30a3ffd4687", async() => {
                BeginContext(701, 10, true);
                WriteLiteral("Go to Back");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-screenId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 19 "G:\01\04\Gather Requirement Project\Views\ScreenComponent\Details.cshtml"
                                              WriteLiteral(Model.ScreenID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["screenId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-screenId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["screenId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(715, 242, true);
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"card-body\" style=\"\">\r\n            <div class=\"row\">\r\n                <div class=\"col-md-10\">\r\n\r\n                </div>\r\n\r\n                <div class=\"col-md-2\">\r\n                    ");
            EndContext();
            BeginContext(957, 54, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "522ff02edfb997bdfbf8b2c11f67b953b30a3ffd7320", async() => {
                BeginContext(1003, 4, true);
                WriteLiteral("Edit");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 30 "G:\01\04\Gather Requirement Project\Views\ScreenComponent\Details.cshtml"
                                           WriteLiteral(Model.ID);

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
            BeginContext(1011, 307, true);
            WriteLiteral(@"

                </div>
            </div>
            <hr />

            <dl class=""text-md-left   table-bordered table-hover row"">
                <dt class=""col-sm-3"">
                    <spn>Screen Name</spn>
                </dt>
                <dd class=""col-sm-9"">
                    ");
            EndContext();
            BeginContext(1319, 49, false);
#line 41 "G:\01\04\Gather Requirement Project\Views\ScreenComponent\Details.cshtml"
               Write(Html.DisplayFor(model => model.Screen.ScreenName));

#line default
#line hidden
            EndContext();
            BeginContext(1368, 199, true);
            WriteLiteral("\r\n                </dd>\r\n\r\n                <dt class=\"col-sm-3\">\r\n                    <span>Name In English </span>\r\n                </dt>\r\n                <dd class=\"col-sm-9\">\r\n                    ");
            EndContext();
            BeginContext(1568, 48, false);
#line 48 "G:\01\04\Gather Requirement Project\Views\ScreenComponent\Details.cshtml"
               Write(Html.DisplayFor(model => model.FieldNameEnglish));

#line default
#line hidden
            EndContext();
            BeginContext(1616, 198, true);
            WriteLiteral("\r\n                </dd>\r\n\r\n                <dt class=\"col-sm-3\">\r\n                    <span> Name In Arabic</span>\r\n                </dt>\r\n                <dd class=\"col-sm-9\">\r\n                    ");
            EndContext();
            BeginContext(1815, 46, false);
#line 55 "G:\01\04\Gather Requirement Project\Views\ScreenComponent\Details.cshtml"
               Write(Html.DisplayFor(model => model.FielNameArabic));

#line default
#line hidden
            EndContext();
            BeginContext(1861, 92, true);
            WriteLiteral("\r\n                </dd>\r\n\r\n\r\n\r\n\r\n                <dt class=\"col-sm-2\">\r\n                    ");
            EndContext();
            BeginContext(1954, 50, false);
#line 62 "G:\01\04\Gather Requirement Project\Views\ScreenComponent\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.InputType.Name));

#line default
#line hidden
            EndContext();
            BeginContext(2004, 85, true);
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
            EndContext();
            BeginContext(2090, 46, false);
#line 65 "G:\01\04\Gather Requirement Project\Views\ScreenComponent\Details.cshtml"
               Write(Html.DisplayFor(model => model.InputType.Name));

#line default
#line hidden
            EndContext();
            BeginContext(2136, 214, true);
            WriteLiteral("\r\n                </dd>\r\n\r\n\r\n\r\n\r\n\r\n                <dt class=\"col-sm-3\">\r\n                    <spn>Service Name For Dropdown</spn>\r\n                </dt>\r\n                <dd class=\"col-sm-9\">\r\n                    ");
            EndContext();
            BeginContext(2351, 54, false);
#line 76 "G:\01\04\Gather Requirement Project\Views\ScreenComponent\Details.cshtml"
               Write(Html.DisplayFor(model => model.ServiceNameForDropdown));

#line default
#line hidden
            EndContext();
            BeginContext(2405, 200, true);
            WriteLiteral("\r\n                </dd>\r\n\r\n                <dt class=\"col-sm-3\">\r\n                    <spn>Component Depend on</spn>\r\n                </dt>\r\n                <dd class=\"col-sm-9\">\r\n                    ");
            EndContext();
            BeginContext(2606, 21, false);
#line 83 "G:\01\04\Gather Requirement Project\Views\ScreenComponent\Details.cshtml"
               Write(ScreenComponentDepend);

#line default
#line hidden
            EndContext();
            BeginContext(2627, 191, true);
            WriteLiteral("\r\n                </dd>\r\n\r\n                <dt class=\"col-sm-3\">\r\n                    <spn>Field Type</spn>\r\n                </dt>\r\n                <dd class=\"col-sm-9\">\r\n                    ");
            EndContext();
            BeginContext(2819, 46, false);
#line 90 "G:\01\04\Gather Requirement Project\Views\ScreenComponent\Details.cshtml"
               Write(Html.DisplayFor(model => model.FieldType.Name));

#line default
#line hidden
            EndContext();
            BeginContext(2865, 223, true);
            WriteLiteral("\r\n                </dd>\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n                <dt class=\"col-sm-3\">\r\n                    <span> Required </span>\r\n                </dt>\r\n                <dd class=\"col-sm-9\" type=\"checkbox\">\r\n                    ");
            EndContext();
            BeginContext(3089, 50, false);
#line 104 "G:\01\04\Gather Requirement Project\Views\ScreenComponent\Details.cshtml"
               Write(Html.DisplayFor(model => model.ValidationRequired));

#line default
#line hidden
            EndContext();
            BeginContext(3139, 216, true);
            WriteLiteral("\r\n                </dd>\r\n\r\n                <dt class=\"col-sm-3\">\r\n                    <span> Message For Required Validation </span>\r\n                </dt>\r\n                <dd class=\"col-sm-9\">\r\n                    ");
            EndContext();
            BeginContext(3356, 57, false);
#line 111 "G:\01\04\Gather Requirement Project\Views\ScreenComponent\Details.cshtml"
               Write(Html.DisplayFor(model => model.ValidationRequiredMessage));

#line default
#line hidden
            EndContext();
            BeginContext(3413, 204, true);
            WriteLiteral("\r\n                </dd>\r\n\r\n                <dt class=\"col-sm-3\">\r\n                    <span> Min </span>\r\n                </dt>\r\n                <dd class=\"col-sm-9\" type=\"checkbox\">\r\n                    ");
            EndContext();
            BeginContext(3618, 45, false);
#line 118 "G:\01\04\Gather Requirement Project\Views\ScreenComponent\Details.cshtml"
               Write(Html.DisplayFor(model => model.ValidationMin));

#line default
#line hidden
            EndContext();
            BeginContext(3663, 211, true);
            WriteLiteral("\r\n                </dd>\r\n\r\n                <dt class=\"col-sm-3\">\r\n                    <span> Message For Min Validation </span>\r\n                </dt>\r\n                <dd class=\"col-sm-9\">\r\n                    ");
            EndContext();
            BeginContext(3875, 52, false);
#line 125 "G:\01\04\Gather Requirement Project\Views\ScreenComponent\Details.cshtml"
               Write(Html.DisplayFor(model => model.ValidationMinMessage));

#line default
#line hidden
            EndContext();
            BeginContext(3927, 204, true);
            WriteLiteral("\r\n                </dd>\r\n\r\n                <dt class=\"col-sm-3\">\r\n                    <span> Max </span>\r\n                </dt>\r\n                <dd class=\"col-sm-9\" type=\"checkbox\">\r\n                    ");
            EndContext();
            BeginContext(4132, 45, false);
#line 132 "G:\01\04\Gather Requirement Project\Views\ScreenComponent\Details.cshtml"
               Write(Html.DisplayFor(model => model.ValidationMax));

#line default
#line hidden
            EndContext();
            BeginContext(4177, 211, true);
            WriteLiteral("\r\n                </dd>\r\n\r\n                <dt class=\"col-sm-3\">\r\n                    <span> Message For Max Validation </span>\r\n                </dt>\r\n                <dd class=\"col-sm-9\">\r\n                    ");
            EndContext();
            BeginContext(4389, 52, false);
#line 139 "G:\01\04\Gather Requirement Project\Views\ScreenComponent\Details.cshtml"
               Write(Html.DisplayFor(model => model.ValidationMaxMessage));

#line default
#line hidden
            EndContext();
            BeginContext(4441, 82, true);
            WriteLiteral("\r\n                </dd>\r\n\r\n            </dl>\r\n        </div>\r\n\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Gather_Requirement_Project.Models.ScreenComponent> Html { get; private set; }
    }
}
#pragma warning restore 1591
