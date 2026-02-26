using Habit.Core.ServiceContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReHabit.Api.DTOs;
using ReHabit.Habit.Core.Models;

namespace ReHabit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthService _authService;
        private readonly ILogger<AccountController> _logger;
        private readonly IImageService _imageService;
        AccountController(UserManager<User> userManager,
                          SignInManager<User> signInManager,
                          IAuthService authService,
                          ILogger<AccountController> logger,
                          IImageService imageService)
        {
            _authService = authService;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _imageService = imageService;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login(UserLoginDto loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.Email);

            if (user == null)
            {
                return BadRequest("Invalid email or password");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginUser.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Invalid email or password");
            }

            var token = await _authService.CreateTokenAsync(user);

            return Ok(new UserDto
            {
                Email = loginUser.Email,
                Token = token,
                Id = user.Id

            });

        }
        [HttpGet("Register")]
        public async Task<IActionResult> Register(UserRegisterDto registerUser, IFormFile image)
        {
            var isExistUser = await _userManager.FindByEmailAsync(registerUser.Email);

            if (isExistUser != null)
            {
                return BadRequest("Email is already exist");
            }

            var imageUrl = await _imageService.SaveImageAsync(image);

            var user = new User
            {
                Email = registerUser.Email,
                UserName = registerUser.Name,
                ImageUrl = imageUrl,
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new UserDto
            {
                Email = registerUser.Email,
                Id = user.Id,
                Token = await _authService.CreateTokenAsync(user)
            });

        }
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged out successfully");
        }
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(new UserDto
            {
                Email = user.Email,
                Id = user.Id,
                Token = await _authService.CreateTokenAsync(user)
            });
        }
        public async Task<IActionResult> UpdateUserPassword(string currentPassword, string newPassword)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Password updated successfully");
        }
    }

}
