using AutoMapper;

namespace gdgoc_aspnet;

public class UpdateService : IUpdateService
{
    readonly IUserRepository _userRepository;
    readonly IMapper _mapper;
    public UpdateService(IUserRepository _userRepo , IMapper _map)
    {
        _mapper = _map;
        _userRepository = _userRepo;
    }

    public async Task<User?> UpdateUserAsync(UserUpdateRequest _request , Guid id)
    {
        //search user 
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null) throw new UnauthorizedAccessException("User not found");
        
        //map
        _mapper.Map(_request, user);

        //password hash
        if (!string.IsNullOrEmpty(_request.password))
        {
            user.password = BCrypt.Net.BCrypt.HashPassword(_request.password);
        }
        
        //update 
        var newUser = await _userRepository.UpdateAsync(user);

        //return 
        return newUser;
    }
}
