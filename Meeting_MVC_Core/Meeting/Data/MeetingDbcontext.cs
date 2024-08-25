using Meeting.Models;
using Microsoft.EntityFrameworkCore;

namespace Meeting.Data
{
    public class MeetingDbcontext : DbContext
    {
        public MeetingDbcontext(DbContextOptions<MeetingDbcontext> options) : base(options) { }

        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<Corporate> Corporates { get; set; }
        public virtual DbSet<cutomer> cutomers { get; set; }
    }
}
