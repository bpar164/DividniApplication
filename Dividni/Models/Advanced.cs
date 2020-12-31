using System;
using System.ComponentModel.DataAnnotations;

namespace Dividni.Models
{ 
    public class Advanced
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Marks { get; set; }
        public string Question { get; set; }
        public string UserEmail { get; set; }

        [DataType(DataType.Date)]
        public DateTime ModifiedDate { get; set; }
    }
}