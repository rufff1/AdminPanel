#pragma checksum "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\Tag\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0feec262b3279016a57b2ed5e667df20fd737856"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(FirstTask.Pages.Tag.Views_Tag_Detail), @"mvc.1.0.view", @"/Views/Tag/Detail.cshtml")]
namespace FirstTask.Pages.Tag
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\_ViewImports.cshtml"
using FirstTask;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\_ViewImports.cshtml"
using FirstTask.DAL;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\_ViewImports.cshtml"
using FirstTask.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\_ViewImports.cshtml"
using FirstTask.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0feec262b3279016a57b2ed5e667df20fd737856", @"/Views/Tag/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0513ad2a501b80386096493bd2a6845d4bcf2138", @"/Views/_ViewImports.cshtml")]
    public class Views_Tag_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FirstTask.Models.Tag>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-dark"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "update", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\Tag\Detail.cshtml"
  
    ViewData["Title"] = "Detail";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div style=\" width: 86%; margin-left: 17%; margin-top: 5%;\">\r\n    <h1>Detail</h1>\r\n\r\n    <div>\r\n        <h4>Tag</h4>\r\n        <hr />\r\n        <dl class=\"row\">\r\n            <dt class=\"col-sm-2\">\r\n                ");
#nullable restore
#line 16 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\Tag\Detail.cshtml"
           Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 19 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\Tag\Detail.cshtml"
           Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt class=\"col-sm-2\">\r\n                ");
#nullable restore
#line 22 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\Tag\Detail.cshtml"
           Write(Html.DisplayNameFor(model => model.IsDeleted));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 25 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\Tag\Detail.cshtml"
           Write(Html.DisplayFor(model => model.IsDeleted));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt class=\"col-sm-2\">\r\n                ");
#nullable restore
#line 28 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\Tag\Detail.cshtml"
           Write(Html.DisplayNameFor(model => model.CreatAt));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 31 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\Tag\Detail.cshtml"
           Write(Html.DisplayFor(model => model.CreatAt));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt class=\"col-sm-2\">\r\n                ");
#nullable restore
#line 34 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\Tag\Detail.cshtml"
           Write(Html.DisplayNameFor(model => model.CreatBy));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 37 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\Tag\Detail.cshtml"
           Write(Html.DisplayFor(model => model.CreatBy));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt class=\"col-sm-2\">\r\n                ");
#nullable restore
#line 40 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\Tag\Detail.cshtml"
           Write(Html.DisplayNameFor(model => model.UpdateAt));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 43 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\Tag\Detail.cshtml"
           Write(Html.DisplayFor(model => model.UpdateAt));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt class=\"col-sm-2\">\r\n                ");
#nullable restore
#line 46 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\Tag\Detail.cshtml"
           Write(Html.DisplayNameFor(model => model.UpdateBy));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 49 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\Tag\Detail.cshtml"
           Write(Html.DisplayFor(model => model.UpdateBy));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt class=\"col-sm-2\">\r\n                ");
#nullable restore
#line 52 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\Tag\Detail.cshtml"
           Write(Html.DisplayNameFor(model => model.DeletedAt));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 55 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\Tag\Detail.cshtml"
           Write(Html.DisplayFor(model => model.DeletedAt));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt class=\"col-sm-2\">\r\n                ");
#nullable restore
#line 58 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\Tag\Detail.cshtml"
           Write(Html.DisplayNameFor(model => model.DeletedBy));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 61 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\Tag\Detail.cshtml"
           Write(Html.DisplayFor(model => model.DeletedBy));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n        </dl>\r\n    </div>\r\n    <div>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0feec262b3279016a57b2ed5e667df20fd73785610224", async() => {
                WriteLiteral("Edit");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 66 "C:\Users\ROG\source\repos\FirstTask\FirstTask\Views\Tag\Detail.cshtml"
                                                      WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
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
            WriteLiteral(" |\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0feec262b3279016a57b2ed5e667df20fd73785612471", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FirstTask.Models.Tag> Html { get; private set; }
    }
}
#pragma warning restore 1591
