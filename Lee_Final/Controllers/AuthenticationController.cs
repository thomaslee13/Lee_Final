﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Lee_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        public AuthenticationController(JwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }


        [AllowAnonymous]
        [HttpPost("Authorize")]
        public IActionResult AuthUser([FromBody] User usr)
        {
            var token = jwtAuthenticationManager.Authenticat(usr.username, usr.password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        [Authorize]
        [Route("TestRoute")]
        [HttpGet]
        public IActionResult test()
        {
            return Ok("Authorized");
        }
        public class User
        {
            public string username { get; set; }
            public string password { get; set; }
        }
    }
}
