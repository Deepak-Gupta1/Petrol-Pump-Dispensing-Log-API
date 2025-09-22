using FuelDispensingAPI.Domain;

public interface IUserRepository
{
    Task<User?> GetUserByUsernameAsync(string username);
}
