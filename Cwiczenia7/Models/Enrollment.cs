using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia6.Models
{
    public class Enrollment
    {
        public int IdEnrollment { set; get; }
        public int Semester { set; get; }
        public int IdStudy { set; get; }
        public DateTime StartDate { set; get; }
    }
}
