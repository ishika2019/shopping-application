using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using project.Entities.identity;
using project.Errors;
using project.Interface;
using System.Security.Claims;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenInterface tokenInterface;
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,ITokenInterface tokenInterface,IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenInterface = tokenInterface;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email=HttpContext.User?.Claims?.FirstOrDefault(x=>x.Type==ClaimTypes.Email)?.Value;

            var user=await userManager.FindByEmailAsync(email);
            return new UserDto { 
                Email=user.Email,
                Token=tokenInterface.CreateToken(user),
                DisplayName=user.DisplayName
            };


        }


        [HttpGet("emailexits")]
        public async Task<ActionResult<bool>> checkEmailExist([FromQuery] string email)
        {
           return await userManager.FindByEmailAsync(email)!=null;

        
        }


        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> getUserAddress()
        {

            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;


            var user = await userManager.Users.Include(x=>x.Address).SingleOrDefaultAsync(x=>x.Email==email);

            return mapper.Map<Address, AddressDto>(user.Address);
            
        }


        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {

            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;


            var user = await userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);

            user.Address=mapper.Map<AddressDto,Address>(address);

            var result= await userManager.UpdateAsync(user);

            return mapper.Map<Address, AddressDto>(user.Address);

        }



        [HttpPost("login")]

        public  async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user=await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized(401);
            }

            var result =await signInManager.CheckPasswordSignInAsync(user,loginDto.Password,false);
            if(!result.Succeeded)
            {
                return Unauthorized(401);
            }
            else
            {
                var answer = new UserDto
                {
                    Email = loginDto.Email,
                    DisplayName = user.DisplayName,
                    Token = tokenInterface.CreateToken(user)
                };
                return Ok(answer);
            }
        }

        [HttpPost("Register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if(checkEmailExist(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationError
                {
                    Errors = new[] { "Email address is in use" }
                });
            }
            var user = new AppUser
            {
                Email = registerDto.Email,
                DisplayName = registerDto.Displayname,
                UserName = registerDto.Email
            };

            var result =await userManager.CreateAsync(user,registerDto.Password);

            if(!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            }
            else
            {
                return new UserDto
                {
                    Email = registerDto.Email,
                    DisplayName = registerDto.Displayname,
                    Token = tokenInterface.CreateToken(user)

                };
            }
        }


    }
}
