using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Cwiczenia6.Models;
using Cwiczenia7.Services;

namespace Cwiczenia6.Services
{
    public class SqlServerStudentDbService : IStudentDbService
    {
        private static string _connectionString = "Data Source=db-mssql;Initial Catalog=s14958;Integrated Security=True";
        private IAuthService _authService;

        public SqlServerStudentDbService(IAuthService authService)
        {
            _authService = authService;
        }

        public IEnumerable<Student> GetStudents() { 
            using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();

                command.CommandText = "select * from Student;";

                var reader = command.ExecuteReader();

                var students = new List<Student>();

                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        BirthDate = DateTime.Parse(reader["BirthDate"].ToString()),
                        IdEnrollment = (int) reader["IdEnrollment"],
                        IndexNumber = reader["IndexNumber"].ToString()
                    });
                }


                return students;
            }
        }

        public Student GetStudent(string index)
        {
            using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from Student where IndexNumber = @index";
                command.Parameters.AddWithValue("index", index);

                var reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    return null;
                }

                return new Student
                {
                    IndexNumber = reader["IndexNumber"].ToString(),
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    BirthDate = DateTime.Parse(reader["BirthDate"].ToString()),
                    IdEnrollment = (int)reader["IdEnrollment"],
                    Password = reader["Password"].ToString(),
                    RefreshToken = reader["RefreshToken"].ToString()
                };
            }
        }

        public void SetStudentPassword(string index, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "update Student set Student.password = @password where IndexNumber = @indexNumber";
                command.Parameters.AddWithValue("indexNumber", index);
                command.Parameters.AddWithValue("password", _authService.HashPassword(password));
                command.ExecuteNonQuery();
            }   
        }

        public void SetStudentRefreshToken(string index, string refreshToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "update Student set RefreshToken = @refreshToken where IndexNumber = @indexNumber";
                command.Parameters.AddWithValue("indexNumber", index);
                command.Parameters.AddWithValue("refreshToken", refreshToken);
                command.ExecuteNonQuery();
            }
        }
    }
}
