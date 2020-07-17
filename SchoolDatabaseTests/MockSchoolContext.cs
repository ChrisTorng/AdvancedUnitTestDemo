using System.Collections.Generic;
using System.Linq;

namespace SchoolDatabase.Tests
{
#pragma warning disable CA1063 // Implement IDisposable Correctly
    public class MockSchoolContext : ISchoolDatabase
#pragma warning restore CA1063 // Implement IDisposable Correctly
    {
        public MockSchoolContext(IEnumerable<Student> students)
        {
            this.Students = students.AsQueryable();
        }

        public IQueryable<Student> Students { get; }

#pragma warning disable CA1063 // Implement IDisposable Correctly
#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
        public void Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
#pragma warning restore CA1063 // Implement IDisposable Correctly
        {
        }
    }
}
