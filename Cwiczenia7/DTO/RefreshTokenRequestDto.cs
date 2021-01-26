using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia7.DTO
{
    public class RefreshTokenRequestDto
    {
        public string Login { get; set; }
        public string RefreshToken { get; set; }
    }
}
