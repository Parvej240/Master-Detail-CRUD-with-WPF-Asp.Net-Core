using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meeting.Models
{
    public class Corporate
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public string M_Place { get; set; }
        [Required]
        public string Clint { get; set; }
        [Required]
        public string Side { get; set; }
        [Required]
        public string Descussion { get; set; }
        [Required]
        public string Decion { get; set; }
        public virtual List<Experience> Experiences { get; set; } = new List<Experience>();
        [ForeignKey("Cust")]//very important
        public int CustId { get; set; }
    }
}
