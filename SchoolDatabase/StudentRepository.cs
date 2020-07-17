using System;
using System.Linq;

namespace SchoolDatabase
{
    public class StudentRepository
    {
        private readonly ISchoolDatabase schoolDatabase;
        private readonly IDateTime dateTime;

        public StudentRepository(ISchoolDatabase schoolDatabase, IDateTime dateTime = null!)
        {
            this.schoolDatabase = schoolDatabase;
            this.dateTime = dateTime ?? new DefaultDateTime();
        }

        public IQueryable<Student> AllStudents =>
            this.schoolDatabase.Students;

        public IQueryable<Student> CurrentStudents =>
            this.schoolDatabase.Students.Where(s =>
                s.EnrollmentDate >= this.CurrentStudentStartYear);

        public DateTime CurrentStudentStartYear
        {
            get
            {
                var isThisYear = this.dateTime.Now.Month >= 8;
                var thisYearStart = isThisYear ?
                    new DateTime(this.dateTime.Now.Year, 8, 1) :
                    new DateTime(this.dateTime.Now.Year - 1, 8, 1);
                var currentStudentStartDate = thisYearStart.AddYears(-2);
                return currentStudentStartDate;
            }
        }
    }
}
