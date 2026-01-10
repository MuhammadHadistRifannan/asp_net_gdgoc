namespace gdgoc_aspnet;

public interface IUserRepository
{
    public Task<bool> EmailExist(string email);
    public Task<User?> UpdateAsync(User user);
    public Task<User?> AddUserAsync(User user);
    public Task<User?> GetEmailUserAsync(string email);
    public Task<User?> GetUserByIdAsync(Guid? id);
}
