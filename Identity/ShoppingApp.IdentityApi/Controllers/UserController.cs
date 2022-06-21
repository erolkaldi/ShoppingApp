
namespace ShoppingApp.IdentityApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult> RegisterUser(RegisterUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
