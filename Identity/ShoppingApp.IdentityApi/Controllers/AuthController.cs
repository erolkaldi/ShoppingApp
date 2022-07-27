

namespace ShoppingApp.IdentityApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly IMediator _mediator;

        public AuthController(IConfiguration configuration,IMediator mediator)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<Token> GetToken(GetUserByEmailCommand command)
        {
            Token token = new Token();
            GetUserByEmailResponse result = await _mediator.Send(command);
            if (result.Succeeded)
            {
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", result.AppUser.Id),
                        new Claim("DisplayName", result.AppUser.FullName),
                        new Claim("UserName", result.AppUser.UserName),
                        new Claim("Email", result.AppUser.Email)
                    };
                token.Expiration = DateTime.UtcNow.AddHours(7);
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var tokn = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    null,
                    claims,
                    expires: token.Expiration,
                    signingCredentials: signIn);
               token.AccessToken= new JwtSecurityTokenHandler().WriteToken(tokn);
            }
            else
            {
                token.Message =result.Message;
            }
            
            return token;
        }
        [HttpGet]
        [Authorize]
        public IActionResult TestToken()
        {
            return Ok("Working");
        }

    }
}
