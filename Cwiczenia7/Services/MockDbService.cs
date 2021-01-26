using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cwiczenia6.Models;

namespace Cwiczenia6.Services
{
    public class MockDbService : IStudentDbService
    {
        public Student GetStudent(string index)
        {
            return new Student
            {
                IndexNumber = "s14958",
                FirstName = "John",
                LastName = "Doe",
                BirthDate = DateTime.Parse("1989.09.28")
            };
        }

        public IEnumerable<Student> GetStudents()
        {
            var studentList = new List<Student>();

            studentList.Add(new Student
            {
                IndexNumber = "s123",
                FirstName = "Jan",
                LastName = "Kowalski",
                BirthDate = DateTime.Parse("1900.11.06"),
                IdEnrollment = 12
            });

            studentList.Add(new Student
            {
                IndexNumber = "s234",
                FirstName = "Andrzej",
                LastName = "Malewski",
                BirthDate = DateTime.Parse("1990.02.12"),
                IdEnrollment = 6
            });

            return studentList;
        }

        public void SetStudentPassword(string index, string password)
        {
            // updating student password ...
        }

        public void SetStudentRefreshToken(string index, string refreshToken)
        {
            // updating student refresh token ...
        }
    }
}
