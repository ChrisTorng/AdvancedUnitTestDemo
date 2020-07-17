using System;

namespace SchoolDatabase
{
    public class DefaultDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
