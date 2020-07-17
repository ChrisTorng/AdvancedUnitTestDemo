using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SchoolDatabase
{
    public class SchoolContext : DbContext, ISchoolDatabase
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = null!;

        IQueryable<Student> ISchoolDatabase.Students =>
            this.Students;
    }
}
