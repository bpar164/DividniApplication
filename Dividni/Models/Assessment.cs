using System;
using System.ComponentModel.DataAnnotations;

namespace Dividni.Models
{
    public class Assessment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9][a-zA-Z0-9-]*")]
        public string Name { get; set; }

        public string CoverPage { get; set; }

        [Required]
        public string QuestionList { get; set; }

        public string Appendix { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [DataType(DataType.Date)]
        public DateTime ModifiedDate { get; set; }
    }
}



