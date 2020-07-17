using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolDatabaseTests;

namespace SchoolDatabase.Tests
{
    [TestClass]
    public class MockStudentRepositoryTests
    {
        [TestMethod]
        public void StudentRepository_EmptyAllStudents_Test()
        {
            using var schoolContext = new MockSchoolContext(Array.Empty<Student>());
            var studentRepository = new StudentRepository(schoolContext);

            Assert.AreEqual(0, studentRepository.AllStudents.ToArray().Length);
        }

        [TestMethod]
        public void StudentRepository_AllStudentsQuery_Test()
        {
            using var schoolContext = new MockSchoolContext(new Student[]
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

        [TestMethod]
        [DataRow(2020, 7, 31, 2017, 8, 1)]
        [DataRow(2020, 8, 1, 2018, 8, 1)]
        public void CurrentStudentStartYear_DataRow_Test(
            int thisYear, int thisMonth, int thisDay,
            int expectYear, int expectMonth, int expectDay)
        {
            var studentRepostory = new StudentRepository(null!,
                new MockDateTime(new DateTime(thisYear, thisMonth, thisDay)));

            Assert.AreEqual(new DateTime(expectYear, expectMonth, expectDay),
                studentRepostory.CurrentStudentStartYear);
        }

        [TestMethod]
        [DynamicData(nameof(CurrentStudentsTestData), DynamicDataSourceType.Method)]

        public void CurrentStudentStartYear_DynamicData_Test(
            DateTime thisDate, DateTime expectDate)
        {
            var studentRepostory = new StudentRepository(null!,
                new MockDateTime(thisDate));

            Assert.AreEqual(expectDate, studentRepostory.CurrentStudentStartYear);
        }

        private static IEnumerable<object[]> CurrentStudentsTestData()
        {
            yield return new object[] { new DateTime(2020, 7, 31), new DateTime(2017, 8, 1) };
            yield return new object[] { new DateTime(2020, 8, 1), new DateTime(2018, 8, 1) };
        }
    }
}
