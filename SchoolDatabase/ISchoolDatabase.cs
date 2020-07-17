using System;
using System.Linq;

namespace SchoolDatabase
{
    public interface ISchoolDatabase : IDisposable
    {
        IQueryable<Student> Students { get; }
    }
}
