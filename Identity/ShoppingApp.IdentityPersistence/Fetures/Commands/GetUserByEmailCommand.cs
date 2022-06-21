

namespace ShoppingApp.IdentityPersistence.Fetures.Commands
{
    public class GetUserByEmailCommand : IRequest<GetUserByEmailResponse>
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
    public class GetUserByEmailResponse
    {
        public string Message { get; set; } = "";
        public bool Succeeded { get; set; }
        public AppUser AppUser { get; set; }
    }
    public class GetUserByEmailCommandHandler : IRequestHandler<GetUserByEmailCommand, GetUserByEmailResponse>
    {
        readonly SignInManager<AppUser> _signInManager;
        readonly UserManager<AppUser> _userManager;

        public GetUserByEmailCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<GetUserByEmailResponse> Handle(GetUserByEmailCommand request, CancellationToken cancellationToken)
        {
            GetUserByEmailResponse response = new();
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    response.Message = "User Not Found";
                    response.Succeeded = false;
                    return response;
                }
                var passwordResult =await  _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (passwordResult.Succeeded == false)
                {
                    response.Message = "Password is wrong";
                    response.Succeeded = false;
                    return response;
                }
                response.Succeeded = true;
                response.AppUser= user;
                return response;
            }
            catch (Exception exc)
            {
                response.Succeeded = false;
                response.Message = exc.Message;

            }
            return response;
        }
    }
}
