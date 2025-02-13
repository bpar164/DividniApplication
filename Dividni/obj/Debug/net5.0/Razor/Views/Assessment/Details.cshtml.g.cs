#pragma checksum "C:\Users\benpa\DividniApplication\Dividni\Views\Assessment\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "36df5424cf77356a4b580f0d4f46f3e78ed9c00b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Assessment_Details), @"mvc.1.0.view", @"/Views/Assessment/Details.cshtml")]
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
#nullable restore
#line 1 "C:\Users\benpa\DividniApplication\Dividni\Views\_ViewImports.cshtml"
using Dividni;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\benpa\DividniApplication\Dividni\Views\_ViewImports.cshtml"
using Dividni.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"36df5424cf77356a4b580f0d4f46f3e78ed9c00b", @"/Views/Assessment/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"32802f05557281687e406deee1568c4325db043d", @"/Views/_ViewImports.cshtml")]
    public class Views_Assessment_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Dividni.Models.Assessment>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("waves-effect waves-light btn"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "C:\Users\benpa\DividniApplication\Dividni\Views\Assessment\Details.cshtml"
  
    ViewData["Title"] = "Assessments: Details";
    ViewData["Heading"] = "Assessments";
    ViewData["Description"] = "View the details of an assessment";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<section class=\"section container blue lighten-5\" id=\"main\">\r\n    <div class=\"row\">\r\n        <div class=\"col\">\r\n            <h5 class=\"teal-text\">Assessment Details</h5>\r\n            <div>\r\n                <b>Name: </b>");
#nullable restore
#line 14 "C:\Users\benpa\DividniApplication\Dividni\Views\Assessment\Details.cshtml"
                        Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</br>\r\n                </br><b>Cover Page:</b></br>\r\n                <div id=\"coverPage\"");
            BeginWriteAttribute("value", " value=\"", 548, "\"", 598, 1);
#nullable restore
#line 16 "C:\Users\benpa\DividniApplication\Dividni\Views\Assessment\Details.cshtml"
WriteAttributeValue("", 556, Html.DisplayFor(model => model.CoverPage), 556, 42, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></div></br>\r\n                <b>Question List:</b>\r\n                <ol id=\"questionList\"");
            BeginWriteAttribute("value", " value=\"", 689, "\"", 742, 1);
#nullable restore
#line 18 "C:\Users\benpa\DividniApplication\Dividni\Views\Assessment\Details.cshtml"
WriteAttributeValue("", 697, Html.DisplayFor(model => model.QuestionList), 697, 45, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></ol>\r\n                <b>Appendix:</b></br>\r\n                <div id=\"appendix\"");
            BeginWriteAttribute("value", " value=\"", 824, "\"", 873, 1);
#nullable restore
#line 20 "C:\Users\benpa\DividniApplication\Dividni\Views\Assessment\Details.cshtml"
WriteAttributeValue("", 832, Html.DisplayFor(model => model.Appendix), 832, 41, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></div></br>     \r\n            </div>\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "36df5424cf77356a4b580f0d4f46f3e78ed9c00b6081", async() => {
                WriteLiteral("\r\n                Return to list<i class=\"material-icons right\">list</i>\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
        </div>
    </div>
</section>

<!-- Preview Modal -->
<div id=""previewModal"" class=""modal modal-fixed-footer"">
  <div class=""modal-content"">
    <h4>Assessment Preview</h4>
    <div id=""previewModalContent""></div>
  </div>
  <div class=""modal-footer"">
    <a href=""#!"" class=""modal-close waves-effect waves-green btn-flat"">CLOSE</a>
  </div>
</div>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Dividni.Models.Assessment> Html { get; private set; }
    }
}
#pragma warning restore 1591
