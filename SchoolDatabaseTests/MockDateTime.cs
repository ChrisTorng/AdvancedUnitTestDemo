using System;
using SchoolDatabase;

namespace SchoolDatabaseTests
{
    public class MockDateTime : IDateTime
    {
        public MockDateTime(DateTime now)
        {
            this.Now = now;
        }

        public DateTime Now { get; }
    }
}
