using AutoMapper;
using MyApp.Namespace;

namespace gdgoc_aspnet;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDTO, User>()
        .ForMember(dest => dest.id , opt => opt.Ignore())
        .ForMember(dest => dest.email, opt => opt.Ignore())
        .ForAllMembers(opt => opt.Condition((userdto , user , member) => member != null));

        CreateMap<User,UserDTO>()
        .ForAllMembers(opt => opt.Condition((user , userdto , member) => member != null));

        CreateMap<UserRegistRequest,User>()
        .ForMember(dest => dest.password , opt => opt.Ignore())
        .ForAllMembers(opt => opt.Condition((req , user , member) => member != null));

        CreateMap<UserLoginRequest,User>()
        .ForAllMembers(opt => opt.Condition((req , user , member) => member != null));

        CreateMap<UserUpdateRequest, User>()
        .ForMember(dest => dest.id , opt => opt.Ignore())
        .ForMember(dest => dest.email, opt => opt.Ignore())
        .ForAllMembers(opt => opt.Condition((userdto , user , member) => member != null));
    }
}
