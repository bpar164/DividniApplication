using System;
using System.ComponentModel.DataAnnotations;

namespace Dividni.Models
{
    public class QuestionBank
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9][a-zA-Z0-9 ]*")]
        public string Name { get; set; }

        [Required]
        public string QuestionList { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [DataType(DataType.Date)]
        public DateTime ModifiedDate { get; set; }
    }
}

