namespace gdgoc_aspnet;

public interface IDeleteService
{
    public Task<User?> DeleteUserAsync(User user);
    public Task<User?> DeleteUserAsync(Guid id);
}   
