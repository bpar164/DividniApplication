using System;
using System.ComponentModel.DataAnnotations;

namespace Dividni.Models
{
    public class QuestionBank
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string QuestionList { get; set; }
        public string UserEmail { get; set; }

        [DataType(DataType.Date)]
        public DateTime ModifiedDate { get; set; }
    }
}

