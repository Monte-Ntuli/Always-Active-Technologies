using AAT_Crud.Entities;
using AAT_Crud.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedClasses.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AAT_Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JwtConfig _jwtConfig;

        public UserController(IUnitOfWork unitOfWork, ILogger<UserController> logger, IMapper mapper, IConfiguration config,
           UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<JwtConfig> jwtConfig)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _config = config;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtConfig = jwtConfig.Value;

        }

        #region Register new user
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDTO userDTO)
        {

            var user = new AppUser()
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                UserName = userDTO.Email,
                PhoneNumber = userDTO.PhoneNum,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };

            var check = await _userManager.FindByEmailAsync(user.Email);

            if (check == null)
            {
                var result = await _userManager.CreateAsync(user, userDTO.Password);
                if (result.Succeeded)
                {
                    return Accepted(user);
                }
            }
            if (check != null)
            {
                return BadRequest("User Already Exists");
            }
            return BadRequest();


        }
        #endregion

        #region Login the user
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginUserRequest)
        {
            if (loginUserRequest.Email == "" || loginUserRequest.Password == "")
            {
                return BadRequest();
            }
            var result = await _signInManager.PasswordSignInAsync(loginUserRequest.Email, loginUserRequest.Password, false, false);
            if (result.Succeeded)
            {
                var appUser = await _userManager.FindByEmailAsync(loginUserRequest.Email);
                var user = new UserDTO(appUser.FirstName, appUser.LastName, appUser.Email, appUser.UserName, appUser.PhoneNumber, appUser.DateCreated);
                user.Token = GenerateToken(appUser);

                return Accepted(user);
            }
            return BadRequest(result);
        }
        #endregion

        #region Token generation
        private string GenerateToken(AppUser user) // Generate a token to user with identity framkework
        {
            var jwTokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Key);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[] {
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtConfig.Audience,
                Issuer = _jwtConfig.Issuer,
            };
            var token = jwTokenhandler.CreateToken(tokenDescription);
            return jwTokenhandler.WriteToken(token);
        }
        #endregion

        #region Get User Details 
        [HttpGet("GetUserByEmail/{email}")]
        public async Task<IActionResult> GetUserDetails(string email)
        {

            var appUser = await _userManager.FindByEmailAsync(email);
            if (appUser != null) { return Accepted(appUser); } else { return BadRequest(appUser); }

        }
        #endregion

        #region Find Email User To Change Password
        [HttpPost("FindEmail/{email}")]
        public async Task<IActionResult> FindEmail(string email)
        {
            var appUser = await _userManager.FindByEmailAsync(email);
            if (appUser != null) { return Accepted(); } else { return BadRequest(); }
        }
        #endregion

        #region Forgot User Password
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] LoginDTO loginUserRequest)
        {
            var appUser = await _userManager.FindByEmailAsync(loginUserRequest.Email);
            if (appUser != null)
            {
                var passWordResetToken = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                var resetPassWord = await _userManager.ResetPasswordAsync(appUser, passWordResetToken, loginUserRequest.Password);
                return Accepted(resetPassWord);
            }
            else
            {
                return BadRequest(appUser);
            }
        }
        #endregion

        #region Change user Password
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] LoginDTO loginUserRequest)
        {
            var appUser = await _userManager.FindByEmailAsync(loginUserRequest.Email);
            if (appUser != null)
            {
                var passWordResetToken = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                var resetPassWord = await _userManager.ResetPasswordAsync(appUser, passWordResetToken, loginUserRequest.Password);
                return Accepted(resetPassWord);
            }
            else
            {
                return BadRequest(appUser);
            }
        }
        #endregion
    }
}
