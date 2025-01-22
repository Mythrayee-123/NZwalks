using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
		private readonly UserManager<IdentityUser> userManager;
		private readonly ITokenRepository tokenRepository;

		//need to inject usermanager class that the
		//identity provides us to register a user
		public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
			this.userManager = userManager;
			this.tokenRepository = tokenRepository;
		}
        //Post
        //api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };

           var identityResult= await userManager.CreateAsync(identityUser,registerRequestDto.Password);
            if (identityResult.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
					//Add roles to the user
					identityResult= await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User Registered successfully");
                    }
                }
            }
            return BadRequest("Something went wrong");

        }
        [HttpPost]
        //api/Auth/login
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.UserName);
            if (user != null)
            {
               var passwordResult= await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (passwordResult)
                {
                    //get the roles for the user
                    var roles=await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
						//CREATE A token
					var JwtToken=	tokenRepository.CreateJWTToken(user, roles.ToList());
                        var Response = new LoginResponseDto
                        {
                            JwtToken = JwtToken
                        };
						return Ok(Response);
					}
                  
                   
                }
            }
            return BadRequest("User Or password incorrect");
	}

    }
}
