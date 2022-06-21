



namespace ShoppingApp.IdentityPersistence.Fetures.Commands
{
    public class RegisterUserCommand : IRequest<CreateUserResponse>
    {
        public string Email { get; set; } = "";
        public string Name { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Password { get; set; } = "";
        public string ConfirmPassword { get; set; } = "";
    }
    public class CreateUserResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = "";
    }
    public class CreateUserCommandHandler : IRequestHandler<RegisterUserCommand, CreateUserResponse>
    {
        readonly UserManager<AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            CreateUserResponse response = new();
            try
            {
                AppUser user = new() { Id = Guid.NewGuid().ToString(),UserName=request.Name, FullName = request.FullName, Email = request.Email };
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    response.Succeeded = true;
                }
                else
                {
                    response.Succeeded = false;
                    response.Message = "";
                    foreach (var err in result.Errors)
                    {
                        response.Message += err.Code + "-" + err.Description + "\n";
                    }
                }
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
