using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolDatabase.Tests
{
    [TestClass]
    public class InMemoryDatabaseStudentRepositoryTests
    {
        [TestMethod]
        public void StudentRepository_EmptyAllStudents_Test()
        {
            using var schoolContext = new InMemoryDatabaseSchoolContext(Array.Empty<Student>());
            var studentRepository = new StudentRepository(schoolContext);

            Assert.AreEqual(0, studentRepository.AllStudents.ToArray().Length);
        }

        [TestMethod]
        public void StudentRepository_AllStudentsQuery_Test()
        {
            using var schoolContext = new InMemoryDatabaseSchoolContext(new Student[]
                {
                    new Student
                    {
                        FirstMidName = "a",
                        LastName = "b",
                        EnrollmentDate = new DateTime(2, 2, 2),
                    },
                    new Student
                    {
                        FirstMidName = "c",
                        LastName = "d",
                        EnrollmentDate = new DateTime(3, 3, 3),
                    },
                });

            var studentRepository = new StudentRepository(schoolContext);
            Assert.IsTrue(studentRepository.AllStudents.Any());
            Assert.AreEqual(2, studentRepository.AllStudents.Count());

            var student = studentRepository.AllStudents
                .Where(s => s.FirstMidName == "c").Single();
            Assert.AreEqual("d", student.LastName);
            Assert.AreEqual(new DateTime(3, 3, 3), student.EnrollmentDate);
        }
    }
}
