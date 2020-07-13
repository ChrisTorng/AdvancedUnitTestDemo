using Microsoft.EntityFrameworkCore;

namespace AdvancedUnitTestDemo.Model
{
    public class SchoolContext : DbContext
    {
        public SchoolContext()
            : base(new DbContextOptionsBuilder<SchoolContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SchoolContext;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options)
        {
        }

        public DbSet<Student> Students { get; set; } = null!;
    }
}
