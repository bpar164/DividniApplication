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

namespace Dividni.Services
{
    public class AssessmentService
    {
        private readonly ApplicationDbContext _context;
        public AssessmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Boolean createAssessment(Assessment assessment, DownloadRequest downloadRequest)
        {
            //Create a subfolder called Assessments (may exist already)
            var folderPath = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length-7) + "Assessments"; //Replace 'Dividni' with 'Assessments'
            System.IO.Directory.CreateDirectory(folderPath);
            var assessmentFolderName = assessment.Name;
            var assessmentPath = folderPath + "\\" + assessmentFolderName;
            //Create a folder specifically for this assessment
            System.IO.Directory.CreateDirectory(assessmentPath);
            //Create a folder specifically for papers
            System.IO.Directory.CreateDirectory(assessmentPath + "\\papers");
            //String listing question ids for command line 
            var questionIds = "";
            //String for document contents
            var HTML = "<div id=\"Questions\"><ol class=\"qlist\">";
            //Get list of question ids from assessment model
            var questionList = JsonSerializer.Deserialize<Question[]>(assessment.QuestionList);
            //For counting questions (excludes instruction sections)
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
                        HTML += "<li class=\"q\"><p class=\"cws_code_q\">Q" + questionNumber + "</p></li>";
                        //Convert question to C# string
                        var questionString = simpleToString(simple, questionNumber);
                        //Save C# string to a file
                        System.IO.File.WriteAllText(assessmentPath + "\\papers\\Q" + questionNumber + ".cs", questionString);
                        questionNumber++;
                    }

                }
                else if (questionList[i].type == "Advanced")
                {
                    var advanced = fetchAdvancedQuestion(new Guid(questionList[i].id));
                    if (advanced != null)
                    {
                        //Save id to list for the commands
                        questionIds += "Q" + questionNumber + ".cs ";
                        //Save id to list for the document
                        HTML += "<li class=\"q\"><p class=\"cws_code_q\">Q" + questionNumber + "</p></li>";
                        //Save C# string to a file
                        System.IO.File.WriteAllText(assessmentPath + "\\Q" + questionNumber + ".cs", advanced.Question);
                        questionNumber++;
                    }
                }
                else
                {
                    HTML += questionList[i].value; //Add instruction section contents to document 
                }
            }
            //Close the HTML string
            HTML += "</ol></div>";
            //Run the Dividni commands, based on the assessment type
            if (downloadRequest.Type == "standard") {
                //Compile all of the questions
                executeCommand("/c cd .. & cd Assessments\\" + assessmentFolderName + "\\papers & csc -t:library -lib:\"C:\\Program Files\\Dividni.com\\Dividni\" -r:Utilities.Courses.dll -out:QHelper.dll " + questionIds);
            }

            //Delete the Assessments folder and any subdirectories
            //System.IO.Directory.Delete(folderPath, true);
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
                    if (i == (correctAnswers.Length-1)) { //Close list with last answer
                        questionString += "@\"" + correctAnswers[i] + "\");";
                    } else {
                        questionString += "@\"" + correctAnswers[i] + "\",";
                    }  
                }
            //Incorrect answers
            questionString += "q.AddIncorrects(";
            var incorrectAnswers = JsonSerializer.Deserialize<string[]>(simple.IncorrectAnswers);
                for (int i = 0; i < incorrectAnswers.Length; i++)
                {
                    if (i == (incorrectAnswers.Length-1)) { //Close list with last answer
                        questionString += "@\"" + incorrectAnswers[i] + "\");";
                    } else {
                        questionString += "@\"" + incorrectAnswers[i] + "\",";
                    }     
                }
            questionString += "return q; }}}";
            return questionString;
        }

        public void executeCommand(string command) {
            Console.WriteLine(command);

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = command;
            process.StartInfo = startInfo;
            process.Start(); 
        }
    }
}