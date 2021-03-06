﻿using System;
using System.Linq;
using SchoolDatabase;

namespace AdvancedUnitTestDemo.Model
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any())
            {
                // DB has been seeded
                return;
            }

            var students = new Student[]
            {
                new Student
                {
                    FirstMidName = "Carson",
                    LastName = "Alexander",
                    EnrollmentDate = DateTime.Parse("2016-09-01"),
                },
                new Student
                {
                    FirstMidName = "Meredith",
                    LastName = "Alonso",
                    EnrollmentDate = DateTime.Parse("2018-09-01"),
                },
                new Student
                {
                    FirstMidName = "Arturo",
                    LastName = "Anand",
                    EnrollmentDate = DateTime.Parse("2019-09-01"),
                },
                new Student
                {
                    FirstMidName = "Gytis",
                    LastName = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2018-09-01"),
                },
                new Student
                {
                    FirstMidName = "Yan",
                    LastName = "Li",
                    EnrollmentDate = DateTime.Parse("2018-09-01"),
                },
                new Student
                {
                    FirstMidName = "Peggy",
                    LastName = "Justice",
                    EnrollmentDate = DateTime.Parse("2017-09-01"),
                },
                new Student
                {
                    FirstMidName = "Laura",
                    LastName = "Norman",
                    EnrollmentDate = DateTime.Parse("2019-09-01"),
                },
                new Student
                {
                    FirstMidName = "Nino",
                    LastName = "Olivetto",
                    EnrollmentDate = DateTime.Parse("2011-09-01"),
                },
            };

            context.Students.AddRange(students);
            context.SaveChanges();
        }
    }
}
