using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meeting.Models
{
    public class Experience
    {
        public Experience()
        {
        }

        [Key]
        public int ExperienceId { get; set; }
        [ForeignKey("Corporate")]//very important
        public int CorporateId { get; set; }
        public virtual Corporate Corporates { get; private set; }
        [Required]
        public string Service { get; set; }
        public int Quantiy { get; set; }
        
        public int Unit { get; set; }
    }
}
