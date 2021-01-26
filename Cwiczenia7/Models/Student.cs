using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia6.Models
{
    public class Student
    {
        public string IndexNumber { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public DateTime BirthDate { set; get; }
        public int IdEnrollment { set; get; }
        public string Password { set; get; }
        public string RefreshToken { set; get; }
    }
}
