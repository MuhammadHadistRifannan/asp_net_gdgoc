using AutoMapper;

namespace gdgoc_aspnet;

public class RegisterService : IRegisterService
{
    readonly IUserRepository _userRepository;
    readonly IMapper _mapper;
    public RegisterService(IUserRepository _userRepo , IMapper _map)
    {
        _userRepository = _userRepo;
        _mapper = _map;
    }

    public async Task<User?> RegisterUserAsync(UserRegistRequest _request)
    {
        //search email 
        var isExist = await _userRepository.EmailExist(_request.email!);
        if (isExist) throw new UnauthorizedAccessException("Email is already exist");

        //map 
        var newUser = new User
        {
            id = Guid.NewGuid(),
            password = BCrypt.Net.BCrypt.HashPassword(_request.password),
            created_at = DateTime.UtcNow,
            updated_at = DateTime.UtcNow
        };
        
        _mapper.Map(_request , newUser);

        //save 
        await _userRepository.AddUserAsync(newUser);

        //return 
        return newUser;
    }
    
}
