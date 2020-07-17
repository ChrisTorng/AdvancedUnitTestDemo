using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SchoolDatabase.Tests
{
    [TestClass]
    public class MoqStudentRepositoryTests
    {
        [TestMethod]
        public void StudentRepository_EmptyAllStudents_Test()
        {
            var schoolContextMock = new Mock<ISchoolDatabase>();
            var studentRepository = new StudentRepository(schoolContextMock.Object);

            Assert.AreEqual(0, studentRepository.AllStudents.ToArray().Length);
        }

        [TestMethod]
        public void StudentRepository_AllStudentsQuery_Test()
        {
            var schoolContextMock = new Mock<ISchoolDatabase>();
            schoolContextMock.Setup(s => s.Students)
                .Returns(new Student[]
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
                }.AsQueryable());

            var studentRepository = new StudentRepository(schoolContextMock.Object);
            Assert.IsTrue(studentRepository.AllStudents.Any());
            Assert.AreEqual(2, studentRepository.AllStudents.Count());

            var student = studentRepository.AllStudents
                .Where(s => s.FirstMidName == "c").Single();
            Assert.AreEqual("d", student.LastName);
            Assert.AreEqual(new DateTime(3, 3, 3), student.EnrollmentDate);
        }
    }
}
