#pragma checksum "C:\Users\benpa\Desktop\DividniApplication\Dividni\Views\Tutorial\Assessment.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ef6dfed835beb82e883fd30315341f766b75e410"
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
#line 1 "C:\Users\benpa\Desktop\DividniApplication\Dividni\Views\_ViewImports.cshtml"
using Dividni;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\benpa\Desktop\DividniApplication\Dividni\Views\_ViewImports.cshtml"
using Dividni.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef6dfed835beb82e883fd30315341f766b75e410", @"/Views/Tutorial/Assessment.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"32802f05557281687e406deee1568c4325db043d", @"/Views/_ViewImports.cshtml")]
    public class Views_Tutorial_Assessment : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Assessment", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("target", new global::Microsoft.AspNetCore.Html.HtmlString("_blank"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Manage", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "C:\Users\benpa\Desktop\DividniApplication\Dividni\Views\Tutorial\Assessment.cshtml"
  
    ViewData["Title"] = "Tutorial: Assessments";
    ViewData["Heading"] = "Tutorial: Assessments";
    ViewData["Description"] = "Learn how to create assessments using your questions";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<section class=\"section container blue lighten-5\" id=\"main\">\r\n    <div class=\"row\">\r\n        <div class=\"col s12\">\r\n            <h5 class=\"teal-text\">Instructions</h5>\r\n            <p>Follow these instructions to create an ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ef6dfed835beb82e883fd30315341f766b75e4104943", async() => {
                WriteLiteral("exam <i class=\"tiny material-icons\">open_in_new</i>");
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
            <div id=""instructions"">
                <ol>
                    <p>
                        <li>Fill-in the exam name (text, numbers, and dashes only. Please use dashes instead of spaces).
                            This is to
                            help you identify the exam later.
                        </li>
                    </p>
                    <p>
                        <li>Next, enter the number of versions of the exam (paper count) that you want to generate. This
                            value can
                            be between 1 and 100.</li>
                    </p>
                    <p>
                        <li>Check the cover page box to create an editor window for the exam cover page. This section
                            can contain
                            formatting, and is optional.</li>
                    </p>
                    <p>
                        <li>The quest");
            WriteLiteral(@"ions section will list all of your created questions. Check the box next to a
                            question to
                            include it in the exam.
                            Clicking on the <i class=""tiny material-icons"">zoom_in </i> icon will display the question
                            contents.
                            (Questions can be edited on the ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ef6dfed835beb82e883fd30315341f766b75e4107883", async() => {
                WriteLiteral("my\r\n                                multiple-choice questions\r\n                                <i class=\"tiny material-icons\">open_in_new</i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@" page.)
                        </li>
                    </p>
                    <p>
                        <li>Instruction sections are essentially questions without answers. As the name suggests, they
                            can be used
                            for writing instructions for a subset of the questions.
                            Click on the <i class=""tiny material-icons"">add_circle</i> icon to create an editor window
                            for a new
                            instruction section.
                            You can write the section (formatting is allowed) and click on the save button to add the
                            section to your
                            questions. It will be included in the exam automatically.
                            Unchecking the section will prevent it from being included in the exam. Previewing works the
                            same as with
                            regular questions.
               ");
            WriteLiteral(@"             You can also click on the <i class=""tiny material-icons"">edit</i> icon to make changes to
                            the section.
                        </li>
                    </p>
                    <li>
                        </p>Re-order the questions and instruction sections by clicking and dragging them into the order
                        you would
                        like them to appear in on the exam.</li>
                    </p>
                    <p>
                        <li>Check the appendix box to create an editor window for the appendix. This section can contain
                            formatting,
                            and is optional.</li>
                    </p>
                    <p>
                        <li>Click the <b>PREVIEW</b> button to see a preview of the exam.
                            If you are happy with the exam, you can click on <b>GENERATE</b> to create the exam.
                            Otherwise, you can stil");
            WriteLiteral(@"l make changes and click on <b>PREVIEW</b> to view the modified exam.
                        </li>
                    </p>
                    <p>
                        <li>Clicking the <b>GENERATE</b> button will bring up a window with a list of the available exam
                            types.
                            See the next section for an explanation of these types.
                            Make a selection and click on <b>SELECT</b> to generate the exam. This may take a minute or
                            so.
                            The window will display the outcome of the action. If there was a problem, you will be able
                            to modify the
                            exam details and try again.
                            If the exam generated multiple versions of the paper (e.g. the paper count was greater than
                            one and the
                            exam type was standard),
                            you can c");
            WriteLiteral(@"lick on the <b>Merge PDFs?</b> button. This will create a combined pdf of all of
                            the different
                            versions of the exam, for ease of printing.
                            The final step is to click on <b>Download Exam</b>. The exam will download as a .zip file.
                            See the <i>Exam .zip File Contents</i> section for an explanation of this file.
                            You will then have the options to create another exam, or view all of the exams that you
                            have created.
                        </li>
                    </p>
                </ol>
            </div>
            <h5 class=""teal-text"">Exam Types</h5>
            <p>When generating an exam, the final step is to choose the exam type:</p>
            <p>Selecting <i>standard</i> will create the desired number of papers in pdf format.
                The generated exams will incorporate any of the following if present: the cover pag");
            WriteLiteral(@"e, appendix, and
                instruction
                sections.
            </p>
            <p>Selecting any of the learning management system options will generate a zip file (using the exam name),
                that
                can be imported into the corresponding system.
                At present these options DO NOT allow instruction sections to be included in the exam, so these will be
                ignored.
            </p>
            <h5 class=""teal-text"">Exam .zip File Contents</h5>
            <p>Every type of exam downloads as a single .zip folder, which will have the name given to the exam when it
                was
                created. Extract the contents to start.</p>
            <p>For <i>standard</i> exams, the folder will consist of two subfolders and some files.
                The <i>answers</i> folder contains text files with the solutions to each version of the exam.
                The <i>papers</i> folder will contain pdfs of each version of the ");
            WriteLiteral(@"exam, and a combined version (it will
                have the
                same name as the exam), if it was generated.
                Most users will not need any of the other files in the main folder or any of the non-pdf files in the
                <i>papers</i> folder. These can be deleted.
            </p>
            <p>For learning management system exams (except Moodle), the folder will contain some files and another .zip
                folder with the same
                name as the exam.
                This second .zip folder can be uploaded to the learning management system to create an exam.
                The other files can be ignored or deleted.
            </p>
            <p>For Moodle exams, the folder will contain files and another regular folder with the same
                name as the exam.
                This second folder can be uploaded to Moodle to create an exam.
                The other files can be ignored or deleted.
            </p>
            <h5 c");
            WriteLiteral("lass=\"teal-text\">Viewing your Exams</h5>\r\n            <p>The ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ef6dfed835beb82e883fd30315341f766b75e41015873", async() => {
                WriteLiteral("my created exams <i\r\n                        class=\"tiny material-icons\">open_in_new</i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@" page
                displays all of the exams that you have created/have been shared with you.
            </p>
            <p>You can click on the <i class=""tiny material-icons"">more_vert</i> next to the exam name to see the list
                of
                actions that you can perform.</p>
            <p>Select <b>PREVIEW</b> to see the details of the exam.</p>
            <h5 class=""teal-text"">Downloading an Existing Exam</h5>
            <p>Select <b>DOWNLOAD</b> from the actions list. A window will be displayed asking you to confirm your
                choice.</p>
            <p>You will be redirected to the create exam page and the options window will display automatically to show
                the
                status of the exam generation process.
                You can then download the exam.
            </p>
            <h5 class=""teal-text"">Editing an Exam</h5>
            <p>Select <b>EDIT</b> from the actions list. A window will be displayed asking you to confirm y");
            WriteLiteral(@"our choice.
            </p>
            <p>You will be redirected to the create exam page and the details of the exam will be
                pre-loaded into the form.</p>
            <p>To save any changes, you will need to preview and generate the exam.</p>
            <h5 class=""teal-text"">Deleting an Exam</h5>
            <p>Select <b>DELETE</b> from the actions list. A window will be displayed asking you to confirm your choice.
            </p>
            <h5 class=""teal-text"">Using an Exam as a Template</h5>
            <p>Select <b>TEMPLATE</b> from the actions list. A window will be displayed asking you to confirm your
                choice.</p>
            <p>You will be redirected to the create exam page and the details of the exam will be
                pre-loaded into the form.</p>
            <p>To save the new exam, you will need to preview and generate it. Note that using an exam as a template
                leaves
                the original exam unchanged.</p>
          ");
            WriteLiteral(@"  <h5 class=""teal-text"">Sharing an Exam</h5>
            <p>Select <b>SHARE</b> from the actions list. A window will be displayed asking you to confirm your choice.
            </p>
            <p>A small form will be displayed where you can enter the email of the user with whom you wish to share the
                exam. Click <b>SHARE </b><i class=""tiny material-icons"">send</i> to complete the process.</p>
            <p>Note that the other user will receive a copy of the exam, and any changes he or she makes to it will not
                be reflected on your version of the exam. Sharing an exam does not automatically share the questions in
                that
                exam.
                The other user will be able to view the details of the questions and include them in the exam, but
                cannot edit
                them.
                You will need to share individual questions with the other user if he/she will need to make any changes
                to them.
     ");
            WriteLiteral("       </p>\r\n        </div>\r\n</section>");
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
