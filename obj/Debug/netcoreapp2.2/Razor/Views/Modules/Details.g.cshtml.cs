#pragma checksum "C:\Users\software team\Desktop\gitRepo\GatherRequirmentProject\Views\Modules\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f87057aeff4f3d953de2fda450edb3655b6b566d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Modules_Details), @"mvc.1.0.view", @"/Views/Modules/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Modules/Details.cshtml", typeof(AspNetCore.Views_Modules_Details))]
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
#line 1 "C:\Users\software team\Desktop\gitRepo\GatherRequirmentProject\Views\_ViewImports.cshtml"
using Gather_Requirement_Project;

#line default
#line hidden
#line 2 "C:\Users\software team\Desktop\gitRepo\GatherRequirmentProject\Views\_ViewImports.cshtml"
using Gather_Requirement_Project.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f87057aeff4f3d953de2fda450edb3655b6b566d", @"/Views/Modules/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d1b7013e94b18a1b4839ca552d19bbd548f16f86", @"/Views/_ViewImports.cshtml")]
    public class Views_Modules_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Gather_Requirement_Project.Models.Module>
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
            BeginContext(49, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\software team\Desktop\gitRepo\GatherRequirmentProject\Views\Modules\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
            BeginContext(94, 316, true);
            WriteLiteral(@"
<div class=""card "">
    <div class=""card-header"">
        <h4> <i class=""fas fa-table""></i>&nbsp&nbsp Details Module </h4>
    </div>
    <div class=""card-body"" style="""">
        <div class=""row"">
            <div class=""col-md-10"">

            </div>
            <div class=""col-md-2"">
                ");
            EndContext();
            BeginContext(410, 83, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f87057aeff4f3d953de2fda450edb3655b6b566d4500", async() => {
                BeginContext(479, 10, true);
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
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-programId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 17 "C:\Users\software team\Desktop\gitRepo\GatherRequirmentProject\Views\Modules\Details.cshtml"
                                               WriteLiteral(Model.CustomerProgramID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["programId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-programId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["programId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(493, 265, true);
            WriteLiteral(@"
            </div>
        </div>
        <hr />



        <dl class=""text-md-left  row"">
            <dt class=""col-sm-3"">
                <span>Module English Name In Single</span>
            </dt>
            <dd class=""col-sm-9"">
                ");
            EndContext();
            BeginContext(759, 51, false);
#line 29 "C:\Users\software team\Desktop\gitRepo\GatherRequirmentProject\Views\Modules\Details.cshtml"
           Write(Html.DisplayFor(model => model.EnglishNameInSingle));

#line default
#line hidden
            EndContext();
            BeginContext(810, 188, true);
            WriteLiteral("\r\n            </dd>\r\n\r\n            <dt class=\"col-sm-3\">\r\n                <span>Module English Name In Plural</span>\r\n            </dt>\r\n            <dd class=\"col-sm-9\">\r\n                ");
            EndContext();
            BeginContext(999, 51, false);
#line 36 "C:\Users\software team\Desktop\gitRepo\GatherRequirmentProject\Views\Modules\Details.cshtml"
           Write(Html.DisplayFor(model => model.EnglishNameInPlural));

#line default
#line hidden
            EndContext();
            BeginContext(1050, 187, true);
            WriteLiteral("\r\n            </dd>\r\n\r\n            <dt class=\"col-sm-3\">\r\n                <span>Module Arabic Name In Single</span>\r\n            </dt>\r\n            <dd class=\"col-sm-9\">\r\n                ");
            EndContext();
            BeginContext(1238, 50, false);
#line 43 "C:\Users\software team\Desktop\gitRepo\GatherRequirmentProject\Views\Modules\Details.cshtml"
           Write(Html.DisplayFor(model => model.ArabicNameInSingle));

#line default
#line hidden
            EndContext();
            BeginContext(1288, 189, true);
            WriteLiteral("\r\n            </dd>\r\n\r\n\r\n            <dt class=\"col-sm-3\">\r\n                <span>Module Arabic Name In Plural</span>\r\n            </dt>\r\n            <dd class=\"col-sm-9\">\r\n                ");
            EndContext();
            BeginContext(1478, 50, false);
#line 51 "C:\Users\software team\Desktop\gitRepo\GatherRequirmentProject\Views\Modules\Details.cshtml"
           Write(Html.DisplayFor(model => model.ArabicNameInPlural));

#line default
#line hidden
            EndContext();
            BeginContext(1528, 183, true);
            WriteLiteral("\r\n            </dd>\r\n\r\n\r\n            <dt class=\"col-sm-3\">\r\n                <span>Module URL For Service</span>\r\n            </dt>\r\n            <dd class=\"col-sm-9\">\r\n                ");
            EndContext();
            BeginContext(1712, 45, false);
#line 59 "C:\Users\software team\Desktop\gitRepo\GatherRequirmentProject\Views\Modules\Details.cshtml"
           Write(Html.DisplayFor(model => model.URLForService));

#line default
#line hidden
            EndContext();
            BeginContext(1757, 88, true);
            WriteLiteral("\r\n            </dd>\r\n\r\n        </dl>\r\n\r\n\r\n        <dl class=\"col-sm-5\">\r\n            <b>");
            EndContext();
            BeginContext(1845, 54, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f87057aeff4f3d953de2fda450edb3655b6b566d9980", async() => {
                BeginContext(1891, 4, true);
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
#line 66 "C:\Users\software team\Desktop\gitRepo\GatherRequirmentProject\Views\Modules\Details.cshtml"
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
            BeginContext(1899, 92, true);
            WriteLiteral(" </b>\r\n        </dl>\r\n\r\n\r\n\r\n    </div>\r\n    <div>\r\n       \r\n        \r\n    </div>\r\n    </div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Gather_Requirement_Project.Models.Module> Html { get; private set; }
    }
}
#pragma warning restore 1591
