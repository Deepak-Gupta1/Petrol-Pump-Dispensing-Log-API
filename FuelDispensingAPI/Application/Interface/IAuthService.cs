using FuelDispensingAPI.Domain;

namespace FuelDispensingAPI.Application.Interface
{
    public interface IAuthService
    {
        Task<string?> AuthenticateAsync(LoginRequest request);
    }
}
