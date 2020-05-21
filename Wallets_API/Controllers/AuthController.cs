using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Wallets_API.Authorization;
using Wallets_API.DTO;
using Wallets_API.Models;

namespace Wallets_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _roleManager = roleManager;

        }



        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegister)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userForRegister.Username);
            if (user == null)
            {
                var userToCreate = new ApplicationUser
                {
                    UserName = userForRegister.Username,
                    Email = userForRegister.Username + "@mail.com"
                };
                //TODO: нужно ли здесь делать If?
                var result = await _userManager.CreateAsync(userToCreate, userForRegister.Password);
                if(await _roleManager.RoleExistsAsync(userForRegister.Role))
                    await _userManager.AddToRoleAsync(userToCreate, userForRegister.Role);
                else
                    await _userManager.AddToRoleAsync(userToCreate, "Member");

                if (result.Succeeded)
                {
                    var userToReturn = _mapper.Map<UserToReturnAfterRegistrationDto>(userToCreate);
                    return Ok(new
                    {
                        data = "User has been created",
                        user = userToReturn
                    });
                }
                return BadRequest("Something went wrong");
            }
            return BadRequest("Such user has been already registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLoginDTO)
        {
            var user = await _userManager.FindByNameAsync(userForLoginDTO.Username);
            if (user == null)
                return Unauthorized();
            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDTO.Password, false);
            if (result.Succeeded)
            {
                return Ok(new
                {
                    token = GenerateJWTToken(user).Result,
                    user
                });
            }
            return Unauthorized();
        }

        private async Task<string> GenerateJWTToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var now = DateTime.Now;
            var creds = new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
                Issuer = AuthOptions.ISSUER,
                Audience = AuthOptions.AUDIENCE
            };

            //var jwt = new JwtSecurityToken(
            //         issuer: AuthOptions.ISSUER,
            //         audience: AuthOptions.AUDIENCE,
            //         notBefore: now,
            //         claims: claims,
            //         expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            //         signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            //var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);


            return tokenHandler.WriteToken(token);
            //return encodedJwt;

        }
    }
}