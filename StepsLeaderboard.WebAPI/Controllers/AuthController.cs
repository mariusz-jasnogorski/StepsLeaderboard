using Microsoft.AspNetCore.Mvc;
using StepsLeaderboard.WebAPI.Authentication;

namespace StepsLeaderboard.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        // A dummy in-memory user store for demonstration
        private static readonly Dictionary<string, string> _users = new Dictionary<string, string>
        {
            { "john", "password123" },
            { "mariusz", "mypassword" }
        };

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public ActionResult<object> Login([FromBody] LoginRequest request)
        {
            // Validate the user credentials (dummy example)
            if (_users.TryGetValue(request.Username.ToLower(), out var storedPassword))
            {
                if (storedPassword == request.Password)
                {
                    // Generate the JWT token
                    var token = _tokenService.GenerateToken(request.Username);
                    return Ok(new { token });
                }
            }

            // Return 401 if invalid
            return Unauthorized("Invalid username/password");
        }
    }
}
