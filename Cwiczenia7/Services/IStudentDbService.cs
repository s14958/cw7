using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cwiczenia6.Models;

namespace Cwiczenia6.Services
{
    public interface IStudentDbService
    {
        public Student GetStudent(string index);

        public IEnumerable<Student> GetStudents();

        public void SetStudentPassword(string index, string password);

        public void SetStudentRefreshToken(string index, string refreshToken);
    }
}
