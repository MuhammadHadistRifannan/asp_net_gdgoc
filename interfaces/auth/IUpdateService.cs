namespace gdgoc_aspnet;

public interface IUpdateService
{
    public Task<User?> UpdateUserAsync(UserUpdateRequest _request , Guid id);
}
