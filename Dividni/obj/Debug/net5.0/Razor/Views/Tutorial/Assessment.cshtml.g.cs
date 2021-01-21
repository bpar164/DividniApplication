#pragma checksum "C:\Users\benpa\DividniApplication\Dividni\Views\Tutorial\Assessment.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7e5340f8ffc91e37b474ce7e532c57befcc0bde7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Tutorial_Assessment), @"mvc.1.0.view", @"/Views/Tutorial/Assessment.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7e5340f8ffc91e37b474ce7e532c57befcc0bde7", @"/Views/Tutorial/Assessment.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"32802f05557281687e406deee1568c4325db043d", @"/Views/_ViewImports.cshtml")]
    public class Views_Tutorial_Assessment : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "QuestionBank", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("target", new global::Microsoft.AspNetCore.Html.HtmlString("_blank"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Assessment", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 1 "C:\Users\benpa\DividniApplication\Dividni\Views\Tutorial\Assessment.cshtml"
  
    ViewData["Title"] = "Tutorial: Assessments";
    ViewData["Heading"] = "Tutorial: Assessments";
    ViewData["Description"] = "Learn how to create and manage assessments using your questions";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<section class=\"section container blue lighten-5\" id=\"main\">\r\n    <div class=\"row\">\r\n        <div class=\"col s12\">\r\n            <h5 class=\"teal-text\">Instructions</h5>\r\n            <p>Follow these instructions to create an ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7e5340f8ffc91e37b474ce7e532c57befcc0bde74931", async() => {
                WriteLiteral("assessment <i class=\"tiny material-icons\">open_in_new</i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@" using your existing
                questions.</p>
            <div id=""questionBanks"">
                <h6 class=""teal-text"">Question Banks</h5>
                    <p>To create an assessment, you first need to have made a question bank.</p>
                    <p>Create a question bank by entering a name (text, numbers, and spaces only), and clicking on <b>CREATE</b>.</p>
                    <p>Add questions to your question banks by selecting the <i class=""tiny material-icons"">playlist_add</i> option on the question management pages.</p>
                    <p>Click on the question bank display or the <b>DETAILS</b> link to view the question bank contents.</p>
                    <p>
                        You can rearrange the question order, remove questions <i class=""tiny material-icons"">delete</i>, or rename the question bank, whilst viewing the contents.
                        If you make any changes to the question bank, you must click on <b>SAVE</b> to make the changes permanent.
     ");
            WriteLiteral(@"               </p>
                    <p>The <b>DELETE</b> option will remove the entire question bank.</p>
                    <p>The <b>CREATE ASSESSMENT</b> link will redirect you to the create assessment page where you can fill-in the assessment details.</p>
            </div>
            <div id=""instructions"">
                <h6 class=""teal-text"">Creating the Assessment</h5>
                    <ol>
                        <p>
                            <li>Fill-in the assessment name (text, numbers, and dashes only. Please use dashes instead
                                of spaces).
                                This is to help you identify the assessment later.
                            </li>
                        </p>
                        <p>
                            <li>Check the cover page box to create an editor window for the assessment cover page. This
                                section
                                can contain formatting, and is optional");
            WriteLiteral(@".</li>
                        </p>
                        <p>
                            <li>Initially, the question list section will list all of the questions from the question
                                bank.
                                It will also list instruction sections and individual questions as they are added.
                                Clicking on a question item will toggle whether it is included in the assessment (a
                                white background means that the item is NOT included).
                                Clicking on the <i class=""tiny material-icons"">pageview </i> icon will display the
                                question
                                details in a new window. Questions can be edited on their corresponding management page.
                            </li>
                        </p>
                        <p>
                            <li>Instruction sections are essentially questions without answers. As the n");
            WriteLiteral(@"ame suggests,
                                they
                                can be used
                                for writing instructions for a subset of the questions.
                                Click on the <i class=""tiny material-icons"">add_circle</i> icon to create an editor
                                window
                                for a new
                                instruction section.
                                You can write the section (formatting is allowed) and click on the save button to add
                                the
                                section to the question list.
                                Clicking on the <i class=""tiny material-icons"">pageview </i> icon will display the
                                section details in a pop-up window.
                                You can also click on the <i class=""tiny material-icons"">edit</i> icon to make changes
                                to the section.
          ");
            WriteLiteral(@"                  </li>
                        </p>
                        <p>
                            <li>You can add an individual question (not from the question bank) by visiting that
                                question's management page, and clicking on the
                                <i class=""tiny material-icons"">link</i> icon next to the question's name to copy that
                                question's id to the clipboard.
                                (The clipboard is where the items that you copy when you press 'control c' or
                                'right-click copy' are stored.)
                                Paste the id into the input provided, select the correct question type, and click on the
                                <b>ADD</b> button to add the question to the list.
                            </li>
                        </p>
                        <li>
                            </p>Re-order the questions and instruction sections by c");
            WriteLiteral(@"licking and dragging them into the
                            order
                            you would like them to appear in on the assessment.</li>
                        </p>
                        <p>
                            <li>Check the appendix box to create an editor window for the appendix. This section can
                                contain
                                formatting, and is optional.</li>
                        </p>
                        <p>
                            <li>Click the <b>PREVIEW</b> button to see a preview of the assessment.
                                If you are happy with the assessment, you can click on <b>GENERATE</b> to create the
                                assessment.
                                Otherwise, you can still make changes and click on <b>PREVIEW</b> to view the modified
                                assessment.
                            </li>
                        </p>
                        <p>
");
            WriteLiteral(@"                            <li>Clicking the <b>GENERATE</b> button will create the assessment.
                                If there are any problems, these will be listed in a dialog.
                                Once the assessment is generated, you will be redirected to the management page.
                            </li>
                        </p>
                    </ol>
            </div>
            <h5 class=""teal-text"">Managing your Assessments</h5>
            <p>The ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7e5340f8ffc91e37b474ce7e532c57befcc0bde713287", async() => {
                WriteLiteral("assessment management <i\r\n                        class=\"tiny material-icons\">open_in_new</i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@" page displays all of the assessments that
                you have
                created/that have been shared with you.
            </p>
            <p>The assessments are sorted newest to oldest by default (you can also sort by name), and you can search
                for
                an assessment by name.</p>
            <p>You can click on the icons next to each assessment to perform a number of actions (which will open
                a new page):</p>
            <h6 class=""teal-text"">Viewing an Assessment <i class=""material-icons"">pageview</i></h6>
            <p>Displays all of the assessment details.</p>
            <h6 class=""teal-text"">Editing an Assessment <i class=""material-icons"">edit</i></h6>
            <p>Loads the assessment details into a form that can be edited. Any changes need to be saved before
                exiting.</p>
            <h6 class=""teal-text"">Deleting an Assessment <i class=""material-icons"">delete</i></h6>
            <p>The assessment details are dis");
            WriteLiteral(@"played before you confirm the deletion.</p>
            <h6 class=""teal-text"">Using an Assessment as a Template <i class=""material-icons"">content_copy</i>
            </h6>
            <p>Loads the assessment details into a form that can be edited. The assessment must be generated
                before exiting.</p>
            <p>Note that using an assessment as a template generates a new assessment and leaves the original unchanged.
            </p>
            <h6 class=""teal-text"">Sharing an Assessment <i class=""material-icons"">share</i></h6>
            <p>An input for the user's email is displayed underneath the assessment details.</p>
            <p>The user must have an account before you are able to share assessments with him/her.</p>
            <p>The user receives a copy of the assessment and cannot make any changes to the original.</p>
            <p>Checking the 'share all questions' box will share a copy of each question with the new user.</p>
            <p>If the box is unchecked,");
            WriteLiteral(@" the other user will be able to view the details of the questions and include
                them in the assessment, but cannot edit them.
            <p>You can also share individual questions from the corresponding question management page.</p>
            <h6 class=""teal-text"">Downloading an Assessment <i class=""material-icons"">download</i></h6>
            <p>Under the Download Details heading, enter the number of unique versions of the assessment that you want
                to generate (between 1 and 300).</p>
            <p>Select the assessment type (see below) and click on <b>GENERATE</b>. If there are no errors, the
                <b>DOWNLOAD</b> option will then become available. Clicking this button will download a .zip folder
                containing the assessment files (see the last section for an explanation of these files).
            </p>
            <h6 class=""teal-text"">Assessment Types</h5>
                <p>When downloading an assessment, the final step is to choose the");
            WriteLiteral(@" assessment type:</p>
                <p>Selecting <i>standard</i> will create the desired number of papers in html format.
                    The generated assessments will incorporate any of the following if present: the cover page,
                    appendix,
                    and instruction sections.
                </p>
                <p>Selecting any of the learning management system options will generate a zip file (using the
                    assessment
                    name), that can be imported into the corresponding system.
                    At present these options DO NOT allow instruction sections to be included in the assessment, so
                    these
                    will be ignored.
                </p>
                <h6 class=""teal-text"">Assessment .zip File Contents</h5>
                    <p>Every type of assessment downloads as a single .zip folder, which will have the name given to the
                        assessment when it was created. Extra");
            WriteLiteral(@"ct the contents to start.</p>
                    <p>For <i>standard</i> assessments, the folder will consist of two subfolders and some files.
                        The <i>answers</i> folder contains text files with the solutions to each version of the
                        assessment.
                        The <i>papers</i> folder will contain html documents representing version of the assessment.
                        Most users will not need any of the other files in the main folder - these can be deleted.
                    </p>
                    <p>For learning management system assessments (except Moodle), the folder will contain some files
                        and
                        another .zip folder with the same name as the assessment.
                        This second .zip folder can be uploaded to the learning management system to create an
                        assessment.
                        The other files can be ignored or deleted.
                    ");
            WriteLiteral(@"</p>
                    <p>For Moodle assessments, the folder will contain files and another regular folder with the same
                        name as the assessment.
                        This second folder can be uploaded to Moodle to create an assessment.
                        The other files can be ignored or deleted.
                    </p>
        </div>
</section>");
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
