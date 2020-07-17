using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace SchoolDatabase.Tests
{
    internal class SqlServerSchoolContext : ISchoolDatabase
    {
        private readonly SchoolContext schoolContext;
        private readonly IDbContextTransaction transaction;
        private bool disposed = false;

        public IQueryable<Student> Students =>
            this.schoolContext.Students;

        public SqlServerSchoolContext(IEnumerable<Student>? students = null)
        {
            this.schoolContext = new SchoolContext(new DbContextOptionsBuilder<SchoolContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SchoolContext;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options);

            this.schoolContext.Database.EnsureCreated();

            this.transaction = this.schoolContext.Database.BeginTransaction();

            if (students != null)
            {
                this.schoolContext.Database.ExecuteSqlRaw("DELETE FROM Students");

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
                this.transaction.Dispose();
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
