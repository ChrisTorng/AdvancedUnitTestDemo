using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolDatabase;

namespace AdvancedUnitTestDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentRepository studentRepository;
        private readonly ILogger<HomeController> logger;

        public HomeController(StudentRepository studentRepository, ILogger<HomeController> logger)
        {
            this.studentRepository = studentRepository;
            this.logger = logger;
        }

        public IActionResult Index(string sortOrder, string searchString)
        {
            this.logger.LogInformation($"{nameof(HomeController)}.{nameof(this.Index)}({sortOrder},{searchString})");

            this.ViewData["NameSortParm"] =
                string.IsNullOrEmpty(sortOrder) ? "name_desc" : string.Empty;

            this.ViewData["DateSortParm"] =
                sortOrder == "date" ? "date_desc" : "date";

            this.ViewData["SearchString"] = searchString;

            ////using var studentContext = new SchoolContext(new DbContextOptionsBuilder<SchoolContext>()
            ////    .UseSqlServer(Startup.Configuration.GetConnectionString("SchoolContext"))
            ////    .Options);

            var studentRepository = this.studentRepository;

            var students = studentRepository.AllStudents;
            IQueryable<Student> searchedStudents = students;

            if (!string.IsNullOrEmpty(searchString))
            {
#pragma warning disable CA1307 // Specify StringComparison
                searchedStudents = students.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
#pragma warning restore CA1307 // Specify StringComparison
            }

            searchedStudents = sortOrder switch
            {
                "name_desc" => searchedStudents.OrderByDescending(s => s.LastName),
                "date" => searchedStudents.OrderBy(s => s.EnrollmentDate),
                "date_desc" => searchedStudents.OrderByDescending(s => s.EnrollmentDate),
                _ => searchedStudents.OrderBy(s => s.LastName),
            };

            return this.View(nameof(this.Index), searchedStudents.ToArray());
        }
    }
}
