using Microsoft.AspNetCore.Mvc;
using System;

namespace AdminPanel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly FileUserService _userService;

        public AuthController(FileUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet("GetGenderData")]
        public IActionResult GetRandomDonutData()
        {
            var random = new Random();
            var malePercentage = random.Next(0, 101);
            var femalePercentage = 100 - malePercentage;

            var donutData = new
            {
                labels = new[] { "Female", "Male" },
                datasets = new[]
                {
                    new
                    {
                        backgroundColor = new[] { "#41B883", "#E46651" },
                        data = new[] { femalePercentage, malePercentage }
                    }
                }
            };

            return Ok(donutData);
        }

        [HttpGet("GetTopData")]
        public TopData GetTopData()
        {
            Random rnd = new Random();
            return new TopData
            { 
               StoryViewsPercent = (Math.Abs((rnd.NextDouble() * 2.0) - 1.0) * 100).ToString().Substring(0, 5) + "%",
               ConversionRate = rnd.Next(1, 100).ToString(),
               ConversionRatePercent = (Math.Abs((rnd.NextDouble() * 2.0) - 1.0) * 100).ToString().Substring(0, 5) + "%",
               Explore = rnd.Next(1, 100).ToString(),
               ExplorePercent = (Math.Abs((rnd.NextDouble() * 2.0) - 1.0) * 100).ToString().Substring(0, 5) + "%",
               Followers = rnd.Next(1, 100).ToString(),
               FollowersPercent = (Math.Abs((rnd.NextDouble() * 2.0) - 1.0) * 100).ToString().Substring(0, 5) + "%",
               StoryViews = rnd.Next(1, 100).ToString()
            };

        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("All fields are required.");
            }

            var success = _userService.Register(user);
            if (!success)
            {
                return Conflict("User with the same username or email already exists.");
            }

            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginRequest)
        {
            if (string.IsNullOrWhiteSpace(loginRequest.UserName) || string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                return BadRequest("Username and password are required.");
            }

            var user = _userService.Login(loginRequest.UserName, loginRequest.Password);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok();
        }
    }

}
