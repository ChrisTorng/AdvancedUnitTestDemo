using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AdvancedUnitTestDemo.Model
{
    public class SchoolContext : DbContext
    {
        public SchoolContext()
            : base(new DbContextOptionsBuilder<SchoolContext>()
                .UseSqlServer(Startup.Configuration.GetConnectionString("SchoolContext"))
                .Options)
        {
        }

        public DbSet<Student> Students { get; set; } = null!;
    }
}
