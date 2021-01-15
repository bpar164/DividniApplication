using System;
using System.ComponentModel.DataAnnotations;

namespace Dividni.Models
{
    public class Advanced
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9][a-zA-Z0-9 ]*")]
        public string Name { get; set; }

        [Required]
        [StringLength(8)] //Can only be: Advanced
        public string Type { get; set; }

        [Range(0, 10)]
        public int Marks { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [DataType(DataType.Date)]
        public DateTime ModifiedDate { get; set; }
    }
}