using System;
using System.ComponentModel.DataAnnotations;

namespace Dividni.Models
{ 
    public class Simple
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Marks { get; set; }
        public string QuestionText { get; set; }
        public string CorrectAnswers { get; set; }
        public string IncorrectAnswers { get; set; }
        public string UserEmail { get; set; }
    }
}