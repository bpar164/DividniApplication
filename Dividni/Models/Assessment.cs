using System;
using System.ComponentModel.DataAnnotations;

namespace Dividni.Models
{
    public class Assessment
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CoverPage { get; set; }
        public string QuestionList { get; set; }
        public string Appendix { get; set; }
        public string UserEmail { get; set; }

        [DataType(DataType.Date)]
        public DateTime ModifiedDate { get; set; }
    }
}



