using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dividni.Data;
using Dividni.Models;
using System.Text.Json;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Dividni.Services
{
    public class AssessmentService
    {
        private readonly ApplicationDbContext _context;
        public AssessmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        //Fetch each question and create a copy of it belonging to the new user
        public string shareAllQuestions(string questionListJSON, string userEmail, DateTime modifiedDate)
        {
            var questionList = JsonSerializer.Deserialize<Question[]>(questionListJSON);
            for (int i = 0; i < questionList.Length; i++)
            {
                if (questionList[i].type == "Simple")
                {
                    var simple = fetchSimpleQuestion(new Guid(questionList[i].id));
                    if (simple != null)
                    {
                        //Modify details
                        simple.Id = Guid.NewGuid();
                        simple.UserEmail = userEmail;
                        simple.ModifiedDate = modifiedDate;
                        //Save the modified question as a new object in the database
                        _context.Add(simple);
                        _context.SaveChanges();
                        //Update the questionList so that it refers to the new question
                        questionList[i].id = simple.Id.ToString();
                    }
                }
                else if (questionList[i].type == "Advanced")
                {
                    var advanced = fetchAdvancedQuestion(new Guid(questionList[i].id));
                    if (advanced != null)
                    {
                        //Modify details
                        advanced.Id = Guid.NewGuid();
                        advanced.UserEmail = userEmail;
                        advanced.ModifiedDate = modifiedDate;
                        //Save the modified question as a new object in the database
                        _context.Add(advanced);
                        _context.SaveChanges();
                        //Update the questionList so that it refers to the new question
                        questionList[i].id = advanced.Id.ToString();
                    }
                }
            }
            return JsonSerializer.Serialize<Question[]>(questionList);
        }

        public string getDirectory()
        {
            //Return the current directory, but remove Dividni (the code folder) from the path
            return Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 7);
        }

        public Boolean generateAssessment(Assessment assessment, DownloadRequest downloadRequest)
        {
            try
            {
                //Create a subfolder called Assessments (may exist already)
                var folderPath = getDirectory() + "Assessments"; //Replace 'Dividni' with 'Assessments'
                System.IO.Directory.CreateDirectory(folderPath);
                var assessmentPath = folderPath + "\\" + assessment.Name;
                //Create a folder specifically for this assessment
                System.IO.Directory.CreateDirectory(assessmentPath);
                //String listing question ids for command line 
                var questionIds = "";
                //String for document contents
                var questionHTML = "<div id=\"Questions\"><ol class=\"qlist\">";
                //Get list of question ids from assessment model
                var questionList = JsonSerializer.Deserialize<Question[]>(assessment.QuestionList);
                //For counting simple questions 
                var questionNumber = 1;
                //Process questions based on type, and add them to the HTML document
                for (int i = 0; i < questionList.Length; i++)
                {
                    if (questionList[i].type == "Simple")
                    {
                        var simple = fetchSimpleQuestion(new Guid(questionList[i].id));
                        if (simple != null)
                        {
                            //Save id to list for the commands
                            questionIds += "Q" + questionNumber + ".cs ";
                            //Save id to list for the document
                            questionHTML += "<li class=\"q\"><p class=\"cws_code_q\">Q" + questionNumber + "</p></li>";
                            //Convert question to C# string
                            var questionString = simpleToString(simple, questionNumber);
                            //Save C# string to a file
                            System.IO.File.WriteAllText(assessmentPath + "\\Q" + questionNumber + ".cs", questionString);
                            questionNumber++;
                        }

                    }
                    else if (questionList[i].type == "Advanced")
                    {
                        var advanced = fetchAdvancedQuestion(new Guid(questionList[i].id));
                        if (advanced != null)
                        {
                            //Save id to list for the commands
                            questionIds += questionList[i].name + ".cs ";
                            //Save id to list for the document
                            questionHTML += "<li class=\"q\"><p class=\"cws_code_q\">" + questionList[i].name + "</p></li>";
                            //Save C# string to a file
                            System.IO.File.WriteAllText(assessmentPath + "\\" + questionList[i].name + ".cs", JsonSerializer.Deserialize<string>(advanced.Question));
                        }
                    }
                    else
                    {
                        questionHTML += questionList[i].value; //Add instruction section contents to document 
                    }
                }
                //Close the HTML string
                questionHTML += "</ol></div>";
                //Run the Dividni commands, based on the assessment type
                if (downloadRequest.Type == "standard")
                {
                    
                    //Create HTML template
                    var templateHTML = "";
                    //Cover page
                    if (assessment.CoverPage != "") templateHTML += "<div id=\"coverPage\">" + assessment.CoverPage + "</div><p style=\"page-break-after: always;\" />";
                    //Questions
                    templateHTML += "<div id=\"questions\">" + questionHTML + "</div>";
                    //Appendix
                    if (assessment.Appendix != "") templateHTML += "<p style=\"page-break-before: always;\"/><div id=\"appendix\">" + assessment.Appendix + "</div>";
                    //Create the file
                    createHTMLDocument(assessmentPath + "\\Assessment.Template.html", assessment.Name, templateHTML);
                    //Compile all of the questions
                    executeCommand("/c cd .. & cd Assessments\\" + assessment.Name + " & csc -t:library -lib:\"C:\\Program Files\\Dividni.com\\Dividni\" -r:Utilities.Courses.dll -out:QHelper.dll " + questionIds);
                    //Generate assessment
                    executeCommand("/c cd .. & cd Assessments\\" + assessment.Name + " & TestGen -lib QHelper.dll -htmlFolder papers -answerFolder answers -paperCount " + downloadRequest.Versions + " Assessment.Template.html");
                }
                else
                { //LMS Assessment
                    //Cover Page and Appendix
                    if (assessment.CoverPage != null)
                    {
                        createHTMLDocument(assessmentPath + "\\Assessment.CoverPage.html", assessment.Name + " Cover Page", assessment.CoverPage);
                        questionIds = "Assessment.CoverPage.html " + questionIds;
                    }
                    if (assessment.Appendix != null)
                    {
                        createHTMLDocument(assessmentPath + "\\Assessment.Appendix.html", assessment.Name + " Appendix", assessment.Appendix);
                        questionIds += "Assessment.Appendix.html";
                    }
                    if (downloadRequest.Type == "moodle")
                    {
                        executeCommand("/c cd .. & cd Assessments\\" + assessment.Name + " & MoodleGen -variants " + downloadRequest.Versions + " -xmlFolder questions -bank " + assessment.Name + " " + questionIds);
                    }
                    else
                    {
                        //Determine qtiVers
                        var qtiVers = "";
                        if (downloadRequest.Type == "canvas")
                        {
                            qtiVers = "1.2";
                        }
                        else
                        {
                            qtiVers = "2.1";
                        }
                        //Create LMS compatible zip 
                        executeCommand("/c cd .. & cd Assessments\\" + assessment.Name + " & QtiGen -qtiVersion " + qtiVers + " -variants " + downloadRequest.Versions + " -id " + assessment.Name + " " + questionIds);
                    }
                }
                //Compress the folder contents into a .zip archive
                executeCommand("/c cd .. & cd Assessments & tar cf " + assessment.Name + ".zip " + assessment.Name);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public Simple fetchSimpleQuestion(Guid id)
        {
            return _context.Simple.FirstOrDefault(q => q.Id == id);
        }

        public Advanced fetchAdvancedQuestion(Guid id)
        {
            return _context.Advanced.FirstOrDefault(q => q.Id == id);
        }

        public string simpleToString(Simple simple, int questionNumber)
        {
            //Build the C# file from the question details
            var questionString = "using System;namespace Utilities.Courses {public partial class QHelper : IQHelper";
            questionString += "{public static string Q" + questionNumber + "(Random random, Action<string, ushort> registerAnswer, bool isProof)";
            questionString += "{var q = Quest_Q" + questionNumber + "(random, isProof);string rval = q.GetQuestion(registerAnswer);return rval;}";
            questionString += "public static QuestionBase Quest_Q" + questionNumber + "(Random random, bool isProof) { var q = new " + simple.Type + "Question(random, isProof);";
            questionString += "q.Id = \"Q" + questionNumber + "\";";
            questionString += "q.Marks = " + simple.Marks + "; q.ShowMarks = false;";
            questionString += "q.Stem = @\"" + simple.QuestionText + "\";";
            //Correct answers
            questionString += "q.AddCorrects(";
            var correctAnswers = JsonSerializer.Deserialize<string[]>(simple.CorrectAnswers);
            for (int i = 0; i < correctAnswers.Length; i++)
            {
                if (i == (correctAnswers.Length - 1))
                { //Close list with last answer
                    questionString += "@\"" + correctAnswers[i] + "\");";
                }
                else
                {
                    questionString += "@\"" + correctAnswers[i] + "\",";
                }
            }
            //Incorrect answers
            questionString += "q.AddIncorrects(";
            var incorrectAnswers = JsonSerializer.Deserialize<string[]>(simple.IncorrectAnswers);
            for (int i = 0; i < incorrectAnswers.Length; i++)
            {
                if (i == (incorrectAnswers.Length - 1))
                { //Close list with last answer
                    questionString += "@\"" + incorrectAnswers[i] + "\");";
                }
                else
                {
                    questionString += "@\"" + incorrectAnswers[i] + "\",";
                }
            }
            questionString += "return q; }}}";
            return questionString;
        }

        public void executeCommand(string command)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = command;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }

        //Creates HTML document from a given string
        public void createHTMLDocument(string path, string title, string content)
        {
            //CSS string
            var CSS = "body {font-family: \"Palatino Linotype\", \"Book Antiqua\", Palatino, serif;} ol.qlist > li.q {margin: 2em 0;} li > ol.a {list-style: upper-alpha;} .xyz {padding-top: 1em; padding-bottom: 1em;}";
            //Header with title
            var HTML = "<html><head><meta charset=\"utf-8\"/><title>" + title + "</title><style>" + CSS + "</style></head><body>";
            HTML += "<div>" + content + "</div></body></html>";
            //Create file
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(HTML);
            }
        }

        public byte[] getAssessmentFile(string name)
        {
            byte[] data = System.IO.File.ReadAllBytes(getDirectory() + "\\Assessments\\" + name + ".zip");
            deleteAssessmentFolder();
            return data;
        }

        //Delete the Assessments folder and any subdirectories
        public void deleteAssessmentFolder()
        {
            System.IO.Directory.Delete(getDirectory() + "Assessments", true);
        }
    }
}