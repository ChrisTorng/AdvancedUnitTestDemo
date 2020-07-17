using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SchoolDatabase.Tests
{
    internal class InMemoryDatabaseSchoolContext : ISchoolDatabase
    {
        private readonly SchoolContext schoolContext;
        private bool disposed = false;

        public IQueryable<Student> Students =>
            this.schoolContext.Students;

        public InMemoryDatabaseSchoolContext(IEnumerable<Student>? students = null)
        {
            this.schoolContext = new SchoolContext(new DbContextOptionsBuilder<SchoolContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

            this.schoolContext.Database.EnsureCreated();

            if (students != null)
            {
                this.schoolContext.Students.AddRange(students);
                this.schoolContext.SaveChanges();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.schoolContext.Dispose();
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
