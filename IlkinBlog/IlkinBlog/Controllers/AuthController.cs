using IlkinBlog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace IlkinBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        //Controller
        public AuthController(DataContext context, IConfiguration configuration)
        {
            this._configuration = configuration;
            this._context = context;
        }


        //Static user
        public static User user = new User();


        //Data context



        //Resgistering admin

        //Only one admin, no other users for simple blog
        [HttpPost("registerAdmin")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {

            CreatePasswordHash(request.Password, out byte[] passHash, out byte[] passSalt);

            user.Username = request.Username;
            user.PasswordHash = passHash;
            user.PasswordSalt = passSalt;

            _context.users.Add(user);

            await _context.SaveChangesAsync();

            return Ok(user);
        }


        //Admin login
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {

            user = _context.users.FirstOrDefault();

            if (request.Username != user.Username)
            {
                return BadRequest("Username is not correct!");
            }
            if(!VerifyPassHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Password is not correct!");
            }

            string token = CreateToken(user);

            return Ok(token);
        }


        //Create token

        //Writing secret word into appsettings.json
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims : claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }



        //Creating Password Hash
        private void CreatePasswordHash(string password, out byte[] passHash, out byte[] passSalt)
        {
            using( var hmac = new HMACSHA512())
            {
                passSalt = hmac.Key;
                passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        //Verify Password
        private bool VerifyPassHash(string password, byte[] passHash, byte[] passSalt)
        {
            using ( var hmac = new HMACSHA512(passSalt))
            {

                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passHash);
            }
        }

    }
}
