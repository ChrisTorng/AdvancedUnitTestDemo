using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SchoolDatabase;

namespace AdvancedUnitTestDemo.Controllers.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void IndexTest()
        {
            var mockLogger = new Mock<ILogger<HomeController>>();
            var mockSchoolDatabase = new Mock<ISchoolDatabase>();
            var studentRepository = new StudentRepository(mockSchoolDatabase.Object);
            using var homeController = new HomeController(studentRepository, mockLogger.Object);

            var actionResult = homeController.Index(null!, null!);

            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));

            ViewResult viewResult = (actionResult as ViewResult)!;

            Assert.IsInstanceOfType(viewResult.Model, typeof(Student[]));

            Student[] students = (viewResult.Model as Student[])!;

            Assert.AreEqual(0, students.Length);
        }

        [TestMethod]
        public void Index_Students_Null_Test()
        {
            var allStudents = new Student[]
            {
                new Student
                {
                    FirstMidName = "f1",
                    LastName = "l2",
                    EnrollmentDate = new DateTime(2020, 1, 1),
                },
                new Student
                {
                    FirstMidName = "f3",
                    LastName = "l4",
                    EnrollmentDate = new DateTime(2017, 8, 1),
                },
                new Student
                {
                    FirstMidName = "f4",
                    LastName = "l3",
                    EnrollmentDate = new DateTime(2019, 12, 31),
                },
                new Student
                {
                    FirstMidName = "f2",
                    LastName = "l1",
                    EnrollmentDate = new DateTime(2017, 7, 31),
                },
            };

            var logger = Mock.Of<ILogger<HomeController>>();
            var schoolDatabase = Mock.Of<ISchoolDatabase>(s =>
                s.Students == allStudents.AsQueryable());
            var studentReposity = new StudentRepository(schoolDatabase);

            using var controller = new HomeController(studentReposity, logger);

            var actionResult = controller.Index(null!, null!);
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));

            var viewResult = (actionResult as ViewResult)!;
            Assert.IsInstanceOfType(viewResult.Model, typeof(Student[]));

            var students = (viewResult.Model as Student[])!;
            Assert.AreEqual(3, students.Length);
            Assert.AreEqual(allStudents[0], students[0]);
            Assert.AreEqual(allStudents[1], students[2]);
            Assert.AreEqual(allStudents[2], students[1]);
        }

        [TestMethod]
        public void Index_Students_OrderByDateDesc_Search3_Test()
        {
            var allStudents = new Student[]
            {
                new Student
                {
                    FirstMidName = "f1",
                    LastName = "l2",
                    EnrollmentDate = new DateTime(2020, 1, 1),
                },
                new Student
                {
                    FirstMidName = "f3",
                    LastName = "l4",
                    EnrollmentDate = new DateTime(2017, 8, 1),
                },
                new Student
                {
                    FirstMidName = "f4",
                    LastName = "l3",
                    EnrollmentDate = new DateTime(2019, 12, 31),
                },
                new Student
                {
                    FirstMidName = "f2",
                    LastName = "l1",
                    EnrollmentDate = new DateTime(2017, 7, 31),
                },
            };

            var logger = Mock.Of<ILogger<HomeController>>();
            var schoolDatabase = Mock.Of<ISchoolDatabase>(s =>
                s.Students == allStudents.AsQueryable());
            var studentReposity = new StudentRepository(schoolDatabase);

            using var controller = new HomeController(studentReposity, logger);

            var actionResult = controller.Index("date_desc", "3");
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));

            var viewResult = (actionResult as ViewResult)!;
            Assert.IsInstanceOfType(viewResult.Model, typeof(Student[]));

            var students = (viewResult.Model as Student[])!;
            Assert.AreEqual(2, students.Length);
            Assert.AreEqual(allStudents[2], students[0]);
            Assert.AreEqual(allStudents[1], students[1]);
        }
    }
}
