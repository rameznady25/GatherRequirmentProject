#pragma checksum "E:\Excel System Work\9-9-2019\05\05\Gather Requirement Project\Views\CustomerPrograms\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "401ed13492638245483424b1c5d6225d8760fade"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_CustomerPrograms_Details), @"mvc.1.0.view", @"/Views/CustomerPrograms/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/CustomerPrograms/Details.cshtml", typeof(AspNetCore.Views_CustomerPrograms_Details))]
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
#line 1 "E:\Excel System Work\9-9-2019\05\05\Gather Requirement Project\Views\_ViewImports.cshtml"
using Gather_Requirement_Project;

#line default
#line hidden
#line 2 "E:\Excel System Work\9-9-2019\05\05\Gather Requirement Project\Views\_ViewImports.cshtml"
using Gather_Requirement_Project.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"401ed13492638245483424b1c5d6225d8760fade", @"/Views/CustomerPrograms/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d1b7013e94b18a1b4839ca552d19bbd548f16f86", @"/Views/_ViewImports.cshtml")]
    public class Views_CustomerPrograms_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Gather_Requirement_Project.Models.CustomerProgram>
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
#line 3 "E:\Excel System Work\9-9-2019\05\05\Gather Requirement Project\Views\CustomerPrograms\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
            BeginContext(103, 317, true);
            WriteLiteral(@"
<div class=""card "">
    <div class=""card-header"">
        <h4> <i class=""fas fa-table""></i>&nbsp&nbsp Details Program </h4>
    </div>
    <div class=""card-body"" style="""">
        <div class=""row"">
            <div class=""col-md-10"">

            </div>
            <div class=""col-md-2"">
                ");
            EndContext();
            BeginContext(420, 36, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "401ed13492638245483424b1c5d6225d8760fade4583", async() => {
                BeginContext(442, 10, true);
                WriteLiteral("go to Back");
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
            BeginContext(456, 266, true);
            WriteLiteral(@"
            </div>
        </div>
        <hr />



        <dl class=""text-md-left  row"">
            <dt class=""col-sm-3"">
                <span>Program English Name In Single</span>
            </dt>
            <dd class=""col-sm-9"">
                ");
            EndContext();
            BeginContext(723, 51, false);
#line 29 "E:\Excel System Work\9-9-2019\05\05\Gather Requirement Project\Views\CustomerPrograms\Details.cshtml"
           Write(Html.DisplayFor(model => model.EnglishNameInSingle));

#line default
#line hidden
            EndContext();
            BeginContext(774, 189, true);
            WriteLiteral("\r\n            </dd>\r\n\r\n            <dt class=\"col-sm-3\">\r\n                <span>Program English Name In Plural</span>\r\n            </dt>\r\n            <dd class=\"col-sm-9\">\r\n                ");
            EndContext();
            BeginContext(964, 51, false);
#line 36 "E:\Excel System Work\9-9-2019\05\05\Gather Requirement Project\Views\CustomerPrograms\Details.cshtml"
           Write(Html.DisplayFor(model => model.EnglishNameInPlural));

#line default
#line hidden
            EndContext();
            BeginContext(1015, 188, true);
            WriteLiteral("\r\n            </dd>\r\n\r\n            <dt class=\"col-sm-3\">\r\n                <span>Program Arabic Name In Single</span>\r\n            </dt>\r\n            <dd class=\"col-sm-9\">\r\n                ");
            EndContext();
            BeginContext(1204, 50, false);
#line 43 "E:\Excel System Work\9-9-2019\05\05\Gather Requirement Project\Views\CustomerPrograms\Details.cshtml"
           Write(Html.DisplayFor(model => model.ArabicNameInSingle));

#line default
#line hidden
            EndContext();
            BeginContext(1254, 190, true);
            WriteLiteral("\r\n            </dd>\r\n\r\n\r\n            <dt class=\"col-sm-3\">\r\n                <span>Program Arabic Name In Plural</span>\r\n            </dt>\r\n            <dd class=\"col-sm-9\">\r\n                ");
            EndContext();
            BeginContext(1445, 50, false);
#line 51 "E:\Excel System Work\9-9-2019\05\05\Gather Requirement Project\Views\CustomerPrograms\Details.cshtml"
           Write(Html.DisplayFor(model => model.ArabicNameInPlural));

#line default
#line hidden
            EndContext();
            BeginContext(1495, 184, true);
            WriteLiteral("\r\n            </dd>\r\n\r\n\r\n            <dt class=\"col-sm-3\">\r\n                <span>Program URL For Service</span>\r\n            </dt>\r\n            <dd class=\"col-sm-9\">\r\n                ");
            EndContext();
            BeginContext(1680, 45, false);
#line 59 "E:\Excel System Work\9-9-2019\05\05\Gather Requirement Project\Views\CustomerPrograms\Details.cshtml"
           Write(Html.DisplayFor(model => model.URLForService));

#line default
#line hidden
            EndContext();
            BeginContext(1725, 88, true);
            WriteLiteral("\r\n            </dd>\r\n\r\n        </dl>\r\n\r\n\r\n        <dl class=\"col-sm-5\">\r\n            <b>");
            EndContext();
            BeginContext(1813, 54, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "401ed13492638245483424b1c5d6225d8760fade9093", async() => {
                BeginContext(1859, 4, true);
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
#line 66 "E:\Excel System Work\9-9-2019\05\05\Gather Requirement Project\Views\CustomerPrograms\Details.cshtml"
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
            BeginContext(1867, 92, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Gather_Requirement_Project.Models.CustomerProgram> Html { get; private set; }
    }
}
#pragma warning restore 1591