namespace gdgoc_aspnet;

public class DeleteService : IDeleteService
{
    readonly IUserRepository _userRepository; 
    public DeleteService(IUserRepository _userRepo)
    {
        _userRepository = _userRepo;
    }

    public async Task<User?> DeleteUserAsync(User user)
    {
        if (user == null) throw new UnauthorizedAccessException("User is not exist");
        await _userRepository.DeleteAsync(user);
        return user;
    }

    public async Task<User?> DeleteUserAsync(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null) throw new UnauthorizedAccessException("User is not exist");
        await _userRepository.DeleteAsync(user!);
        return user;
    }
}
