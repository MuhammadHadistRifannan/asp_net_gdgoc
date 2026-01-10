namespace gdgoc_aspnet;

public interface IRegisterService
{
    public Task<User?> RegisterUserAsync(UserRegistRequest _request);
}
