using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace SchoolDatabase.Tests
{
    internal class InMemorySqliteSchoolContext : ISchoolDatabase
    {
        private readonly DbConnection connection;
        private readonly SchoolContext schoolContext;
        private bool disposed = false;

        public IQueryable<Student> Students =>
            this.schoolContext.Students;

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        public InMemorySqliteSchoolContext(IEnumerable<Student>? students = null)
        {
            this.connection = CreateInMemoryDatabase();
            this.schoolContext = new SchoolContext(new DbContextOptionsBuilder<SchoolContext>()
                .UseSqlite(this.connection)
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
                this.connection.Dispose();
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
