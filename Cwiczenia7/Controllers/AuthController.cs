using Cwiczenia6.Models;
using Cwiczenia6.Services;
using Cwiczenia7.DTO;
using Cwiczenia7.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenia7.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IStudentDbService _dbService;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IStudentDbService studentDbService, IAuthService authService, IConfiguration configuration) {
            _dbService = studentDbService;
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequestDto request)
        {
            Student student = _dbService.GetStudent(request.Login);
            if (student == null)
            {
                return BadRequest($"Student o indeksie {request.Login} nie istnieje w bazie danych");
            }

            _dbService.SetStudentPassword(request.Login, request.Password);
            return Ok("Student registered");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequestDto request)
        {
            // check if is in DB
            Student student = _dbService.GetStudent(request.Login);
            if (student == null)
            {
                return BadRequest($"Student o indeksie {request.Login} nie istnieje w bazie danych");
            }

            var dbPassword = student.Password;
            var reqHashedPassword = _authService.HashPassword(request.Password);

            // verify password
            if (!reqHashedPassword.Equals(dbPassword))
            {
                return BadRequest($"Niepoprawne hasło dla studenta o indeksie: {request.Login}");
            }

            var refreshToken = _authService.GenerateRefreshToken(request.Login);
            _dbService.SetStudentRefreshToken(request.Login, refreshToken);

            return Ok(new
            {
                token = _authService.GenerateJwtToken(request.Login),
                refreshToken = refreshToken
            });
        }

        [HttpPost("refresh")]
        public IActionResult Refresh(RefreshTokenRequestDto request)
        {
            var student = _dbService.GetStudent(request.Login);

            if (student == null)
            {
                return BadRequest($"Student o indeksie {request.Login} nie istnieje w bazie danych");
            }

            if (!student.RefreshToken.Equals(request.RefreshToken))
            {
                return BadRequest($"Niepoprawny refreshToken dla studenta o indeksie: {request.Login}");
            }

            var refreshToken = _authService.GenerateRefreshToken(request.Login);
            _dbService.SetStudentRefreshToken(request.Login, refreshToken);

            return Ok(new
            {
                token = _authService.GenerateJwtToken(request.Login),
                refreshToken = refreshToken
            });
        }
    }
}
