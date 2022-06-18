
namespace ShoppingApp.IdentityApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetToken()
        {
            return Ok("Working");
        }
    }
}
