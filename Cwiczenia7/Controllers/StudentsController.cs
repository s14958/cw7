using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cwiczenia6.Models;
using Cwiczenia6.Services;
using Microsoft.AspNetCore.Authorization;
using Cwiczenia7.DTO;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Cwiczenia6.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/students")]
    public class StudentsController : Controller
    {
        private readonly IStudentDbService _dbService;
        private readonly IConfiguration _configuration;

        public StudentsController(IStudentDbService studentDbService, IConfiguration configuration)
        {
            _dbService = studentDbService;
            _configuration = configuration;
        }

        [HttpGet("{index}")]
        public IActionResult GetStudent(string index)
        {
            return Ok(_dbService.GetStudent(index));
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_dbService.GetStudents());
        }
    }
}
