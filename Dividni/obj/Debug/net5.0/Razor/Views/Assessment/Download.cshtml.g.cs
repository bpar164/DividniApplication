#pragma checksum "C:\Users\benpa\DividniApplication\Dividni\Views\Assessment\Download.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "86c2e97181a6c849c5a48bae7fb6f6518abcc60e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Assessment_Download), @"mvc.1.0.view", @"/Views/Assessment/Download.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"86c2e97181a6c849c5a48bae7fb6f6518abcc60e", @"/Views/Assessment/Download.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"32802f05557281687e406deee1568c4325db043d", @"/Views/_ViewImports.cshtml")]
    public class Views_Assessment_Download : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Dividni.Models.Assessment>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onSubmit", new global::Microsoft.AspNetCore.Html.HtmlString("return downloadAssessment(event)"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("waves-effect waves-light btn"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\benpa\DividniApplication\Dividni\Views\Assessment\Download.cshtml"
  
    ViewData["Title"] = "Assessments: Download";
    ViewData["Heading"] = "Assessments";
    ViewData["Description"] = "Download an assessment";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<section class=""section container blue lighten-5"" id=""main"">
    <div class=""row"">
        <div class=""col"">
            <h5 class=""teal-text"">Assessment Details</h5>
            <p>Download the following assessment?</p>
            <div>
                <b>Name: </b>");
#nullable restore
#line 15 "C:\Users\benpa\DividniApplication\Dividni\Views\Assessment\Download.cshtml"
                        Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</br>\r\n                </br><b>Cover Page:</b></br>\r\n                <div id=\"coverPage\"");
            BeginWriteAttribute("value", " value=\"", 593, "\"", 643, 1);
#nullable restore
#line 17 "C:\Users\benpa\DividniApplication\Dividni\Views\Assessment\Download.cshtml"
WriteAttributeValue("", 601, Html.DisplayFor(model => model.CoverPage), 601, 42, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></div></br>\r\n                <b>Question List:</b>\r\n                <ol id=\"questionList\"");
            BeginWriteAttribute("value", " value=\"", 734, "\"", 787, 1);
#nullable restore
#line 19 "C:\Users\benpa\DividniApplication\Dividni\Views\Assessment\Download.cshtml"
WriteAttributeValue("", 742, Html.DisplayFor(model => model.QuestionList), 742, 45, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></ol>\r\n                <b>Appendix:</b></br>\r\n                <div id=\"appendix\"");
            BeginWriteAttribute("value", " value=\"", 869, "\"", 918, 1);
#nullable restore
#line 21 "C:\Users\benpa\DividniApplication\Dividni\Views\Assessment\Download.cshtml"
WriteAttributeValue("", 877, Html.DisplayFor(model => model.Appendix), 877, 41, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></div></br>\r\n            </div>\r\n            <h6 class=\"teal-text\">Download Details</h6>\r\n             ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "86c2e97181a6c849c5a48bae7fb6f6518abcc60e6839", async() => {
                WriteLiteral("\r\n                <div class=\"input-field\"><input id=\"assessmentID\" name=\"assessmentID\"");
                BeginWriteAttribute("value", " value=\"", 1160, "\"", 1203, 1);
#nullable restore
#line 25 "C:\Users\benpa\DividniApplication\Dividni\Views\Assessment\Download.cshtml"
WriteAttributeValue("", 1168, Html.DisplayFor(model => model.Id), 1168, 35, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" type=""hidden"")></div>
                <div class=""input-field"">
                    <label for=""assessmentVersions"">Versions</label>
                    <input type=""number"" min=""1"" max=""100"" step=""1"" value=""1"" id=""assessmentVersions"" name=""assessmentVersions"" required>
                </div>
                <div>
                    <p>Exam type:</p>
                        <p><label>
                            <input class=""with-gap"" type=""radio"" name=""assessmentType"" value=""standard"" required checked>
                            <span>Standard</span>
                        </label></p>
                        <p><label>
                            <input class=""with-gap"" type=""radio"" name=""assessmentType"" value=""canvas"" required>
                            <span>Canvas</span>
                        </label></p>
                        <p><label>
                            <input class=""with-gap"" type=""radio"" name=""assessmentType"" value=""moodle"" required>
                            <");
                WriteLiteral(@"span>Moodle</span>
                        </label></p>
                        <p><label>
                            <input class=""with-gap"" type=""radio"" name=""assessmentType"" value=""inspera"" required>
                            <span>Inspera</span>
                        </label></p>
                </div>
                <button type=""submit"" class=""waves-effect waves-light btn orange"">Download<i
                        class=""material-icons right"">download</i></button>
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("                 \r\n            <p></p>\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "86c2e97181a6c849c5a48bae7fb6f6518abcc60e10312", async() => {
                WriteLiteral("\r\n                Return to list<i class=\"material-icons right\">list</i>\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
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
