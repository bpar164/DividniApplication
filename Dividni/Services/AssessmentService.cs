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
using System.Text.RegularExpressions;

namespace Dividni.Services
{
    public class AssessmentService
    {
        private readonly ApplicationDbContext _context;
        public AssessmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Boolean createAssessment(Assessment assessment)
        {
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
                        //Save id to list for the document
                        HTML += "<li class=\"q\"><p class=\"cws_code_q\">Q" + questionNumber + "</p></li>";
                        
                        //Convert question to C# string
                        simpleToString(simple, questionNumber);
                        //Save C# string to a file
                        questionNumber++;
                    }

                }
                else if (questionList[i].type == "Advanced")
                {
                    var advanced = fetchAdvancedQuestion(new Guid(questionList[i].id));
                    if (advanced != null)
                    {
                        //Save id to list for the document
                        HTML += "<li class=\"q\"><p class=\"cws_code_q\">Q" + questionNumber + "</p></li>";

                        //Save C# string to a file
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
            Console.WriteLine(HTML);
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
            questionString += "q.Stem = @\"" + Regex.Replace(simple.QuestionText, "\"", "\\\"") + "\";";
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

            Console.WriteLine(questionString);

            return "";
        }
    }
}