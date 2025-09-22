using FuelDispensingAPI.Application.Interface;
using FuelDispensingAPI.Domain;
using FuelDispensingAPI.Helper;

namespace FuelDispensingAPI.Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }

        public async Task<string?> AuthenticateAsync(LoginRequest request)
        {
            var user = await _userRepo.GetUserByUsernameAsync(request.Username);
            if (user == null || user.PasswordHash != request.Password) // Use hashing in real apps
                return null;

            return JwtHelper.GenerateToken(user.Username, _config);
        }
    }

}
