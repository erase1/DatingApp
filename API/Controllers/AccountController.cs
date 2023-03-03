using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
   
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username))
            {
                return BadRequest("Username is taken");
            }

            using var hmac = new HMACSHA512(); //will be our password salt (randomly genrated key)

            var user = new AppUser
             {  
                UserName = registerDto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Username)),
                PasswordSalt = hmac.Key                
            };
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };

        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(user => user.UserName.ToLower() == loginDto.Username.ToLower());

           if (user == null) return Unauthorized("Invalid username");

           using var hmac = new HMACSHA512(user.PasswordSalt);
           
           var computedPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

           for (int i = 0; i < computedPasswordHash.Length; i++)
           {
                if (computedPasswordHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
           }
        
           //to see contents of the token for testing purpose, paste the token at https://jwt.ms/ everything can be seen except the signature because jwt.ms is the client and doesn't have the symmetric key
           return  new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };

        }
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(user => user.UserName.ToLower() == username.ToLower()); //found then true else false
        }
    }
}