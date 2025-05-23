﻿using DatingApp_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp_API.Controllers
{
    public class AccountController(DataContext context,ITokenService tokenService) : BaseController
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDTO registerDTO)
        {
            if (await UserExists(registerDTO.Username)) return BadRequest("Username is taken");
            return Ok();
            // using var hmac = new HMACSHA512();
            //var user = new AppUser
            //{
            //    UserName = registerDTO.Username.ToLower(),
            //    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
            //    PasswordSalt = hmac.Key
            //};
            //context.Users.Add(user);
            //await context.SaveChangesAsync();

            //return new UserDto { Username = user.UserName, Token = tokenService.CreateToken(user) };
        }

        private async Task<bool> UserExists(string username)
        {
            return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDTO loginDTO)
        {
            var user = await context.Users.FirstOrDefaultAsync(x=>x.UserName==loginDTO.Username.ToLower());
            
            if (user==null) return Unauthorized("Invalid Username");

            using var hmac=new HMACSHA512(user.PasswordSalt);
           
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            
            for (int i = 0;i<computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Unauthorised");
            }
            return new UserDto {Username=user.UserName, Token=tokenService.CreateToken(user)};
        }
        
    }
}
