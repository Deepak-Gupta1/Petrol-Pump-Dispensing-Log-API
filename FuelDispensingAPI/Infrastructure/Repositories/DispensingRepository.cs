using Dapper;
using FuelDispensingAPI.Domain;
using Web.API.Test.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly DapperContext _context;

    public UserRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        var query = "SELECT * FROM Users WHERE Username = @Username";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<User>(query, new { Username = username });
    }
}
