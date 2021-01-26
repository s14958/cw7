using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia7.Services
{
    public interface IAuthService
    {
        public string HashPassword(string password);
        public string GenerateJwtToken(string index);
        public string GenerateRefreshToken(string index);
    }
}
