using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using MyApp.Namespace;

namespace gdgoc_aspnet;

public class LoginService : ILoginService
{
    readonly IUserRepository _userRepository;
    readonly IMapper _mapper;
    readonly IJwtHandler _jwtservice;

    public LoginService(IUserRepository _userRepo , IMapper _map , IJwtHandler _jwts)
    {
        _userRepository = _userRepo;
        _mapper = _map;
        _jwtservice = _jwts;
    }

    public async Task<LoginResult> Authenticate(UserLoginRequest _request)
    {
        //Check Email
        var user = await _userRepository.GetEmailUserAsync(_request.email!);
        if (user == null) throw new UnauthorizedAccessException("Email was not found");

        //Check Password
        var correctPass = BCrypt.Net.BCrypt.Verify(
            _request.password, 
            user.password
        );

        if (!correctPass) throw new UnauthorizedAccessException("Invalid Credential");

        //Generate JWT token 
        string token = _jwtservice.Generate(user);

        //Map User object to DTO
        var dto = new UserDTO();
        _mapper.Map(user,dto);

        //Return 
        return new LoginResult(
            message: "success",
            data: dto,
            token: token
        );
    }
}
