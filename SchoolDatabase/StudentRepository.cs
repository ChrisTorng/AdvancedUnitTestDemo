using System.Linq;

namespace SchoolDatabase
{
    public class StudentRepository
    {
        private readonly ISchoolDatabase schoolDatabase;

        public StudentRepository(ISchoolDatabase schoolDatabase)
        {
            this.schoolDatabase = schoolDatabase;
        }

        public IQueryable<Student> AllStudents =>
            this.schoolDatabase.Students;
    }
}
